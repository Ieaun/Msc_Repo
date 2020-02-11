using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace SML_Extensions
{
    [Serializable]
    class DisplayImage : BaseInstruction
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
                if (VirtualMachine.Stack.Count < 1)
                {
                    throw new SvmRuntimeException(String.Format(BaseInstruction.StackUnderflowMessage,
                                                    this.ToString(), VirtualMachine.ProgramCounter));
                }

                // make sure its an image
                if (VirtualMachine.Stack.Peek().GetType() == typeof(Bitmap))
                {                   
                    Thread Debugggg = new Thread(new ThreadStart(image_Show));
                    Debugggg.Start();
                }
                else
                {
                    Console.WriteLine("Item on stack is not of type Bitmap");
                    throw new SvmRuntimeException("Item on stack is not of type Bitmap" + String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
                }
              
            }
            catch (InvalidCastException)
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }
        }

        public void image_Show()
        {
            try
            {
                Image Display_image = (Image)VirtualMachine.Stack.Pop();
                Form Imageform = new Form();
                Imageform.BackgroundImage = Display_image;
                Imageform.BackgroundImageLayout = ImageLayout.Stretch;
                Imageform.Name = "Your Image";
                Imageform.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(Imageform);
            }
            catch
            {
                Console.WriteLine("Failed to load image form");
            }
        }

        #endregion
    }
}
