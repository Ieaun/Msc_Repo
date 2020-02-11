using System;
using System.Collections;

namespace UntrustedLibrary
{
    public class SML_UntrustedAdd
    {
        public void Execute(ref Stack stack)
        {
            try
            {
                if (stack.Count < 2)
                {
                    throw new InvalidOperationException("Stack was too small in " + this.ToString());
                }

                int op1 = (int)stack.Pop();
                int op2 = (int)stack.Pop();
                stack.Push(op1 + op2);
            }
            catch (InvalidCastException e)
            {
                throw new InvalidOperationException("Invalid values on the stack in " + this.ToString(), e);
            }
        }
    }
}
