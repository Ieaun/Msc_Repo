using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine.Debug;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using SVM;
using SVM.VirtualMachine;


namespace Debuggers 
{

    public class Debugger : MarshalByRefObject, IDebugger
    {
        #region TASK 5 - TO BE IMPLEMENTED BY THE STUDENT
        SvmVirtualMachine virtualMachine;
        public volatile bool Sleep_Mainthread = true;
        IDebugFrame debugFramer = null;
        Debug_UI Debugg_form;
        int DebugWindow_Open;

        delegate void Debugg_Update(IDebugFrame debugFrame, IVirtualMachine VirtualMachine);

        public void Break(IDebugFrame debugFrame)
        {
            
            Console.WriteLine();
            Console.WriteLine("-------------in Debugger Break ----------------");
            debugFramer = debugFrame;
            if (debugFramer == null)
            {
                Console.WriteLine("debugFrame isNull :" + debugFrame == null);
            }


            // open form
            Thread Debugggg = new Thread(new ThreadStart(Thread_run));
            Debugggg.Start();

            int counter = 0;
           // Console.WriteLine("Entering delay");           
            Thread.Sleep(5000); 
           // Console.WriteLine("out delay");           

            int Threads_Running_atStart = Process.GetCurrentProcess().Threads.Count;
            //Console.WriteLine("Going to sleep with Threads_Running_at_start:"+Threads_Running_atStart);
            while (Sleep_Mainthread)
            {                
                Console.WriteLine("Thread Sleep counter :"+counter +" Active Threads:"+ Process.GetCurrentProcess().Threads.Count + "Need to be over:"+ (Threads_Running_atStart+ 3));
                Thread.Sleep(1000);
                counter++;

                if (Process.GetCurrentProcess().Threads.Count > Threads_Running_atStart + 3)
                {
                    //Console.WriteLine("Thread Sleep counter :" + counter + " Active Threads:" + Process.GetCurrentProcess().Threads.Count);
                    Sleep_Mainthread = false;
                }
            }
            //Debugggg.Abort();
            Sleep_Mainthread = true;

            Console.WriteLine("-------------out Debugger Break ----------------");                    
        }

        public void Thread_run()
        {
            DebugWindow_Open = (int)virtualMachine.Stack.Pop();
            if (DebugWindow_Open == 0)
            {
                Debugg_form = new Debug_UI(debugFramer, virtualMachine);
                DebugWindow_Open = 1;

                Application.Run(Debugg_form);
            }
            else
            {
                Debugg_Update Debugg_Update = new Debugg_Update(Debugg_form.SetVM);
                Debugg_form.Invoke(Debugg_Update, debugFramer, virtualMachine);
            }

        }

        public SvmVirtualMachine VirtualMachine
        {
            set
            {
                virtualMachine = value;
            }
        }

        public bool Wake_Up_Thread
        {
            get
            {
                return Sleep_Mainthread;
            }
            set
            {
                Sleep_Mainthread = value;
            }
        }
   
        #endregion
    }
}
