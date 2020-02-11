using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Student_Administration_Design_1.Layers;

namespace Student_Administration_Design_1
{
    public partial class RegisterForm : Form
    {

        Student_admin_API Logic_API = new Student_admin_API();

        // --------- These are all just help messages and initializations-------------------------------------------
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void LblWhatisthis_Regform_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Every student would have been sent a one time registration passcode \nwhen registering at the University of Hull. Please check your inbox or contact the ICT helpdesk at: ICT@hull.ac.uk");
        }

        private void LblEnrolledProgram_whatis_regform_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All courses currently offered are displayed.\nIf your course in not visable in the list then please contact the ICT helpdesk at: ICT@hull.ac.uk");
        }

        private void LblUSERID_whatis_regform_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your 6 digit user ID recieved on registration of your course in the form [543262]\nIf you require assistance please email the ICT helpdesk at: ICT@hull.ac.uk");
        }

        private void BtnClear_Register_UI_Click(object sender, EventArgs e)
        {
            tbUSerID_Regform.Clear();
            tbPassword_Regform.Clear();

        }
        // ------------------------------------------------------------------------------------

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string UserID = cbCohort.Text + tbUSerID_Regform.Text;

            //does it meet the required rules (length /unique)
            bool Meets_Rules = Logic_API.Business_layer.Meets_required_Rules("Students", "StudentID", UserID, 10);

            if (!Meets_Rules)
            {
                this.tbUSerID_Regform.Focus();
                MessageBox.Show("Does not meet required rules, UserID must be 6 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string User_Name = tbUserName_Reg.Text.ToString();
                    string User_ID = tbUSerID_Regform.Text.ToString();
                    string Password = tbPassword_Regform.Text.ToString();
                    //student only
                    string Department = cbDepartments.Text.ToString();
                    string Cohort = cbCohort.Text.ToString();
                    string Program_Enrolled = cbEnrolled_regform.Text.ToString();

                    int User_Type_no;


                    string[] UserData;
                    if (rbstaff.Checked == true)
                    {
                        User_ID += Logic_API.Business_layer.GiveMe_ARandomNumber(0, 9).ToString();
                        UserData = Logic_API.Business_layer.Try_Login(User_ID, Password); // staff memeber
                    }
                    else
                    {
                        UserData = Logic_API.Business_layer.Try_Login(Cohort + User_ID, Password); // student
                    }

                    if (UserData == null)//if null then no user with that id exists
                    {
                        if (rbstaff.Checked == true)
                        {
                            User_Type_no = 0;
                            Logic_API.Data_Storage.Create_Staff_member(User_Name, User_ID, Password);
                        }
                        else
                        {
                            User_Type_no = 1;
                            // enroll the new user onto all core modules
                            Logic_API.Business_layer.EnrollOnto_AllCoreModules(User_ID, Program_Enrolled, User_Name, Password, Department, Cohort);
                        }

                        // need these to be set in order to login 
                        UserData = new string[7];
                        UserData[4] = Program_Enrolled;
                        if (rbstaff.Checked != true) { UserData[0] = Cohort + User_ID; } // student
                        else { UserData[0] = User_ID; } //staff
                        
                        UserData[1] = User_Name;
                        UserData[6] = Cohort;
                        UserData[2] = Password;

                        MessageBox.Show(string.Format("New [{0}] created, Please note your ID [{1}] as you will need it to log in next time", User_Name, User_ID), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm Mainform = new MainForm(User_Type_no, UserData);
                        this.Hide();
                        Mainform.Show();
                    }
                    else
                    {
                        MessageBox.Show("User with that ID already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exeption)
                {
                    MessageBox.Show("Cannot create new user" + exeption, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void BtnAutofill_Reg_Click(object sender, EventArgs e)
        {
            tbUSerID_Regform.Text = Logic_API.Business_layer.GiveMe_ARandomNumber(100000, 999999).ToString();
            tbUserName_Reg.Text = "Random";
            tbPassword_Regform.Text = "Carrot";
            cbDepartments.SelectedIndex = 0;
            cbEnrolled_regform.SelectedIndex = 0;
            cbCohort.SelectedIndex = Logic_API.Business_layer.GiveMe_ARandomNumber(0,9);
            rbstaff.Checked = true;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            string[] Program_List = Logic_API.Data_Storage.Get_Program_List();

            for (int i = 0; i < Program_List.Length; i++)
            {
                cbEnrolled_regform.Items.Add(Program_List[i]);
            }
        }

        private void Rbstaff_CheckedChanged(object sender, EventArgs e)
        {
            if (rbstaff.Checked == true)
            {
                lblDepartment_reg.Visible = false;
                lblEnrolledProgram_whatis_regform.Visible = false;
                lbl_Program_Enrolled.Visible = false;
                label1.Visible = false;
                cbDepartments.Visible = false;
                cbEnrolled_regform.Visible = false;
                cbCohort.Visible = false;
            }

            if (rbStudent.Checked == true)
            {
                lblDepartment_reg.Visible = true;
                lblEnrolledProgram_whatis_regform.Visible = true;
                lbl_Program_Enrolled.Visible = true;
                label1.Visible = true;
                cbDepartments.Visible = true;
                cbEnrolled_regform.Visible = true;
                cbCohort.Visible = true;
            }
        }

        private void BtnSigninreturn_Click(object sender, EventArgs e)
        {
            LoginWindow Logger1 = new LoginWindow();
            this.Hide();
            this.Close();
            Logger1.Show();
        }
    }
}
