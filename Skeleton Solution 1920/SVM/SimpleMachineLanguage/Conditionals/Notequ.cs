using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine;

namespace SVM.SimpleMachineLanguage
{
    [Serializable]
    public class Notequ : BaseInstructionWithOperand
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        #endregion

        #region Non-public methods
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

        #region IInstruction Members

        public override void Run()
        {
            //foreach (string i in Operands)
            //{
            //    Console.WriteLine("Opperands :" + i);
            //}

            Stack steck = new Stack(VirtualMachine.Stack);
            try
            {
                int Value_Already_onStack_1 = (int)steck.Pop();
                int Value_Already_onStack_2 = (int)steck.Pop();

                if (Value_Already_onStack_1 != Value_Already_onStack_2)
                {
                    VirtualMachine.Stack.Push(Operands[0]);
                    //Console.WriteLine(Operands[0] + " Pushed to stack");
                }
            }
            catch
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                            this.ToString(), VirtualMachine.ProgramCounter));
            }             
        }
        #endregion
    }
}
