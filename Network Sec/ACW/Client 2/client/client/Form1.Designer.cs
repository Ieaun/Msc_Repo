namespace client
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btn_SendMessage = new System.Windows.Forms.Button();
            this.RTB_Message = new System.Windows.Forms.RichTextBox();
            this.RTB_Output = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RTB_Device_List = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeviceList = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbAll_other_packets = new System.Windows.Forms.GroupBox();
            this.tbFlag = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbSequence_Number = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbTtl = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbpacketID = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbSource_Port = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDestination_Port = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbProtocol = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbLayerType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbSNMP_Decode = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.tbPDUTYpe = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label42 = new System.Windows.Forms.Label();
            this.tbString_snmp = new System.Windows.Forms.TextBox();
            this.tb_varbind_len = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.tbOid_value_snmp = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.tb_SNMP_oid = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.tb_SNMP_type_ei = new System.Windows.Forms.TextBox();
            this.tb_SNMP_Val_ei = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.tb_SNMP_Len_eri = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tb_SNMP_type_e = new System.Windows.Forms.TextBox();
            this.tb_SNMP_Val_er = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.tb_SNMP_Len_err = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tb_SNMP_type_rid = new System.Windows.Forms.TextBox();
            this.tb_SNMP_Val_rid = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tb_SNMP_Len_rid = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tblenght_SNMP = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.tbVersion_SNMP = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tbcommunity = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpPacketTools = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.gBstep3 = new System.Windows.Forms.GroupBox();
            this.gbFilter_2 = new System.Windows.Forms.GroupBox();
            this.rbALl = new System.Windows.Forms.RadioButton();
            this.rbICMP = new System.Windows.Forms.RadioButton();
            this.rBUDP = new System.Windows.Forms.RadioButton();
            this.rbTCP = new System.Windows.Forms.RadioButton();
            this.rbSNMP = new System.Windows.Forms.RadioButton();
            this.gbcapturepacks = new System.Windows.Forms.GroupBox();
            this.cbHostNames = new System.Windows.Forms.CheckBox();
            this.rbNormalMode = new System.Windows.Forms.RadioButton();
            this.RB_Promiscuous = new System.Windows.Forms.RadioButton();
            this.gbfilter_1 = new System.Windows.Forms.GroupBox();
            this.cBTR = new System.Windows.Forms.CheckBox();
            this.CBETHER = new System.Windows.Forms.CheckBox();
            this.CBARP = new System.Windows.Forms.CheckBox();
            this.CBRARP = new System.Windows.Forms.CheckBox();
            this.CBIP = new System.Windows.Forms.CheckBox();
            this.CBIP6 = new System.Windows.Forms.CheckBox();
            this.cbTCP = new System.Windows.Forms.CheckBox();
            this.CBUDP = new System.Windows.Forms.CheckBox();
            this.btnCapturePackets = new System.Windows.Forms.Button();
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.gbstep1 = new System.Windows.Forms.GroupBox();
            this.cbAutoScroll = new System.Windows.Forms.CheckBox();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.tPSNMP = new System.Windows.Forms.TabPage();
            this.btnClearSnmpResponse = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.gbSNMP = new System.Windows.Forms.GroupBox();
            this.gbSNMP_Value = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbSET_Trap_Value = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbRequestType = new System.Windows.Forms.ComboBox();
            this.gb_OID = new System.Windows.Forms.GroupBox();
            this.tbOID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.CbOIDs_RequestTypes = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.rtbSNMPResponse = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rtbByteData = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbRecievePackets = new System.Windows.Forms.ListBox();
            this.lBByteData = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.gbAll_other_packets.SuspendLayout();
            this.gbSNMP_Decode.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpPacketTools.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gBstep3.SuspendLayout();
            this.gbFilter_2.SuspendLayout();
            this.gbcapturepacks.SuspendLayout();
            this.gbfilter_1.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.gbstep1.SuspendLayout();
            this.tPSNMP.SuspendLayout();
            this.gbSNMP.SuspendLayout();
            this.gbSNMP_Value.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.gb_OID.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SendMessage
            // 
            this.btn_SendMessage.Location = new System.Drawing.Point(17, 291);
            this.btn_SendMessage.Name = "btn_SendMessage";
            this.btn_SendMessage.Size = new System.Drawing.Size(135, 33);
            this.btn_SendMessage.TabIndex = 0;
            this.btn_SendMessage.Text = "Send Message";
            this.btn_SendMessage.UseVisualStyleBackColor = true;
            this.btn_SendMessage.Click += new System.EventHandler(this.Btn_SendMessage_Click);
            // 
            // RTB_Message
            // 
            this.RTB_Message.Location = new System.Drawing.Point(17, 49);
            this.RTB_Message.Name = "RTB_Message";
            this.RTB_Message.Size = new System.Drawing.Size(480, 236);
            this.RTB_Message.TabIndex = 1;
            this.RTB_Message.Text = "";
            // 
            // RTB_Output
            // 
            this.RTB_Output.Location = new System.Drawing.Point(536, 49);
            this.RTB_Output.Name = "RTB_Output";
            this.RTB_Output.Size = new System.Drawing.Size(499, 236);
            this.RTB_Output.TabIndex = 2;
            this.RTB_Output.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(548, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Console:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Input :";
            // 
            // RTB_Device_List
            // 
            this.RTB_Device_List.Location = new System.Drawing.Point(6, 66);
            this.RTB_Device_List.Name = "RTB_Device_List";
            this.RTB_Device_List.Size = new System.Drawing.Size(224, 1129);
            this.RTB_Device_List.TabIndex = 5;
            this.RTB_Device_List.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Device:";
            // 
            // btnDeviceList
            // 
            this.btnDeviceList.Location = new System.Drawing.Point(12, 25);
            this.btnDeviceList.Name = "btnDeviceList";
            this.btnDeviceList.Size = new System.Drawing.Size(186, 43);
            this.btnDeviceList.TabIndex = 7;
            this.btnDeviceList.Text = "Obtain Device List";
            this.btnDeviceList.UseVisualStyleBackColor = true;
            this.btnDeviceList.Click += new System.EventHandler(this.BtnDeviceList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbAll_other_packets);
            this.groupBox1.Controls.Add(this.gbSNMP_Decode);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.tabControl2);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.rtbByteData);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbRecievePackets);
            this.groupBox1.Controls.Add(this.lBByteData);
            this.groupBox1.Controls.Add(this.RTB_Device_List);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2048, 1224);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network Datagram Decoder";
            // 
            // gbAll_other_packets
            // 
            this.gbAll_other_packets.Controls.Add(this.tbFlag);
            this.gbAll_other_packets.Controls.Add(this.label24);
            this.gbAll_other_packets.Controls.Add(this.tbSequence_Number);
            this.gbAll_other_packets.Controls.Add(this.label22);
            this.gbAll_other_packets.Controls.Add(this.tbTtl);
            this.gbAll_other_packets.Controls.Add(this.label21);
            this.gbAll_other_packets.Controls.Add(this.tbpacketID);
            this.gbAll_other_packets.Controls.Add(this.label20);
            this.gbAll_other_packets.Controls.Add(this.tbSource_Port);
            this.gbAll_other_packets.Controls.Add(this.label12);
            this.gbAll_other_packets.Controls.Add(this.tbDestination_Port);
            this.gbAll_other_packets.Controls.Add(this.label13);
            this.gbAll_other_packets.Controls.Add(this.tbProtocol);
            this.gbAll_other_packets.Controls.Add(this.label6);
            this.gbAll_other_packets.Controls.Add(this.tbVersion);
            this.gbAll_other_packets.Controls.Add(this.label11);
            this.gbAll_other_packets.Controls.Add(this.tbLength);
            this.gbAll_other_packets.Controls.Add(this.label10);
            this.gbAll_other_packets.Controls.Add(this.tbLayerType);
            this.gbAll_other_packets.Controls.Add(this.label4);
            this.gbAll_other_packets.Location = new System.Drawing.Point(1134, 392);
            this.gbAll_other_packets.Name = "gbAll_other_packets";
            this.gbAll_other_packets.Size = new System.Drawing.Size(320, 372);
            this.gbAll_other_packets.TabIndex = 46;
            this.gbAll_other_packets.TabStop = false;
            this.gbAll_other_packets.Text = "Packet Data";
            // 
            // tbFlag
            // 
            this.tbFlag.Location = new System.Drawing.Point(168, 326);
            this.tbFlag.Name = "tbFlag";
            this.tbFlag.Size = new System.Drawing.Size(148, 26);
            this.tbFlag.TabIndex = 63;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(164, 303);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(40, 20);
            this.label24.TabIndex = 62;
            this.label24.Text = "Flag";
            // 
            // tbSequence_Number
            // 
            this.tbSequence_Number.Location = new System.Drawing.Point(4, 326);
            this.tbSequence_Number.Name = "tbSequence_Number";
            this.tbSequence_Number.Size = new System.Drawing.Size(138, 26);
            this.tbSequence_Number.TabIndex = 61;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(0, 303);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(142, 20);
            this.label22.TabIndex = 60;
            this.label22.Text = "Sequence Number";
            // 
            // tbTtl
            // 
            this.tbTtl.Location = new System.Drawing.Point(4, 255);
            this.tbTtl.Name = "tbTtl";
            this.tbTtl.Size = new System.Drawing.Size(138, 26);
            this.tbTtl.TabIndex = 59;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(0, 232);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 20);
            this.label21.TabIndex = 58;
            this.label21.Text = "Time to Live";
            // 
            // tbpacketID
            // 
            this.tbpacketID.Location = new System.Drawing.Point(4, 184);
            this.tbpacketID.Name = "tbpacketID";
            this.tbpacketID.Size = new System.Drawing.Size(138, 26);
            this.tbpacketID.TabIndex = 57;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(0, 161);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 20);
            this.label20.TabIndex = 56;
            this.label20.Text = "Packet ID";
            // 
            // tbSource_Port
            // 
            this.tbSource_Port.Location = new System.Drawing.Point(168, 116);
            this.tbSource_Port.Name = "tbSource_Port";
            this.tbSource_Port.Size = new System.Drawing.Size(144, 26);
            this.tbSource_Port.TabIndex = 55;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(168, 93);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 20);
            this.label12.TabIndex = 54;
            this.label12.Text = "Source Port";
            // 
            // tbDestination_Port
            // 
            this.tbDestination_Port.Location = new System.Drawing.Point(168, 45);
            this.tbDestination_Port.Name = "tbDestination_Port";
            this.tbDestination_Port.Size = new System.Drawing.Size(144, 26);
            this.tbDestination_Port.TabIndex = 53;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(164, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 20);
            this.label13.TabIndex = 52;
            this.label13.Text = "Destination Port";
            // 
            // tbProtocol
            // 
            this.tbProtocol.Location = new System.Drawing.Point(168, 255);
            this.tbProtocol.Name = "tbProtocol";
            this.tbProtocol.Size = new System.Drawing.Size(144, 26);
            this.tbProtocol.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(168, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 50;
            this.label6.Text = "Protocol";
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(168, 184);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(144, 26);
            this.tbVersion.TabIndex = 49;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(168, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 20);
            this.label11.TabIndex = 48;
            this.label11.Text = "Version";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(4, 116);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(138, 26);
            this.tbLength.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 20);
            this.label10.TabIndex = 46;
            this.label10.Text = "Length";
            // 
            // tbLayerType
            // 
            this.tbLayerType.Location = new System.Drawing.Point(4, 45);
            this.tbLayerType.Name = "tbLayerType";
            this.tbLayerType.Size = new System.Drawing.Size(138, 26);
            this.tbLayerType.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "Layer Type";
            // 
            // gbSNMP_Decode
            // 
            this.gbSNMP_Decode.Controls.Add(this.groupBox4);
            this.gbSNMP_Decode.Controls.Add(this.groupBox3);
            this.gbSNMP_Decode.Location = new System.Drawing.Point(238, 770);
            this.gbSNMP_Decode.Name = "gbSNMP_Decode";
            this.gbSNMP_Decode.Size = new System.Drawing.Size(1216, 425);
            this.gbSNMP_Decode.TabIndex = 45;
            this.gbSNMP_Decode.TabStop = false;
            this.gbSNMP_Decode.Text = "SNMP Specific";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.tbPDUTYpe);
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Location = new System.Drawing.Point(209, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(991, 385);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SNMP PDU";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(12, 26);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(130, 20);
            this.label38.TabIndex = 53;
            this.label38.Text = "SNMP PDU Type";
            // 
            // tbPDUTYpe
            // 
            this.tbPDUTYpe.Location = new System.Drawing.Point(12, 49);
            this.tbPDUTYpe.Name = "tbPDUTYpe";
            this.tbPDUTYpe.Size = new System.Drawing.Size(289, 26);
            this.tbPDUTYpe.TabIndex = 54;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label42);
            this.groupBox9.Controls.Add(this.tbString_snmp);
            this.groupBox9.Controls.Add(this.tb_varbind_len);
            this.groupBox9.Controls.Add(this.label41);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.tbOid_value_snmp);
            this.groupBox9.Controls.Add(this.label37);
            this.groupBox9.Controls.Add(this.tb_SNMP_oid);
            this.groupBox9.Location = new System.Drawing.Point(648, 87);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(334, 281);
            this.groupBox9.TabIndex = 48;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Varbind";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 159);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(96, 20);
            this.label42.TabIndex = 65;
            this.label42.Text = "String Value";
            // 
            // tbString_snmp
            // 
            this.tbString_snmp.Location = new System.Drawing.Point(6, 182);
            this.tbString_snmp.Name = "tbString_snmp";
            this.tbString_snmp.Size = new System.Drawing.Size(252, 26);
            this.tbString_snmp.TabIndex = 66;
            // 
            // tb_varbind_len
            // 
            this.tb_varbind_len.Location = new System.Drawing.Point(10, 244);
            this.tb_varbind_len.Name = "tb_varbind_len";
            this.tb_varbind_len.Size = new System.Drawing.Size(248, 26);
            this.tb_varbind_len.TabIndex = 64;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 221);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(59, 20);
            this.label41.TabIndex = 63;
            this.label41.Text = "Length";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 88);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(124, 20);
            this.label39.TabIndex = 61;
            this.label39.Text = "Numerical Value";
            // 
            // tbOid_value_snmp
            // 
            this.tbOid_value_snmp.Location = new System.Drawing.Point(6, 111);
            this.tbOid_value_snmp.Name = "tbOid_value_snmp";
            this.tbOid_value_snmp.Size = new System.Drawing.Size(252, 26);
            this.tbOid_value_snmp.TabIndex = 62;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 22);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(38, 20);
            this.label37.TabIndex = 59;
            this.label37.Text = "OID";
            // 
            // tb_SNMP_oid
            // 
            this.tb_SNMP_oid.Location = new System.Drawing.Point(6, 45);
            this.tb_SNMP_oid.Name = "tb_SNMP_oid";
            this.tb_SNMP_oid.Size = new System.Drawing.Size(252, 26);
            this.tb_SNMP_oid.TabIndex = 60;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.tb_SNMP_type_ei);
            this.groupBox5.Controls.Add(this.tb_SNMP_Val_ei);
            this.groupBox5.Controls.Add(this.label35);
            this.groupBox5.Controls.Add(this.label36);
            this.groupBox5.Controls.Add(this.tb_SNMP_Len_eri);
            this.groupBox5.Location = new System.Drawing.Point(424, 87);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 220);
            this.groupBox5.TabIndex = 46;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Error Index";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 29);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(43, 20);
            this.label34.TabIndex = 57;
            this.label34.Text = "Type";
            // 
            // tb_SNMP_type_ei
            // 
            this.tb_SNMP_type_ei.Location = new System.Drawing.Point(6, 52);
            this.tb_SNMP_type_ei.Name = "tb_SNMP_type_ei";
            this.tb_SNMP_type_ei.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_type_ei.TabIndex = 58;
            // 
            // tb_SNMP_Val_ei
            // 
            this.tb_SNMP_Val_ei.Location = new System.Drawing.Point(6, 182);
            this.tb_SNMP_Val_ei.Name = "tb_SNMP_Val_ei";
            this.tb_SNMP_Val_ei.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_Val_ei.TabIndex = 56;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 159);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(50, 20);
            this.label35.TabIndex = 55;
            this.label35.Text = "Value";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 101);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(59, 20);
            this.label36.TabIndex = 53;
            this.label36.Text = "Length";
            // 
            // tb_SNMP_Len_eri
            // 
            this.tb_SNMP_Len_eri.Location = new System.Drawing.Point(6, 124);
            this.tb_SNMP_Len_eri.Name = "tb_SNMP_Len_eri";
            this.tb_SNMP_Len_eri.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_Len_eri.TabIndex = 54;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label31);
            this.groupBox8.Controls.Add(this.tb_SNMP_type_e);
            this.groupBox8.Controls.Add(this.tb_SNMP_Val_er);
            this.groupBox8.Controls.Add(this.label32);
            this.groupBox8.Controls.Add(this.label33);
            this.groupBox8.Controls.Add(this.tb_SNMP_Len_err);
            this.groupBox8.Location = new System.Drawing.Point(218, 87);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(200, 220);
            this.groupBox8.TabIndex = 47;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Error";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 29);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(43, 20);
            this.label31.TabIndex = 57;
            this.label31.Text = "Type";
            // 
            // tb_SNMP_type_e
            // 
            this.tb_SNMP_type_e.Location = new System.Drawing.Point(6, 52);
            this.tb_SNMP_type_e.Name = "tb_SNMP_type_e";
            this.tb_SNMP_type_e.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_type_e.TabIndex = 58;
            // 
            // tb_SNMP_Val_er
            // 
            this.tb_SNMP_Val_er.Location = new System.Drawing.Point(9, 182);
            this.tb_SNMP_Val_er.Name = "tb_SNMP_Val_er";
            this.tb_SNMP_Val_er.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_Val_er.TabIndex = 56;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(9, 159);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(50, 20);
            this.label32.TabIndex = 55;
            this.label32.Text = "Value";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(5, 101);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 20);
            this.label33.TabIndex = 53;
            this.label33.Text = "Length";
            // 
            // tb_SNMP_Len_err
            // 
            this.tb_SNMP_Len_err.Location = new System.Drawing.Point(6, 124);
            this.tb_SNMP_Len_err.Name = "tb_SNMP_Len_err";
            this.tb_SNMP_Len_err.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_Len_err.TabIndex = 54;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.tb_SNMP_type_rid);
            this.groupBox7.Controls.Add(this.tb_SNMP_Val_rid);
            this.groupBox7.Controls.Add(this.label27);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.tb_SNMP_Len_rid);
            this.groupBox7.Location = new System.Drawing.Point(12, 87);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 220);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Request ID";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 29);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(43, 20);
            this.label30.TabIndex = 51;
            this.label30.Text = "Type";
            // 
            // tb_SNMP_type_rid
            // 
            this.tb_SNMP_type_rid.Location = new System.Drawing.Point(6, 52);
            this.tb_SNMP_type_rid.Name = "tb_SNMP_type_rid";
            this.tb_SNMP_type_rid.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_type_rid.TabIndex = 52;
            // 
            // tb_SNMP_Val_rid
            // 
            this.tb_SNMP_Val_rid.Location = new System.Drawing.Point(6, 182);
            this.tb_SNMP_Val_rid.Name = "tb_SNMP_Val_rid";
            this.tb_SNMP_Val_rid.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_Val_rid.TabIndex = 50;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 159);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(26, 20);
            this.label27.TabIndex = 49;
            this.label27.Text = "ID";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 101);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(59, 20);
            this.label29.TabIndex = 47;
            this.label29.Text = "Length";
            // 
            // tb_SNMP_Len_rid
            // 
            this.tb_SNMP_Len_rid.Location = new System.Drawing.Point(6, 124);
            this.tb_SNMP_Len_rid.Name = "tb_SNMP_Len_rid";
            this.tb_SNMP_Len_rid.Size = new System.Drawing.Size(158, 26);
            this.tb_SNMP_Len_rid.TabIndex = 48;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tblenght_SNMP);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.tbVersion_SNMP);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.tbcommunity);
            this.groupBox3.Location = new System.Drawing.Point(6, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 385);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Top Level";
            // 
            // tblenght_SNMP
            // 
            this.tblenght_SNMP.Location = new System.Drawing.Point(10, 185);
            this.tblenght_SNMP.Name = "tblenght_SNMP";
            this.tblenght_SNMP.Size = new System.Drawing.Size(165, 26);
            this.tblenght_SNMP.TabIndex = 48;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 162);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(173, 20);
            this.label40.TabIndex = 47;
            this.label40.Text = "Length of entire packet";
            // 
            // tbVersion_SNMP
            // 
            this.tbVersion_SNMP.Location = new System.Drawing.Point(10, 116);
            this.tbVersion_SNMP.Name = "tbVersion_SNMP";
            this.tbVersion_SNMP.Size = new System.Drawing.Size(165, 26);
            this.tbVersion_SNMP.TabIndex = 46;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 93);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(63, 20);
            this.label28.TabIndex = 45;
            this.label28.Text = "Version";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 31);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(88, 20);
            this.label26.TabIndex = 41;
            this.label26.Text = "Community";
            // 
            // tbcommunity
            // 
            this.tbcommunity.Location = new System.Drawing.Point(10, 54);
            this.tbcommunity.Name = "tbcommunity";
            this.tbcommunity.Size = new System.Drawing.Size(165, 26);
            this.tbcommunity.TabIndex = 42;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(836, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 20);
            this.label25.TabIndex = 44;
            this.label25.Text = "Data";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(688, 35);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 20);
            this.label23.TabIndex = 41;
            this.label23.Text = "Flag";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpPacketTools);
            this.tabControl2.Controls.Add(this.tPSNMP);
            this.tabControl2.Location = new System.Drawing.Point(1481, 25);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(561, 917);
            this.tabControl2.TabIndex = 34;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tpPacketTools
            // 
            this.tpPacketTools.Controls.Add(this.panel1);
            this.tpPacketTools.Location = new System.Drawing.Point(4, 29);
            this.tpPacketTools.Name = "tpPacketTools";
            this.tpPacketTools.Padding = new System.Windows.Forms.Padding(3);
            this.tpPacketTools.Size = new System.Drawing.Size(553, 884);
            this.tpPacketTools.TabIndex = 0;
            this.tpPacketTools.Text = "Packet Tools";
            this.tpPacketTools.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.gBstep3);
            this.panel1.Controls.Add(this.gbStep2);
            this.panel1.Controls.Add(this.gbstep1);
            this.panel1.Controls.Add(this.cbAutoScroll);
            this.panel1.Controls.Add(this.Btn_Clear);
            this.panel1.Controls.Add(this.btn_Stop);
            this.panel1.Location = new System.Drawing.Point(14, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 764);
            this.panel1.TabIndex = 11;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(154, 708);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(129, 36);
            this.btnReset.TabIndex = 22;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // gBstep3
            // 
            this.gBstep3.Controls.Add(this.gbFilter_2);
            this.gBstep3.Controls.Add(this.gbcapturepacks);
            this.gBstep3.Enabled = false;
            this.gBstep3.Location = new System.Drawing.Point(15, 202);
            this.gBstep3.Name = "gBstep3";
            this.gBstep3.Size = new System.Drawing.Size(513, 500);
            this.gBstep3.TabIndex = 21;
            this.gBstep3.TabStop = false;
            this.gBstep3.Text = "Step 3";
            // 
            // gbFilter_2
            // 
            this.gbFilter_2.Controls.Add(this.rbALl);
            this.gbFilter_2.Controls.Add(this.rbICMP);
            this.gbFilter_2.Controls.Add(this.rBUDP);
            this.gbFilter_2.Controls.Add(this.rbTCP);
            this.gbFilter_2.Controls.Add(this.rbSNMP);
            this.gbFilter_2.Enabled = false;
            this.gbFilter_2.Location = new System.Drawing.Point(10, 345);
            this.gbFilter_2.Name = "gbFilter_2";
            this.gbFilter_2.Size = new System.Drawing.Size(433, 80);
            this.gbFilter_2.TabIndex = 19;
            this.gbFilter_2.TabStop = false;
            this.gbFilter_2.Text = "Filter (What type of packets to display)";
            // 
            // rbALl
            // 
            this.rbALl.AutoSize = true;
            this.rbALl.Checked = true;
            this.rbALl.Location = new System.Drawing.Point(329, 32);
            this.rbALl.Name = "rbALl";
            this.rbALl.Size = new System.Drawing.Size(55, 24);
            this.rbALl.TabIndex = 4;
            this.rbALl.TabStop = true;
            this.rbALl.Text = "All ";
            this.rbALl.UseVisualStyleBackColor = true;
            this.rbALl.CheckedChanged += new System.EventHandler(this.Check_Filter_RB);
            // 
            // rbICMP
            // 
            this.rbICMP.AutoSize = true;
            this.rbICMP.Location = new System.Drawing.Point(13, 32);
            this.rbICMP.Name = "rbICMP";
            this.rbICMP.Size = new System.Drawing.Size(77, 24);
            this.rbICMP.TabIndex = 3;
            this.rbICMP.Text = "ICMP ";
            this.rbICMP.UseVisualStyleBackColor = true;
            this.rbICMP.CheckedChanged += new System.EventHandler(this.Check_Filter_RB);
            // 
            // rBUDP
            // 
            this.rBUDP.AutoSize = true;
            this.rBUDP.Location = new System.Drawing.Point(170, 32);
            this.rBUDP.Name = "rBUDP";
            this.rBUDP.Size = new System.Drawing.Size(68, 24);
            this.rBUDP.TabIndex = 2;
            this.rBUDP.Text = "UDP";
            this.rBUDP.UseVisualStyleBackColor = true;
            this.rBUDP.CheckedChanged += new System.EventHandler(this.Check_Filter_RB);
            // 
            // rbTCP
            // 
            this.rbTCP.AutoSize = true;
            this.rbTCP.Location = new System.Drawing.Point(99, 32);
            this.rbTCP.Name = "rbTCP";
            this.rbTCP.Size = new System.Drawing.Size(64, 24);
            this.rbTCP.TabIndex = 1;
            this.rbTCP.Text = "TCP";
            this.rbTCP.UseVisualStyleBackColor = true;
            this.rbTCP.CheckedChanged += new System.EventHandler(this.Check_Filter_RB);
            // 
            // rbSNMP
            // 
            this.rbSNMP.AutoSize = true;
            this.rbSNMP.Location = new System.Drawing.Point(244, 32);
            this.rbSNMP.Name = "rbSNMP";
            this.rbSNMP.Size = new System.Drawing.Size(79, 24);
            this.rbSNMP.TabIndex = 0;
            this.rbSNMP.Text = "SNMP";
            this.rbSNMP.UseVisualStyleBackColor = true;
            this.rbSNMP.CheckedChanged += new System.EventHandler(this.Check_Filter_RB);
            // 
            // gbcapturepacks
            // 
            this.gbcapturepacks.Controls.Add(this.cbHostNames);
            this.gbcapturepacks.Controls.Add(this.rbNormalMode);
            this.gbcapturepacks.Controls.Add(this.RB_Promiscuous);
            this.gbcapturepacks.Controls.Add(this.gbfilter_1);
            this.gbcapturepacks.Controls.Add(this.btnCapturePackets);
            this.gbcapturepacks.Location = new System.Drawing.Point(10, 25);
            this.gbcapturepacks.Name = "gbcapturepacks";
            this.gbcapturepacks.Size = new System.Drawing.Size(433, 307);
            this.gbcapturepacks.TabIndex = 18;
            this.gbcapturepacks.TabStop = false;
            this.gbcapturepacks.Text = "Capture Packets";
            // 
            // cbHostNames
            // 
            this.cbHostNames.AutoSize = true;
            this.cbHostNames.Location = new System.Drawing.Point(13, 64);
            this.cbHostNames.Name = "cbHostNames";
            this.cbHostNames.Size = new System.Drawing.Size(225, 24);
            this.cbHostNames.TabIndex = 35;
            this.cbHostNames.Text = "Try to Map host name to IP";
            this.toolTip1.SetToolTip(this.cbHostNames, "Significantly reduces performace");
            this.cbHostNames.UseVisualStyleBackColor = true;
            // 
            // rbNormalMode
            // 
            this.rbNormalMode.AutoSize = true;
            this.rbNormalMode.Location = new System.Drawing.Point(204, 25);
            this.rbNormalMode.Name = "rbNormalMode";
            this.rbNormalMode.Size = new System.Drawing.Size(132, 24);
            this.rbNormalMode.TabIndex = 14;
            this.rbNormalMode.Text = "Normal Mode:";
            this.rbNormalMode.UseVisualStyleBackColor = true;
            // 
            // RB_Promiscuous
            // 
            this.RB_Promiscuous.AutoSize = true;
            this.RB_Promiscuous.Checked = true;
            this.RB_Promiscuous.Location = new System.Drawing.Point(13, 25);
            this.RB_Promiscuous.Name = "RB_Promiscuous";
            this.RB_Promiscuous.Size = new System.Drawing.Size(173, 24);
            this.RB_Promiscuous.TabIndex = 9;
            this.RB_Promiscuous.TabStop = true;
            this.RB_Promiscuous.Text = "Promiscuous Mode:";
            this.RB_Promiscuous.UseVisualStyleBackColor = true;
            // 
            // gbfilter_1
            // 
            this.gbfilter_1.Controls.Add(this.cBTR);
            this.gbfilter_1.Controls.Add(this.CBETHER);
            this.gbfilter_1.Controls.Add(this.CBARP);
            this.gbfilter_1.Controls.Add(this.CBRARP);
            this.gbfilter_1.Controls.Add(this.CBIP);
            this.gbfilter_1.Controls.Add(this.CBIP6);
            this.gbfilter_1.Controls.Add(this.cbTCP);
            this.gbfilter_1.Controls.Add(this.CBUDP);
            this.gbfilter_1.Location = new System.Drawing.Point(7, 116);
            this.gbfilter_1.Name = "gbfilter_1";
            this.gbfilter_1.Size = new System.Drawing.Size(373, 114);
            this.gbfilter_1.TabIndex = 12;
            this.gbfilter_1.TabStop = false;
            this.gbfilter_1.Text = "Filter (What type of packets to capture)";
            // 
            // cBTR
            // 
            this.cBTR.AutoSize = true;
            this.cBTR.Location = new System.Drawing.Point(274, 25);
            this.cBTR.Name = "cBTR";
            this.cBTR.Size = new System.Drawing.Size(56, 24);
            this.cBTR.TabIndex = 15;
            this.cBTR.Text = "TR";
            this.cBTR.UseVisualStyleBackColor = true;
            // 
            // CBETHER
            // 
            this.CBETHER.AutoSize = true;
            this.CBETHER.Location = new System.Drawing.Point(274, 66);
            this.CBETHER.Name = "CBETHER";
            this.CBETHER.Size = new System.Drawing.Size(90, 24);
            this.CBETHER.TabIndex = 14;
            this.CBETHER.Text = "ETHER";
            this.CBETHER.UseVisualStyleBackColor = true;
            // 
            // CBARP
            // 
            this.CBARP.AutoSize = true;
            this.CBARP.Location = new System.Drawing.Point(181, 25);
            this.CBARP.Name = "CBARP";
            this.CBARP.Size = new System.Drawing.Size(68, 24);
            this.CBARP.TabIndex = 13;
            this.CBARP.Text = "ARP";
            this.CBARP.UseVisualStyleBackColor = true;
            // 
            // CBRARP
            // 
            this.CBRARP.AutoSize = true;
            this.CBRARP.Location = new System.Drawing.Point(181, 66);
            this.CBRARP.Name = "CBRARP";
            this.CBRARP.Size = new System.Drawing.Size(80, 24);
            this.CBRARP.TabIndex = 12;
            this.CBRARP.Text = "RARP";
            this.CBRARP.UseVisualStyleBackColor = true;
            // 
            // CBIP
            // 
            this.CBIP.AutoSize = true;
            this.CBIP.Location = new System.Drawing.Point(106, 25);
            this.CBIP.Name = "CBIP";
            this.CBIP.Size = new System.Drawing.Size(50, 24);
            this.CBIP.TabIndex = 11;
            this.CBIP.Text = "IP";
            this.CBIP.UseVisualStyleBackColor = true;
            // 
            // CBIP6
            // 
            this.CBIP6.AutoSize = true;
            this.CBIP6.Location = new System.Drawing.Point(106, 66);
            this.CBIP6.Name = "CBIP6";
            this.CBIP6.Size = new System.Drawing.Size(59, 24);
            this.CBIP6.TabIndex = 10;
            this.CBIP6.Text = "IP6";
            this.CBIP6.UseVisualStyleBackColor = true;
            // 
            // cbTCP
            // 
            this.cbTCP.AutoSize = true;
            this.cbTCP.Location = new System.Drawing.Point(19, 25);
            this.cbTCP.Name = "cbTCP";
            this.cbTCP.Size = new System.Drawing.Size(65, 24);
            this.cbTCP.TabIndex = 9;
            this.cbTCP.Text = "TCP";
            this.cbTCP.UseVisualStyleBackColor = true;
            // 
            // CBUDP
            // 
            this.CBUDP.AutoSize = true;
            this.CBUDP.Location = new System.Drawing.Point(19, 66);
            this.CBUDP.Name = "CBUDP";
            this.CBUDP.Size = new System.Drawing.Size(69, 24);
            this.CBUDP.TabIndex = 8;
            this.CBUDP.Text = "UDP";
            this.CBUDP.UseVisualStyleBackColor = true;
            // 
            // btnCapturePackets
            // 
            this.btnCapturePackets.Location = new System.Drawing.Point(7, 249);
            this.btnCapturePackets.Name = "btnCapturePackets";
            this.btnCapturePackets.Size = new System.Drawing.Size(186, 38);
            this.btnCapturePackets.TabIndex = 10;
            this.btnCapturePackets.Text = "Capture packets";
            this.btnCapturePackets.UseVisualStyleBackColor = true;
            this.btnCapturePackets.Click += new System.EventHandler(this.BtnCapturePackets_Click);
            // 
            // gbStep2
            // 
            this.gbStep2.Controls.Add(this.cbDeviceList);
            this.gbStep2.Enabled = false;
            this.gbStep2.Location = new System.Drawing.Point(15, 110);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(510, 86);
            this.gbStep2.TabIndex = 20;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Step 2) Select Device";
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            this.cbDeviceList.Location = new System.Drawing.Point(17, 36);
            this.cbDeviceList.Name = "cbDeviceList";
            this.cbDeviceList.Size = new System.Drawing.Size(357, 28);
            this.cbDeviceList.TabIndex = 17;
            this.cbDeviceList.SelectedIndexChanged += new System.EventHandler(this.CbDeviceList_SelectedIndexChanged);
            // 
            // gbstep1
            // 
            this.gbstep1.Controls.Add(this.btnDeviceList);
            this.gbstep1.Location = new System.Drawing.Point(13, 10);
            this.gbstep1.Name = "gbstep1";
            this.gbstep1.Size = new System.Drawing.Size(512, 84);
            this.gbstep1.TabIndex = 19;
            this.gbstep1.TabStop = false;
            this.gbstep1.Text = "Step 1) Obtain Device List";
            // 
            // cbAutoScroll
            // 
            this.cbAutoScroll.AutoSize = true;
            this.cbAutoScroll.Checked = true;
            this.cbAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoScroll.Location = new System.Drawing.Point(414, 715);
            this.cbAutoScroll.Name = "cbAutoScroll";
            this.cbAutoScroll.Size = new System.Drawing.Size(112, 24);
            this.cbAutoScroll.TabIndex = 16;
            this.cbAutoScroll.Text = "Auto Scroll";
            this.cbAutoScroll.UseVisualStyleBackColor = true;
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(32, 708);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(114, 36);
            this.Btn_Clear.TabIndex = 8;
            this.Btn_Clear.Text = "Clear";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(295, 708);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(111, 36);
            this.btn_Stop.TabIndex = 11;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // tPSNMP
            // 
            this.tPSNMP.Controls.Add(this.btnClearSnmpResponse);
            this.tPSNMP.Controls.Add(this.label18);
            this.tPSNMP.Controls.Add(this.gbSNMP);
            this.tPSNMP.Controls.Add(this.rtbSNMPResponse);
            this.tPSNMP.Location = new System.Drawing.Point(4, 29);
            this.tPSNMP.Name = "tPSNMP";
            this.tPSNMP.Padding = new System.Windows.Forms.Padding(3);
            this.tPSNMP.Size = new System.Drawing.Size(553, 884);
            this.tPSNMP.TabIndex = 1;
            this.tPSNMP.Text = "SNMP";
            this.tPSNMP.UseVisualStyleBackColor = true;
            // 
            // btnClearSnmpResponse
            // 
            this.btnClearSnmpResponse.Location = new System.Drawing.Point(16, 839);
            this.btnClearSnmpResponse.Name = "btnClearSnmpResponse";
            this.btnClearSnmpResponse.Size = new System.Drawing.Size(139, 31);
            this.btnClearSnmpResponse.TabIndex = 35;
            this.btnClearSnmpResponse.Text = "Clear";
            this.btnClearSnmpResponse.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 513);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(150, 20);
            this.label18.TabIndex = 35;
            this.label18.Text = "SNMP Results View";
            // 
            // gbSNMP
            // 
            this.gbSNMP.Controls.Add(this.gbSNMP_Value);
            this.gbSNMP.Controls.Add(this.groupBox6);
            this.gbSNMP.Controls.Add(this.gb_OID);
            this.gbSNMP.Controls.Add(this.btnExecute);
            this.gbSNMP.Location = new System.Drawing.Point(16, 12);
            this.gbSNMP.Name = "gbSNMP";
            this.gbSNMP.Size = new System.Drawing.Size(523, 492);
            this.gbSNMP.TabIndex = 23;
            this.gbSNMP.TabStop = false;
            this.gbSNMP.Text = "SMNP";
            // 
            // gbSNMP_Value
            // 
            this.gbSNMP_Value.Controls.Add(this.label19);
            this.gbSNMP_Value.Controls.Add(this.tbSET_Trap_Value);
            this.gbSNMP_Value.Location = new System.Drawing.Point(11, 333);
            this.gbSNMP_Value.Name = "gbSNMP_Value";
            this.gbSNMP_Value.Size = new System.Drawing.Size(335, 107);
            this.gbSNMP_Value.TabIndex = 11;
            this.gbSNMP_Value.TabStop = false;
            this.gbSNMP_Value.Text = "Step 2.2) Enter Value";
            this.gbSNMP_Value.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(50, 20);
            this.label19.TabIndex = 9;
            this.label19.Text = "Value";
            // 
            // tbSET_Trap_Value
            // 
            this.tbSET_Trap_Value.Location = new System.Drawing.Point(15, 54);
            this.tbSET_Trap_Value.Name = "tbSET_Trap_Value";
            this.tbSET_Trap_Value.Size = new System.Drawing.Size(212, 26);
            this.tbSET_Trap_Value.TabIndex = 8;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.cbRequestType);
            this.groupBox6.Location = new System.Drawing.Point(11, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(324, 100);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Step 1) select request type";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(108, 20);
            this.label17.TabIndex = 6;
            this.label17.Text = "Request Type";
            // 
            // cbRequestType
            // 
            this.cbRequestType.FormattingEnabled = true;
            this.cbRequestType.Items.AddRange(new object[] {
            "Get",
            "Get-Next",
            "Set",
            "Trap"});
            this.cbRequestType.Location = new System.Drawing.Point(14, 45);
            this.cbRequestType.Name = "cbRequestType";
            this.cbRequestType.Size = new System.Drawing.Size(284, 28);
            this.cbRequestType.TabIndex = 5;
            this.cbRequestType.SelectedIndexChanged += new System.EventHandler(this.cbRequestType_SelectedIndexChanged);
            // 
            // gb_OID
            // 
            this.gb_OID.Controls.Add(this.tbOID);
            this.gb_OID.Controls.Add(this.label15);
            this.gb_OID.Controls.Add(this.CbOIDs_RequestTypes);
            this.gb_OID.Controls.Add(this.label16);
            this.gb_OID.Location = new System.Drawing.Point(10, 140);
            this.gb_OID.Name = "gb_OID";
            this.gb_OID.Size = new System.Drawing.Size(325, 180);
            this.gb_OID.TabIndex = 7;
            this.gb_OID.TabStop = false;
            this.gb_OID.Text = "Step 2) Specify which Object Identifier";
            // 
            // tbOID
            // 
            this.tbOID.Location = new System.Drawing.Point(17, 61);
            this.tbOID.Name = "tbOID";
            this.tbOID.Size = new System.Drawing.Size(282, 26);
            this.tbOID.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbOID, "Enter an OID in the format \'1.3.6.1.2.1.1.1.1.0\'");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 20);
            this.label15.TabIndex = 1;
            this.label15.Text = "OID:";
            // 
            // CbOIDs_RequestTypes
            // 
            this.CbOIDs_RequestTypes.FormattingEnabled = true;
            this.CbOIDs_RequestTypes.Location = new System.Drawing.Point(17, 126);
            this.CbOIDs_RequestTypes.Name = "CbOIDs_RequestTypes";
            this.CbOIDs_RequestTypes.Size = new System.Drawing.Size(282, 28);
            this.CbOIDs_RequestTypes.TabIndex = 2;
            this.CbOIDs_RequestTypes.SelectedIndexChanged += new System.EventHandler(this.CbOIDs_RequestTypes_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 103);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(106, 20);
            this.label16.TabIndex = 3;
            this.label16.Text = "Common OID";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(11, 446);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(258, 31);
            this.btnExecute.TabIndex = 4;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // rtbSNMPResponse
            // 
            this.rtbSNMPResponse.Location = new System.Drawing.Point(16, 536);
            this.rtbSNMPResponse.Name = "rtbSNMPResponse";
            this.rtbSNMPResponse.Size = new System.Drawing.Size(532, 286);
            this.rtbSNMPResponse.TabIndex = 34;
            this.rtbSNMPResponse.Text = "";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(219, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 20);
            this.label14.TabIndex = 33;
            this.label14.Text = "Protocol";
            // 
            // rtbByteData
            // 
            this.rtbByteData.Location = new System.Drawing.Point(1131, 90);
            this.rtbByteData.Name = "rtbByteData";
            this.rtbByteData.Size = new System.Drawing.Size(315, 296);
            this.rtbByteData.TabIndex = 28;
            this.rtbByteData.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1127, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "Data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(523, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Destination";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(395, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Source";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Time";
            // 
            // lbRecievePackets
            // 
            this.lbRecievePackets.FormattingEnabled = true;
            this.lbRecievePackets.ItemHeight = 20;
            this.lbRecievePackets.Location = new System.Drawing.Point(236, 60);
            this.lbRecievePackets.Name = "lbRecievePackets";
            this.lbRecievePackets.Size = new System.Drawing.Size(889, 704);
            this.lbRecievePackets.TabIndex = 14;
            this.lbRecievePackets.SelectedIndexChanged += new System.EventHandler(this.LbRecievePackets_SelectedIndexChanged);
            // 
            // lBByteData
            // 
            this.lBByteData.FormattingEnabled = true;
            this.lBByteData.ItemHeight = 20;
            this.lBByteData.Location = new System.Drawing.Point(1131, 60);
            this.lBByteData.Name = "lBByteData";
            this.lBByteData.Size = new System.Drawing.Size(319, 24);
            this.lBByteData.TabIndex = 13;
            this.lBByteData.SelectedIndexChanged += new System.EventHandler(this.LBByteData_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RTB_Output);
            this.groupBox2.Controls.Add(this.btn_SendMessage);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.RTB_Message);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1127, 366);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send Packet";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2068, 1341);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(2060, 1308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Datagram Decoder";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(2060, 1308);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Talk to yourself";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.Tag = "in format 1.3.6...";
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2092, 1274);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Network Datagram Decoder and SNMP agent";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbAll_other_packets.ResumeLayout(false);
            this.gbAll_other_packets.PerformLayout();
            this.gbSNMP_Decode.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tpPacketTools.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gBstep3.ResumeLayout(false);
            this.gbFilter_2.ResumeLayout(false);
            this.gbFilter_2.PerformLayout();
            this.gbcapturepacks.ResumeLayout(false);
            this.gbcapturepacks.PerformLayout();
            this.gbfilter_1.ResumeLayout(false);
            this.gbfilter_1.PerformLayout();
            this.gbStep2.ResumeLayout(false);
            this.gbstep1.ResumeLayout(false);
            this.tPSNMP.ResumeLayout(false);
            this.tPSNMP.PerformLayout();
            this.gbSNMP.ResumeLayout(false);
            this.gbSNMP_Value.ResumeLayout(false);
            this.gbSNMP_Value.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.gb_OID.ResumeLayout(false);
            this.gb_OID.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SendMessage;
        private System.Windows.Forms.RichTextBox RTB_Message;
        private System.Windows.Forms.RichTextBox RTB_Output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox RTB_Device_List;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.RadioButton RB_Promiscuous;
        private System.Windows.Forms.Button btnCapturePackets;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.GroupBox gbfilter_1;
        private System.Windows.Forms.CheckBox CBUDP;
        private System.Windows.Forms.CheckBox cbTCP;
        private System.Windows.Forms.CheckBox CBIP;
        private System.Windows.Forms.CheckBox CBIP6;
        private System.Windows.Forms.CheckBox CBARP;
        private System.Windows.Forms.CheckBox CBRARP;
        private System.Windows.Forms.CheckBox cBTR;
        private System.Windows.Forms.CheckBox CBETHER;
        private System.Windows.Forms.RadioButton rbNormalMode;
        private System.Windows.Forms.CheckBox cbAutoScroll;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.GroupBox gbcapturepacks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gBstep3;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.GroupBox gbstep1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ListBox lBByteData;
        private System.Windows.Forms.ListBox lbRecievePackets;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rtbByteData;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox gbSNMP;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbRequestType;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox CbOIDs_RequestTypes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbOID;
        private System.Windows.Forms.Button btnClearSnmpResponse;
        private System.Windows.Forms.RichTextBox rtbSNMPResponse;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpPacketTools;
        private System.Windows.Forms.TabPage tPSNMP;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbSET_Trap_Value;
        private System.Windows.Forms.GroupBox gb_OID;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbSNMP_Value;
        private System.Windows.Forms.CheckBox cbHostNames;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.GroupBox gbFilter_2;
        private System.Windows.Forms.RadioButton rbICMP;
        private System.Windows.Forms.RadioButton rBUDP;
        private System.Windows.Forms.RadioButton rbTCP;
        private System.Windows.Forms.RadioButton rbSNMP;
        private System.Windows.Forms.RadioButton rbALl;
        private System.Windows.Forms.GroupBox gbSNMP_Decode;
        private System.Windows.Forms.TextBox tbcommunity;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbVersion_SNMP;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox tbPDUTYpe;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox tbOid_value_snmp;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox tb_SNMP_oid;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox tb_SNMP_type_ei;
        private System.Windows.Forms.TextBox tb_SNMP_Val_ei;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox tb_SNMP_Len_eri;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tb_SNMP_type_e;
        private System.Windows.Forms.TextBox tb_SNMP_Val_er;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tb_SNMP_Len_err;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tb_SNMP_type_rid;
        private System.Windows.Forms.TextBox tb_SNMP_Val_rid;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tb_SNMP_Len_rid;
        private System.Windows.Forms.TextBox tb_varbind_len;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox tblenght_SNMP;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox gbAll_other_packets;
        private System.Windows.Forms.TextBox tbFlag;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbSequence_Number;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbTtl;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbpacketID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbSource_Port;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDestination_Port;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbProtocol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbLayerType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox tbString_snmp;
    }
}

