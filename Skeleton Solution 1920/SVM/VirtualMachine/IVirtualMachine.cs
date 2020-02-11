using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVM.VirtualMachine
{
    public interface IVirtualMachine
    {       
        int ProgramCounter { get;}
        Stack Stack { get; set; }
        Hashtable labels { get; }
        List<int> linesWithBreakPoints { get; }
    }
}
