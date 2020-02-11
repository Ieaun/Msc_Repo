using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using System.Threading;

namespace client
{
    public partial class Form1 : Form
    {
     
        string Packetz = "";
        static string TimeNow_i = DateTime.Now.ToString().Replace("/","_").Replace(":","_").Replace(" ","_").Trim();
        string FileName = TimeNow_i + ".txt";
        string Packet_ByteData = "";
        string LocalAddress;
        string protocol_Type;
        // what types of packets to display (usign global variable to avoid cross thread error)
        string Filter_Mode = "ALL";

        //counts how many times we have tried to communicate with snmp agent
        int counter_Lis=0;

        bool Listen_for_SNMP_Response = false; // when active ,program listens for snmp agent reply
        string ReadData;

        NetworkStream socketStream; //holds localhost stream
        CaptureDeviceList devices; //holds the list of devices

        // thread that listens for SNMP response
        Thread handle_SNMP_Response;

        // thread that listens for TCP (Telnet) coms from the localhost
        Thread Local_Host_TCP_Thread;

        // all of these update the gui on their main threads (is there another way of doing this?)
        public delegate void updateGUI(string Packet_data); 
        public delegate void Update_Rtb_devicelist(string text); 
        public delegate void Update_lb_RecievePacks(string text);
        public delegate void Update_RTB_SNMP(string text);
        public delegate void Reactivate_Execute_Button();

        //Listens for SNMP response 
        TcpListener ListenerTCP_SNMP;

        //used to time operations
        Stopwatch sw; 


        public Form1()
        {
            InitializeComponent();
            Populate_OIDs_List();
        }

        private void Populate_OIDs_List()
        {
            //Sys
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.1.0 (sysDescr)");
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.2.0 (sysObjectID)");
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.3.0 (sysUpTime)");
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.4.0 (sysContact)");
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.5.0 (sysName)");
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.6.0 (sysLocation)");
            CbOIDs_RequestTypes.Items.Add("1.3.6.1.2.1.1.7.0 (sysServices)");

            //hrSystem
            CbOIDs_RequestTypes.Items.Add(".1.3.6.1.2.1.25.1.1.0 (hrSystemUptime)");
            CbOIDs_RequestTypes.Items.Add(".1.3.6.1.2.1.25.1.2.0 (hrSystemDate)");
            CbOIDs_RequestTypes.Items.Add(".1.3.6.1.2.1.25.1.5.0 (hrSystemNumUsers)");
            CbOIDs_RequestTypes.Items.Add(".1.3.6.1.2.1.25.1.6.0 (hrSystemProcesses)");
            CbOIDs_RequestTypes.Items.Add(".1.3.6.1.2.1.25.1.7.0 (hrSystemMaxProcesses)");

            //hrStorage
            CbOIDs_RequestTypes.Items.Add(".1.3.6.1.2.1.25.2.2.0 (hrMemorySize)");


        }


        private void Btn_SendMessage_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage(string Message = "")
        {
            // if talking to the snmp agent use port 161
            int Port_Number = 161;

            // if you want to send message to the client then use telnet port
            if (tabControl1.SelectedIndex == 1)
            { Port_Number = 43; }

            //string Server_IP_Address = "localhost";
            string Server_IP_Address = "150.237.93.193";

            try
            {
                TcpClient client = new TcpClient();
                IPAddress Server_IP_Address2 = IPAddress.Any;
                client.Connect(Server_IP_Address, Port_Number);

                StreamWriter sw = new StreamWriter(client.GetStream());
                if (Message == "")
                { Message = RTB_Message.Text; }

                if (Message != "")
                {

                    sw.WriteLine(Message,0,Message.Length);
                    sw.Flush();                        //tell windows to Send data packet  
                    sw.Close();
                    client.Close();
                }
                else
                {
                    RTB_Message.AppendText("No args supplied");
                }

            }
            catch (Exception e)
            {
                RTB_Message.AppendText(@"\n Somethings gone wrong" + e.ToString());
            }

        }

        private void BtnDeviceList_Click(object sender, EventArgs e)
        {
            // Print SharpPcap version 
            string ver = SharpPcap.Version.VersionString;
            RTB_Device_List.AppendText(string.Format("SharpPcap {0}", ver));
         
            //update the gui
            gbstep1.Enabled = false;
            gbStep2.Enabled = true;

            try
            {
                // Retrieve the device list
                devices = CaptureDeviceList.Instance;
            }
            catch
            {
                // If no devices were found print an error
                RTB_Device_List.AppendText("\nNo devices were found on this machine");
                ////add host names to cb
                cbDeviceList.Items.Add("LocalHost");

                return;
            }

            RTB_Device_List.AppendText("\nThe following devices are available on this machine:");
            RTB_Device_List.AppendText("\n----------------------------------------------------\n");
            RTB_Device_List.AppendText("Please choose a device in drop down (step 2) to capture from \n \n");

            int j = 0;
            foreach (PcapDevice dev in devices)
            {
                /* Device Description */
                RTB_Device_List.AppendText(string.Format("[{0}],{1} \n", j, devices[j].Description.ToString()));
                //cbDeviceList.Items.Add(dev.Interface.FriendlyName);
                cbDeviceList.Items.Add(dev.Description);
                j++;
            }
            ////add host names to cb
            cbDeviceList.Items.Add("LocalHost");
        }


        private void Btn_Clear_Click(object sender, EventArgs e)
        {          
            RTB_Device_List.Clear();
            lbRecievePackets.Items.Clear();
            lBByteData.Items.Clear();
        }

        // when a user choses what network device to monitor 
        private void CbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = cbDeviceList.SelectedIndex;
            RTB_Device_List.Clear();
            if (devices != null)
            {
                if (cbDeviceList.SelectedIndex < devices.Count - 1)
                { RTB_Device_List.AppendText(devices[Index].ToString()); }
            }
            gBstep3.Enabled = true;


        }

        //--------------------------------------------------------------------------------------------------------

        private string Format_Filter()
        {
            string [] Filter = new string [8] ;
            string Filter_Phrase = "";

            /* if any filter is selected then set that 
               array pos to the filter name, if not then empty space*/
            Filter[0] = CBIP.Checked ?  "ip" :  "";
            Filter[1] = cbTCP.Checked ?  "tcp" : "";
            Filter[2] = CBUDP.Checked ?  "udp" : "";
            Filter[3] = CBARP.Checked ?  "arp" : "";
            Filter[4] = CBRARP.Checked ?  "rarp" : "";
            Filter[5] = CBIP6.Checked ?  "ip6" : "";
            Filter[6] = cBTR.Checked ?  "tr" : "";
            Filter[7] = CBETHER.Checked ?  "ether" : "";

            for (int i = 0; i < Filter.Length - 1 ; i++)                          // Format the array into a string in the form "Ip and TCP and UDP"
            {
                if (Filter[i] != "")
                { 
                    
                    if (i != Filter.Length - 1 && Filter_Phrase != "" )
                    {
                        Filter_Phrase += " and";
                    }
                    Filter_Phrase = Filter_Phrase + " " + Filter[i];
                }
            }
       
            return Filter_Phrase;
        }



        private void BtnCapturePackets_Click(object sender, EventArgs e)
        {
            // get the selected network device
            int Choice = cbDeviceList.SelectedIndex;
            int Leng_cb = cbDeviceList.Items.Count;
            if (cbDeviceList.SelectedItem.ToString() == "LocalHost")
            {
                RTB_Device_List.Text = "Searching for packets on localhost...";
                Local_Host_TCP_Thread = new Thread(new ThreadStart( Using_Local_Host ));
                Local_Host_TCP_Thread.Start();
                
            }
            else
            {
                //----------------------------------------- adapted code ----------------------------------------------------------
                // Extract a device from the list
                ICaptureDevice device = devices[Choice];


                // Register our handler function to the
                // 'packet arrival' event
                device.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);
                File.WriteAllText(FileName, "Time     Length              [Length,Protocol,Version,DestinationPort,SourcePort]\n");
                gbFilter_2.Enabled = true;
                gbStep2.Enabled = false;
                gbcapturepacks.Enabled = false;
                



                // Open the device for capturing
                int readTimeoutMilliseconds = 1000;     //timeout
                if (rbNormalMode.Checked)
                {
                    device.Open(DeviceMode.Normal, readTimeoutMilliseconds);     // only capture packets address to current network adapter
                }
                if (RB_Promiscuous.Checked)
                {
                    device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);  //capture all packets
                }
                //--------------------------------------------------------------------------------------------------------------

                tbLayerType.Text = devices[Choice].LinkType.ToString();

                string Filter_Packets = Format_Filter().Trim();  /// format filter data

                if (Filter_Packets != "")
                {
                    RTB_Device_List.AppendText(string.Format("Applying Filter for [{0}]" + "\n", Filter_Packets));
                    device.Filter = Filter_Packets;
                }
                else
                {
                    RTB_Device_List.AppendText("No Filter " + "\n");
                }

                RTB_Device_List.AppendText(string.Format("\n -- Listening on {0}......." + "\n", device.Description));

                // Start the capturing process
                device.StartCapture();
                
            }
        }

        //listening for snmp response
        private void Using_Local_Host()
        {
            // if talking to the snmp agent use port 43
            int Port_Number = 43;
      
            Socket connection;          
            IPAddress Server_IP_Address = IPAddress.Any;  

            ListenerTCP_SNMP = new TcpListener(Server_IP_Address, Port_Number);
            ListenerTCP_SNMP.Start();

            //As soon as client has connected
            connection = ListenerTCP_SNMP.AcceptSocket();
            connection.ReceiveTimeout =1000;  //stop listening after 1 second
            LocalAddress = connection.LocalEndPoint.ToString(); // get address (always 127.0.0.1)
            protocol_Type = connection.ProtocolType.ToString(); // get protocol (likely tcp)
         
          
                //update the coms listbox
                //string Message = string.Format("{0} {1} -> {1} ", protocol_Type, LocalAddress);
                //Update_lb_RecievePacks UpdateGUI3 = new Update_lb_RecievePacks(add_to_lb_recievepacks);
                //this.RTB_Device_List.Invoke(UpdateGUI3, Message);

                counter_Lis++;
                string text2 = string.Format("Connection found ({0})", counter_Lis);
                Update_Rtb_devicelist UpdateGUI2 = new Update_Rtb_devicelist(Rtb_device);
                this.RTB_Device_List.Invoke(UpdateGUI2, text2);
            

            socketStream = new NetworkStream(connection);
            socketStream.ReadTimeout=1000;
            DoRequest();

            //Close the connections
            ListenerTCP_SNMP.Stop();          
            connection.Close();
            socketStream.Close();               
            Listen_for_SNMP_Response = false;
        }


        public void DoRequest()
        {
            string Message = string.Format("{0}", "SNMP");
            try
            {
                StreamReader sr = new StreamReader(socketStream);
                
                ReadData = sr.ReadLine();
                Message += ReadData;
                Message = Message.Trim();

                // if we were listening for snmp respons we just got it, so stop
                Listen_for_SNMP_Response = false;
               
                Update_lb_RecievePacks UpdateGUI = new Update_lb_RecievePacks(add_to_lb_recievepacks);
                this.RTB_Device_List.Invoke(UpdateGUI, Message);
            }
            catch (Exception e)
            {
                string text = ("Somethings gone at 'Do Request'" + e.ToString());
                Update_Rtb_devicelist UpdateGUI = new Update_Rtb_devicelist(Rtb_device);
                this.RTB_Device_List.Invoke(UpdateGUI, text);
            }
        }

        // called from another thread, this appends text to the rtb on the main thread
        public void Rtb_device(string packet_string)
        {
            RTB_Device_List.AppendText("\n"+packet_string);
        }

        public void add_to_lb_recievepacks(string Item)
        {
            lbRecievePackets.Items.Add(Item);
            lBByteData.Items.Add("SNMP Packet");
        }



        private void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);         
            var Ip_Packet = packet.Extract<PacketDotNet.IPPacket>();                      

            // this will contain the type of protocol e. Tcp,Icmp,UDP
            string protocol = "";     

            if (Ip_Packet != null)
            {
                protocol = Ip_Packet.Protocol.ToString();

                //determine what type of packet we are dealthing with 
                switch (protocol)
                {
                    case "Tcp" :
                        if (Filter_Mode == "ALL" || Filter_Mode == "Tcp")
                        { TCP_Packet_Handler(e); }
                        break;

                    case "Udp":
                        if (Filter_Mode == "ALL" || Filter_Mode == "Udp" || Filter_Mode == "Snmp")
                            UDP_Packet_Handler(e);
                        break;

                    case "Icmp":
                        if (Filter_Mode == "ALL" || Filter_Mode == "Icmp")
                            ICMP_Packet_Handler(e);
                        break;

                    case "IcmpV6":
                        if (Filter_Mode == "ALL" || Filter_Mode == "Icmp")
                            ICMPV6_Packet_Handler(e);
                        break;

                    case "Igmp":
                        Igmp_Packet_Handler(e);
                        break;

                    default:
                        break;
                }
            }
        }

        public void Igmp_Packet_Handler(CaptureEventArgs e)
        {
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var igmpPacket = packet.Extract<PacketDotNet.IgmpV2Packet>();
            var ipPacket = (PacketDotNet.IPPacket)igmpPacket.ParentPacket;

            DateTime time = e.Packet.Timeval.Date;
            string Protocol = ipPacket.Protocol.ToString();

            //parse packet data
            byte[] data3 = ipPacket.PayloadPacket.PayloadData;
            Packet_ByteData = "";
            if (data3 != null)
            { Packet_ByteData = System.Text.RegularExpressions.Regex.Replace(System.Text.Encoding.ASCII.GetString(data3), @"[^a-zA-Z_0-9\.\@\- ]", ""); }



            Packetz = string.Format("{0} {1} {2}", Protocol, time, Packet_ByteData);
            updateGUI UpdateGUI = new updateGUI(AddPacketData);
            try
            {
                RTB_Device_List.Invoke(UpdateGUI, Packetz);
            }
            catch { }
        }




        public void TCP_Packet_Handler(CaptureEventArgs e)
        {
            //parse data into packet
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();
            var ipPacket = (PacketDotNet.IPPacket)tcpPacket.ParentPacket;

            //extract data out of packet 
            //get time / length /  source / destination / Protocol data / time to live
            //sequence number / Packet ID 
            System.Net.IPAddress srcIp = ipPacket.SourceAddress;
            System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
            int srcPort = tcpPacket.SourcePort;
            int dstPort = tcpPacket.DestinationPort;
            string Protocol = ipPacket.Protocol.ToString();
            string Version = ipPacket.Version.ToString();
            DateTime time = e.Packet.Timeval.Date;
            int len = e.Packet.Data.Length;
            string sequence_Number = tcpPacket.SequenceNumber.ToString();
            bool Sync_Pack = tcpPacket.Synchronize;
            //using property info to get the property called Id within the tcp packet
            System.Reflection.PropertyInfo pi_2 = tcpPacket.ParentPacket.GetType().GetProperty("Id");
            string tll = "Unavailable";
            string Packet_ID = "Unavailable";
            if (pi_2 != null)
            {
                 Packet_ID = pi_2.GetValue(tcpPacket.ParentPacket, null).ToString();
                //using property info to get the property called HopLimit within the tcp packet
                System.Reflection.PropertyInfo pi = tcpPacket.ParentPacket.GetType().GetProperty("HopLimit");
                tll = pi.GetValue(tcpPacket.ParentPacket, null).ToString();
            }

            // get flag (SYC,RST,URG,ACK...)
            string flag = tcpPacket.Flags.ToString();
            flag = determine_flag(flag);

            
            // set up data decode
            byte[] data3;
            string Parsed_Data="";
            if (flag != "")
            { Parsed_Data = string.Format("({0})          ", flag); }
            else { Parsed_Data = "                        "; }

            //if its syn packet then no data is sent, only TCP/IP handshake
            if (!Sync_Pack)
            {
                //parse packet data
                data3 = ipPacket.PayloadPacket.PayloadData;

                byte[] Data_Yes = new byte[data3.Length];
                for (int i = 0; i < data3.Length - 160; i++)
                {
                    Data_Yes[i] = data3[160 + i];
                }
                Parsed_Data += System.Text.RegularExpressions.Regex.Replace(System.Text.Encoding.ASCII.GetString(Data_Yes), @"[^a-zA-Z_0-9\.\@\- ]", "");

            }
            



            string IP_Src = "";
            string IP_Dst = "";
            //try to resolve the host names turning ip addresses into readable data
            if (cbHostNames.Checked)
            {
                try
                {
                    //try to do a dns lookup of the IP 
                    IPHostEntry entry = Dns.GetHostEntry(srcIp);
                    IP_Src = entry.HostName;
                    IPHostEntry entry2 = Dns.GetHostEntry(dstIp);                 
                    IP_Dst = entry2.HostName;
                }
                catch
                {
                    // if one of them failed then it will still be "" so set it to the IP
                    if (IP_Src == "")
                    { IP_Src = srcIp.ToString(); }
                    if (IP_Dst == "")
                    { IP_Dst = dstIp.ToString(); }  
                }
            }
            else
            {
                IP_Src = srcIp.ToString();
                IP_Dst = dstIp.ToString();
            }

            // update gui on a thread
            Packet_ByteData = Parsed_Data;
            if (Packet_ByteData == "")
            { Packet_ByteData = "_"; }

            string Additional_String = len + "," + Protocol + "," + Version + "," + dstPort + "," + srcPort + "," + tll + "," + sequence_Number + "," + Packet_ID + "," + flag;         
            Packetz = string.Format("{0,-5} {1}:{2}:{3,-5} {4}:{5,-1} -> {6}:{7,-10} {8,-110}"+
                "  [{9}]", Protocol, time.Hour, time.Minute, time.Second, IP_Src, srcPort, IP_Dst, dstPort, Packet_ByteData, Additional_String);
            updateGUI UpdateGUI = new updateGUI(AddPacketData);
            try
            {
                RTB_Device_List.Invoke(UpdateGUI, Packetz);
            }
            catch { }

        }

        //handle UDP packets
        public void UDP_Packet_Handler(CaptureEventArgs e)
        {
            //parse data into packet
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var UDP_Packet = packet.Extract<PacketDotNet.UdpPacket>();


            //get time / length /  source / destination / Protocol data
            var ipPacket = (PacketDotNet.IPPacket)UDP_Packet.ParentPacket;
            System.Net.IPAddress srcIp = ipPacket.SourceAddress;
            System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
            int srcPort = UDP_Packet.SourcePort;
            int dstPort = UDP_Packet.DestinationPort;

            
            string Protocol = ipPacket.Protocol.ToString();
            string Version = ipPacket.Version.ToString();
            DateTime time = e.Packet.Timeval.Date;
            int len = e.Packet.Data.Length;
            string sequence_Number = "None";
            //using property info to get the property called Id within the tcp packet
            System.Reflection.PropertyInfo pi_2 = UDP_Packet.ParentPacket.GetType().GetProperty("Id");
            string Packet_ID = "None";
            try
            { Packet_ID = pi_2.GetValue(UDP_Packet.ParentPacket, null).ToString(); }
            catch {  }
            
            //using property info to get the property called HopLimit within the tcp packet
            System.Reflection.PropertyInfo pi = UDP_Packet.ParentPacket.GetType().GetProperty("HopLimit");
            string tll = pi.GetValue(UDP_Packet.ParentPacket, null).ToString();

            //parse packet data
            byte[] data3 = ipPacket.PayloadPacket.PayloadData;
            Packet_ByteData = System.Text.RegularExpressions.Regex.Replace(System.Text.Encoding.ASCII.GetString(data3), @"[^a-zA-Z_0-9\.\@\- ]", "");

            string All_SNMP_Values = "";
            if (srcPort == 161 || dstPort ==161 || srcPort == 162 || dstPort == 162)
            {
                string hexc = BitConverter.ToString(data3);
                string[] Hex = hexc.Split('-');

                All_SNMP_Values = Parse_This_SNMP_Packet(Hex);

                Protocol = "Snmp";
            }


            // update gui on a thread
            string Additional_String;
            if (Protocol == "Snmp")
            {
                Additional_String = All_SNMP_Values;
            }
            else
            {
                Additional_String = len + "," + Protocol + "," + Version + "," + dstPort + "," + srcPort + "," + tll + "," + sequence_Number + "," + Packet_ID;
            }

            Packetz = string.Format("{0}   {1}:{2}:{3}     {4}:{5} -> {6}:{7,-28}      {8},"+
                "                                                                    [{9}]", Protocol, time.Hour, time.Minute, time.Second, srcIp, srcPort, dstIp, dstPort, Packet_ByteData, Additional_String);

            // filter mode == Protocol == Snmp or UDP 
            if (Filter_Mode == Protocol || Filter_Mode == "ALL")
            { updateGUI UpdateGUI = new updateGUI(AddPacketData);

                try
                {
                    RTB_Device_List.Invoke(UpdateGUI, Packetz);
                }
                catch { }
            }

        }

        /// <summary>
        ///1/[0-1] / 30-35                    // length = 0x35 = 53
        ///2/[2-4] /-02-01-00                 // version = 0x00 = version 1
        ///3/[5-12]/-04-06-70-75-62-6C-69-63  // community string in clear text 
        ///4/[13-14]/-A2-28                   // 0xa2 = snmp get response with lenght 0x28
        ///5/[15-20]/-02-04-3F-8A-D4-07       // Request ID 
        ///6/[21-23]/-02-01-00                // Error code
        ///7/[24-26]/-02-01-00                // Error index
        ///8/[27-28]/-30-1A
        ///9/[29-30]/-30-18
        ///10/-06-08-2B-06-01-02-01-01-02-00       //OID
        ///11/-06-0C-2B-06-01-04-01-82-37-01-01-03-01-01
        ///
        /// RFC 1592
        /// 
        /// 
        /// RFC 1592 
        /// 
        /// </summary>
        /// <param name="hex"></param>


        // packets are encoded in ASN.1 
        // this methode decodes them
        private string Parse_This_SNMP_Packet(string[] hex)
        {

            string SNMP_Message_Type = CheckIdentifier(hex[0]);

            //1 Length 
            string Length = Hex_to_int(hex[1]);

            //2 Version
            string Version = hex[4];
            switch (Version)
            {
                case "00":
                    Version = "v1";
                    break;
                case "01":
                    Version = "v2";
                    break;
                default:
                    Version = "Unknown";
                    break;
            }

            //3 Community
            string Community = "";
            string Com_SNMP_Type = CheckIdentifier(hex[5]);
            string length_of_community_Name = Hex_to_int(hex[6]);

            int Len_C_Name = int.Parse(length_of_community_Name);

            //comunity name is in pos 7 + length of the name
            for (int i = 0; i < Len_C_Name; i++)
            {
                Community += hex[7 + i];
            }
            Community = Hex_to_String(Community);

            //4 request Type and length 
            string request_Type = CheckIdentifier(hex[7 + Len_C_Name]);
            string request_Length = Hex_to_int(hex[7 + Len_C_Name + 1]);

            // incomplete, only half of the packet has been decoded
            if (request_Type == "TrapRequest PDU")
            {
                Len_C_Name = 7 + Len_C_Name + 2;

                string Trap_SNMP = "";
                string _Type___ = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;

                
                string lennggg = Hex_to_int(hex[Len_C_Name]);
                Len_C_Name++;
                string[] OID_ = new string[int.Parse(lennggg)];
                for (int i = 0; i < int.Parse(lennggg); i++)
                {
                    OID_[i] = hex[Len_C_Name];
                    Len_C_Name++;
                }
                string OID = hex_to_oid(OID_);
                string here = "";

                _Type___ = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                lennggg = Hex_to_int(hex[Len_C_Name]);
                Len_C_Name++;


                string Agent_IP_Address= "";
                string[] Agent_IP = new string[int.Parse(lennggg)];

                for (int i = 0; i < int.Parse(lennggg); i++)
                {
                    string temp = Hex_to_int(hex[Len_C_Name]);
                    Agent_IP_Address += temp + ".";
                    Len_C_Name++;                
                }

                here = "";

                return Trap_SNMP;
            }
            else
            {


                //5.1 Request ID 
                Len_C_Name = 7 + Len_C_Name + 2;
                string Request_ID_type = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Request_ID_Length = Hex_to_int(hex[Len_C_Name]);
                string[] Request_ID = new string[int.Parse(Request_ID_Length)];
                for (int i = 0; i < int.Parse(Request_ID_Length); i++)
                {
                    Request_ID[i] = hex[Len_C_Name];
                    Len_C_Name++;
                }
                string Req_ID_value = hex_to_32Bit(Request_ID);
                Len_C_Name++;


                //5.2 Error
                string Error_Code_type = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Error_Code_Length = Hex_to_int(hex[Len_C_Name]);
                string[] Error_Code = new string[int.Parse(Error_Code_Length)];
                for (int i = 0; i < int.Parse(Error_Code_Length); i++)
                {
                    Error_Code[i] = hex[Len_C_Name];
                    Len_C_Name++;
                }
                string Error_Code_Value = hex_to_32Bit(Error_Code);
                Len_C_Name++;


                //5.3 error index
                string Error_index_type = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Error_index_Length = Hex_to_int(hex[Len_C_Name]);
                string[] Error_index_Code = new string[int.Parse(Error_index_Length)];
                for (int i = 0; i < int.Parse(Error_index_Length); i++)
                {
                    Error_index_Code[i] = hex[Len_C_Name];
                    Len_C_Name++;
                }
                string Error_index_Value = hex_to_32Bit(Error_index_Code);
                Len_C_Name++;


                //6.1 Varbind List
                string Varbind_List_Type = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Varbind_List_Length = Hex_to_int(hex[Len_C_Name]);
                Len_C_Name++;

                //6.2 VarBind
                string Varbind_Type = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Varbind_Length = Hex_to_int(hex[Len_C_Name]);
                Len_C_Name++;


                //7 Object Identifier
                string Object_Identifier_Type = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Object_Identifier_Length = Hex_to_int(hex[Len_C_Name]);
                Len_C_Name++;
                string[] OID_ = new string[int.Parse(Object_Identifier_Length)];
                for (int i = 0; i < int.Parse(Object_Identifier_Length); i++)
                {
                    OID_[i] = hex[Len_C_Name];
                    Len_C_Name++;
                }
                string OID = hex_to_oid(OID_);

                //7.2 Value
                string Val___ = CheckIdentifier(hex[Len_C_Name]);
                Len_C_Name++;
                string Val___Lenght = Hex_to_int(hex[Len_C_Name]);
                Len_C_Name++;

                string Result___ = "None";
                string string_result_Final = "None";
                if (int.Parse(Val___Lenght) > 0)
                {
                    string[] Result_Array = new string[int.Parse(Val___Lenght)];
                    for (int i = 0; i < int.Parse(Val___Lenght); i++)
                    {
                        Result_Array[i] = hex[Len_C_Name];
                        Len_C_Name++;
                    }
                    Result___ = hex_to_oid(Result_Array);

                    string Results_array_string = array_to_string(Result_Array);
                    // get the string representation of all the hex
                    string_result_Final = Hex_to_String(Results_array_string);

                }


                // all of the snmp vlaues are stored below
                string[] AllSNMP_Values = new string[20];
                AllSNMP_Values[0] = string_result_Final;
                AllSNMP_Values[1] = Length;
                AllSNMP_Values[2] = Version;
                AllSNMP_Values[3] = Community;
                AllSNMP_Values[4] = request_Type;
                AllSNMP_Values[5] = request_Length;
                AllSNMP_Values[6] = Req_ID_value;
                AllSNMP_Values[7] = Error_Code_type;
                AllSNMP_Values[8] = Error_Code_Length;
                AllSNMP_Values[9] = Error_Code_Value;
                AllSNMP_Values[10] = Error_index_type;
                AllSNMP_Values[11] = Error_index_Length;
                AllSNMP_Values[12] = Error_index_Value;
                AllSNMP_Values[13] = Object_Identifier_Type;
                AllSNMP_Values[14] = Object_Identifier_Length;
                AllSNMP_Values[15] = OID;
                AllSNMP_Values[16] = Result___;

                string SNMP_Values = array_to_string(AllSNMP_Values);

                return SNMP_Values;
            }
        }

        // make a single string out of an array 
        private string array_to_string(string [] Array__)
        {
            string Single_String  ="";

            for (int i = 0; i < Array__.Length; i++)
            {
                if (Array__[i] != "" && Array__[i] != null)
                {
                    Single_String += Array__[i];
                    if (i < Array__.Length - 1 && (Array__[i + 1] != "" && Array__[i + 1] != null))
                    {
                        Single_String += ",";
                    }
                }
            }

            return Single_String;
        }

        // use this when trying to convert hex into OID 
        private string hex_to_oid(string [] hexoid)
        {
            string [] OID = new string[hexoid.Length];
            
            for (int i = 0; i < OID.Length; i++)
            {
                OID[i] = Convert.ToInt32(hexoid[i], 16).ToString();
            }
            string Oid_Str = string.Join(".", OID);

            return Oid_Str;

        }

        // assign the correct identiffier to the hex value
        private string CheckIdentifier(string hex)
        {
            string identifier = "";
            switch (hex)
            {
                case "02":
                    identifier = "integer";
                    break;
                case "04":
                    identifier = "Octet String";
                    break;
                case "05":
                    identifier = "Null";
                    break;
                case "06":
                    identifier = "Object Identifier";
                    break;
                case "30":
                    identifier = "Sequence";
                    break;
                case "A0":
                    identifier = "GetRequest PDU";
                    break;
                case "A1":
                    identifier = "GetNext PDU";
                    break;
                case "A2":
                    identifier = "GetResponse PDU";
                    break;
                case "A3":
                    identifier = "SetRequest PDU";
                    break;
                case "A4":
                    identifier = "TrapRequest PDU";
                    break;
                default:
                    identifier = string.Format("Unknown :{0}", hex);
                    break;
            }
            return identifier;
        }

        //convert hex string to int
        private string Hex_to_int(string hex)
        {
            byte Temp = Convert.ToByte(hex, 16);
            hex = Temp.ToString();
            return hex;
        }



        private string hex_to_32Bit(string [] Hex)
        {
            int total = 0;
            foreach (string hex_ in Hex)
            {
                // Convert the number to int
                int Number = Convert.ToInt32(hex_, 16);
                // Get the character corresponding to the int 
                total+= Number;
            }


            return total.ToString();
        }

        //convert the hex value (7075626C6963) to byte then to string
        private string Hex_to_String(string Hex)
        {
            if (Hex.Contains(','))
            {
                Hex = Hex.Replace(",","");
            }
            byte [] data = new byte[Hex.Length / 2];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(Hex.Substring(i * 2, 2), 16);
            }

            string result = "";
            result = Encoding.ASCII.GetString(data);

            return result;
        }


        //handle ICMP packets
        public void ICMP_Packet_Handler(CaptureEventArgs e)
        {
            //parse data into packet
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var ICMP_Packet = packet.Extract<PacketDotNet.IcmpV4Packet>();
            var ICMP_Packetv6 = packet.Extract<PacketDotNet.IcmpV6Packet>();

            if (ICMP_Packet != null)
            {
                //get time / length /  source / destination / Protocol data
                var ipPacket = (PacketDotNet.IPPacket)ICMP_Packet.ParentPacket;
                System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                DateTime time = e.Packet.Timeval.Date;
                int len = e.Packet.Data.Length;
                string Protocol = ipPacket.Protocol.ToString();
                string Version = ipPacket.Version.ToString();
                //Request type
                string Typ_request = ipPacket.PayloadPacket.ToString();
                string[] temp = Typ_request.Split('=');
                Typ_request = temp[1].Replace("]", "").Replace("}", "").Trim();

                //parse packet data
                byte[] data3 = ipPacket.PayloadPacket.PayloadData;
                Packet_ByteData = System.Text.RegularExpressions.Regex.Replace(System.Text.Encoding.ASCII.GetString(data3), @"[^a-zA-Z_0-9\.\@\- ]", "");
                


                // update gui on a thread
                string Additional_String = len + "," + Protocol + "," + Version;              
                Packetz = string.Format("{0}  {1}:{2}:{3}       {4}-> {5}      Type:{6}                                 "+
                    "[{7}]", Protocol, time.Hour, time.Minute, time.Second, srcIp, dstIp, Typ_request, Additional_String);
                updateGUI UpdateGUI = new updateGUI(AddPacketData);
                try
                { RTB_Device_List.Invoke(UpdateGUI, Packetz); }
                catch { }
            }
        }


        //handle ICMP packets
        public void ICMPV6_Packet_Handler(CaptureEventArgs e)
        {
            //parse data into packet
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var ICMP_Packetv6 = packet.Extract<PacketDotNet.IcmpV6Packet>();
            Packet_ByteData = "";

            if (ICMP_Packetv6 != null)
            {
                //get time / length /  source / destination / Protocol data
                var ipPacket = (PacketDotNet.IPPacket)ICMP_Packetv6.ParentPacket;
                System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                DateTime time = e.Packet.Timeval.Date;
                int len = e.Packet.Data.Length;
                string Protocol = ipPacket.Protocol.ToString();
                string Version = ipPacket.Version.ToString();
                //Request type
                string Typ_request = ipPacket.PayloadPacket.ToString();
                string[] temp = Typ_request.Split('=');
                Typ_request = temp[1].Replace("]", "").Replace("}", "").Trim();

               
                //parse packet data
                byte[] data3 = ipPacket.PayloadPacket.PayloadData;
                if (data3 != null)
                { Packet_ByteData = System.Text.RegularExpressions.Regex.Replace(System.Text.Encoding.ASCII.GetString(data3), @"[^a-zA-Z_0-9\.\@\- ]", ""); }



                // update gui on a thread
                string Additional_String = len + "," + Protocol + "," + Version;
                Packetz = string.Format("{0}  {1}:{2}:{3}       {4}-> {5}      Type:{6}                                 " +
                    "[{7}]", Protocol, time.Hour, time.Minute, time.Second, srcIp, dstIp, Typ_request, Additional_String);
                updateGUI UpdateGUI = new updateGUI(AddPacketData);
                try
                {
                    RTB_Device_List.Invoke(UpdateGUI, Packetz);
                }
                catch { }
            }
        }



        //after packet is processed and decoded the data is sent here to be displayed
        public void AddPacketData(string packet_string)
        {
            //File.AppendAllText(FileName, packet_string);              
            if (Packet_ByteData != "" && packet_string != "")
            {               
                this.lbRecievePackets.Items.Add(packet_string); // time, Source, Destination
                this.lBByteData.Items.Add(Packet_ByteData);
            }
            if (cbAutoScroll.Checked)
            {
                lbRecievePackets.SelectedIndex = lbRecievePackets.Items.Count - 1;
                lbRecievePackets.SelectedIndex = -1;
            }
        }

        // determine what the flag no represents
        private string determine_flag(string flagnum)
        {
            int flagno = int.Parse(flagnum);
            string flag;
            switch (flagno)
            {
                case 1:
                    flag = "Fin";
                    break;
                case 2:
                    flag = "Syn";
                    break;
                case 4:
                    flag = "Rst";
                    break;
                case 8:
                    flag = "Psh";
                    break;
                case 16:
                    flag = "Ack";
                    break;
                case 20:
                    flag = "Ack Rst";
                    break;
                case 24:
                    flag = "Ack Psh";
                    break;
                case 32:
                    flag = "Urg";
                    break;
                default:
                    flag = "";
                    break;
            }
            return flag;
        }


        //compare port number to common ports
        private string check_Common_Port(int Port_No)
        {
            string Application = Port_No.ToString();//worst case just return the port no if this doesnt find anything
            switch (Port_No)
            {
                case 20:
                    Application += " (FTP)";
                    break;
                case 23:
                    Application += " (Telnet)";
                    break;
                case 25:
                    Application += " (SMTP)";
                    break;
                case 80:
                    Application += " (HTTP)";
                    break;
                case 53:
                    Application += " (DNS)";
                    break;
                case 67:
                    Application += " (BOOTP)";
                    break;
                case 69:
                    Application += " (TFTP)";
                    break;
                case 123:
                    Application += " (NTP)";
                    break;
                case 161:
                    Application += " (SNMP)";
                    break;
                case 443:
                    Application += " (SSL HTTP)";
                    break;
                default:
                    break;
            }
            return Application;
        }



        //update gui
        private void InitDecoder()
        {
            RTB_Device_List.Clear();
            gbstep1.Enabled = true;
            gbStep2.Enabled = false;
            gBstep3.Enabled = false;
            cbDeviceList.Items.Clear();
            rtbByteData.Clear();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            InitDecoder();
        }

        private void LbRecievePackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lBByteData.SelectedIndex = lbRecievePackets.SelectedIndex;
                string packet_string = lbRecievePackets.SelectedItem.ToString();
                string[] data = packet_string.Split('[');
                string[] temp = data;
                data = data[1].Replace("]", "").Split(','); // length, version, protocol

                if (temp[0].Contains("Snmp"))
                {
                    do_SNMP_GUI_Update(data);
                    gbAll_other_packets.Visible = false;
                    gbSNMP_Decode.Visible = true;
                }
                else
                {
                    gbAll_other_packets.Visible = true;
                    gbSNMP_Decode.Visible = false;     
                    this.tbLength.Text = data[0];
                    this.tbVersion.Text = data[1];
                    this.tbProtocol.Text = data[2];
                    this.tbSource_Port.Text = check_Common_Port(int.Parse(data[3]));
                    this.tbDestination_Port.Text = check_Common_Port(int.Parse(data[4]));

                    try
                    {
                        // this data is not present in all packet types
                        this.tbTtl.Text = data[5];
                        this.tbSequence_Number.Text = data[6];
                        this.tbpacketID.Text = data[7];
                        this.tbFlag.Text = data[8];
                    }
                    catch { tbFlag.Text = "None"; }
                }                                             
            }
            catch
            {
                //if it fails here its most likely due to the snmp commands being logged with the normal packets
            }
        }

        //update gui with snmp packet data
        private void do_SNMP_GUI_Update(string []array)
        {
            string here = "";
            tbcommunity.Text = array[3];
            tbVersion_SNMP.Text = array[2];
            tblenght_SNMP.Text = array[1];
            tbPDUTYpe.Text = array[4];

            tb_SNMP_type_rid.Text = array[4];
            tb_SNMP_Len_rid.Text = array[5];
            tb_SNMP_Val_rid.Text = array[6];

            tb_SNMP_type_e.Text = array[7];
            tb_SNMP_Len_err.Text = array[8];
            tb_SNMP_Val_er.Text = array[9];

            tb_SNMP_type_ei.Text = array[10];
            tb_SNMP_Len_eri.Text = array[11];
            tb_SNMP_Val_ei.Text = array[12];

            tb_SNMP_oid.Text = array[15];
            tbOid_value_snmp.Text = array[16];
            tb_varbind_len.Text = array[14];

            tbString_snmp.Text = array[0];
        }

        private void LBByteData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string data = lBByteData.SelectedItem.ToString();
            rtbByteData.Text = data;
        }

        private void CbOIDs_RequestTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbOID.Text = CbOIDs_RequestTypes.Text;
        }

        // this method sends the data to the snmp agent to execute a specific request
        private void btnExecute_Click(object sender, EventArgs e)
        {
            Query_Agent(tbOID.Text);
        }



        private void SNMP_Response()
        {
           
            Listen_for_SNMP_Response = true; //tells program, listening for snmp agent not telnet port
            sw = new Stopwatch();
            sw.Start();

            //MessageBox.Show("still listening ");
            if (ListenerTCP_SNMP != null)
            {
                // make sure the previous listner isnt still listening 
                ListenerTCP_SNMP.Stop();
                ListenerTCP_SNMP = null;
            }


            //update the richtextbox
            string text__ = ("started listening...");
            Update_Rtb_devicelist UpdateGUI__ = new Update_Rtb_devicelist(Rtb_device);
            this.RTB_Device_List.Invoke(UpdateGUI__, text__);


            //keep listening for 2 seconds or until we have confirmation of a response 
            while (Listen_for_SNMP_Response && sw.ElapsedMilliseconds < 2000)
            {
                // listen for response 
                Using_Local_Host();
            }
        
            Listen_for_SNMP_Response = false;
            sw.Stop();

            string text = "Stopped Listening \n-------------\n";
            Update_Rtb_devicelist UpdateGUI_ = new Update_Rtb_devicelist(Rtb_device);
            this.RTB_Device_List.Invoke(UpdateGUI_, text);

            //response from the snmp
            string response = ReadData;
            Update_RTB_SNMP UpdateGUI = new Update_RTB_SNMP(Add_to_SNMP_RTB);
            this.rtbSNMPResponse.Invoke(UpdateGUI, response);       

            // reactivate the execute button
            Reactivate_Execute_Button UpdateGUI4 = new Reactivate_Execute_Button(reactivate_exe_button);
            this.rtbSNMPResponse.Invoke(UpdateGUI4);

            handle_SNMP_Response.Abort();
            
        }

        private void reactivate_exe_button()
        {
            btnExecute.Enabled = true;
        }



        private void Add_to_SNMP_RTB(string response)
        {
            // meaning there is a notification along with the response
            if (response.Contains('['))
            {
                string[] temp = response.Replace("]", "").Split('[');         
                string Response___ = temp[0]; // results for the get request or whatever it was originally doing
                string [] notification= new string[6];
                for (int i = 1; i < temp.Length; i++ )
                {
                    notification[i-1] = temp[i]; // the notification added to the original request
                }


                rtbSNMPResponse.Text = Response___.Trim();
                notification = notification.Where(n => n != null).ToArray();
                rtbSNMPResponse.AppendText("\n\n-------Notification-------\n");

                string[] temp2 = new string[3];
                for (int i = 0; i < notification.Length; i++)
                {
                    temp2 = notification[i].Split(',');
                    rtbSNMPResponse.AppendText(string.Format("OID: {0}\n", temp2[0]));
                    rtbSNMPResponse.AppendText(string.Format("State Changed from: [{0}]  to  [{1}]\n", temp2[1], temp2[2]));
                }                                                         
            }
            else { rtbSNMPResponse.Text = response; }
            
        }



        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //abort all additional threads
            if (handle_SNMP_Response != null)
            { handle_SNMP_Response.Abort(); }

            if (Local_Host_TCP_Thread != null)
            { Local_Host_TCP_Thread.Abort(); }

            //close socket
            if (socketStream != null)
            { socketStream.Close(); }

            //stop packet capture
            ICaptureDevice device = devices[cbDeviceList.SelectedIndex];
            if (device != null)
            {
                device.StopCapture();
            }


        }

        //makes UI changes based on the request type
        private void cbRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RequestType = cbRequestType.Text.Trim();
            if (RequestType != "")
            {
                switch (RequestType)
                {
                    case "Get":
                        gbSNMP_Value.Visible = false;
                        break;
                    case "Get-Next":
                        gbSNMP_Value.Visible = false;
                        break;
                    case "Set":
                        gbSNMP_Value.Visible = true;
                        break;
                    case "Trap":
                        gbSNMP_Value.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

            
            
        

        private void Query_Agent(string OID)
        {
            btnExecute.Enabled = false;            
                                                    //OID =  1.3.6.1.2.1.1.1.0 (sysDescr)
            string request = cbRequestType.Text;   // SET
            string Value = tbSET_Trap_Value.Text;  // Macbook Pro
            string Message = "";                   // SET | 1.3.6.1.2.1.1.1.0 | Macbook Pro

            try
            {
                if (OID != "" && request != "")
                {
                    // get oid we are working with
                    if (OID.Contains("("))
                    {
                        string[] temp = OID.Split('('); // 1.3.6.1.2.1.1.1.0 (sysDescr)
                        OID = temp[0].Trim(); // throw away the rest
                    }


                    // make message string according to the request type
                    if (request.Contains("Get")) //Get next or get
                    {
                        Message = request + " | " + OID; // Get and Get-Next have same structure
                    }
                    else
                    {                 
                        // Trap request and OID
                        Message = request + " | " + OID;

                    }

                    //send the message to the agent
                    SendMessage(Message);

                    handle_SNMP_Response = new Thread(SNMP_Response);
                    handle_SNMP_Response.Start();

                }
                else
                {
                    throw new Exception("Value not set");                   
                }
            }
            catch (Exception Excep)
            {
                MessageBox.Show("Either OID, Request or Value has not been set", "Set Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Reactivate_Execute_Button UpdateGUI4 = new Reactivate_Execute_Button(reactivate_exe_button);
                this.rtbSNMPResponse.Invoke(UpdateGUI4);
            }

            gbSNMP_Value.Visible = false;
        }

        private void Check_Filter_RB(object sender, EventArgs e)
        {
            if (rbALl.Checked == true)
            {
                Filter_Mode = "ALL";
                return;
            }

            if (rBUDP.Checked == true)
            {
                Filter_Mode = "Udp";
                return;
            }

            if (rbSNMP.Checked == true)
            {
                Filter_Mode = "Snmp";
                return;
            }

            if (rbTCP.Checked == true)
            {
                Filter_Mode = "Udp";
                return;
            }
 
            if (rbICMP.Checked == true)
            {
                Filter_Mode = "Icmp";
                return;
            }
                    
                
            
        }
    }
}

