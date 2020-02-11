namespace SVM.SimpleMachineLanguage
{
    #region Using directives
    using System;
    using SVM.VirtualMachine;
    #endregion
    /// <summary>
    /// Implements the SML Incr  instruction
    /// Increments the integer value stored on top of the stack, 
    /// leaving the result on the stack
    /// </summary>
    /// 
    [Serializable]
    public class Incr : BaseInstruction
    {
        #region TASK 3 - TO BE IMPLEMENTED BY THE STUDENT
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

        #region IInstruction Members

        public override void Run()
        {
            try
            {
                // if nothing on the stack (empty stack check)
                if (VirtualMachine.Stack.Count < 1)
                {
                    throw new SvmRuntimeException(String.Format(BaseInstruction.StackUnderflowMessage,
                                                    this.ToString(), VirtualMachine.ProgramCounter));
                }

                object StackItem = VirtualMachine.Stack.Pop();
                //if top stack value not an integer (Incorrect data types check)
                if (StackItem.GetType() != typeof(int) && StackItem.GetType() != typeof(Int16) && StackItem.GetType() != typeof(Int64))
                {
                    throw new SvmRuntimeException(String.Format(BaseInstruction.StackUnderflowMessage,
                                                    this.ToString(), VirtualMachine.ProgramCounter));
                }
                else
                {
                    int Number = (int)StackItem;
                    //Console.WriteLine("This is your number good sir :"+ Number);
                    Number = Number + 1;
                    VirtualMachine.Stack.Push(Number);
                    //Console.WriteLine("Tis but now on the stack :" + Number);
                }
               
            }
            catch (InvalidCastException)
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }
        }

        #endregion
        #endregion
    }
}
