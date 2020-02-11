using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Trustworthy_Coursework
{
    public partial class Method_List_Form : Form
    {
        object [,] Methods_recieved = null;
        object[] Assembly_classes;
        MethodInfo[][] Method_Info;
        object Result_of_execution_Object;
        int selectedAssmebly_Index = 0;


        public Method_List_Form(object [,] Given_methods, MethodInfo[][] metInfo)
        {         
            InitializeComponent();
            this.Text = string.Format("Methods [in app domain {0}]", AppDomain.CurrentDomain);
            initMethodsForm();

            Assembly_classes = new object[Given_methods.Length / 2];
            Method_Info = metInfo;
            Methods_recieved = Given_methods;
            for (int i = 0; i< Given_methods.Length/2; i++)
            {
                Assembly_classes[i] = Given_methods[i, 1];
                cbAssem.Items.Add(Assembly_classes[i]);
            }

            
            cbAssem.SelectedIndex = 0;

            PopulateLB();
        }

        /// <summary>
        /// initialize all ui componenets
        /// </summary>
        public void initMethodsForm()
        {
            DisableAllParams();
            lbMethods.Items.Clear();
            lblselected.Text = "Null";
        }

        /// <summary>
        /// disable all parameter textboxes and clear their text
        /// </summary>
        public void DisableAllParams()
        {
            tbparam1.Enabled = false;
            tbparam2.Enabled = false;
            tbparam3.Enabled = false;
            tbparam4.Enabled = false;
            tbparam5.Enabled = false;
            tbparam1.Text = "";
            tbparam2.Text = "";
            tbparam3.Text = "";
            tbparam4.Text = "";
            tbparam5.Text = "";
        }

        /// <summary>
        /// Update ui to refelect selected method, enable paramerters tb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbAssem.SelectedIndex;

            DisableAllParams();
            string method = lbMethods.SelectedItem.ToString();
            lblselected.Text = method;

            int number_of_params = 0;
            string[] paramList = new string[5];
            int counter = 0;


            for (int i = 0; i < int.Parse(lblCounter.Text); i++)
            {
                MethodInfo mi = Method_Info[selectedAssmebly_Index][i];
                if (mi.ToString() == method)
                {
                    number_of_params = mi.GetParameters().Length;
                    foreach (ParameterInfo p in mi.GetParameters())
                    {
                        paramList[counter] = p.ToString();
                        counter++;
                    }
                }
            }


            lblparamcount.Text = number_of_params.ToString();

            switch (number_of_params)
            {
                case 1:
                    tbparam1.Enabled = true;
                    tbparam1.Text = paramList[0];
                    break;
                case 2:
                    tbparam1.Enabled = true;
                    tbparam2.Enabled = true;
                    tbparam1.Text = paramList[0];
                    tbparam2.Text = paramList[1];
                    break;
                case 3:
                    tbparam1.Enabled = true;
                    tbparam2.Enabled = true;
                    tbparam3.Enabled = true;
                    tbparam1.Text = paramList[0];
                    tbparam2.Text = paramList[1];
                    tbparam3.Text = paramList[2];
                    break;
                case 4:
                    tbparam1.Enabled = true;
                    tbparam2.Enabled = true;
                    tbparam3.Enabled = true;
                    tbparam4.Enabled = true;
                    tbparam1.Text = paramList[0];
                    tbparam2.Text = paramList[1];
                    tbparam3.Text = paramList[2];
                    tbparam4.Text = paramList[3];
                    break;
                case 5:                      
                    tbparam1.Enabled = true;
                    tbparam2.Enabled = true;
                    tbparam3.Enabled = true;
                    tbparam4.Enabled = true;
                    tbparam5.Enabled = true;
                    tbparam1.Text = paramList[0];
                    tbparam2.Text = paramList[1];
                    tbparam3.Text = paramList[2];
                    tbparam4.Text = paramList[3];
                    tbparam5.Text = paramList[4];
                    break;
                default:
                    DisableAllParams();
                    break;
            }            
        }

        /// <summary>
        /// populate the list box with the given methods
        /// </summary>
        public void PopulateLB()
        {
            lbMethods.Items.Clear();
            int index = cbAssem.SelectedIndex;
            lblCounter.Text = Method_Info[index].Length.ToString();

            for (int i = 0; i < int.Parse(lblCounter.Text); i++)
            {
                string Method = Method_Info[selectedAssmebly_Index][i].ToString();
                lbMethods.Items.Add(Method);
            }           
        }


        private void tbparam1_Enter(object sender, EventArgs e)
        {
            tbparam1.Text = "";
        }

        private void tbparam2_Enter(object sender, EventArgs e)
        {
            tbparam2.Text = "";
        }

        private void tbparam3_Enter(object sender, EventArgs e)
        {
            tbparam3.Text = "";
        }

        private void tbparam4_Enter(object sender, EventArgs e)
        {
            tbparam4.Text = "";
        }

        private void tbparam5_Enter(object sender, EventArgs e)
        {
            tbparam5.Text = "";
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (Method_Info != null)
            {
                int index = cbAssem.SelectedIndex;
                string method = lbMethods.SelectedItem.ToString();
                
                //invoke the method
                Invokemethod_please(Method_Info[selectedAssmebly_Index][index]);                                            
            }
            else
            {
                MessageBox.Show("No Methods to display", "No methods in Methods_recieved array", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        private void Invokemethod_please(MethodInfo m)
        {
            dynamic [] parameters = new dynamic[int.Parse(lblparamcount.Text)];
            ParameterInfo[] par_info = m.GetParameters();
            var here = par_info[0].Name;
            Type m_type1;
            Type m_type2;
            Type m_type3;
            Type m_type4;
            Type m_type5;
           
            //switch on the count of params           
            switch (int.Parse(lblparamcount.Text))
            {
                case 0:
                    var retVal0 = m.Invoke(null, parameters);
                    break;
                case 1:
                    m_type1 = par_info[0].ParameterType;
                    parameters[0] = Convert.ChangeType((object)tbparam1.Text, m_type1);
                    break;
                case 2:
                    m_type1 = par_info[0].ParameterType;
                    m_type2 = par_info[1].ParameterType;
                    parameters[0] = Convert.ChangeType((object)tbparam1.Text, m_type1);
                    parameters[1] = Convert.ChangeType((object)tbparam2.Text, m_type2);                     
                    break;
                case 3:                    
                    m_type1 = par_info[0].ParameterType;
                    m_type2 = par_info[1].ParameterType;
                    m_type3 = par_info[2].ParameterType;
                    parameters[0] = Convert.ChangeType((object)tbparam1.Text, m_type1);
                    parameters[1] = Convert.ChangeType((object)tbparam2.Text, m_type2);
                    parameters[2] = Convert.ChangeType((object)tbparam3.Text, m_type3);
                    break;
                case 4:
                    m_type1 = par_info[0].ParameterType;
                    m_type2 = par_info[1].ParameterType;
                    m_type3 = par_info[2].ParameterType;
                    m_type4 = par_info[3].ParameterType;
                    parameters[0] = Convert.ChangeType((object)tbparam1.Text, m_type1);
                    parameters[1] = Convert.ChangeType((object)tbparam2.Text, m_type2);
                    parameters[2] = Convert.ChangeType((object)tbparam3.Text, m_type3);
                    parameters[3] = Convert.ChangeType((object)tbparam4.Text, m_type4);
                    break;
                case 5:
                    m_type1 = par_info[0].ParameterType;
                    m_type2 = par_info[1].ParameterType;
                    m_type3 = par_info[2].ParameterType;
                    m_type4 = par_info[3].ParameterType;
                    m_type5 = par_info[4].ParameterType;
                    parameters[0] = Convert.ChangeType((object)tbparam1.Text, m_type1);
                    parameters[1] = Convert.ChangeType((object)tbparam2.Text, m_type2);
                    parameters[2] = Convert.ChangeType((object)tbparam3.Text, m_type3);
                    parameters[3] = Convert.ChangeType((object)tbparam4.Text, m_type4);
                    parameters[4] = Convert.ChangeType((object)tbparam5.Text, m_type5);                   
                    break;
                default:
                    break;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var resultys = m.Invoke(null, parameters);
            sw.Stop();
            Result_of_execution_Object = resultys;
            rtbResults.Text = string.Format("Result :{0} \nType :{1} \nExecution Time :{2} seconds", resultys.ToString(), resultys.GetType(), TimeSpan.FromTicks(sw.ElapsedTicks).TotalSeconds ) ;
            string her22e = "";

        }

        private void cbAssem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAssmebly_Index = cbAssem.SelectedIndex;
            PopulateLB();
        }
    }
}

