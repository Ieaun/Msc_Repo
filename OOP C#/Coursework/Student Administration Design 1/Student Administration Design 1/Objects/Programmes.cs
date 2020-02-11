using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Administration_Design_1.Layers;

namespace Student_Administration_Design_1
{
    class Programmes : Description
    {
        readonly int Program_Length;
        // Year 1 =  Core_Modules[0]  // Year 2 =  Core_Modules[1] // Year 3 =  Core_Modules[2]
        readonly string[] Core_Modules;   
        readonly string[] Optional_Modules;

        public Programmes(string P_Title, string P_ID, int program_Length)
        {
            bool Conforms = DoesIDFollowRules(P_ID, 6);
            if (Conforms)
            {
                Obj_Name = P_Title;
                Obj_ID = P_ID;
                Program_Length = program_Length;

                string [] Program_Values =Logic_API.Data_Storage.Get_Entire_Program(Obj_ID);

                Core_Modules = Get_ALL_Modules("Core", Program_Values);
                Optional_Modules = Get_ALL_Modules("Optional", Program_Values);
            }
            else
            {
                //MessageBox.Show("ID does not follow rules", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Get Methods
        public int Get_Program_Length()     { return Program_Length; }
        public string[] Get_All_C_Modules() { return Core_Modules; }
        public string[] Get_All_O_Modules() { return Optional_Modules; }


        // returns all the {" "} modules of a program, Module_Kind = "Core" or "Optional"
        private string[] Get_ALL_Modules(string Module_Kind, string[] Program_Value)
        {
            string[] All_Modules = new string[3];
            string Year1_Core_modules_String = "";
            string Year2_Core_modules_String = "";
            string Year3_Core_modules_String = "";


            // Core_Count + position of optional module split = Optional module position
            int Core_Count = 0;

            if (Module_Kind != "Core" && Module_Kind != "Optional")
            {
                //Module_Kind not correct
            }
            else
            {
                int Core_Optional;
                if (Module_Kind == "Core")
                {
                    Core_Optional = 0;
                }
                else
                {
                    Core_Optional = 1;

                    //get the amount of core modules so we know where the optional modules begin                    
                    string[] Temp = Program_Value[3].Split('(');
                    Core_Count = int.Parse(Temp[0].Trim());
                }


                //Get Split

                string[] Work_Out_Years = Program_Value[3 + Core_Optional].Replace(")", "").Split('('); //splits into 2, throw away first part
                Work_Out_Years = Work_Out_Years[1].Split(',');
                int Length = Work_Out_Years.Length - 1;

                // Split contains the amount of modules in a year and translates to database coloumn positions,
                //e.g 4 means Modules1 -> thereafter 8 means modules 4 +1 ->8             
                
                int Year1_Split = int.Parse(Work_Out_Years[1]);
                int Year2_Split = 0;
                int Year3_Split = 0;
                // adds total number of core modules to split to address coloumns correctly
                int Optional_Adder = 0;
                Year2_Core_modules_String = "";             
                Year3_Core_modules_String = "";


                int Start_AtThis_Point = 1;
                if (Core_Optional == 1) // optional
                {
                    Start_AtThis_Point += Core_Count;
                    Year1_Split += Core_Count;
                    Optional_Adder = Core_Count;
                }

                string[] Years_Modules = new string [Year1_Split - Core_Count];
                for (int i = Start_AtThis_Point;i<= Year1_Split;i++)
                {
                    Years_Modules[i- Start_AtThis_Point] = Program_Value[4+i];
                }

                //incrementally search values in modules columns in program table where Program ID = Program ID
                //string[] Years_Modules = Logic_API.Increment_Search("Module_", Start_AtThis_Point, Year1_Split, "Programs", "ProgramID", Obj_ID);
                // turn 2d array into string
                Year1_Core_modules_String = Logic_API.Business_layer.Make_Array_Single_String(Years_Modules, 0);

                if (Length >= 2)
                {
                    Year2_Split = int.Parse(Work_Out_Years[2]) + Optional_Adder;
                    Years_Modules = new string[Year2_Split - Year1_Split];
                    for (int i = Year1_Split + 1; i <= Year2_Split; i++)
                    {
                        Years_Modules[i - (Year1_Split + 1)] = Program_Value[4 + i];
                    }

                    Year2_Core_modules_String = Logic_API.Business_layer.Make_Array_Single_String(Years_Modules, 0);
                }

                if (Length >= 3)
                {
                    Year3_Split = int.Parse(Work_Out_Years[3]) + Optional_Adder;

                    Years_Modules = new string[Year3_Split- Year2_Split];
                    for (int i = Year2_Split + 1; i <= Year3_Split; i++)
                    {
                        Years_Modules[i - (Year2_Split + 1)] = Program_Value[4 + i];
                    }

                    Year3_Core_modules_String = Logic_API.Business_layer.Make_Array_Single_String(Years_Modules, 0);
                }
            }

            All_Modules[0] = Year1_Core_modules_String;
            All_Modules[1] = Year2_Core_modules_String;
            All_Modules[2] = Year3_Core_modules_String;


            return All_Modules;
        }

    }
}
