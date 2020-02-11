namespace SVM.VirtualMachine
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    #endregion
    /// <summary>
    /// Utility class which generates compiles a textual representation
    /// of an SML instruction into an executable instruction instance
    /// </summary>
    internal static class JITCompiler
    {
        #region Constants
        #endregion

        #region Fields
        static List<Assembly> dll_Assemblies = new List<Assembly>();
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        #endregion

        #region Non-public methods
        internal static IInstruction CompileInstruction(string opcode)
        {
            IInstruction instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT

            //// first find all Dlls of type BaseInstruction in directory
            //if (dll_Assemblies.Count == 0)
            //{
            //    try
            //    {
            //        foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
            //        {
            //            Assembly A = Assembly.LoadFile(dll);
            //            string Fullname = A.FullName.ToLower();
            //            //Console.WriteLine(Fullname);
            //            string[] Temp = Fullname.Split(',');
            //            foreach (string section in Temp)
            //            {
            //                if (section.Contains("publickeytoken="))
            //                {
            //                    Console.WriteLine(section);
            //                    if (!section.Contains("null"))
            //                    {
            //                        Console.WriteLine(section);
            //                        dll_Assemblies.Add(A);
            //                        //Console.WriteLine(A); 
            //                    }
            //                }
            //            }                                                              
            //        }
            //    }
            //    catch
            //    { 
            //        //not an assembly
            //    }
            //}

            // first find all Dlls of type BaseInstruction in directory
            if (dll_Assemblies.Count == 0)
            {
                try
                {
                    foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
                    {
                        Assembly A = Assembly.LoadFile(dll);
                        dll_Assemblies.Add(A);
                        //Console.WriteLine(A);                   
                    }
                }
                catch
                {
                    //not an assembly
                }
            }
            if (dll_Assemblies.Count == 0) // if still 0 then add null so it doesnt search again this runtime
            {
                dll_Assemblies.Add(null);
            }

            //Console.WriteLine("Opcode:" + opcode);            
            // load all assemblies
            Type[] thissss = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type T in thissss)
            {
                //Console.WriteLine(T.Name);
                //find assembly that matches the opcode
                if (T.Name.ToLower() == opcode.ToLower())
                {                  
                    // if it implemented type IInstruction
                    if (T.BaseType.GetInterfaceMap(typeof(IInstruction)).InterfaceType.ToString() == "SVM.VirtualMachine.IInstruction")
                    {                       
                        instruction = Activator.CreateInstance(T) as IInstruction;
                        //Console.WriteLine("Created _ instruction :" + T.Name);
                    }
                }
            }

            //try search in other dll list
            if (instruction == null)
            {
                foreach (Assembly i in dll_Assemblies)
                {
                    Type[] Ts = i.GetTypes();

                    foreach (Type t in Ts)
                    {
                        if (t.BaseType == typeof(SVM.VirtualMachine.BaseInstruction))
                        {
                            if (t.ToString().ToLower().Contains(opcode.ToLower()))
                            {
                               // Console.WriteLine("+");
                                //Console.WriteLine("Opcode :"+ opcode +" Matched with dll: "+ t);
                                instruction = Activator.CreateInstance(t) as IInstruction;
                               // Console.WriteLine("+");
                            }
                        }
                    }
                }
            }


            //if still null then cant find it
            if (instruction == null)
            {
                Console.WriteLine("Throwing exeption on Opcode:" + opcode);
                new SvmCompilationException("Invalid SML instruction found in SML source");
            }
            //Console.WriteLine();
            #endregion

            return instruction;
        }

        internal static IInstruction CompileInstruction(string opcode, params string[] operands)
        {
            IInstructionWithOperand instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT

            //// first find all Dlls of type BaseInstruction in directory
            //if (dll_Assemblies.Count == 0)
            //{
            //    try
            //    {
            //        foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
            //        {
            //            Assembly A = Assembly.LoadFile(dll);
            //            string Fullname = A.FullName.ToLower();
            //            string[] Temp = Fullname.Split(',');
            //            foreach (string section in Temp)
            //            {
            //                if (section.Contains("publickeytoken="))
            //                {
            //                    Console.WriteLine(section);
            //                    if (!section.Contains("null"))
            //                    {
            //                        dll_Assemblies.Add(A);
            //                        //Console.WriteLine(A); 
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        //not an assembly
            //    }
            //}

            // first find all Dlls of type BaseInstruction in directory
            if (dll_Assemblies.Count == 0)
            {
                try
                {
                    foreach (string dll in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
                    {
                        Assembly A = Assembly.LoadFile(dll);
                        dll_Assemblies.Add(A);
                        //Console.WriteLine(A);                   
                    }
                }
                catch
                {
                    //not an assembly
                }
            }
            if (dll_Assemblies.Count == 0) // if still 0 then add null so it doesnt search again this runtime
            {
                dll_Assemblies.Add(null);
            }



            //Console.WriteLine();
            //Console.WriteLine("Opcode:" + opcode);          
            // load all assemblies
            Type[] thissss = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type T in thissss)
            {
                //Console.WriteLine(T.Name);
                //find assembly that matches the opcode
                if (T.Name.ToLower() == opcode.ToLower())
                {                    
                    // if it implemented type IInstruction
                    if (T.BaseType.GetInterfaceMap(typeof(IInstruction)).InterfaceType.ToString() == "SVM.VirtualMachine.IInstruction")
                    {                       
                        instruction = Activator.CreateInstance(T) as IInstructionWithOperand;
                        instruction.Operands = operands;
                        //Console.WriteLine("Created _ instruction :"  + T.Name+ "with opperands :"+ operands[0]);
                    }
                }
            }

            //try search in other dll
            if (instruction == null)
            {
                foreach (Assembly i in dll_Assemblies)
                {
                    Type[] Ts = i.GetTypes();

                    foreach (Type t in Ts)
                    {
                        if ( t.BaseType == typeof(SVM.VirtualMachine.BaseInstructionWithOperand))
                        {
                            //Console.WriteLine(t.BaseType + " == " +"BaseInstruction");
                            if (t.ToString().ToLower().Contains(opcode.ToLower()))
                            {
                                //Console.WriteLine("+");
                                //Console.WriteLine("Opcode :" + opcode + " Matched with: " + t);
                                instruction = Activator.CreateInstance(t) as IInstructionWithOperand;
                                instruction.Operands = operands;
                                //Console.WriteLine("+");
                            }
                        }
                    }
                }
            }

            if (instruction == null)
            {
                Console.WriteLine("Throwing exeption, operands Opcode: " + opcode);
                new SvmCompilationException("Invalid SML instruction found in SML source");
            }
            #endregion

            return instruction;
        }

        #endregion

    }
}
