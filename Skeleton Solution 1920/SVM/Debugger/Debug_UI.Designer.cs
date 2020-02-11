namespace Debuggers
{
    partial class Debug_UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnContinue = new System.Windows.Forms.Button();
            this.lbCode = new System.Windows.Forms.ListBox();
            this.lbStack = new System.Windows.Forms.ListBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(40, 821);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(1015, 71);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lbCode
            // 
            this.lbCode.FormattingEnabled = true;
            this.lbCode.ItemHeight = 20;
            this.lbCode.Location = new System.Drawing.Point(40, 52);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(488, 744);
            this.lbCode.TabIndex = 1;
            // 
            // lbStack
            // 
            this.lbStack.FormattingEnabled = true;
            this.lbStack.ItemHeight = 20;
            this.lbStack.Location = new System.Drawing.Point(567, 52);
            this.lbStack.Name = "lbStack";
            this.lbStack.Size = new System.Drawing.Size(488, 744);
            this.lbStack.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(36, 20);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(47, 20);
            this.lblCode.TabIndex = 3;
            this.lblCode.Text = "Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(563, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Stack";
            // 
            // Debug_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 921);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lbStack);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.btnContinue);
            this.Name = "Debug_UI";
            this.Text = "SML Debugger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.ListBox lbCode;
        private System.Windows.Forms.ListBox lbStack;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label1;
    }
}