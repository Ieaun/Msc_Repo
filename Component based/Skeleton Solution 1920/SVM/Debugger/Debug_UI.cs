using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SVM.VirtualMachine.Debug;
using SVM.VirtualMachine;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace Debuggers
{
    public partial class Debug_UI : Form
    {
        Thread Thread1;
        Thread Thread2;
        Thread Thread3;
        Thread Thread4;
        Thread Thread5;

        bool Thread_5_Started = false;

        IVirtualMachine Vm_Stack;
        IDebugFrame debugFrame;

        public IVirtualMachine vm_Stack
        {
            get {return Vm_Stack; }
            set {Vm_Stack = value;}
        }

        public IDebugFrame DebugFrame
        {
            get { return debugFrame; }
            set { debugFrame = value; }
        }

        public void SetVM(IDebugFrame Debu, IVirtualMachine vm)
        {
            Vm_Stack = vm;
            debugFrame = Debu;
            rbUpdate.Checked = false;
            btnContinue.Enabled = true;
        }


        //List<SVM.VirtualMachine.IInstruction> CodeFrame
        public Debug_UI(IDebugFrame DebugFrame, IVirtualMachine vm)
        {
            Console.WriteLine("*********** IN Debug UI ***********");
            debugFrame = DebugFrame;
            Vm_Stack = vm;

            InitializeComponent();
            SetLines();
        }

        public void SetLines()
        {
            lbCode.Items.Clear();
            lbStack.Items.Clear();
            int Index_of_current = 0;
            int counter = 0;

            //display each instruction
            foreach (IInstruction line in debugFrame.CodeFrame)
            {               
                if (line == debugFrame.CurrentInstruction)
                {                   
                    Index_of_current = counter;
                }
                lbCode.Items.Add(line.ToString());
                counter++;
            }
            //highlight current
            lbCode.SelectedIndex = Index_of_current;
            lbCode.Items[Index_of_current] = lbCode.Items[Index_of_current] +"----------";
            //


            List<string> Stacker = new List<string>();
            Stack stack = new Stack(Vm_Stack.Stack);
            foreach (var value in stack)
            {
                try
                {
                    Stacker.Add(value.ToString());
                }
                catch { Stacker.Add("Cannot add an item to the stack list"); }
            }

            Stacker.Reverse();

            foreach ( string value in Stacker)
            {
                try
                {
                    lbStack.Items.Add(value.ToString());
                }
                catch
                {
                    lbStack.Items.Add("Cannot add an item to the stack");
                }
            }

            rbUpdate.Checked = true;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = false;
            //contiue program execution
            Signal_Continue(); //signal continue by starting 5 threads and then closing them
            //Console.WriteLine("Threads: " + Process.GetCurrentProcess().Threads.Count);    
        }

        public void Signal_Continue()
        {
            //Console.WriteLine("Threads: " + Process.GetCurrentProcess().Threads.Count);
            Thread1 = new Thread(new ThreadStart(Loopthis));
            Thread1.IsBackground = true;
            Thread1.Start();
            Thread2 = new Thread(new ThreadStart(Loopthis));
            Thread2.IsBackground = true;
            Thread2.Start();
            Thread3 = new Thread(new ThreadStart(Loopthis));
            Thread3.IsBackground = true;
            Thread3.Start();
            Thread4 = new Thread(new ThreadStart(Loopthis));
            Thread4.IsBackground = true;
            Thread4.Start();
            Thread5 = new Thread(new ThreadStart(Loopthisss));
            Thread5.IsBackground = true;
            Thread5.Start();
            Thread_5_Started = true;
        }

        private void KillTheThreads()
        {
            Thread1.Abort();
            Thread2.Abort();
            Thread3.Abort();
            Thread4.Abort();                     
        }

        private void Loopthisss() // thread 5
        {
            int Threads_Running = Process.GetCurrentProcess().Threads.Count;
            //Console.WriteLine("-------------"+ Threads_Running);
            int k = 0;
            while (k < 1) // wait for threads count to get picked up by other class
            {
                Threads_Running = Process.GetCurrentProcess().Threads.Count;
                //Console.WriteLine(string.Format("Running {0}", Threads_Running));
                Thread.Sleep(1000);
                k++;
            }

            KillTheThreads(); // kill all threads                      
            //Console.WriteLine("Out of thread 5 loop");
            Thread5.Abort();
        }

        private void Loopthis() // thread 1-4
        {
            int i = 0;
            while (!Thread_5_Started || i < 2) // do this until all threads have started
            {
                Thread.Sleep(1000);
            }           
        }

        private void rbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUpdate.Checked == false)
            {
                SetLines();
            }         
        }

        private void Debug_UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            //btnContinue.Enabled = false;     
           // Signal_Continue();
        }
    }
}
