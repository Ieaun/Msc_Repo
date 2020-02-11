using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Administration_Design_1
{
    class Module : Description
    {
        readonly string[,] Title_of_Assesments_and_weighting;    // in the form {title1,percentage1},{title2,percetage2}  
        readonly string[] grades = new string[5];
        int Final_Grade = 0;

        public Module(string title, string module_Code, string[,] number_of_assesm)
        {
            bool Conforms = DoesIDFollowRules(module_Code, 5);
            if (Conforms)
            {
                Obj_Name = title;
                Obj_ID = module_Code;
                Title_of_Assesments_and_weighting = number_of_assesm;
                //MessageBox.Show(string.Format("New Module [{0}] Created", Obj_Name.ToString()), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //MessageBox.Show("ID does not follow rules", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetGrades(string [] Grade)
        {
            for (int i = 0; i < Grade.Length; i++)
            { grades[i] = Grade[i]; }
            Final_Grade = int.Parse(Grade[Grade.Length-1]);
        }
        public void Set_FinalGrade(int Final_mark) { Final_Grade = Final_mark; }

        public string[]  GetGrades() {return grades;}
        public string[,] Get_Title_of_Assesments_and_weighting() { return Title_of_Assesments_and_weighting; }    
        public int Get_FinalGrade() { return Final_Grade; }
    }
}
