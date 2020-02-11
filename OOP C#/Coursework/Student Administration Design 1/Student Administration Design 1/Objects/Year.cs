using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Administration_Design_1
{
    class Year
    {
        // Module titles are saved before the module obj is created (Module1 can exist without Module1_obj)
        readonly string Module1;
        //Module OBJ contains module assesment information (Module1_obj cannot exist without Module1 )
        Module Module1_obj;
        readonly string Module2;
        Module Module2_obj;
        readonly string Module3;
        Module Module3_obj;
        readonly string Module4;
        Module Module4_obj;
        readonly string Module5;
        Module Module5_obj;
        readonly string Module6;
        Module Module6_obj;

        // if the user has already enrolled and finalized these 6 modules then isYearSet = true
        readonly bool Has_Student_ENrolled = false;

        // if we have not recived all data (e.g grades, assignment titles) year is not fully set 
        bool isYearFullySet = false;

        public Year(string [] Modules)
        {
            Module1 = Modules[0];
            Module2 = Modules[1];
            Module3 = Modules[2];
            Module4 = Modules[3];
            Module5 = Modules[4];
            Module6 = Modules[5];
            Has_Student_ENrolled = true;
        }

        public bool Get_yearFully_Set()
        {
            return isYearFullySet;
        }

        //returns a single module 
        public Module Get_Module(int Module_Number)
        {
            switch (Module_Number)
            {
                case 1:
                    return Module1_obj;
                case 2:
                    return Module2_obj;  
                case 3:
                    return Module3_obj;
                case 4:
                    return Module4_obj;
                case 5:
                    return Module5_obj;
                case 6:
                    return Module6_obj;
                default:
                    return Module1_obj;
            }
        }

        /// <summary>
        /// Create the module obj with all assesment data 
        /// </summary>
        public void Set_Module(string ModuleTitle,string Module_Code ,string [,] Title_of_Assesments_and_weighting, string [] Grades)
        {
            if (Has_Student_ENrolled)
            {                
                if (ModuleTitle == Module1)
                {
                    Module1_obj = new Module(ModuleTitle, Module_Code, Title_of_Assesments_and_weighting);
                    Module1_obj.SetGrades(Grades);
                    return;
                }

                if (ModuleTitle == Module2)
                {
                    Module2_obj = new Module(ModuleTitle, Module_Code, Title_of_Assesments_and_weighting);
                    Module2_obj.SetGrades(Grades);
                    return;
                }

                if (ModuleTitle == Module3)
                {
                    Module3_obj = new Module(ModuleTitle, Module_Code, Title_of_Assesments_and_weighting);
                    Module3_obj.SetGrades(Grades);
                    return;
                }

                if (ModuleTitle == Module4)
                {
                    Module4_obj = new Module(ModuleTitle, Module_Code, Title_of_Assesments_and_weighting);
                    Module4_obj.SetGrades(Grades);
                    return;
                }

                if (ModuleTitle == Module5)
                {
                    Module5_obj = new Module(ModuleTitle, Module_Code, Title_of_Assesments_and_weighting);
                    Module5_obj.SetGrades(Grades);
                    return;
                }

                if (ModuleTitle == Module6)
                {
                    Module6_obj = new Module(ModuleTitle, Module_Code, Title_of_Assesments_and_weighting);
                    Module6_obj.SetGrades(Grades);
                    isYearFullySet = true; // once this method gets called once its assumed it will be called 6 times to set all modules
                    return;
                }
            }
        }

        public bool Get_isYear_Set() { return Has_Student_ENrolled; }
        public string[] Get_Modules()
        {
            string[] Modules = new string[6];
            Modules[0] = Module1;
            Modules[1] = Module2;
            Modules[2] = Module3;
            Modules[3] = Module4;
            Modules[4] = Module5;
            Modules[5] = Module6;

            return Modules;
        }

        

        public int Get_Average(string StudentID)
        {
            int average = 0;

            // get all the final grades for the modules
            float[] Results = new float[6];
            Results[0] = Module1_obj.Get_FinalGrade();
            Results[1] = Module2_obj.Get_FinalGrade();
            Results[2] = Module3_obj.Get_FinalGrade();
            Results[3] = Module4_obj.Get_FinalGrade();
            Results[4] = Module5_obj.Get_FinalGrade();
            Results[5] = Module6_obj.Get_FinalGrade();

            for (int i = 0; i < 6; i++)
            {
                if (Results[i] != 0)
                {
                    average += (int)(Results[i] / 6);// the average mark for the module
                }
                else
                {
                    // if 1 module has an average mark of 0 then entire program is undefined 
                    average = 0;
                    i = 8;
                }
            }
            return average;
        }
    }
}
