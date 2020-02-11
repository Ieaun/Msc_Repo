using System;
using System.IO;
using System.Net;
using SnmpSharpNet;
using System.Net.Sockets;
using System.Diagnostics;

namespace SNMP_Agent
{
    class Program
    {
        public static string[,] Trap_OID = new string [10,3];
        public static bool Any_Traps_Set = false;
        public static int Notification = 0; //the position of Trap_oid that needs to be sent as a notification
        public static int Trap_Size = 1;  // size of Trap_OID array
        public static bool Use_Custom_trap = false;


        static void Main(string[] args)
        {
            
            bool Check_Trap = true;

            if (Use_Custom_trap == false)
            { Check_Trap = false; }
            
            Trap_OID[0, 0] = "OID";
            Trap_OID[0, 1] = "Original_State";
            Trap_OID[0, 2] = "Current_State" ;

            


            while (true)
            {
                // return this if cant return anything else
                string[] Return_These_Values = new string[10];
                Return_These_Values[0] = "Error Agent side";

                try
                {
                    Console.WriteLine("//------------------------------------------");
                    // listen until we get a find connection 
                    string Command = SearchForConnection();

                    // get the data requested by the command 
                    Return_These_Values = Request_Snmp(Command);

                    // if no response then adjust the return string
                    if (Return_These_Values[0] == null)
                    {
                        Return_These_Values[0] = "Unable to find that OID";
                    }

                    //get notifications if there are any
                    Return_These_Values = Do_I_Have_to_DO_Traps(Check_Trap, Any_Traps_Set, Trap_OID, Return_These_Values);


                    // give the snmp manager back the data it requested
                    Respond_Snmp(Return_These_Values);


                }
                catch (Exception e)
                {
                    //land here if something goes wrong 
                    Console.WriteLine("Error {0}", e);
                    try
                    { // try to inform the snmp manager of the issue
                        Respond_Snmp(Return_These_Values);
                    }
                    catch { }
                }
            }
        }

        public static string[] Do_I_Have_to_DO_Traps(bool Check_Trap, bool Any_Traps_Set, string[,] Trap_OID, string[] Return_These_Values)
        {
            // if there are any traps to check
            if (Any_Traps_Set)
            {
                // if manager wants us to check traps
                if (Check_Trap)
                {
                    string[] Values = new string [Trap_Size - 1];
                    for (int i = 0; i < Trap_Size - 1; i++) //for each trap value 
                    {
                        // get its current value
                        string Command = string.Format("Get | {0}", Trap_OID[i+1,0]);
                        Values = Request_Snmp(Command);

                        string Current = Values[0];                  //current measured value
                        string Original = Trap_OID[Trap_Size - 1, 1];// origianl value

                        // if the current value isnt the same as the original value
                        if (Current != Original)
                        {
                            //set current value
                            Trap_OID[i + 1, 2] = Values[0];
                            Notification = i + 1;
                            Return_These_Values[Trap_Size-1] += " [" +Trap_OID[i + 1, 0] + "," + Trap_OID[i + 1, 1] + "," + Trap_OID[i + 1, 2]+"]";
                            Console.WriteLine("Found {0} Notifications", Notification);
                        }
                    }                   
                }
            }

            return Return_These_Values;
        }


        // used to wait until a port is free
        public static void Delay(int second)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < (second * 1000))
            {

            }
            sw.Stop();

        }


        public static string SearchForConnection()
        {
            Socket connection;
            TcpListener Server;            
            IPAddress Server_IP_Address = IPAddress.Any;
            NetworkStream socketStream;


            string Command = "";
            try
            {
                //try to set up tcp listener
                Server = new TcpListener(Server_IP_Address, 161);
                Server.Start();

                Console.WriteLine("[Step 1] Server started listening...");
                bool gotConnection = false;
                while (!gotConnection)
                {                  
                    connection = Server.AcceptSocket();

                    //As soon as client has connected
                    Console.WriteLine("Connection found");
                    socketStream = new NetworkStream(connection);

                    // get command from the other client
                    Command = GetCommand(socketStream);

                    //when we have our command exit loop
                    gotConnection = true;

                    Server.Stop();
                    connection.Close();
                    socketStream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Somethings gone wrong while trying to connect " + e.ToString());              
            }

            return Command;
        }


        public static string GetCommand(NetworkStream socketStream)
        {
            string Command = "";
            try
            {
                StreamReader sr = new StreamReader(socketStream);

                // get the command from the packet sniffer
                Command = sr.ReadToEnd().Trim();

                Console.WriteLine("[Step 2] Got Command {0}", Command);
                return Command;
            }
            catch (Exception e)
            {
                Console.WriteLine("Somethings gone wrong while trying to read command " + e.ToString());
                return Command;
            }
        }


        public static string [] Request_Snmp(string Command)
        {
            string[] Temp = Command.Trim().Split('|');           
            string OIDs = Temp[1].Trim(); // Contains OID 
            string Request = Temp[0].Trim(); // contains [Get/ Get.next / Set]

            string Value = "None";
            // if it doesnt contain get then must be trap/set
            if (Request.Contains("Set"))
            {
                Value = Temp[2].Trim(); // Contains either new set value or Trap value
            }

            Console.WriteLine("[Step 3] Requesting from SNMP [Request: '{0}',OID: '{1}', Value '{2}']", Request, OIDs, Value);

            //this string [] contains the values to be return
            string[] Return_These_Values = new string[5];


                ///------------------------------------------------------------------------------------
                //all example code from snmpsharpnet

                // SNMP community name      
                OctetString community = new OctetString("public");

                // Define agent parameters class
                AgentParameters param = new AgentParameters(community);
                // Set SNMP version to 1 (or 2)
                param.Version = SnmpVersion.Ver1;
            // Construct the agent address object
            // IpAddress class is easy to use here because
            //  it will try to resolve constructor parameter if it doesn't
            //  parse to an IP address
            //192.168.8.100 127.0.0.1 localhost
            // 192.168.8.107 laptp
            IpAddress agent = new IpAddress("192.168.8.107");

                // Construct target
                UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 3);

            //---------------------------------------------------------------------------------
                // find out what type of request it is 
                Pdu pdu; //PDU class used for all requests in the libary
                SnmpV1Packet result = null;
                switch (Request)
                {
                    case "Get":
                        pdu = new Pdu(PduType.Get);
                        result = Get_SNMP(OIDs, pdu, param, target);
                        break;

                    case "Get-Next":
                        pdu = new Pdu(PduType.GetNext);
                        result = Get_SNMP(OIDs, pdu, param, target);
                        break;

                    case "Trap":
                        pdu = new Pdu(PduType.Trap);
                        Trap_SNMP(OIDs);
                        Return_These_Values[0] = "Trap Sent";
                        return Return_These_Values;


                default:
                        throw new System.ArgumentException("Does not match any known Request type", "Request_Snmp");

                }

                // If result is null then agent didn't reply or we couldn't parse the reply.
                if (result != null)
                {
                    // ErrorStatus other then 0 is an error returned by 
                    // the Agent - see SnmpConstants for error definitions
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        // agent reported an error with the request
                        Console.WriteLine("Error in SNMP reply, Null response. Error {0} index {1}",
                            result.Pdu.ErrorStatus,
                            result.Pdu.ErrorIndex);
                    }
                    else
                    {
                        // Reply variables are returned in the same order as they were added
                        //  to the VbList
                        for (int i = 0; i < result.Pdu.VbList.Count; i++)
                        {
                            Return_These_Values[i] = result.Pdu.VbList[i].Value.ToString();
                        }
                        Console.WriteLine("[Step 3] Got Values from SNMP '{0}'", Return_These_Values);
                    }
                }
                else
                {
                    // if it was a get or get-next and we got no reply 
                    if (Value == "None")
                    {
                        Console.WriteLine("No response received from SNMP agent.");
                    }

                }
                target.Close();
   
            if (Request.Contains("Trap") && Use_Custom_trap)
            {
                Trap_SNMP_Custom(OIDs, Return_These_Values[0]);
                Console.WriteLine("[Step 3.2] Trap Sent");
                Return_These_Values[0] = "Trap_Sent";
            }


            return Return_These_Values;
        }

        
          //-------------------------------------------------------------------------------
        //not my method 
        static void Trap_SNMP(String OID)
        {
            TrapAgent agent = new TrapAgent();

            // Variable Binding collection to send with the trap
            VbCollection col = new VbCollection();
            col.Add(new Oid("OID"), new OctetString("Test string"));

            // Send the trap to the localhost port 162
            agent.SendV1Trap(new IpAddress("192.168.8.107"),    //Host to send trap to
                162,                                        //port
                "public",                                   //community 
                new Oid("1.3.6.1.2.1.1.1.0"),               //sysObjectID value of your host (the sender)
                new IpAddress("127.0.0.1"),                 //Your hosts (sender) IP address
                SnmpConstants.LinkUp,                       //Generic TRAP integer value
                0,                                          //Specific TRAP integer value
                13432,                                      //Your hosts sysUpTime value.
                col);                                       //VbCollection holding OID/value pairs to include in the trap
 
        }
        //-----------------------------------------------------------------

        static void Trap_SNMP_Custom(String OID, string value)
        {
            int leng = Trap_Size;
            Trap_Size += 1;
            Trap_OID[leng, 0] = OID;
            Trap_OID[leng, 1] = value; //original value
            Trap_OID[leng, 2] = value; //current
            Any_Traps_Set = true;
        }



        static SnmpV1Packet Get_SNMP(String OID ,Pdu pdu, AgentParameters param, UdpTarget target)
        {
            pdu.VbList.Add(OID);

            // Make SNMP request
            SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);
            return result;
        }


        static void Respond_Snmp(string [] Return_These_Values)
        {
            Console.WriteLine("[Step 4] Responding to Manager");
            try
            {
                //make new client
                TcpClient client = new TcpClient();
                //192.168.8.100   localhost
                client.Connect("localhost", 43);
                StreamWriter sw = new StreamWriter(client.GetStream());

                if (Return_These_Values.Length != 0)
                {                  
                    string Message = string.Join("", Return_These_Values);
                    sw.WriteLine(Message, 0, Message.Length);

                    sw.Flush(); //tell windows to Send data packet 
                    Console.WriteLine("[Step 5] Sent '{0}' ", Return_These_Values);
                    Console.WriteLine("");
                    Console.WriteLine("");

                    // close the connections to the sniffer client/ snmp manager
                    sw.Close();
                    client.Close();
                }
                else
                {
                    Console.WriteLine("No args supplied");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(@"\n Somethings gone wrong" + e.ToString());
            }

        }

    }
}