using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine;

namespace SVM.SimpleMachineLanguage
{
    [Serializable]
    public class Equint : BaseInstructionWithOperand
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
           
            int opValue;
            if (!Int32.TryParse(this.Operands[0].ToString(), out opValue))
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }

            //foreach (string i in Operands)
            //{
            //    Console.WriteLine("Opperands :" + i);
            //}

            try
            {
                int Value_Already_onStack = (int)VirtualMachine.Stack.Peek();
                if (Value_Already_onStack == int.Parse(Operands[0]))
                {
                    VirtualMachine.Stack.Push(Operands[1]);
                    //Console.WriteLine(Operands[1] + " Pushed to stack");
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
