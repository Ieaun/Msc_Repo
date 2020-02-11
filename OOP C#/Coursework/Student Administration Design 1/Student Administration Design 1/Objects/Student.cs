using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Administration_Design_1.Layers;

namespace Student_Administration_Design_1
{
    class Student : User
    {
        public readonly Programmes Student_Program;
        //set of students registered for a degree in a given academic year (e.g 2018 or 2017)
        public readonly string Cohort;

        public Year Year1;
        public Year Year2;
        public Year Year3;      

        public Student(int User_Type, string program, string user_id, string user_name, string cohort)
        {
            Cohort = cohort;//this will be used in ID check bellow, so set it before

            bool Conforms = DoesIDFollowRules(user_id, 10);
            if (Conforms)
            {
                Obj_ID = user_id;
                Obj_Name = user_name;              

                if (User_Type == 0)
                { Type_ofUser = UserType.Staff; }
                else
                {
                    Type_ofUser = UserType.Student;
                }

                string ProgramID = Logic_API.Data_Storage.GetProgramID(program);  //one off read to get Program ID
                int Program_Length = Logic_API.Data_Storage.GetProgramLength(ProgramID); //one off read to get Program length

                Student_Program = new Programmes(program, ProgramID, Program_Length);

                //MessageBox.Show(string.Format("User [{0}] Logged in", Obj_Name), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //MessageBox.Show("ID does not follow rules", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override bool DoesIDFollowRules(string ID, int Length)
        {
            bool Conforms = false;  // if it follows rules makes this true
            if (ID.Length == Length)
            {
                string First_4_Chars = ID.Substring(0, 4); // cohort
                if (First_4_Chars == Cohort) //if the user id starts with cohort year
                {
                    Conforms = true;
                }
            }
            return Conforms;
        }

        //public string Get_Program() { return Program; }
        public string Get_Cohort() { return Cohort; }
        public Programmes Get_Student_Program() { return Student_Program; }


        // create the year objects with the module titles (soft set)
        public void Set_Year(string[] Modules, int Year)
        {
            switch (Year)
            {
                case 1:
                    Year1 = new Year(Modules);
                    break;
                case 2:
                    Year2 = new Year(Modules);
                    break;
                case 3:
                    Year3 = new Year(Modules);
                    break;
                default:
                    break;
            }
        }

        // return year object
        public Year Get_Year(int Year)
        {
            switch (Year)
            {
                case 1:
                    return Year1;
                case 2:
                    return Year2;
                case 3:
                    return Year3;
                default:
                    return null;
            }
        }
    }
}
