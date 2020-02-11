using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVM.VirtualMachine.Debug
{
    [Serializable]
    class Debug_Data : IDebugFrame
    {
        public Debug_Data(IInstruction currentInstruction, List<IInstruction> codeFrame)
        {
            if (currentInstruction == null || codeFrame == null)
            {
                Console.WriteLine("-------------IN DEBUGGER constructor-----------------");
                Console.WriteLine("currentInstruction isNull :"+ currentInstruction == null);
                Console.WriteLine("codeFrame isNull :"+ codeFrame ==null);
            }

            CodeFrame = codeFrame;
            CurrentInstruction = currentInstruction;

            //Console.WriteLine("-------------OUT DEBUGGER constructor-----------------");
        }

        public IInstruction CurrentInstruction
        { get; }

        public List<IInstruction> CodeFrame
        { get; }

    }
}
