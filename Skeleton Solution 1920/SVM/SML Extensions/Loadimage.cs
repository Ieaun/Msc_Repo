using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine;
using System.Drawing;

namespace SML_Extensions
{
    [Serializable]
    class Loadimage : BaseInstructionWithOperand
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
            try
            {
                if (Operands[0].GetType() != typeof(string))
                {
                    throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                    this.ToString(), VirtualMachine.ProgramCounter));
                }
            }
            catch
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.VirtualMachineErrorMessage,
                                        this.ToString(), VirtualMachine.ProgramCounter));
            }

           // Console.WriteLine("IN LOAD IMAGE");
            foreach (string op in Operands)
            {
                Console.WriteLine("Operands in loadimage :"+op);
            }

            try
            {
                Image VarImage = Image.FromFile(Operands[0]);
                VirtualMachine.Stack.Push(VarImage); 
            }
            catch // not valid
            {
                throw new SvmRuntimeException("Could not push image onto stack" + String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }
        }

        #endregion
    }
}
