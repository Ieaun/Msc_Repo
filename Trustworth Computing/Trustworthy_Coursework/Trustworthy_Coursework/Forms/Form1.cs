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
using System.IO;



namespace Trustworthy_Coursework
{
    public partial class Form1 : Form
    {
        string PathToExecutable = null;
        string NameofFile = null;
        int ExecutablesInFolder = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = string.Format("Trusty ACW1 [in app domain {0}]",AppDomain.CurrentDomain);
        }
/*
        private void BrowseSingle(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string path in files)
                    {
                        if (path.Contains(".exe"))
                        {
                            PathToExecutable = path;
                        }
                    }

                    if (PathToExecutable == null)
                    {
                        MessageBox.Show("Cannot Find an executable in that folder", "Check path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Write_DB_Path(PathToExecutable);
                    }
                }
            }
        }*/

        public void FindFiles(string Path)
        {
            lbExeM.Items.Clear();
            int counter = 0;
            string TypeFIle;

            if (rbExe.Checked)
            { TypeFIle = "EXE"; }
            else { TypeFIle = "DLL"; }
                       
            //string[] files = Directory.GetFiles(fbd.SelectedPath,"*."+TypeFIle, SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(@Path, "*."+TypeFIle, SearchOption.AllDirectories);
            foreach (string path in files)
            {
                counter++;
                string[] Levels = path.Split('\\');
                lbExeM.Items.Add(string.Format("[{0}] {1,-50} | {2} ", counter, Levels[Levels.Length - 1], @path));
            }

            if (lbExeM.Items.Count == 0)
            {
                MessageBox.Show(string.Format("Cannot Find any {0} in that directory",TypeFIle), "Check path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //update GUI
                lblpathM.Text = @Path;
                lblfilestoexecute.Text = counter.ToString();
                lbExeM.SelectedIndex = 0;

                //used to store total number of exe's
                ExecutablesInFolder = counter;
            }
        }


        private void BrowseMulti(object sender, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {                   
                    FindFiles(fbd.SelectedPath);
                    gbSelectFile.Enabled = true;
                }
            }
        }

        private void StartSandbox()
        {         
            Sandboxer Executable = new Sandboxer();
            Executable.Start(PathToExecutable, NameofFile, "");
        }


        private void LbExeM_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] item_Selected = lbExeM.SelectedItem.ToString().Split('|'); // name and path
            rtbDetailsM.Text = string.Format("Name: {0} \nPath: {1}", item_Selected[0], item_Selected[1]);
            lblexeorder.Text = item_Selected[0];
        }


        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            if (rtbDetailsM.Text != "")
            {
                string[] item_Selected = lbExeM.SelectedItem.ToString().Split('|');
                string[] Folders = item_Selected[1].Split('\\');
                int count = 0;
                string Path = "";
                foreach (string folder in Folders)
                {
                    if (count < Folders.Length-1)
                    {
                        Path += folder + '\\';
                        count++;
                    }
                }
                System.Diagnostics.Process.Start("explorer.exe", @Path);
            }
            else { MessageBox.Show("Error unable to open folder", ".exe not selected ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("Settings.txt");
                if (cb1Exepf.Checked)
                {
                    sw.WriteLine("1 Exe per folder:E");
                }
                else
                {
                    sw.WriteLine("1 Exe per folder:N");
                }

                if (cbPromptBExe.Checked)
                {
                    sw.WriteLine("Prompt Before execution:E");
                }
                else
                {
                    sw.WriteLine("Prompt Before execution:N");
                }                
                sw.Close();
                MessageBox.Show("Save", "Save Succesfull",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception f)
            {
                Console.WriteLine("Exception: " + f.Message);
            }
        }

        private void BtnExecution(object sender, EventArgs e)
        {
            if (lbExeM.Items.Count != 0)
            {
                string[] item_Selected = lbExeM.SelectedItem.ToString().Split('|'); // exe name and path
                if (rbWindows.Checked)
                {
                    //execute within windows
                    if (rtbDetailsM.Text != "")
                    {
                        System.Diagnostics.Process.Start("explorer.exe", @item_Selected[1]);
                    }
                    else { MessageBox.Show("Error unable to execute", ".exe not selected", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    //start sandbox
                    Thread Sand = new Thread(new ThreadStart(StartSandbox));

                    PathToExecutable = @item_Selected[1];
                    NameofFile = item_Selected[0];
                    NameofFile = NameofFile.Split(']')[1].Split('.')[0].Trim(); // "[0] FirstDll.Dll" -> "FirstDll"


                    if (PathToExecutable != null)
                    {
                        Sand.Start();
                    }
                    else
                    {
                        MessageBox.Show("No path given", "Executable path is null", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("No Item selected", "Please select an Exe / dll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lblfilestoexecute.Text = "1";
        }

        private void rbMulti_CheckedChanged(object sender, EventArgs e)
        {
            lblfilestoexecute.Text = ExecutablesInFolder.ToString();
        }

        private void rbWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSandbox.Checked)
            {
                lblusesSandbox.Text = "true";
            }
            else
            {
                lblusesSandbox.Text = "false";
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            FindFiles(@tbPathM.Text);
            gbSelectFile.Enabled = true;
        }

        private void rbExe_CheckedChanged(object sender, EventArgs e)
        {
            gbFindPathM.Enabled = true;
        }

        private void rbDll_CheckedChanged(object sender, EventArgs e)
        {
            gbFindPathM.Enabled = true;
        }
    }
}
