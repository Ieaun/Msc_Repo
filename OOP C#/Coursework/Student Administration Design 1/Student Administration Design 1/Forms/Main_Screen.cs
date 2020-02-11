using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using Student_Administration_Design_1.Layers;



namespace Student_Administration_Design_1
{
    public partial class MainForm : Form
    {
        // API containing the business logic
        Student_admin_API Logic_API = new Student_admin_API();

        //holds the user data that is brought in from the login/register screen berfor its loaded into a complex object
        string [] User_Data = null;     

        string User_Type_Logged_In = "None"; // staff or student

        public MainForm(int User_type, string [] UserData)
        {
            InitializeComponent();
            User_Data = UserData;
            string Program_Enrolled = UserData[4];
            string UID = UserData[0];
            string UName = UserData[1];
            string Cohort = UserData[6];

            InitUser(User_type, UName, UID, Program_Enrolled, Cohort);
            InitGui(UName, UID, Program_Enrolled);
        }


        //this method signs the user out of the current window and sends them back to the login screen
        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            LoginWindow Logger1 = new LoginWindow();
            Close(); // close form        
            Dispose();
            Logger1.Show();        
        }


        //--------------------------------------------------------------------------------------------
        //PURE UI
        //------------------------------------------------------------------------------------------------------------
        public void InitUser(int User_Type, string UName, string UID, string Program_Enrolled, string Cohort)
        {
            if (User_Type == 1)
            {
                //student login
                Logic_API.Business_layer.Student_MemeberLogged_in = new Student(User_Type, Program_Enrolled, UID, UName, Cohort);
                User_Type_Logged_In = "Student";
            }
            else
            {
                // staff login 
                Logic_API.Business_layer.Staff_Member_Logged_in = new Staff(User_Type, UID, UName);
                User_Type_Logged_In = "Staff";
                Logic_API.Business_layer.Staff_Member_Logged_in = Logic_API.Business_layer.SetModuleData_staff_account();
            }
        }

        public void Update_UI_Image_(Image img)
        {
            this.BackgroundImage = img;
        }

        //------------------------------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------------------------
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (User_Type_Logged_In == "Staff")
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:  //Home Page
                        InitHomePageTab();
                        break;
                    case 1:  //Student Page
                        InitStudentTab1();
                        break;
                    case 2:  //Modules
                        InitModulesTab();
                        break;
                    case 3:  //Programs
                        InitProgramsTab();
                        break;
                    case 4:  //Grades
                        InitGradesTab();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:  //Home Page
                        InitHomePageTab();
                        break;
                    case 1:  //Student Page
                        InitStudentTab1();
                        break;
                    default:
                        break;
                }
            }
        }


        //------------------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------------------------
        public void InitHomePageTab()
        {
            if (User_Type_Logged_In == "Student")
            {
                lblLoggedinas.Text = "Logged in as: " + Logic_API.Business_layer.Student_MemeberLogged_in.Get_Name();
                lblIDHome.Text = "ID: " + Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID();
            }
            else
            {
                lblLoggedinas.Text = "Logged in as: " + Logic_API.Business_layer.Staff_Member_Logged_in.Get_Name();
                lblIDHome.Text = "ID: " + Logic_API.Business_layer.Staff_Member_Logged_in.Get_ID();
            }

        }

        //-------------------------------------------------------------------------------------------------------
        //UI WITH CALLS TO LOGIC API AND USER CLASSES
        //-------------------------------------------------------------------------------------------------------------
        public void InitGui(string UName, string UID, string Program_Enrolled)
        {
            InitHomePageTab();

            int Current_Type = 0;
            if (User_Type_Logged_In == "Student")
            {
                Current_Type = Logic_API.Business_layer.Student_MemeberLogged_in.Get_User_Type();
            }
            else
            {
                Current_Type = Logic_API.Business_layer.Staff_Member_Logged_in.Get_User_Type();
            }

            // if a student then remove the staff tabs 
            if (Current_Type == 1)
            {   
                tabControl1.TabPages.RemoveAt(4);  // Grades Tab
                tabControl1.TabPages.RemoveAt(3);  // Programs Tab
                tabControl1.TabPages.RemoveAt(2);  // Modules Tab
            }

            // if a staff then remove the student tabs 
            if (Current_Type == 0)
            {
                //tabControl1.TabPages.RemoveAt(1);  //Grades Tab
            }
        }


        //-------------------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------------------------
        public void InitProgramsTab()
        {
            // this section essentially just handles adding an updated module list to the cbaddcoremodule component
            InitProgramsTab_GUI();

            if (Logic_API.Business_layer.Staff_Member_Logged_in.Get_Update_Modules())
            {
                Logic_API.Business_layer.Staff_Member_Logged_in = Logic_API.Business_layer.SetModuleData_staff_account();
            }
            string[] ModuleTitles = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleTitles();

            cbaddcoremodule.Items.Clear();
            for (int i = 0; i < ModuleTitles.Length; i++)
            {
                cbaddcoremodule.Items.Add(ModuleTitles[i].Trim()); // add module title to the cb
            }
        }


        //-------------------------------------------------------------------------------------------------------------
        //UI WITH CALLS TO LOGIC API
        //-------------------------------------------------------------------------------------------------------------
        private void BtnNewModule_Click(object sender, EventArgs e)
        {
            // get data out of UI
            string Title = tbModuleName.Text.ToString();
            string Module_Code = tbModuleCode.Text.ToString();        
            string[,] Number_of_Assesments_and_Weighting = new string[4, 2] {{ tbAssesment1.Text, tbWeighting1.Text },
                                                                             { tbAssesment2.Text, tbWeighting2.Text },
                                                                             { tbAssesment3.Text, tbWeighting3.Text },
                                                                             { tbAssesment4.Text, tbWeighting4.Text }};
            string Linked = "None";
            if ((tbAssesment4.Text != "None"))
            {
                //[Name of module], [Number of assignement] e.g 2nd assignment
                Linked = string.Format("[{0}, {1}] ", cbModule_Names_Modules.Text, cbAssignment_Name_Modules.SelectedIndex.ToString());

                //Bind this module to the linked module 
                Logic_API.Business_layer.Bind_to_linked_Module(Title, cbModule_Codes_M.Text);
            }

            //Create the new module and appent to rtb
            rtbStaff.AppendText(Logic_API.Data_Storage.CreateNewModule(Title, Module_Code, Number_of_Assesments_and_Weighting, Linked));

            MessageBox.Show("Succefully created new module", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //tell staff class that its module array needs to be updated
            Logic_API.Business_layer.Staff_Member_Logged_in.Set_Update_Modules(true);
            InitModulesTab();     
        }


        //--------------------------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------------------------
        private void AddCoreModuleToProgram()
        {
            if (rbCoreModule.Checked == false && rbOptionalModule.Checked == false)
            {
                MessageBox.Show("Please indicate if this module is Core or Optional", "Missing module type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // get the module name from cb
                    string AddtoOverview = cbaddcoremodule.SelectedItem.ToString();  

                    if (rbCoreModule.Checked)
                    {
                        // add it to the rich text preview
                        rtbPreview.AppendText(AddtoOverview + " (Core),\n");
                        int COunter = int.Parse(LblCoreMod_.Text) + 1;
                        LblCoreMod_.Text = COunter.ToString();
                    }
                    else
                    {
                        // add it to the rich text preview
                        rtbPreview.AppendText(AddtoOverview + " (Optional),\n");
                        int COunter = int.Parse(LblOpMod_.Text) + 1;
                        LblOpMod_.Text = COunter.ToString();
                    }

                    cbaddcoremodule.Items.Remove(cbaddcoremodule.SelectedItem);  //clear
                    cbaddcoremodule.Text = "";
                    rtbPreview.Text.Trim();

                    string[] Programs = rtbPreview.Text.Replace("(Core),", "").Replace("(Optional),", "").Split('\n'); //Get data already in the preview plane and split it according to new lines

                    cbRemovemodule.Items.Clear();
                    for (int i = 0; i <= Programs.Length - 2; i++)   // programs.lenght -2 inorder to avoid the "" on other end of split
                    {
                        // add results to other cb so users can remove any module they are not satisfied with
                        cbRemovemodule.Items.Add(Programs[i]); 
                    }
                    PercentageBar_Programs();
                }
                catch
                {
                    MessageBox.Show("Please select another module to add", "Problem with module select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAddCoreModule_Click(object sender, EventArgs e)
        {
            AddCoreModuleToProgram();
        }

        public void PercentageBar_Programs()
        {
            //TODO: Add a check for the core modules as you cant have more than 5 core modules with optionals
            string COunter = cbRemovemodule.Items.Count.ToString();
            float ValueT = (float.Parse(COunter) / 6) * 100;
            if (ValueT <= 100 && ValueT >= 0)
            {
                pbPrograms.Value = (int)ValueT;
            }
        }


        //------------------------------------------------------------------------------------------
        // PURE UI
        //-------------------------------------------------------------------------------------------------------------
        private void BtnRemoveCoreModule_Click(object sender, EventArgs e)
        {
            try
            {
                //add the item back to the add module cb           
                cbaddcoremodule.Items.Add(cbRemovemodule.SelectedItem.ToString().Trim());

                // take all data in rtb and split it according to newlines
                string[] Programs = rtbPreview.Text.Split('\n');
                Programs = Programs.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                rtbPreview.Clear();      
                
                for (int i = 0; i <= Programs.Length - 1; i++)
                {
                    // get the item user wants to remove from program
                    string SelectedModule_Core = cbRemovemodule.SelectedItem.ToString().Trim() + " (Core),";     
                    string SelectedModule_Optional = cbRemovemodule.SelectedItem.ToString().Trim() + " (Optional),";
                    if (Programs[i] != SelectedModule_Core && Programs[i] != SelectedModule_Optional && Programs[i] != "")
                    {
                        rtbPreview.AppendText(Programs[i] + "\n");
                    }
                    else
                    {
                        if (Programs[i].Contains("Core"))
                        {
                            int COunter = int.Parse(LblCoreMod_.Text) - 1;
                            LblCoreMod_.Text = COunter.ToString();
                        }
                        else
                        {
                            int COunter = int.Parse(LblOpMod_.Text) - 1;
                            LblOpMod_.Text = COunter.ToString();
                        }
                    }
                }
                cbRemovemodule.Text = "";
                cbRemovemodule.Items.Remove(cbRemovemodule.SelectedItem);
                rtbPreview.Text.Trim();

                // UPDATE THE PERCENTAGE BAR WITH THE MODULE PROGRESS 1/6-> 2/6-> 3/6
                PercentageBar_Programs();
            }
            catch
            {
                MessageBox.Show("Check all fields contain a value", "Cannot remove module from program overview", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     


        //-----------------------------------------------------------------------------------------------------
        //Pure UI
        //-------------------------------------------------------------------------------------------------------------
        private void TbWeighting1_TextChanged(object sender, EventArgs e)
        {
            DoWeightPercentage();
        }


        private void DoWeightPercentage()
        {
            // initialize
            int Percentage = 0;
            int weight1 = 0;
            int weight2 = 0;
            int weight3 = 0;
            int weight4 = 0;

            try
            {
                if (tbWeighting1.Text != "")
                { weight1 = int.Parse(tbWeighting1.Text); }

                if (tbWeighting2.Text != "")
                { weight2 = int.Parse(tbWeighting2.Text); }

                if (tbWeighting3.Text != "")
                { weight3 = int.Parse(tbWeighting3.Text); }

                if (tbWeighting4.Text != "" && cbModule_Codes_M.Text!= "" && tbAssesment4.Text != "None")
                { weight4 = int.Parse(tbWeighting4.Text); }

                Percentage = weight1 + weight2 + weight3 + weight4;

                // if the value is between 0 .. 100 
                if (Percentage >= 0 && Percentage <= 100)
                {
                    pbweight.Value = Percentage;
                }
                lblPercentage.Text = Percentage.ToString() + "%";
            }
            catch (Exception)
            {
                MessageBox.Show("Error at percentage calculation", "Error at percentage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //-------------------------------------------------------------------------------------------------------------
        private void BtnClear_Staff_Click(object sender, EventArgs e)
        {
            InitModulesTab();
        }


        //-------------------------------------------------------------------------------------------------------------
        private void BtnPreview_Click(object sender, EventArgs e)
        {       
            if (!(int.Parse(lblPercentage.Text.Replace("%", " ").ToString().Trim()) != 100))
            {
                try  //try to display all the data, if any fields are empty this will fall into catch
                {
                    Check_TB_Modules(); //Make sure no empty fields
                    rtbStaff.Clear();
                    rtbStaff.AppendText(string.Format("Department: {0} \nModule Name: {1} \nModule Code: {2}\n", cbDepartment.SelectedItem.ToString(), tbModuleName.Text.ToString(), tbModuleCode.Text.ToString()));
                    rtbStaff.AppendText(string.Format("Assesment 1:\n{0}\n{1}% \n \nAssesment 2:\n{2}\n{3}%\n \nAssesment 3:\n{4}\n{5}%\n \n", tbAssesment1.Text.ToString(), tbWeighting1.Text.ToString(), tbAssesment2.Text.ToString(), tbWeighting2.Text.ToString(), tbAssesment3.Text.ToString(), tbWeighting3.Text.ToString()));
                    rtbStaff.AppendText(string.Format("Assesment 4:\n{0}\n{1}%", tbAssesment4.Text, tbWeighting4.Text));
                }
                catch (Exception)
                {
                    rtbStaff.Clear();
                    MessageBox.Show("Please check all data fields are correct and try again", "Modules Preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnNewModule.Enabled = true;
            }
            else
            {
                MessageBox.Show("Assignments do not make up 100%\nCannot create module ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //---------------------------------------------------------------------------------------
        //PURE UI
        //---------------------------------------------------------------------------------------
        private void Check_TB_Modules()
        {
            //Titles
            if (tbAssesment1.Text.Trim() == ""){tbAssesment1.Text = "None";}
            if (tbAssesment2.Text.Trim() == ""){tbAssesment2.Text = "None";}
            if (tbAssesment3.Text.Trim() == ""){tbAssesment3.Text = "None";}
            if (tbAssesment4.Text.Trim() == ""){tbAssesment4.Text = "None";}

            // Weights
            if (tbWeighting1.Text.Trim() == ""){tbWeighting1.Text = "0";}
            if (tbWeighting2.Text.Trim() == ""){tbWeighting2.Text = "0";}
            if (tbWeighting3.Text.Trim() == ""){tbWeighting3.Text = "0";}
            if (tbWeighting4.Text.Trim() == ""){tbWeighting4.Text = "0"; }
        }


        //----------------------------------------------------------------------------------------------------------
        //PURE UI [PROGRAMS TAB]
        //-------------------------------------------------------------------------------------------------------------
        private void BtnAddYear_Click(object sender, EventArgs e)
        {
            try
            {
                int breaker = 0; 
                string[,] Linked_modules = Logic_API.Business_layer.Find_Linked_Modules(rtbPreview.Text);
                if (Linked_modules != null) // if null then there are no linked modules
                {
                    for (int i = 0; i < Linked_modules.GetLength(0); i++)
                    {
                        if (Linked_modules[i, 2] == null)
                        {
                            MessageBox.Show(string.Format("Module [{0}] is Linked to Module [{1}], Both must be present in the same year and both must be core. Remove [{0}] or Add [{1}] to proceed", Linked_modules[i, 0], Linked_modules[i, 1]),"Linked modules mismatch" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                            //throw new ArgumentException(string.Format("Module {0} is Linked to Module {1}, Both must be present in the same year", "Remove {0} or Add {1} to proceed", Linked_modules[i, 0], Linked_modules[i, 1]));
                            breaker = 1;
                        }
                    }
                }


                bool Acceptable = Logic_API.Business_layer.Check_ModuleList_follows_rules(int.Parse(LblOpMod_.Text), int.Parse(LblCoreMod_.Text));
                if (Acceptable && breaker==0)
                {                 
                    btnAutoPopulate_Programs.Enabled = true; // renable the auto populate button
                    if (pbPrograms.Value == 100)   // it is only 100 when 6 or more modules have been added to the program
                    {
                        PnlProgramDetails.Enabled = false;
                        LblOpMod_.Text = "0";
                        LblCoreMod_.Text = "0";

                        if (rtbProgramOverview.Text.Trim() == "") // first time adding data so first year
                        {
                            rtbProgramOverview.Text = string.Format("Program Title: {0}\nProgram ID: {1}\nLength of Program: {2} Years", tbProgramTitle_Programs.Text, tbProgramID.Text, cbProgramLength.SelectedItem.ToString());
                            rtbProgramOverview.AppendText(string.Format("\n\n|Year {0} Modules \n{1}\n", lblYears_UI.Text.ToString(), rtbPreview.Text));
                            cbRemovemodule.Text = "";                  

                            if (cbProgramLength.SelectedItem.ToString() == "2" || cbProgramLength.SelectedItem.ToString() == "3")
                            {
                                lblYears_UI.Text = "2";
                                btnVerifypreview.Enabled = false;
                            }
                            else
                            {
                                gbCoreModule.Enabled = false;
                                btnVerifypreview.Enabled = true;
                            }
                            UpdateProgramGuiModules();
                        }
                        else
                        {
                            if (lblYears_UI.Text == "2")
                            {
                                rtbProgramOverview.AppendText(string.Format("\n|Year {0} Modules \n{1}\n", lblYears_UI.Text, rtbPreview.Text));
                                cbRemovemodule.Text = "";
                                if (cbProgramLength.SelectedItem.ToString() == "3")
                                {
                                    lblYears_UI.Text = "3";
                                    btnVerifypreview.Enabled = false;
                                }
                                else
                                {
                                    gbCoreModule.Enabled = false;
                                    btnVerifypreview.Enabled = true;
                                }
                                UpdateProgramGuiModules();
                            }
                            else
                            {
                                if (lblYears_UI.Text == "3")
                                {
                                    cbRemovemodule.Text = "";
                                    rtbProgramOverview.AppendText(string.Format("\n|Year {0} Modules \n{1}\n", lblYears_UI.Text, rtbPreview.Text));
                                    btnAddYear.Enabled = false;
                                    btnAutoPopulate_Programs.Enabled = false;
                                    btnVerifypreview.Enabled = true;
                                    UpdateProgramGuiModules();
                                }
                            }
                        }
                    }
                    else { MessageBox.Show("Please ensure you have atleast 6 modules available in your program year", "Not enough modules in program", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else {MessageBox.Show("Please ensure you have less than 10 modules in your year, of which core cannot be more than 5 of them", "Modules selected break rules", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception) {MessageBox.Show("Please ensure you have filled all apropriate fields before proceding", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        public void UpdateProgramGuiModules()
        {
            rtbPreview.Clear();
            cbRemovemodule.Items.Clear();
            cbRemovemodule.TabIndex = 0;  
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            InitProgramsTab();
        }

        private void InitProgramsTab_GUI()
        {
            ClearProgramModules();
            gbCoreModule.Enabled = true;
            pbPrograms.Value = 0;
            btnAddYear.Enabled = true;
            rtbProgramOverview.Clear();
            lblYears_UI.Text = "1";
            tbProgramID.Clear();
            tbProgramTitle_Programs.Clear();
            rtbPreview.Clear();
            rbCoreModule.Checked = false;
            rbOptionalModule.Checked = false;
            cbProgramLength.Text = "";
            btnFinalize.Enabled = false;
            btnAutoPopulate_Programs.Enabled = true;
            cbProgramLength.Enabled = true;
            PnlProgramDetails.Enabled = true;
            LblCoreMod_.Text = "0";
            LblOpMod_.Text = "0";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearProgramModules();
        }

        // moves all modules that were in remove module check box to add module check box
        private void ClearProgramModules()
        {
            rtbPreview.Clear();
            for (int i = 0; i < cbRemovemodule.Items.Count; i++)
            {
                cbaddcoremodule.Items.Add(cbRemovemodule.Items[i].ToString().Trim());
            }
            cbRemovemodule.Items.Clear();
            pbPrograms.Value = 0;
        }


        private void BtnVerifypreview_Click(object sender, EventArgs e)
        {
            btnFinalize.Enabled = true;
        }


        private void TbModuleCode_Leave(object sender, EventArgs e)
        {
            string ModuleID = tbModuleCode.Text;

            //does it meet the required rules (length /unique)
            bool Meets_Rules = Logic_API.Business_layer.Meets_required_Rules("Modules", "ModuleID", ModuleID, 5);

            if (!Meets_Rules)
            {
                this.tbModuleCode.Focus();
            }
        }


        private void TbProgramID_Leave(object sender, EventArgs e)
        {
            string Program_ID_Text = tbProgramID.Text;

            //does it meet the required rules (length /unique)
            bool Meets_Rules = Logic_API.Business_layer.Meets_required_Rules("Programs", "ProgramID", Program_ID_Text, 6);

            if (!Meets_Rules)
            {
                this.tbProgramID.Focus();
            }
        }


        private void BtnFinalize_Click(object sender, EventArgs e)
        {
            string Program_Title = tbProgramTitle_Programs.Text.ToString();
            string Program_ID = tbProgramID.Text.ToString();
            string Lenght_Of_program = cbProgramLength.SelectedItem.ToString();

            string[] Command_ModuleList = Logic_API.Business_layer.Dissassemble_Program_Overview(Program_Title, Program_ID, Lenght_Of_program, rtbProgramOverview.Text.ToString());
            string[] Modules = Command_ModuleList[2].Trim().Split(',');

            string[] Similarity_Data = Logic_API.Business_layer.Is_Program_Unique(Modules);
            float Is_Unique = float.Parse(Similarity_Data[0]); // similarity %
            string Most_Similar_Program_Name = Similarity_Data[1]; //Title

            if (Is_Unique < 80)
            {
                Logic_API.Business_layer.CreateNewProgram(Is_Unique,Command_ModuleList[0], Command_ModuleList[1]);
                
                if (Is_Unique == 0)
                {
                    MessageBox.Show(string.Format("Program [{0}] Created", tbProgramTitle_Programs.Text.ToString()), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else            
                MessageBox.Show(string.Format("Program [{0}] Created, [{1}%] similarity to [{2}]", tbProgramTitle_Programs.Text.ToString(), Is_Unique, Most_Similar_Program_Name), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(string.Format("Program ['{0}'] does not have enough unique modules so it cannot be created, Too similar [{1} %] to ['{2}']", tbProgramTitle_Programs.Text.ToString(), Is_Unique, Most_Similar_Program_Name), "Program already exists with a different name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btnFinalize.Enabled = false;
            btnVerifypreview.Enabled = false;
        }


        //-----------------------------------------------------------------------------------------------
        //PURE UI (AUTO POPULATES)
        //-----------------------------------------------------------------------------------------------------------------------
        private void BtnAutoPopulate_Programs_Click(object sender, EventArgs e)
        {
            // if no data then give it data
            if (tbProgramTitle_Programs.Text == "")
            { tbProgramTitle_Programs.Text = "Msc Computer Science"; }

            if (tbProgramID.Text == "")
            { tbProgramID.Text = Logic_API.Business_layer.GiveMe_ARandomNumber(100000, 999999).ToString(); }

            if (cbProgramLength.Text == "")
            { cbProgramLength.SelectedIndex = Logic_API.Business_layer.GiveMe_ARandomNumber(0, 3); } // 3 years
            cbProgramLength.Text = cbProgramLength.SelectedItem.ToString();

            // randomize the choice of optional/core modules a bit
            int Number_of_Modules = 0;
            int Number_of_Core_Modules = 0;
            int Number_Of_Optional_Modules = 0;

            while (!(Number_of_Modules > 7 && Number_of_Modules < 10))
            {
                Number_of_Core_Modules = Logic_API.Business_layer.GiveMe_ARandomNumber(2, 5);
                Number_Of_Optional_Modules = Logic_API.Business_layer.GiveMe_ARandomNumber(1, 6);
                Number_of_Modules = Number_of_Core_Modules + Number_Of_Optional_Modules;
            }


            for (int i = 0; i < Number_of_Modules; i++)
            {
                // cycle through the component and add items to the rich text box, 
                // selected index is 0 becasue as items get added they also get deleted from this cb  
                // first "Number_Of_Core_Modules" are core
                int Number_of_Modules_in_Cb = cbaddcoremodule.Items.Count;
                cbaddcoremodule.SelectedIndex = Logic_API.Business_layer.GiveMe_ARandomNumber(0, Number_of_Modules_in_Cb); ;
                if (i < Number_of_Core_Modules) { rbCoreModule.Checked = true; } else { rbOptionalModule.Checked = true; }
                AddCoreModuleToProgram();
            }
            btnAutoPopulate_Programs.Enabled = false;
        }


        private void BtnAutoPopulate_Module_Click(object sender, EventArgs e)
        {
            cbDepartment.SelectedIndex = 0;
            cbDepartment.Text = cbDepartment.SelectedItem.ToString();
            if (tbModuleCode.Text == "")
            {
                tbModuleCode.Text = Logic_API.Business_layer.GiveMe_ARandomNumber(10000, 99999).ToString();
                tbModuleName.Text = "Cyber Security";
            }

            tbAssesment1.Text = "ACW";
            tbWeighting1.Text = "30";
            tbAssesment2.Text = "Acw";
            tbWeighting2.Text = "30";

            if (tbAssesment3.Text == "None")
            {
                tbAssesment3.Text = "Exam";
                tbWeighting3.Text = "40";
            }
        }


        //---------------------------------------------------------------------------------------------------------------
        //[GRADES TAB]
        //-------------------------------------------------------------------------------------------------------------
        private void InitGradesTab()
        {
            InitGradesTab_GUI(); // init gui

            if (Logic_API.Business_layer.Staff_Member_Logged_in.Get_Update_Modules())
            {
                // check if we need to update our module list
                Logic_API.Business_layer.Staff_Member_Logged_in = Logic_API.Business_layer.SetModuleData_staff_account();
            }
            // Add all the module titles to the combo box
            string[] ModuleTitles = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleTitles();
            string[] ModuleID = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleIDs();

            for (int i = 0; i < ModuleTitles.Length; i++)
            {
                cbModule_Grades.Items.Add(ModuleTitles[i]);
                cbModuleCodes_Grades.Items.Add(ModuleID[i]);
            }


        }

        private void InitGradesTab_GUI()
        {
            //initialize gui          
            cbModule_Grades.Text = "";
            cbStudent_grades.Text = "";
            cbModuleCodes_Grades.Text = "";
            cbModule_Grades.Items.Clear();
            cbStudent_grades.Items.Clear();
            cbModuleCodes_Grades.Items.Clear();
            
            tbModule_Number.Clear();
            TextBoxes_Grades();
            btnCommit_Grades.Enabled = false;
        }

        private void TextBoxes_Grades()
        {
            tbGrade1_Grades.Text = "0";
            tbGrade2_Grades.Text = "0";
            tbGrade3_Grades.Text = "0";
            tbGrade4_Grades.Text = "0";

            tbAssignments1_Grades.Text = "None";
            tbAssignments2_Grades.Text = "None";
            tbAssignments3_Grades.Text = "None";
            tbAssignments4_Grades.Text = "None";
            tbLinked1.Text = "None";

            tbWeights1_Grades.Clear();
            tbWeights2_Grades.Clear();
            tbWeights3_Grades.Clear();
            tbWeights4_Grades.Clear();

            tbFinalGrade.Clear();
            tbClassification.Clear();
        }


        private void CbModule_Grades_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxes_Grades();
            // change the other cb to the same index
            cbModuleCodes_Grades.SelectedIndex = cbModule_Grades.SelectedIndex;
            cbModuleCodes_Grades.Text = cbModuleCodes_Grades.SelectedItem.ToString();

            //find students on that module
            cbStudent_grades.Items.Clear();
            cbStudent_grades.Text = "";

            string Module_ID = cbModuleCodes_Grades.Text.ToString();

            // get list of students who are enrolled on the module
            string [] Students = Logic_API.Data_Storage.GetStudent(Module_ID);

            if (Students.Length > 1)
            {
                cbStudent_grades.Enabled = true;
                for (int i = 0; i < Students.Length; i++)
                {
                    if (Students[i] != "")
                    { cbStudent_grades.Items.Add(Students[i].Trim()); }                   
                }
               
            }
            else
            {
                cbStudent_grades.Text = "No students enrolled";
                cbStudent_grades.Enabled = false;
            }
            tbModule_Number.Clear();
            
        }

        //-----------------------------------------------------------------------------------------------------------
        //[MODULES TAB]
        //-------------------------------------------------------------------------------------------------------------
        private void InitModulesTab()
        {
            //update the modules tab gui
            InitModulesTab_GUI();

            if (Logic_API.Business_layer.Staff_Member_Logged_in.Get_Update_Modules())
            {
                Logic_API.Business_layer.Staff_Member_Logged_in = Logic_API.Business_layer.SetModuleData_staff_account();
            }
            // Add all the module titles to the combo box
            string[] ModuleTitles = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleTitles();
            string[] ModuleID = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleIDs();           
            for (int i = 0; i < ModuleTitles.Length; i++)
            {
                cbModule_Names_Modules.Items.Add(ModuleTitles[i]);
                cbModule_Codes_M.Items.Add(ModuleID[i]);
            }         
        }


        private void InitModulesTab_GUI()
        {
            cbModule_Names_Modules.Items.Clear();
            cbAssignment_Name_Modules.Items.Clear();
            cbDepartment.Text = "";
            tbModuleName.Clear();
            tbModuleCode.Clear();
            rtbStaff.Clear();
            rbCoreModule.Checked = false;
            rbOptionalModule.Checked = false;
            rtbPreview.Clear();
            tbAssesment1.Text = "None";
            tbAssesment2.Text = "None";
            tbAssesment3.Text = "None";
            tbAssesment4.Text = "None";

            tbLinked1.Text = "None";

            tbWeighting1.Text = "0";
            tbWeighting2.Text = "0";
            tbWeighting3.Text = "0";
            tbWeighting4.Text = "0";
            lblPercentage.Text = "0%";
            btnNewModule.Enabled = false;

            cbModule_Names_Modules.Text = "";
            cbModule_Codes_M.Items.Clear();
            cbModule_Codes_M.Text = "";
            cbAssignment_Name_Modules.Items.Clear();
            cbAssignment_Name_Modules.Text = "";
        }

        private void CbModule_Names_Modules_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbAssesment4.Text = "None";
            // change the other cb to the same index
            cbModule_Codes_M.SelectedIndex = cbModule_Names_Modules.SelectedIndex;
            cbModule_Codes_M.Text = cbModule_Codes_M.SelectedItem.ToString();

            string ModuleID = cbModule_Codes_M.Text;

            // get the module assesment data
            string[] Assesment_Data = Logic_API.Business_layer.Get_Assesment_Data(ModuleID);
            string[] Titles = Assesment_Data[0].Split(',');

            // Add all the assesment titles to the combo box
            cbAssignment_Name_Modules.Items.Clear();
            cbAssignment_Name_Modules.Text = "";

            for (int i = 0; i < Titles.Length; i++)
            {
                if (Titles[i]!= "None")
                {
                    cbAssignment_Name_Modules.Items.Add(Titles[i]);
                    i = Titles.Length + 1; // break so that only the first assignment of any module can be linked
                }
            }

            bool Already_Linked = false;
            if (cbModule_Names_Modules.Text != "")
            {
                Already_Linked = Logic_API.Business_layer.Check_Is_already_Linked(cbModule_Codes_M.Text);
            }

            if (Already_Linked)
            {
                string Module_Name = cbModule_Names_Modules.Text;
                tbAssesment4.Text = "None";
                tbWeighting4.Text = "0";
                cbAssignment_Name_Modules.Text = "";
                cbModule_Names_Modules.Text = "";
                cbModule_Codes_M.Text = "";
                MessageBox.Show(string.Format("Cannot link with Modules [{0}] as it is already linked with another module", Module_Name),"Linked Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void CbAssignment_Name_Modules_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbAssesment4.Text = cbAssignment_Name_Modules.SelectedItem.ToString();
            tbWeighting4.Text = "0";
        }

    
        //-------------------------------------------------------------------------------------------------------------
        private void CbStudent_grades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStudent_grades.Text != "")
            {
                int ModuleID = int.Parse(cbModuleCodes_Grades.Text);
                int StudentID = int.Parse(cbStudent_grades.SelectedItem.ToString());

                // get students grades
                string[] Student_Grades = Logic_API.Business_layer.Get_Student_Module_Grades(ModuleID, StudentID);

                if (Student_Grades[0] != "0")
                {
                    // get the generic assesment data for the module 
                    string[] Module_Data = Logic_API.Business_layer.Get_Assesment_Data(ModuleID.ToString());

                    Update_Grades_Gui(Module_Data);

                    //Module Number 
                    tbModule_Number.Text = Student_Grades[0];
                    tbGrade1_Grades.Text = Student_Grades[1];
                    tbGrade2_Grades.Text = Student_Grades[2];
                    tbGrade3_Grades.Text = Student_Grades[3];
                    tbGrade4_Grades.Text = Student_Grades[4];
                }
                else
                {
                    MessageBox.Show(string.Format("This user doesnt exist, please manually delete user [{0}] from [{1}]", StudentID, ModuleID), "User not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Update_Grades_Gui(string[] Module_Data)
        {
            //weights
            string[] Values_Weights = Module_Data[1].Split(',');
            string[] Values_Titles = Module_Data[0].Split(',');
            int COunter = 0;

            // do this so weight value is displayed correctly
            for (int i = 0; i < Values_Weights.Length; i++)
            {
                if (Values_Weights[i] == "0")
                {
                    Values_Weights[i] = "";
                }
                if (Values_Titles[i] != "None")
                {
                    COunter++;
                }
            }

            switch (COunter)
            {
                case 0:
                    break;
                case 1:
                    tbWeights1_Grades.Text = Values_Weights[0];
                    tbWeights2_Grades.Text = "";
                    tbWeights3_Grades.Text = "";
                    tbWeights4_Grades.Text = "";

                    //titles
                    tbAssignments1_Grades.Text = Values_Titles[0];
                    tbGrade1_Grades.Enabled = true;
                    tbAssignments2_Grades.Text = "";
                    tbGrade2_Grades.Enabled = false;
                    tbAssignments3_Grades.Text = "";
                    tbGrade3_Grades.Enabled = false;
                    tbAssignments4_Grades.Text = "";
                    tbGrade4_Grades.Enabled = false;
                    break;
                case 2:
                    tbWeights1_Grades.Text = Values_Weights[0];
                    tbWeights2_Grades.Text = Values_Weights[1];
                    tbWeights3_Grades.Text = "";
                    tbWeights4_Grades.Text = "";

                    //titles
                    tbAssignments1_Grades.Text = Values_Titles[0];
                    tbGrade1_Grades.Enabled = true;
                    tbAssignments2_Grades.Text = Values_Titles[1];
                    tbGrade2_Grades.Enabled = true;
                    tbAssignments3_Grades.Text = "";
                    tbGrade3_Grades.Enabled = false;
                    tbAssignments4_Grades.Text = "";
                    tbGrade4_Grades.Enabled = false;
                    tbLinked1.Text = Module_Data[2];
                    break;
                case 3:
                    tbWeights1_Grades.Text = Values_Weights[0];
                    tbWeights2_Grades.Text = Values_Weights[1];
                    tbWeights3_Grades.Text = Values_Weights[2];
                    tbWeights4_Grades.Text = "";

                    //titles
                    tbAssignments1_Grades.Text = Values_Titles[0];
                    tbGrade1_Grades.Enabled = true;
                    tbAssignments2_Grades.Text = Values_Titles[1];
                    tbGrade2_Grades.Enabled = true;
                    tbAssignments3_Grades.Text = Values_Titles[2];
                    tbGrade3_Grades.Enabled = true;
                    tbAssignments4_Grades.Text = "";
                    tbGrade4_Grades.Enabled = false;
                    tbLinked1.Text = Module_Data[2];
                    break;
                case 4:
                    // weights
                    tbWeights1_Grades.Text = Values_Weights[0];
                    tbWeights2_Grades.Text = Values_Weights[1];
                    tbWeights3_Grades.Text = Values_Weights[2];
                    tbWeights4_Grades.Text = Values_Weights[3];

                    //titles
                    tbAssignments1_Grades.Text = Values_Titles[0];
                    tbGrade1_Grades.Enabled = true;
                    tbAssignments2_Grades.Text = Values_Titles[1];
                    tbGrade2_Grades.Enabled = true;
                    tbAssignments3_Grades.Text = Values_Titles[2];
                    tbGrade3_Grades.Enabled = true;
                    tbAssignments4_Grades.Text = Values_Titles[3];
                    tbGrade4_Grades.Enabled = true;
                    tbLinked1.Text = Module_Data[2];
                    break;
                default:
                    break;

            }

            if (Values_Titles[3] != "" && Values_Titles[3] != "None")
            //Linked to another module ?
            { tbLinked1.Text = Module_Data[2]; }

        }


        //------------------------------------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------------------------
        private void CbModule_Codes_M_SelectedIndexChanged(object sender, EventArgs e)
        {
            // change the other cb to the same index
            cbModule_Names_Modules.SelectedIndex = cbModule_Codes_M.SelectedIndex;
            cbModule_Names_Modules.Text = cbModule_Names_Modules.SelectedItem.ToString();

            string ModuleID = cbModule_Codes_M.Text;
            cbAssignment_Name_Modules.Items.Clear();
        }


        private void CbModuleCodes_Grades_SelectedIndexChanged(object sender, EventArgs e)
        {
            // change the other cb to the same index
            cbModuleCodes_Grades.SelectedIndex = cbModule_Grades.SelectedIndex;
            cbModuleCodes_Grades.Text = cbModuleCodes_Grades.SelectedItem.ToString();

            cbStudent_grades.Text = "";
        }


        //-------------------------------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------------------------
        //COmmit the grades on the UI to the students database table 
        private void BtnCommit_Grades_Click(object sender, EventArgs e)
        {
            if (cbStudent_grades.Text == "")
            {
                MessageBox.Show("Please select a student before proceding", "Module Number not found", MessageBoxButtons.OK); ;
            }
            else
            {
                //commit changes for this module
                //0-3 are assignment grades, 4 is final grade
                string Grades = string.Format("[{0},{1},{2},{3},{4}]", tbGrade1_Grades.Text, tbGrade2_Grades.Text, tbGrade3_Grades.Text, tbGrade4_Grades.Text, tbFinalGrade.Text);

                string ModuleID = cbModuleCodes_Grades.Text;
                string StudentID = cbStudent_grades.Text;
                string Module_NUmber = "Grade_" + tbModule_Number.Text.Trim();

                try
                {
                    // Update the database with the new grades
                    Logic_API.Data_Storage.UpdateStudentGrades(StudentID, Module_NUmber, Grades);

                    //if we are working with a linked module then change the second modules mark
                    if (tbLinked1.Text != "None" && tbLinked1.Text != "")
                    {
                        if (tbGrade4_Grades.Text != "0" & tbGrade4_Grades.Text != "" && tbGrade4_Grades.Text != "None")
                        { Logic_API.Business_layer.UpdateLinkedModule(StudentID, ModuleID, int.Parse(tbGrade4_Grades.Text)); }
                        else
                        { Logic_API.Business_layer.UpdateLinkedModule(StudentID, ModuleID, int.Parse(tbGrade1_Grades.Text)); }
                    }

                    MessageBox.Show("Commit Succesful", "Succesful", MessageBoxButtons.OK);
                }
                catch { MessageBox.Show("Database error on Update students grades", "Unsuccesful @[BtnCommit_Grades_Click]", MessageBoxButtons.OK); }
            }
            btnCommit_Grades.Enabled = false;
        }

        private void InitStudentTab_GUI()
        {                                                  
            lbModules_Student.Items.Clear();               
            cbModules_StudentTab.Items.Clear();
            cbModuleID.Items.Clear();
            cbUserID.Items.Clear();
            TextBoxes_Students();
        }

        private void TextBoxes_Students()
        {
            lblProgTitle_Student_UI.Text = "None";
            lblProgramCode_Students_UI.Text = "None";
            lblProgrLenght_Student_UI.Text = "None";
            lblyearAverage.Text = "None";
            lblYourNameHere.Text = "Select a Student";
            lbEnrolledModules.Items.Clear();
            cbYear.Text = "";            
            rtbStudent.Clear();
        }


        //-------------------------------------------------------------------------------------------------------------
        private void InitStudentTab1()
        {
            //clear ui
            InitStudentTab_GUI();

            if (User_Type_Logged_In == "Student") 
            {
                cbUserID.Text = Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID();
                pnl_staff_features.Enabled = false;

                if (Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(1) == null) // if not set at all then create student obj
                {
                    Logic_API.Business_layer.Student_MemeberLogged_in = Logic_API.Business_layer.Create_Student(Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID(), User_Data);
                }
                else
                {
                    bool choice = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(1).Get_yearFully_Set();
                    if (!choice) // if soft set then create student obj
                    {
                        Logic_API.Business_layer.Student_MemeberLogged_in = Logic_API.Business_layer.Create_Student(Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID(), User_Data);
                    }
                }
                SetupNewUserGui_Students();
            }
            else
            {
                btnFinalize_Year.Visible = false;
                pnlOptional_Modules.Visible = false;
                cbModules_StudentTab.Text = "";
                cbModuleID.Text = "";
                cbUserID.Text = "";
                lblYourNameHere.Text = "Select a Student";
                 
                string[] modules = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleTitles();
                string[] modulesIDs = Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleIDs();

                for (int i = 0; i < modules.Length; i++)
                {
                    cbModules_StudentTab.Items.Add(modules[i]);
                    cbModuleID.Items.Add(modulesIDs[i]);
                }
            }
        }


        private void Student_Tab_StudentUse()
        {
            // UI Title
            string Program_Title = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Name();
            lblProgTitle_Student_UI.Text = Program_Title;

            //User Name
            lblYourNameHere.Text = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Name();

            // Length of course          
            int Length = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length();
            lblProgrLenght_Student_UI.Text = Length.ToString();
            cbYear.Items.Add("1");
            cbYear.SelectedIndex = 0;
            if (Length >= 2) { cbYear.Items.Add("2"); }
            if (Length >= 3) { cbYear.Items.Add("3"); }

            // Program ID
            Programmes User_Program = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program();
            string Program_ID = User_Program.Get_ID();
            lblProgramCode_Students_UI.Text = Program_ID;

            //Enrolled Modules        
            int Year_int = int.Parse(cbYear.Text.Trim());
           
            // this soft sets the year, giving the class the module names 
            // does not set "Module1_obj"'s
            Set_year_if_year_Not_Set();

            Year Current_Year = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year_int);
            string[] Modules;

            // if user has already enrolled on his years modules/ if year object is not null
            if (Current_Year != null)
            {
                //get year average
                int Year__ = cbYear.SelectedIndex + 1;
                string[] Values = Logic_API.Business_layer.DO_Year_Average(Logic_API.Business_layer.Student_MemeberLogged_in, Year__); // get average and classification
                lblyearAverage.Text = string.Format("{0}:{1}", Values[0], Values[1]);// set ui to average and classification
            }
            else
            {
                //if user hasnt enrolled then just add core modules
                Modules = User_Program.Get_All_C_Modules();
                string[] OptionalModules = User_Program.Get_All_O_Modules();  //Cointains all years optional modules
                OptionalModules = OptionalModules[Year_int - 1].Split(',');        // get the current years optional modules

                // display optional modules
                lbModules_Student.Items.Clear();
                for (int i = 0; i < OptionalModules.Length; i++)
                {
                    lbModules_Student.Items.Add(OptionalModules[i]);
                }

                //SHow these as user will need them
                Make_optionals_AP_DIssapear();
            }

        }

        //-----------------------------------------------------------------------------------------------------
        //PURE UI
        //-----------------------------------------------------------------------------------------------------
        private void Set_year_if_year_Not_Set()
        {
            //if user already enrolled to 6 modules
            int Year = int.Parse(cbYear.Text.Trim());

            string[] Modules_Enrolled_on = Logic_API.Data_Storage.WhatModulesis_thisStudent_EnrolledOn(Year.ToString(), Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID().ToString());
            Modules_Enrolled_on = Modules_Enrolled_on[0].Split(',');

            // if the user is enrolled to all 6 modules but this data is not set in the class
            if (Modules_Enrolled_on.Length == 6 && Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year) == null)
            {
                Logic_API.Business_layer.Student_MemeberLogged_in.Set_Year(Modules_Enrolled_on, Year);
            }
        }


        private void CbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbEnrolledModules.Items.Clear();
            rtbStudent.Clear();
            lbModules_Student.Items.Clear();
            int Year = int.Parse(cbYear.Text.Trim());

            // get modules in that year for that student
            string[] Modules;
            if (Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year) == null)
            { Modules = Logic_API.Data_Storage.Get_Modules_for_This_Year(Year, Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID().ToString()); }
            else
            { Modules = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year).Get_Modules(); }
       

            // update with modules student is enrolled on
            for (int i = 0; i < Modules.Length; i++)
            {
                lbEnrolledModules.Items.Add(Modules[i]);
            }

            if (lbEnrolledModules.Items.Count != 6)
            {
                //Show ui
                Make_optionals_AP_DIssapear();

                // Update with optional modules
                Programmes User_Program = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program();
                string[] Optional_Modules = User_Program.Get_All_O_Modules(); // contains optional modules for all years
                Optional_Modules = Optional_Modules[Year - 1].Split(',');      // contains optional modules for a selected year


                for (int i = 0; i < Optional_Modules.Length; i++)
                {
                    lbModules_Student.Items.Add(Optional_Modules[i]);
                }
            }
            else // must mean that the user has 6 modules and is enrolled 
            {
                lbModules_Student.Items.Clear();
                Make_optionals_AP_DIssapear("Dissapear");
            }

            //Set_year_if_year_Not_Set();
            //Student_MemeberLogged_in = Logic_API.Create_User_OBJ(Student_MemeberLogged_in, User_Data);
        }
        

        private void Make_optionals_AP_DIssapear(string choice = "Appear")
        {
            if (choice == "Dissapear")
            {
                lbModules_Student.Visible = false;
                button1.Visible = false;
                lblEnrolledmodules.Visible = false;
                btnFinalize_Year.Visible = false;
            }
            else
            {
                lblEnrolledmodules.Visible = true;
                lbModules_Student.Visible = true;
                button1.Visible = true;
                btnFinalize_Year.Visible = true;
            }
        }


        //enroll button
        private void Button1_Click(object sender, EventArgs e)
        {
            if (lbModules_Student.SelectedItem != null)
            {
                string UserID = Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID().ToString();
                string Selected_Item = lbModules_Student.SelectedItem.ToString();
                lbEnrolledModules.Items.Add(Selected_Item);
                lbModules_Student.Items.Remove(Selected_Item);

                //Get IDs that were in there already and add the new user to it
                string Strudent_enrolled_IDs = Logic_API.Data_Storage.GetStudents_Enrolled_Module(Selected_Item);
                UserID = Strudent_enrolled_IDs + ", " + UserID;

                //UPDATE Module DATABASE
                Logic_API.Data_Storage.EnrollOntoModule(UserID, Selected_Item);
            }
            else { MessageBox.Show("No item selected", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }


        private void BtnFinalize_Year_Click(object sender, EventArgs e)
        {
            if (lbEnrolledModules.Items.Count <= 6)
            {
                if (lbEnrolledModules.Items.Count == 6)
                {
                    // get the data
                    string User_ID = Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID().ToString();
                    int Year = int.Parse(cbYear.Text);
                    string[] Module_List = lbEnrolledModules.Items.OfType<string>().ToArray();

                    // add to object (soft)
                    Logic_API.Business_layer.Student_MemeberLogged_in.Set_Year(Module_List, Year);

                    //UPDATE Year1/Year2/Year3 Student DATABASE column
                    Logic_API.Business_layer.EnrollStudent_ontoModules(Module_List, Year, User_ID);

                    // find the first empty column then write the module names in starting from that column
                    bool Finished = false;     
                    while (Finished == false)
                    {
                        Finished = Logic_API.Business_layer.Write_from_Empty_column(Year, User_ID, Module_List, Logic_API.Business_layer.Student_MemeberLogged_in,User_Data);
                    }

                    // output 
                    Make_optionals_AP_DIssapear("Dissapear");
                    Set_year_if_year_Not_Set();
                    MessageBox.Show(string.Format("Succesfully Enrolled onto Year {0} Modules", Year.ToString()), "Success", MessageBoxButtons.OK);
                    InitStudentTab1();
                }
                else
                {
                    MessageBox.Show("You must select 6 modules", "Module error @[BtnFinalize_Year_Click]", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("You can only enroll to a maximum of 6 modules", "Too many modules @[BtnFinalize_Year_Click]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitStudentTab1();
            }
        }   


        private void LbEnrolledModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbEnrolledModules.Items.Count == 6 && lbEnrolledModules.SelectedItem.ToString() != "")
            {
                if (lbModules_Student.Visible == false)
                {
                    rtbStudent.Clear();
                    string selected_Module = lbEnrolledModules.SelectedItem.ToString();
                    int Year = int.Parse(cbYear.Text);
                    Year Students_Year = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year);                   

                    if (Students_Year != null)
                    {
                        string[] Titles = Students_Year.Get_Modules();
                        // try find the module in the year
                        bool foundModule = false;
                        int i = 1;
                        while (!foundModule && i < 7)
                        {
                            try
                            {
                                // returns the title of the module were looking at                             
                                if (selected_Module == Titles[i- 1])
                                {
                                    foundModule = true;
                                }
                                else
                                {
                                    i++;
                                }
                            }
                            catch { foundModule = false; }
                        }

                        if (foundModule != false)
                        {
                            if (!Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year).Get_yearFully_Set()) // make sure we have grades
                            {
                                Logic_API.Business_layer.Student_MemeberLogged_in = Logic_API.Business_layer.Create_Student(Logic_API.Business_layer.Student_MemeberLogged_in.Get_ID(), User_Data);
                                Students_Year = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(Year);
                            }

                            string[] Student_Grades = Students_Year.Get_Module(i).GetGrades();
                            string[,] Title_of_Assesments_and_weighting = Students_Year.Get_Module(i).Get_Title_of_Assesments_and_weighting();

                            float [] Grades = new float [4];
                            float [] weights = new float[4];

                            for (int j = 0; j < 4; j++)
                            {
                                Grades[j] = float.Parse(Student_Grades[j]);
                                weights[j] = float.Parse(Title_of_Assesments_and_weighting[j,1]);
                            }

                            // work out the final grade for the selected module
                            int final_Grade = Students_Year.Get_Module(i).Get_FinalGrade();
                            string Classification = Logic_API.Business_layer.DetermineClassifiation(Grades, weights, final_Grade, "Module");

                            rtbStudent.AppendText(string.Format("Module Title: {0}\n", lbEnrolledModules.SelectedItem.ToString()));
                            rtbStudent.AppendText(string.Format("\nAssesment 1: {0}\n  Weighted: {2}%\n  Grade: {1}%", Title_of_Assesments_and_weighting[0, 0], Student_Grades[0], Title_of_Assesments_and_weighting[0, 1]));
                            if (Title_of_Assesments_and_weighting[1, 0] != "None")
                            { rtbStudent.AppendText(string.Format("\n\nAssesment 2: {0}\n  Weighted: {2}%\n  Grade: {1}%", Title_of_Assesments_and_weighting[1, 0], Student_Grades[1], Title_of_Assesments_and_weighting[1, 1])); }
                            if (Title_of_Assesments_and_weighting[2, 0] != "None")
                            { rtbStudent.AppendText(string.Format("\n\nAssesment 3: {0}\n  Weighted: {2}%\n  Grade: {1}%", Title_of_Assesments_and_weighting[2, 0], Student_Grades[2], Title_of_Assesments_and_weighting[2, 1])); }
                            if (Title_of_Assesments_and_weighting[3, 0] != "None")
                            { rtbStudent.AppendText(string.Format("\n\nAssesment 4: {0}\n  Weighted: {2}%\n  Grade: {1}%", Title_of_Assesments_and_weighting[3, 0], Student_Grades[3], Title_of_Assesments_and_weighting[3, 1])); }
                            rtbStudent.AppendText(string.Format("\n\nFinal Grade: {0}% {1}", final_Grade, Classification));

                        }
                        else { MessageBox.Show("Not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else { MessageBox.Show("Please finish enrolling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("Please finalize you year before proceeding", "Not enrolled", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("Please ensure you have enrolled onto 6 modules before proceeding", "Not enrolled, not enough modules", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        //--------------------------------------------------------------------------------------------------------------
        //PURE UI
        //------------------------------------------------------------------------------------------------------------
        private void TbGrade1_Grades_TextChanged(object sender, EventArgs e)
        {Do_Grades(tbGrade1_Grades); }

        private void TbGrade2_Grades_TextChanged(object sender, EventArgs e)
        {Do_Grades(tbGrade2_Grades); }

        private void TbGrade3_Grades_TextChanged(object sender, EventArgs e)
        {Do_Grades(tbGrade3_Grades);}

        private void TbGrade4_Grades_TextChanged(object sender, EventArgs e)
        { Do_Grades(tbGrade4_Grades);}

        private void Do_Grades(TextBox GradesTextBox)
        {
            if (GradesTextBox.Enabled == true)
            {
                bool is_Number = int.TryParse(GradesTextBox.Text, out int Grade);
                if (is_Number)
                {
                    Grade = Logic_API.Business_layer.Check_Grades(Grade);
                    GradesTextBox.Text = Grade.ToString();
                }
                else
                {
                    MessageBox.Show("Grade must be an integer value between 0 and 100", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GradesTextBox.Text = "0";
                }
            }
        }


        //---------------------------------------------------------------------------------------
        //PURE UI
        //-------------------------------------------------------------------------------------------
        private void BtnFinalGrade_Click(object sender, EventArgs e)
        {
            int Final_Grade = 0;
            string Final_Classification;
            float Grade_1 = 0;
            float Grade_2 = 0;
            float Grade_3 = 0;
            float Grade_4 = 0;

            float Weight_1 = 0;
            float Weight_2 = 0;
            float Weight_3 = 0;
            float Weight_4 = 0;

            // Make sure the text is valid (not empty or 0)
            if (tbGrade1_Grades.Text.Trim() != "" && tbWeights1_Grades.Text != "")
            {   Grade_1 = float.Parse(tbGrade1_Grades.Text);
                Weight_1 = float.Parse(tbWeights1_Grades.Text);
            }
            if (tbGrade2_Grades.Text.Trim() != "" && tbWeights2_Grades.Text != "")
            {
                Grade_2 = float.Parse(tbGrade2_Grades.Text);
                Weight_2 = float.Parse(tbWeights2_Grades.Text);
            }
            if (tbGrade3_Grades.Text.Trim() != "" && tbWeights3_Grades.Text != "")
            {
                Grade_3 = float.Parse(tbGrade3_Grades.Text);
                Weight_3 = float.Parse(tbWeights3_Grades.Text);
            }
            if (tbGrade4_Grades.Text.Trim() != "" && tbWeights4_Grades.Text!= "")
            {
                Grade_4 = float.Parse(tbGrade4_Grades.Text);
                Weight_4 = float.Parse(tbWeights4_Grades.Text);
            }
           
            float[] weights = new float[4] { Weight_1, Weight_2, Weight_3, Weight_4 };
            float[] Grades = new float[4] { Grade_1, Grade_2, Grade_3, Grade_4 };


            // get final grade 
            Final_Grade = Logic_API.Business_layer.Calculate_final_grade(Grades, weights);

            // get classification 
            Final_Classification = Logic_API.Business_layer.DetermineClassifiation(Grades, weights, Final_Grade,"Module");

            // update ui
            tbClassification.Text = Final_Classification;
            tbFinalGrade.Text = Final_Grade.ToString();

            btnCommit_Grades.Enabled = true;
        }


        private void BtnUnenroll_Click(object sender, EventArgs e)
        {
            string ModuleID = cbModuleCodes_Grades.Text;
            if (ModuleID != "")
            {
                Logic_API.Data_Storage.Unenroll_all_Students(ModuleID);
                MessageBox.Show(string.Format("Module [{0}] Cleared of all enrolled students", cbModule_Grades.Text), "Un-enroll succesful", MessageBoxButtons.OK);
            }
        }


        private void LblYears_UI_TextChanged(object sender, EventArgs e)
        {btnVerifypreview.Enabled = true;}

        private void BtnUpdate_Grades_Click(object sender, EventArgs e)
        {InitGradesTab();}

        // refresh
        private void Button2_Click(object sender, EventArgs e)
        {InitStudentTab1(); }

        private void CbUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StudentID = cbUserID.Text.Trim();
            Logic_API.Business_layer.Student_MemeberLogged_in = Logic_API.Business_layer.Create_Student(StudentID);
            if (Logic_API.Business_layer.Student_MemeberLogged_in != null)
            {               
                SetupNewUserGui_Students();
            }
            else { MessageBox.Show( "User Does not exist", "Please delete user from database", MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }

        private void SetupNewUserGui_Students()
        {
            lbEnrolledModules.Items.Clear();
            rtbStudent.Clear();
            lblProgTitle_Student_UI.Text = "None";
            lblProgramCode_Students_UI.Text = "None";
            lblProgrLenght_Student_UI.Text = "None";

            lblProgTitle_Student_UI.Text = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Name();
            lblProgramCode_Students_UI.Text = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_ID();
            lblProgrLenght_Student_UI.Text = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length().ToString();
            lblYourNameHere.Text = Logic_API.Business_layer.Student_MemeberLogged_in.Get_Name();

            cbYear.Items.Clear();
            cbYear.Text = "";
            if (Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(1) != null)
            { cbYear.Items.Add("1"); }
            if (Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(2) != null)
            { cbYear.Items.Add("2"); }
            if (Logic_API.Business_layer.Student_MemeberLogged_in.Get_Year(3) != null)
            { cbYear.Items.Add("3"); }

            if (cbYear.Items.Count < Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length())
            {
                MessageBox.Show("User has not finished enrolling yet", "Please enroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pnlOptional_Modules.Visible = true;
                btnFinalize_Year.Visible = true;

                cbYear.Items.Clear();
                for (int i = 1; i <= Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length(); i++)
                {
                    cbYear.Items.Add(i);
                }
            }
            else
            {
                // if all years enrolled onto 
                if (cbYear.Items.Count == Logic_API.Business_layer.Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length())
                {
                    string[] Average = Logic_API.Business_layer.DO_Year_Average(Logic_API.Business_layer.Student_MemeberLogged_in,1);
                    lblyearAverage.Text = Average[0] +" "+ Average[1];
                }
                else
                {
                    lblyearAverage.Text = "Undefined";
                }
                pnlOptional_Modules.Visible = false;
                btnFinalize_Year.Visible = false;
            }


        }

        private void CbModules_StudentTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxes_Students();
            // change the other cb to the same index
            cbModuleID.SelectedIndex = cbModules_StudentTab.SelectedIndex;
            cbModuleID.Text = cbModuleID.SelectedItem.ToString();
            cbUserID.Items.Clear();
            cbUserID.Text = "";
            lbEnrolledModules.Items.Clear();
            rtbStudent.Clear();

            cbUserID.Enabled = true;

            string moduleID = cbModuleID.Text;
            if (moduleID != "")
            {
                string[] Students_Enrolled = Logic_API.Data_Storage.GetStudent(moduleID);
                if (Students_Enrolled.Length != 1)
                {
                    cbUserID.Text = Students_Enrolled[1];
                    for (int i = 0; i < Students_Enrolled.Length-1; i++)
                    {                     
                        cbUserID.Items.Add(Students_Enrolled[i+ 1] );
                    }
                }
                else
                {
                    cbUserID.Text = "No Students enrolled";
                    cbUserID.Enabled = false;
                }
            }
            else
            { MessageBox.Show("Please enter a valid Module ID","No Module ID",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }

        private void CbModuleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbModules_StudentTab.SelectedIndex = cbModuleID.SelectedIndex;
            cbModules_StudentTab.Text = cbModules_StudentTab.SelectedItem.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Logic_API.Business_layer.Unenroll_all_Students_all_Modules(Logic_API.Business_layer.Staff_Member_Logged_in.Get_ModuleIDs());
            MessageBox.Show("All students unenrolled","Successs",MessageBoxButtons.OK,MessageBoxIcon.Hand);
        }

    }
}
