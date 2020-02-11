namespace Trustworthy_Coursework
{
    partial class Method_List_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbMethods = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblselected = new System.Windows.Forms.Label();
            this.tbparam1 = new System.Windows.Forms.TextBox();
            this.tbparam2 = new System.Windows.Forms.TextBox();
            this.tbparam3 = new System.Windows.Forms.TextBox();
            this.tbparam4 = new System.Windows.Forms.TextBox();
            this.tbparam5 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblparamcount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.rtbResults = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbAssem = new System.Windows.Forms.ComboBox();
            this.lblCounter = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the method to run";
            // 
            // lbMethods
            // 
            this.lbMethods.FormattingEnabled = true;
            this.lbMethods.ItemHeight = 24;
            this.lbMethods.Location = new System.Drawing.Point(29, 120);
            this.lbMethods.Name = "lbMethods";
            this.lbMethods.Size = new System.Drawing.Size(868, 292);
            this.lbMethods.TabIndex = 1;
            this.lbMethods.SelectedIndexChanged += new System.EventHandler(this.lbMethods_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Selected method:";
            // 
            // lblselected
            // 
            this.lblselected.AutoSize = true;
            this.lblselected.Location = new System.Drawing.Point(207, 426);
            this.lblselected.Name = "lblselected";
            this.lblselected.Size = new System.Drawing.Size(45, 25);
            this.lblselected.TabIndex = 3;
            this.lblselected.Text = "Null";
            // 
            // tbparam1
            // 
            this.tbparam1.Location = new System.Drawing.Point(98, 28);
            this.tbparam1.Name = "tbparam1";
            this.tbparam1.Size = new System.Drawing.Size(206, 29);
            this.tbparam1.TabIndex = 4;
            this.tbparam1.Enter += new System.EventHandler(this.tbparam1_Enter);
            // 
            // tbparam2
            // 
            this.tbparam2.Location = new System.Drawing.Point(345, 28);
            this.tbparam2.Name = "tbparam2";
            this.tbparam2.Size = new System.Drawing.Size(208, 29);
            this.tbparam2.TabIndex = 5;
            this.tbparam2.Enter += new System.EventHandler(this.tbparam2_Enter);
            // 
            // tbparam3
            // 
            this.tbparam3.Location = new System.Drawing.Point(610, 29);
            this.tbparam3.Name = "tbparam3";
            this.tbparam3.Size = new System.Drawing.Size(211, 29);
            this.tbparam3.TabIndex = 6;
            this.tbparam3.Enter += new System.EventHandler(this.tbparam3_Enter);
            // 
            // tbparam4
            // 
            this.tbparam4.Location = new System.Drawing.Point(98, 82);
            this.tbparam4.Name = "tbparam4";
            this.tbparam4.Size = new System.Drawing.Size(206, 29);
            this.tbparam4.TabIndex = 7;
            this.tbparam4.Enter += new System.EventHandler(this.tbparam4_Enter);
            // 
            // tbparam5
            // 
            this.tbparam5.Location = new System.Drawing.Point(345, 79);
            this.tbparam5.Name = "tbparam5";
            this.tbparam5.Size = new System.Drawing.Size(208, 29);
            this.tbparam5.TabIndex = 8;
            this.tbparam5.Enter += new System.EventHandler(this.tbparam5_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "1:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "2:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(575, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "3:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(63, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "4:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(310, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 25);
            this.label8.TabIndex = 14;
            this.label8.Text = "5:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblparamcount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbparam1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbparam2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbparam3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbparam4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbparam5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(29, 469);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 129);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // lblparamcount
            // 
            this.lblparamcount.AutoSize = true;
            this.lblparamcount.Location = new System.Drawing.Point(737, 79);
            this.lblparamcount.Name = "lblparamcount";
            this.lblparamcount.Size = new System.Drawing.Size(23, 25);
            this.lblparamcount.TabIndex = 16;
            this.lblparamcount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(575, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Total Parameters:";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(335, 622);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(225, 47);
            this.btnGo.TabIndex = 16;
            this.btnGo.Text = "Execute";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // rtbResults
            // 
            this.rtbResults.Location = new System.Drawing.Point(29, 699);
            this.rtbResults.Name = "rtbResults";
            this.rtbResults.Size = new System.Drawing.Size(868, 282);
            this.rtbResults.TabIndex = 17;
            this.rtbResults.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 671);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 25);
            this.label9.TabIndex = 18;
            this.label9.Text = "Results:";
            // 
            // cbAssem
            // 
            this.cbAssem.FormattingEnabled = true;
            this.cbAssem.Location = new System.Drawing.Point(29, 15);
            this.cbAssem.Name = "cbAssem";
            this.cbAssem.Size = new System.Drawing.Size(868, 32);
            this.cbAssem.TabIndex = 19;
            this.cbAssem.SelectedIndexChanged += new System.EventHandler(this.cbAssem_SelectedIndexChanged);
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(737, 73);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(23, 25);
            this.lblCounter.TabIndex = 20;
            this.lblCounter.Text = "0";
            // 
            // Method_List_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 1027);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.cbAssem);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rtbResults);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblselected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbMethods);
            this.Controls.Add(this.label1);
            this.Name = "Method_List_Form";
            this.Text = "Methods";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbMethods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblselected;
        private System.Windows.Forms.TextBox tbparam1;
        private System.Windows.Forms.TextBox tbparam2;
        private System.Windows.Forms.TextBox tbparam3;
        private System.Windows.Forms.TextBox tbparam4;
        private System.Windows.Forms.TextBox tbparam5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblparamcount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbResults;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbAssem;
        private System.Windows.Forms.Label lblCounter;
    }
}