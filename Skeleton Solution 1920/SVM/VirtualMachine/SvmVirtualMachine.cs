using SVM.VirtualMachine.Debug;


namespace SVM
{
    #region Using directives
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.IO;
    using SVM.VirtualMachine;
    using System.Reflection;
    using System.Reflection.Emit;
    #endregion

    /// <summary>
    /// Implements the Simple Virtual Machine (SVM) virtual machine 
    /// </summary>
    [Serializable]
    public sealed class SvmVirtualMachine : IVirtualMachine
    {
        #region Constants
        private const string CompilationErrorMessage = "An SVM compilation error has occurred at line {0}.\r\n\r\n{1}";
        private const string RuntimeErrorMessage = "An SVM runtime error has occurred.\r\n\r\n{0}";
        private const string InvalidOperandsMessage = "The instruction \r\n\r\n\t{0}\r\n\r\nis invalid because there are too many operands. An instruction may have no more than one operand.";
        private const string InvalidLabelMessage = "Invalid label: the label {0} at line {1} is not associated with an instruction.";
        private const string ProgramCounterMessage = "Program counter violation; the program counter value is out of range";
        #endregion

        #region Fields
        private IDebugger debugger = null;
        private List<IInstruction> program = new List<IInstruction>();
        private Stack stack = new Stack();
        private int programCounter = 0;

        private List<int> LinesWithBreakPoints = new List<int>();
        private int breakpoint_index = 0;

        public Hashtable Labels = new Hashtable();



        #endregion

        #region Constructors

        public SvmVirtualMachine()
        {
            #region Task 5 - Debugging 
            // Do something here to find and create an instance of a type which implements 
            // the IDebugger interface, and assign it to the debugger field

            //Console.WriteLine("Constructor start--------------");
            Type thissss = Type.GetType("SVM.VirtualMachine.Debug.IDebugger");

            if (thissss != null)
            {
                // create new instance of debugger
                AppDomain newDomain = AppDomain.CreateDomain("My Domain");
                debugger = (IDebugger)newDomain.CreateInstanceAndUnwrap("Debugger", "Debuggers.Debugger");               
            }
            else
            {
                Console.WriteLine("No debugger");
            }

            //Console.WriteLine("Constructor End--------------");

                #endregion
        }
            #endregion

            #region Entry Point
            static void Main(string[] args)
        {
            if (CommandLineIsValid(args))
            {
                SvmVirtualMachine vm = new SvmVirtualMachine();
                try
                {
                    vm.Compile(args[0]);
                    vm.Run();
                }
                catch(SvmCompilationException)
                {
                }
                catch (SvmRuntimeException err)
                {
                    Console.WriteLine(RuntimeErrorMessage, err.Message);
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///  Gets a reference to the virtual machine stack.
        ///  This is used by executing instructions to retrieve
        ///  operands and store results
        /// </summary>
        public Stack Stack
        {
            get
            {
                return stack;
            }
            set
            {
                Stack = value;
            }
        }

        public Hashtable labels
        {
            get { return Labels; }
        }

        public List<int> linesWithBreakPoints
        {
            get { return LinesWithBreakPoints; }
        }





        /// <summary>
        /// Accesses the virtual machine 
        /// program counter (see programCounter in the Fields region).
        /// This can be used by executing instructions to 
        /// determine their order (ie. line number) in the 
        /// sequence of executing SML instructions
        /// </summary>
        public int ProgramCounter
        {
            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            get
            {
                return programCounter;
            }
            #endregion
        }
        #endregion

        #region Public Methods

        #endregion

        #region Non-public Methods



        /// <summary>
        /// Reads the specified file and tries to 
        /// compile any SML instructions it contains
        /// into an executable SVM program
        /// </summary>
        /// <param name="filepath">The path to the 
        /// .sml file containing the SML program to
        /// be compiled</param>
        /// <exception cref="SvmCompilationException">
        /// If file is not a valid SML program file or 
        /// the SML instructions cannot be compiled to an
        /// executable program</exception>
        private void Compile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new SvmCompilationException("The file " + filepath + " does not exist");
            }

            int lineNumber = 0;
            try
            {
                using (StreamReader sourceFile = new StreamReader(filepath))
                {
                    while (!sourceFile.EndOfStream)
                    {
                        string instruction = sourceFile.ReadLine();
                        if (!String.IsNullOrEmpty(instruction) && 
                            !String.IsNullOrWhiteSpace(instruction))
                        {
                            ParseInstruction(instruction, lineNumber);
                            lineNumber++;
                        }
                    }
                }
            }
            catch (SvmCompilationException err)
            {
                Console.WriteLine(CompilationErrorMessage, lineNumber, err.Message );
                throw;
            }
        }

        /// <summary>
        /// Executes a compiled SML program 
        /// </summary>
        /// <exception cref="SvmRuntimeException">
        /// If an unexpected error occurs during
        /// program execution
        /// </exception>
        private void Run()
        {
            DateTime start = DateTime.Now;

            #region TASK 2 - TO BE IMPLEMENTED BY THE STUDENT

            SvmVirtualMachine virtualMachine = new SvmVirtualMachine();
            virtualMachine.Labels = Labels;

            ////////Console.WriteLine("In Run");
            //Console.WriteLine("Stack Count:" + stack.Count);

            //IInstruction instruct in program
            while (programCounter < program.Count)
            {
                IInstruction instruct = program[programCounter];
                programCounter++; // set instruction
                instruct.VirtualMachine = virtualMachine;

                #region Console debugging     
                if (instruct == null || virtualMachine==null)
                {
                    Console.WriteLine(string.Format("Instruction : [{0}]", instruct));
                    Console.WriteLine(string.Format("is Instruction null : [{0}]", instruct == null));
                    Console.WriteLine(string.Format("is VM null : [{0}]", virtualMachine == null));
                }
                #endregion

                //Console.WriteLine("Program Counter :"+ programCounter +string.Format(" Instruction : [{0}]", instruct));
                               
                if (LinesWithBreakPoints.Count > breakpoint_index) // prevents LinesWithBreakPoints[breakpoint_index] = null
                {
                    if (programCounter == LinesWithBreakPoints[breakpoint_index]) //breakpoint_index = counter for the list
                    {
                        #region TASKS 5 & 7 - MAY REQUIRE MODIFICATION BY THE STUDENT
                        // For task 5 (debugging), you should construct a IDebugFrame instance and
                        // call the Break() method on the IDebugger instance stored in the debugger field
                        if (debugger != null)
                        {
                            Console.WriteLine(string.Format("Break point found at line {0} Entering debugger", LinesWithBreakPoints[breakpoint_index]));
                            List<IInstruction> Code = new List<IInstruction>();
                            int total_instructions = program.Count;
                            IInstruction Currrent_instruct = null;

                            //previous
                            for (int i = 0; i < 4; i++)
                            {
                                int indexxy = programCounter - 4 + i;
                                if (indexxy > 0)
                                {
                                    //Console.Write(i);
                                    //Console.WriteLine(program[indexxy]);
                                    Code.Add(program[indexxy]);
                                }
                            }

                            //Console.WriteLine("----------------------------");

                            //Code.Add(instruct); // current
                            Currrent_instruct = program[programCounter];

                            // next
                            for (int i = 0; i < 4; i++)
                            {
                                int indexxy = i + programCounter;
                                if (indexxy < total_instructions)
                                {
                                    //Console.Write(i+" Total :"+total_instructions+ " Program Counter: "+ programCounter);
                                    //Console.WriteLine(program[indexxy]);
                                    Code.Add(program[indexxy]);
                                }
                            }
                            Console.WriteLine("Current Instruct: " + Currrent_instruct); // current

                            Debug_Data D_data = new Debug_Data(Currrent_instruct, Code); // debug frame implementation                       
                            debugger.Break(D_data);
                            

                            breakpoint_index++; // index for the breakpoints
                        }                      
                    }
                    #endregion
                }

                //run instruction
                instruct.Run();


                //Console.WriteLine("Stack :"+ virtualMachine.stack.Count);
                if (virtualMachine.stack.Count > 0)
                {
                    string Peek_stack_for_label = virtualMachine.stack.Peek().ToString().Trim();
                    if (Peek_stack_for_label.Contains("%"))
                    {
                        virtualMachine.stack.Pop(); // dont need the lable there anymore 
                        //Console.WriteLine("-------------Cond change prog counter------------");
                        foreach (var key in Labels.Keys)
                        {
                            //Console.WriteLine(key);
                        }

                        //Console.WriteLine("Peek Value :" + Peek_stack_for_label);
                        //Console.WriteLine("Program counter :" + programCounter);
                        programCounter = (int)Labels[Peek_stack_for_label];
                        //Console.WriteLine("Program counter conditional change to :" + programCounter);                        
                        //Console.WriteLine("Going to instruction: ["+ program[programCounter] + "] Stack contents: ["+ virtualMachine.stack.Peek()+"] On Line: "+ programCounter );
                       // Console.WriteLine("-----------------------");
                    }
                }

            }
            //////Console.WriteLine("Out Run");

       
            #endregion
            long memUsed = System.Environment.WorkingSet;
            TimeSpan elapsed = DateTime.Now - start;
            Console.WriteLine(String.Format(
                                        "\r\n\r\nExecution finished in {0} milliseconds. Memory used = {1} bytes",
                                        elapsed.Milliseconds,
                                        memUsed));
        }

        /// <summary>
        /// Parses a string from a .sml file containing a single
        /// SML instruction
        /// </summary>
        /// <param name="instruction">The string representation
        /// of an instruction</param>
        private void ParseInstruction(string instruction, int lineNumber)
        {
            #region TASK 5 & 7 - MAY REQUIRE MODIFICATION BY THE STUDENT
            //Console.WriteLine("Parse Instruction: ["+ instruction+ "] Line number: ["+ lineNumber+"]");
            if (instruction.Contains("* "))
            {
                LinesWithBreakPoints.Add(lineNumber);
                Console.WriteLine("Breakpoint at line: "+ lineNumber);
                instruction = instruction.Replace("*","").Trim();
            }

            if (instruction[0] == '%')
            {
                string [] How_many = instruction.Split('%'); //(%addone%) = lenght of 3 on split
                if (How_many.Length == 3)
                {
                    string [] Values = instruction.Split(' ');
                    string label = Values[0].Trim();
                    instruction = Values[1].Trim();
                    //Console.WriteLine("Label :" + label+ " Instruction" + instruction);
                    Labels.Add(label, lineNumber); //add to label hashtable                   
                }
            }
                #endregion

            string[] tokens = null;
            if (instruction.Contains("\""))
            {
                tokens = instruction.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                
                // Remove any unnecessary whitespace
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i] = tokens[i].Trim();
                }
            }
            else
            {
                // Tokenize the instruction string by separating on spaces
                tokens = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            

            // Ensure the correct number of operands
            if (tokens.Length > 3)
            {
                throw new SvmCompilationException(String.Format(InvalidOperandsMessage, instruction));
            }

            switch (tokens.Length)
            {
                case 1:
                    program.Add(JITCompiler.CompileInstruction(tokens[0]));
                    break;
                case 2:
                    program.Add(JITCompiler.CompileInstruction(tokens[0], tokens[1].Trim('\"')));
                    break;
                case 3:
                    program.Add(JITCompiler.CompileInstruction(tokens[0], tokens[1].Trim('\"'),tokens[2].Trim('\"')));
                    break;
            }
        }


        #region Validate command line
        /// <summary>
        /// Verifies that a valid command line has been supplied
        /// by the user
        /// </summary>
        private static bool CommandLineIsValid(string[] args)
        {
            bool valid = true;

            if (args.Length != 1)
            {
                DisplayUsageMessage("Wrong number of command line arguments");
                valid = false;
            }

            if (valid && !args[0].EndsWith(".sml",StringComparison.CurrentCultureIgnoreCase))
            {
                DisplayUsageMessage("SML programs must be in a file named with a .sml extension");
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Displays comamnd line usage information for the
        /// SVM virtual machine 
        /// </summary>
        /// <param name="message">A custom message to display
        /// to the user</param>
        static void DisplayUsageMessage(string message)
        {
            Console.WriteLine("The command line arguments are not valid. {0} \r\n", message);
            Console.WriteLine("USAGE:");
            Console.WriteLine("svm program_name.sml");
        }
        #endregion
        #endregion

        #region System.Object overrides
        /// <summary>
        /// Determines whether the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object">Object</see> to compare with the current <see cref="System.Object">Object</see>.</param>
        /// <returns><b>true</b> if the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <returns>A <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

    }
}
