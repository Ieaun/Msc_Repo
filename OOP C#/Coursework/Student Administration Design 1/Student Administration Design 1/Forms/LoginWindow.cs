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
using System.IO;
using Student_Administration_Design_1.Layers;

namespace Student_Administration_Design_1
{
    public partial class LoginWindow : Form
    {
        // API 
        Student_admin_API Logic_API = new Student_admin_API();

        public LoginWindow()
        {
            InitializeComponent();
        }


        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                string UserID = tbUSer_ID.Text.ToString();
                string Password = tb_Password.Text.ToString();

                // try to login, if array = null it was unsucceful
                //[] UserData contains all the data about a user if succesful
                string[] UserData = Logic_API.Business_layer.Try_Login(UserID, Password);

                if (UserData != null) // does not exist
                {
                    if (UserData.Length > 5)
                    {
                        string User_Name = UserData[1];
                        string UserType = UserData[3];

                        MessageBox.Show(string.Format("User [{0}] Logged in", User_Name), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (UserType == "Student")
                        {
                            MainForm MainForm1 = new MainForm(1, UserData);
                            this.Hide();
                            MainForm1.Show();
                        }
                        else
                        {
                            // login as staff
                            MainForm MainForm1 = new MainForm(0, UserData);
                            this.Hide();
                            MainForm1.Show();

                        }
                    }
                    else { lblNo_Match.Text = "User ID and Password do not match"; }
                }
                else
                {
                    lblNo_Match.Text = "User doesnt exist";
                }
            }
            catch
            { MessageBox.Show("Cannot Find Database", "Check path", MessageBoxButtons.OK); }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //this.Close();
            RegisterForm Regform = new RegisterForm();
            this.Hide();
            Regform.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tbUSer_ID.Text = "5455633";
            tb_Password.Text = "Carrot";
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            lblNo_Match.Text = "";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tbUSer_ID.Text = "2015454745";
            tb_Password.Text = "Carrot";
        }

        private void BtnFIndDatabase_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                string database_Path = null;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string path in files)
                    {
                        if (path.Contains("Students3.mdb"))
                        {
                            database_Path = path;
                        }
                    }

                    if (database_Path==null)
                    {
                        MessageBox.Show("Cannot Find Database in that folder", "Check path", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {
                        Write_DB_Path(database_Path);
                    }              
                }
            }
        }

        private void Write_DB_Path(string Path)
        {
            File.WriteAllText("DataBase_Path", Path);
            Logic_API.Data_Storage.SetDatastoragePath(Path);
        }

        private void Read_DB_Path(string Path)
        {
            string readText = File.ReadAllText("DataBase_Path");
        }

    }
}
