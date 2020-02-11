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



        //List<SVM.VirtualMachine.IInstruction> CodeFrame
        public Debug_UI(IDebugFrame debugFrame)
        {
            Console.WriteLine("*********** IN Debug UI ***********");
            InitializeComponent();
            SetLines(debugFrame);
        }

        public void SetLines(IDebugFrame debugFrame)
        {
            int Index_of_current = 0;
            int counter = 0;

            //display each instruction
            foreach (IInstruction line in debugFrame.CodeFrame)
            {
                lbCode.Items.Add(line.ToString());
                if (line == debugFrame.CurrentInstruction)
                {
                    Index_of_current = counter;
                }
                counter++;
            }
            //highlight current
            lbCode.SelectedIndex = Index_of_current;


            //display stack contents
            List<string> Stacker = new List<string>();
            for (int i = Index_of_current -4 ; i < Index_of_current; i++)
            {
                if (Index_of_current >= 0)
                {
                    string [] instuct = debugFrame.CodeFrame[i].ToString().Split(' ');
                    if (instuct.Length == 2)
                    {
                        Stacker.Add(instuct[1]);                       
                    }
                }
            }

            Stacker.Reverse(); // flip the list so in LiFo order

            foreach (string Item in Stacker)
            {
                lbStack.Items.Add(Item);
            }
            
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
    }
}
