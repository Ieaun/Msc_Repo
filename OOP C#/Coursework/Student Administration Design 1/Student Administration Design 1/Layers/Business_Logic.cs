using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Student_Administration_Design_1.Layers;


namespace Student_Administration_Design_1
{
    class Business_Logic : IBusiness
    {
        DataStorage_Class Data_Storage = null;

        public Business_Logic(DataStorage_Class data_Storage)
        {
            Data_Storage = data_Storage;
        }

        // Contains all data about the user currently logged in 
        public Staff Staff_Member_Logged_in;
        public Student Student_MemeberLogged_in;

        public Staff staff_Member_Logged_in
        {
            get
            {
                return Staff_Member_Logged_in;
            }
            set
            {
                Staff_Member_Logged_in = value;
            }
        }

        public Student student_MemeberLogged_in
        {
            get
            {
                return Student_MemeberLogged_in;
            }
            set
            {
                Student_MemeberLogged_in = value;
            }
        }


        //-----------------------------Data storage accessers-------------------------------------------------           
        /// <summary>
        /// Unenroll all students from all modules in database
        /// </summary>
        public void Unenroll_all_Students_all_Modules(string[] ModuleIDs)
        {
            for (int i = 0; i < ModuleIDs.Length; i++)
            {
                Data_Storage.Unenroll_all_Students(ModuleIDs[i]);
            }
        }

        /// <summary>
        /// update the module arrays within staff class
        /// </summary>
        public Staff SetModuleData_staff_account()
        {
            // if we have just logged in or new modules has been added so need to update
            if (Staff_Member_Logged_in.Get_Update_Modules())
            {
                string[] ModuleTitles = Data_Storage.Get_all_modulesTitles();
                string[] ModuleID = Data_Storage.Get_all_moduleIDs();

                // set new data to staff class
                Staff_Member_Logged_in.Set_Modules(ModuleID, ModuleTitles);

                //when true, tells class to update the module table
                Staff_Member_Logged_in.Set_Update_Modules(false);
            }
            return Staff_Member_Logged_in;
        }


        // create the student class with basic data (still needs module assesment data)
        public Student Create_Student(string StudentID, string[] Student_Data = null)
        {
            Student Selected_Student = null;

            // if this was not supplied go and get it 
            // if it was supplied but the userhas only just regestered
            if (Student_Data == null || Student_Data.Length < 10)
            { Student_Data = Data_Storage.Get_Student_Data(StudentID); }


            if (Student_Data != null)
            {
                string Program_Enrolled = null;
                string UID = null;
                string UName = null;
                string Cohort = null;


                if (Student_Data != null)
                {
                    Program_Enrolled = Student_Data[4];
                    UID = Student_Data[0];
                    UName = Student_Data[1];
                    Cohort = Student_Data[6];
                    Selected_Student = new Student(1, Program_Enrolled, UID, UName, Cohort);
                }
                int indexer = 8;
                int ProgramLength = Selected_Student.Get_Student_Program().Get_Program_Length();
                bool UserEnrolled = true; // false if user has less than 6 modules
                int Location_of_Grades = 12;
                for (int i = 0; i < ProgramLength && UserEnrolled; i++)//for each year
                {
                    int Year = i + 1;

                    string[] Modules = Remove_Commans(Student_Data[indexer]);

                    if (Modules.Length == 6)
                    {
                        Selected_Student.Set_Year(Modules, Year);

                        for (int ModuleNumber = 0; ModuleNumber < 6; ModuleNumber++)//for each module
                        {
                            string ModulesID = Data_Storage.Get_Module_ID(Modules[ModuleNumber]);
                            string[,] Titles_weights = Get_titles_weights(Modules[ModuleNumber]);
                            string[] Grades = Remove_Commans(Student_Data[Location_of_Grades].Replace("]", "").Replace("[", ""));
                            Location_of_Grades = Location_of_Grades + 2;

                            if (Grades[0] == "")
                            {
                                Grades = new string[5] { "0", "0", "0", "0", "0" };
                            }

                            CreateYear(Selected_Student, ModuleNumber, Year, Modules, ModulesID, Titles_weights, Grades);
                        }
                    }
                    else
                    { UserEnrolled = false; }
                    indexer++;
                }
            }
            return Selected_Student;
        }


        private string[,] Get_titles_weights(string ModuleTitle)
        {
            string ModuleID = Data_Storage.Get_Module_ID(ModuleTitle);
            string[] Module_Assesment_Data = Get_Assesment_Data(ModuleID);//module id
            string[] Titles = Remove_Commans(Module_Assesment_Data[0]);
            string[] Weights = Remove_Commans(Module_Assesment_Data[1]);
            string[,] Titles_weights = Format_title_weights(Titles, Weights);
            return Titles_weights;
        }

        // format titles and weights togehter
        private string[,] Format_title_weights(string[] Titles, string[] weights)
        {
            string[,] Titles_weights = new string[Titles.Length, 2];
            for (int i = 0; i < Titles.Length; i++)
            {
                Titles_weights[i, 0] = Titles[i];
                Titles_weights[i, 1] = weights[i];
            }

            return Titles_weights;
        }

        private void CreateYear(Student Student_MemeberLogged_in, int module_no, int Year, string[] Years_Modules, string ModuleID, string[,] Titles_weights, string[] Grades)
        {
            string[] Module_Data = Get_Assesment_Data(ModuleID);
            switch (Year)
            {
                case 1:
                    Student_MemeberLogged_in.Year1.Set_Module(Years_Modules[module_no], ModuleID, Titles_weights, Grades);
                    break;
                case 2:
                    Student_MemeberLogged_in.Year2.Set_Module(Years_Modules[module_no], ModuleID, Titles_weights, Grades);
                    break;
                case 3:
                    Student_MemeberLogged_in.Year3.Set_Module(Years_Modules[module_no], ModuleID, Titles_weights, Grades);
                    break;
                default:
                    break;
            }
        }


        // turn {1,2,3,4} into {1},{2},{3}
        private string[] Remove_Commans(string Value)
        {
            string[] Values = null;
            try
            {
                Values = Value.Split(',');
            }
            catch { }
            return Values;
        }

        //determine if password for the user is correct 
        public bool Is_Password_correct(string UserID, string Entered_Password)
        {
            bool is_Password_correct = false;
            string RealPassword = Data_Storage.Get_password(UserID);
            if (RealPassword == Entered_Password)
            { is_Password_correct = true; }
            return is_Password_correct;
        }



        // attemt to find a user with the given id, after user found check if password matches
        public string[] Try_Login(string UserID, string Password)
        {
            //check exists, if so get all data needed to login (cohort, program, name)
            string[] USer_exists = Data_Storage.FieldExists("Students", "StudentID", UserID);

            // contains either false or the users password
            if (USer_exists != null)
            {
                string real_Password = USer_exists[2];
                //check if password correct
                if (real_Password != Password)
                { USer_exists = new string[1] { "Passwords doesnt match" }; }
            }

            return USer_exists;
        }


        // get the average for the year
        public string[] DO_Year_Average(Student Student_MemeberLogged_in, int Year__)
        {
            // get average
            int average = 0;
            for (int i = 1; i <= Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length(); i++)
            {
                Year Current_Year___ = Student_MemeberLogged_in.Get_Year(i);
                int Year_Average = Current_Year___.Get_Average(Student_MemeberLogged_in.Get_ID().ToString());
                if (Year_Average != 0)
                {
                    average += Year_Average;
                }
                else
                {
                    // if 1 year has an average mark of 0 then entire program is undefined 
                    average = 0;
                    i = 10;
                }
            }
            average = (int) (((float)average) / ((float)Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length()));

            string classification = DetermineClassifiation(average, 50, 70);

            string[] Values = new string[2] { average.ToString(), classification };
            return Values;
        }


        //---------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a random number after being given a minimum and maximum range
        /// </summary>
        public int GiveMe_ARandomNumber(int MinRange, int MaxRange)
        {
            Random randomNum = new Random();
            int result = randomNum.Next(MinRange, MaxRange);
            return result;
        }  

        // make sure we have 6 modules and they are split corretly (cant have 6 core modules and also optional modules)
        public bool Check_ModuleList_follows_rules(int Optional, int Core)
        {
            bool acceptable = false; // assume false

            //cant have more than 5 core modules and optionals unless core = 6 and optionals = 0
            if (Core <= 5 && Optional >= 1)
            {
                acceptable = true;
            }
            else
            {
                if (Core == 6 && Optional == 0)
                {
                    acceptable = true;
                }
            }

            // no more than 10 modules (to chose from) in a year 
            if (Core + Optional > 10)
            {
                acceptable = false;
            }

            return acceptable;
        }


        /// take an array and turn that array into a string "element1,element2..."
        public string Make_Array_Single_String(string[] Array, int startFrom)
        {
            string This_String = "";

            for (int i = startFrom; i < Array.Length; i++)
            {
                //check if its an actual value
                if (Array[i] != null && Array[i] != "")
                {
                    This_String += Array[i];
                    // if not at the end of the array add a comma
                    if (i < Array.Length - 1)
                    { This_String += ","; }
                }
            }

            if (This_String != "")
            {
                if (This_String[This_String.Length - 1] == ',')
                {
                    // return the string but take out that last ","
                    return This_String.TrimEnd(This_String[This_String.Length - 1]);
                }
                else { return This_String; }
            }
            else { return This_String; }

        }



        // gets the assesment information of a module 
        // returns assesment titles, weighting and whether or not another module has a
        //linked assignment 
        // *does not return student data only general module data
        public string[] Get_Assesment_Data(string Module_ID)
        {

            string[] Module_Data = Data_Storage.Get_all_Module_data(Module_ID);


            // assesment titles and weights
            string[] Assesment_Titles = new string[4] { Module_Data[3], Module_Data[5], Module_Data[7], Module_Data[9] };
            string[] Assesment_Weighting = new string[4] { Module_Data[4], Module_Data[6], Module_Data[8], Module_Data[10] };
            string[] Assesment_Data = new string[3];

            string Titles = Make_Array_Single_String(Assesment_Titles, 0);
            string Weight = Make_Array_Single_String(Assesment_Weighting, 0);

            string Linked = Module_Data[12];

            Assesment_Data[0] = Titles;
            Assesment_Data[1] = Weight;
            Assesment_Data[2] = Linked;

            return Assesment_Data;
        }



        /// get all the grades for a selected student from the datastorage
        public string[] Get_Student_Module_Grades(int ModuleID, int StudentID)
        {
            string[] All_Data = new string[6];
            string Module_Title = Data_Storage.Get_Module_Title(ModuleID);

            string Resultant_Module_Name = "";
            int set = 0; // if this is 0 then no module was found

            // get students personal assesment data ("Their mark/grade")
            // cycle through table until we find our module 
            for (int i = 1; i < 19; i++)
            {                            
                try
                {
                    Resultant_Module_Name = Data_Storage.Get_Students_module(i, StudentID);
                    if (Resultant_Module_Name == Module_Title)
                    {
                        // Number tb (Used later when we save it back to the database so we dont have to loop again)
                        string UserModule_Number = i.ToString();
                        All_Data[0] = UserModule_Number;

                        //Once we have found our module, we have found the grade column besides it so throw data onto gui                        
                        string Grades = Data_Storage.Get_Students_Grade(i, StudentID);

                        // Grades are in form [0,0,0,0]
                        string[] Grade_A_Array = Grades.Replace('[', ' ').Replace(']', ' ').Trim().Split(',');

                        //grade data
                        All_Data[1] = Grade_A_Array[0];
                        All_Data[2] = Grade_A_Array[1];
                        All_Data[3] = Grade_A_Array[2];
                        All_Data[4] = Grade_A_Array[3];
                        All_Data[5] = Grade_A_Array[4];
                        set = 1;

                        // terminate loop
                        i = 20;
                    }
                }
                catch
                {
                    //nothing inside Result[0]
                    // no students enrolled
                }
            }

            if (set == 0)
            {
                //if true then either the student doesnt exist(likely) or the module isnt saved on the students profile (not likely)
                All_Data[0] = "0";
                All_Data[1] = "0";
                All_Data[2] = "0";
                All_Data[3] = "0";
                All_Data[4] = "0";
                All_Data[5] = "0";
            }
            return All_Data;
        }

        /// <summary>
        /// find the first empty column then write the module names in starting from that column
        /// </summary>           
        public bool Write_from_Empty_column(int Year, string User_ID, string[] Module_List, Student Student_MemeberLogged_in, string[] User_Data)
        {
            int Position_Of_EMpty_Column = 0;
            int Year_Compensation = 0;
            bool Finished = false;
            string Password = User_Data[2];
            if (User_Data.Length < 10) // not full data, student just registered
            {
                //[] UserData contains all the data about a user if succesful
                User_Data = Try_Login(User_ID, Password);
            }

            int Indexer = 11; //first module is at position User_Data[11]
            int Start = 0;
            int End = 0;

            // find the empty field by probing positions e.g for year 1 1-6 (Modules 1 to 6)
            switch (Year)
            {
                case 1:
                    Start = 0;
                    End = 11;
                    break;
                case 2:
                    Start = 12;
                    End = 24;
                    Year_Compensation = 6;
                    break;
                case 3:
                    Start = 24;
                    End = 35;
                    Year_Compensation = 12;
                    break;
                default:
                    Finished = true;
                    break;
            }

            int ModuleList_Index = 0;
            int ModuleList_Indexer = 0;
            // find the first empty coloumn to write to 
            for (int i = Start; i <= End; i++)
            {
                if (i >= End)
                {
                    Finished = true;
                }

                if (User_Data[(Indexer + i)] == "")
                {
                    i = End + 1; // break got empty pos                   
                }

                if (ModuleList_Index == 0)
                {
                    ModuleList_Indexer++;
                    ModuleList_Index++;
                }
                else
                {
                    ModuleList_Index = 0;
                }
            }

            if (Finished == false)
            {
                Position_Of_EMpty_Column = ModuleList_Indexer + Year_Compensation;
                Data_Storage.Enroll_onto_module(User_ID, Module_List[ModuleList_Indexer - 1], Position_Of_EMpty_Column, Year_Compensation);
            }
            return Finished;
        }

        /// <summary>
        /// //UPDATE Year1/Year2/Year3 Student DATABASE column
        /// </summary>
        public void EnrollStudent_ontoModules(string[] Module_List, int Year, string User_ID)
        {
            string Module_string = Make_Array_Single_String(Module_List, 0);
            Data_Storage.EnrollStudent_ontoModules(Module_string, Year, User_ID);
        }



        /// <summary>
        /// compares this program to other programs and calculates a simliarity score, returns the similarity score and program it is most similar to
        /// </summary>
        public string[] Is_Program_Unique(string[] Module_List)
        {
            string Highest_SImilarity_Program = "";

            int Number_of_Modules = Module_List.Length;
            int Number_of_identicle_Modules = 0;
            float similarity = 0;

            // get all program IDs
            string[] ProgramIDs = Data_Storage.Get_ProgramID_List();

            // compare each program already created to this new program
            for (int i = 0; i < ProgramIDs.Length; i++)
            {

                // get row that has this ID
                string[] Data = Data_Storage.Get_Entire_Program(ProgramIDs[i]);
                string Name_of_other_Program = Data[1];
                Data = Data.Skip(5).ToArray();
                Data = Data.Where(x => !string.IsNullOrEmpty(x)).ToArray();   // remove all empty elements

                // if they both have e.g 10 modules then carry on. 
                // if not then they cant "have the same set modules" so they dont match then search next
                if (Data.Length == Module_List.Length)
                {

                    for (int j = 0; j < Number_of_Modules; j++)
                    {
                        string Module = Module_List[j].Trim();
                        for (int k = 0; k < Number_of_Modules; k++)
                        {
                            string Table_Module = Data[k].Trim();
                            if (Table_Module == Module)
                            {
                                Number_of_identicle_Modules++;
                            }

                        }
                    }

                    // if true then all modules match and programs are identicle 
                    if (Number_of_identicle_Modules == Number_of_Modules)
                    {
                        Highest_SImilarity_Program = Name_of_other_Program;
                        similarity = 100;
                        i = 99; //force the break out of higher loop
                    }
                    else
                    {
                        float Temp = (((float)Number_of_identicle_Modules / ((float)Module_List.Length + Data.Length)) * 100); // get similarity 
                        if (Temp > similarity) // set new highest similarity 
                        {
                            Highest_SImilarity_Program = Name_of_other_Program;
                            similarity = Temp;
                        }
                    }
                }
            }
            string[] SMiliarity_Name_of_SImilar = new string[2];
            SMiliarity_Name_of_SImilar[0] = similarity.ToString();
            SMiliarity_Name_of_SImilar[1] = Highest_SImilarity_Program;

            return SMiliarity_Name_of_SImilar;
        }


        /// <summary>
        /// reformats all the Data into individual variables and then inserts these values into the datastorage saving the program
        /// </summary>
        public string[] Dissassemble_Program_Overview(string Program_Title, string Program_ID, string Lenght_Of_program, string Data)
        {
            string[] Year = Data.Split('|');                 // each year begins with "|Year"

            int Max_number_of_Modules_per_Year = int.Parse(Lenght_Of_program) * 10;

            string[] Core_Modules = new string[Max_number_of_Modules_per_Year];            // stores the optional and core modules
            string[] Optional_Modules = new string[Max_number_of_Modules_per_Year];
            string[] Return_This = new string[4];

            int Optional_Counter = 0; //Counts how many of each module type
            int Core_Counter = 0;

            string Core_Module_Year_Split = "0";  // this stores e.g 4 core modules in year 1  || 5 core modules year 2 ....
            string Optional_Module_Year_Split = "0";

            // Year[0] contains data we already have (see params)
            // Year[1] contains year 1, Year[2] contains year 2 and so on
            for (int i = 1; i <= Year.Length - 1; i++)  //3 times, 3 years
            {
                string Year_modules = Year[i];   // grab a year


                string[] Modules = Year_modules.Replace("Year 1 Modules", "").Replace("Year 2 Modules", "").Replace("Year 3 Modules", "").Trim().Split(','); // throw away data we dont need and split the year into array of modules

                for (int j = 0; j < Modules.Length; j++)           // cycle through the modules and put them in either the core module array or the optional module array then increase the counter
                {
                    if (Modules[j].Contains("(Core)"))
                    {
                        Core_Modules[Core_Counter] = Modules[j].Replace("(Core)", "").Trim();
                        Core_Counter++;
                    }

                    if (Modules[j].Contains("(Optional)"))
                    {
                        Optional_Modules[Optional_Counter] = Modules[j].Replace("(Optional)", "").Trim();
                        Optional_Counter++;
                    }
                }
                Core_Module_Year_Split += ", " + Core_Counter.ToString();
                Optional_Module_Year_Split += ", " + Optional_Counter.ToString();
            }

            Core_Module_Year_Split = Core_Counter.ToString() + " " + string.Format("({0})", Core_Module_Year_Split);
            Optional_Module_Year_Split = Optional_Counter.ToString() + " " + string.Format("({0})", Optional_Module_Year_Split);

            // format the coloums string so we know where to put data
            // this does not affect the independance of the datastorage layer, an alternative method can chose not to make use of this
            string Columns = "ProgramID, Program_Title, Program_Length, Core_Modules, Optional_Modules";
            for (int i = 1; i <= Optional_Counter + Core_Counter; i++)
            {
                Columns = Columns + " ,Module_" + i.ToString();
            }

            // format the values to insert into the table
            string Values = " '" + Program_ID + "','" + Program_Title + "','" + Lenght_Of_program;
            Values = Values + "','" + Core_Module_Year_Split + "','" + Optional_Module_Year_Split;

            for (int i = 0; i < Core_Counter; i++)
            {
                Values = Values + "','" + Core_Modules[i];
            }


            for (int i = 0; i < Optional_Counter; i++)
            {
                Values = Values + "','" + Optional_Modules[i];
            }

            Values = Values + "'";

            Return_This[0] = Values;
            Return_This[1] = Columns;
            Return_This[2] = Make_Array_Single_String(Core_Modules, 0) + "," + Make_Array_Single_String(Optional_Modules, 0);

            return Return_This;
        }


        // find any linked modules and make sure they are both included in the year 
        public string [,] Find_Linked_Modules(string Modules)
        {
            string[,] Linked_Modules_Temp = new string [10,2]; // max modules is 10
            int linkedCounter = 0;
            //turn into array
            string Modules2 = Modules.Replace("(Core)", "").Replace("(Optional)", "").Replace("\n", "");      
            string[] Modules_Array = Modules2.Split(',');
            foreach (string Module_ in Modules_Array)
            {
                if (Module_ != "")
                {
                    // turn name into ID
                    string ModuleID = Data_Storage.Get_Module_ID(Module_.Trim());
                    bool has_Linked_assignment = Data_Storage.DoesThisModuleHaveLinkedAssignment(ModuleID);
                    if (has_Linked_assignment)
                    {
                        string[] Linked_assignment_Name = Data_Storage.Get_Linked_Module_Name(ModuleID);
                        Linked_Modules_Temp[linkedCounter, 0] = Module_;
                        Linked_Modules_Temp[linkedCounter, 1] = Linked_assignment_Name[0];
                        linkedCounter++;
                    }
                }
            }

            // create new array to stroe values (get rid of null values)
            string[,] Linked_Modules = new string[linkedCounter, 3];
            int Linked_Matcher = 0;
            if (linkedCounter != 0)
            {               
                for (int i = 0; i < linkedCounter; i++)
                {
                    Linked_Modules[i, 0] = Linked_Modules_Temp[i, 0].Trim();
                    Linked_Modules[i, 1] = Linked_Modules_Temp[i, 1].Trim();
                }

                // check to see if all the linked modules are included
                for (int i = 0; i < Linked_Modules.GetLength(0);i++)
                {
                    for (int j = 0; j < Modules_Array.Length; j++)
                    {
                        if (Modules_Array[j] != "")
                        {
                            // we know we have Linked_Modules[i,0] already so test for [i,1]
                            if (Linked_Modules[i, 1] == Modules_Array[j].Trim())
                            {
                                Linked_Modules[i, 2] = "Found";
                                Linked_Matcher++;
                                j = Modules_Array.Length; //break

                            }
                        }
                    }
                }
            }
            else
            {
                Linked_Modules = null;
            }

            string[] Modules_withlabels = Modules.Replace("\n","").Split(',');
            //finally make sure that all modules that are linked together are core (so the user can chose not to take 1 of them later :( )
            for (int i = 0; i < linkedCounter; i++)
            {
                bool found = false;
                for (int j = 0; j < Modules_withlabels.Length; j++)
                {
                    if (Modules_withlabels[j] == (Linked_Modules[i, 1] + " (Core)") )
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    Linked_Modules[i, 2] = null;
                }
            }
            return Linked_Modules;
        }

        public bool Check_Is_already_Linked(string moduleID)
        {
            bool isModuleAlreadyLinked = false;
            string[] Module_Data = Data_Storage.Get_all_Module_data(moduleID);

            if (Module_Data[12] != "None")
            {
                isModuleAlreadyLinked = true;
            }

            return isModuleAlreadyLinked;
        }


        /// <summary>
        /// Checks to see if ID meets naming rules 
        /// </summary>
        public bool Meets_required_Rules(string TableName, string ColName, string Text, int Required_length)
        {           
            if (Text.Length != Required_length)
            {
                MessageBox.Show(string.Format("ID code must be {0} digits", Required_length), string.Format("Incorrect {0} Code", ColName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }         
            else
            {
                if (!int.TryParse(Text, out int TRYCast))
                {
                    MessageBox.Show("ID Cannot contain letters", string.Format("Incorrect {0} Code", ColName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                else
                {
                    bool Unique =Data_Storage.IsPrimaryKey_Unique(TableName, Text, ColName);
                    if (!Unique)
                    {
                        MessageBox.Show("Code must be unique", string.Format("{0} ID already taken", TableName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public void Bind_to_linked_Module(string Name_of_new_module,string ModuleID_of_Linked)
        {
            Data_Storage.BindToLinkedModule(Name_of_new_module, ModuleID_of_Linked);
        }

        /// <summary>
        /// checks if grade is within 0< grade <= 100
        /// </summary>
        public int Check_Grades(int Grade)
        {
            if (Grade < 0 || Grade > 100)
            {
                MessageBox.Show("Grade must be an integer value between 0 and 100", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Grade = 0;
            }
            return Grade;
        }

        /// <summary>
        /// calcualates the final grade when given all other grades and their weights
        /// </summary>
        public int Calculate_final_grade(float[] Grades, float[] weights)
        {
            int Final_Grade = 0;
            for (int i = 0; i < Grades.Length; i++)
            {
                Final_Grade += (int)(Grades[i] * (weights[i] / 100));
            }
            return Final_Grade;
        }

        /// <summary>
        /// Determine classification based on the final grade
        /// </summary>
        public string DetermineClassifiation(float[] Grades,float [] Weights, int Grade, string Type)
        {
            string Classification = "";
            for (int i = 0; i < Grades.Length; i++)
            {
                if (Grades[i] == 0 && Weights[i] != 0)
                {
                    Classification = "Undefined";
                    i = Grades.Length;
                }
            } 

            if (Classification == "")   // not undefined     
            {
                switch (Type)
                {
                    case "Module":
                        Classification = DetermineClassifiation(Grade, 50, 70, 45);
                        break;
                    case "Program":
                        Classification = DetermineClassifiation(Grade, 50, 70);
                        break;
                    default:
                        Classification = "Error";
                        break;
                }
            }        
            return Classification;
        }



        public string DetermineClassifiation(int Grade,int Pass_Mark, int Distinction_mark,int fail_mark = 0)
        {
            if (fail_mark == 0)
            {
                fail_mark = Pass_Mark - 1;
            }

            string Classification = "";
            if (Grade == 0)
            {
                Classification = "Undefined";
            }
            else
            {               
                if (Grade >= Distinction_mark)
                {
                    Classification = "Distinction";
                }
                else
                {
                    if (Grade >= Pass_Mark)
                    {
                        Classification = "Pass";
                    }
                    else
                    {
                        if (Grade > fail_mark)
                        { Classification = "Pass Compensation"; }
                        else
                        { Classification = "Fail"; }
                    }
                }
            }
            return Classification;
        }






        //This method assigns the modules into year groups within the student table 
        // e.g these modules are core in year 1 
        //     these modules are core in year 2
        private string[] SplitCore_Modules_intoYears(string[] Core_Modules, string ProgramID, string Year_SPlit, string StudentID)
        {
            string[] Work_Out_Years = Year_SPlit.Replace(")", "").Split('('); //splits into 2, throw away first part
            Work_Out_Years = Work_Out_Years[1].Split(',');
            int Length = Work_Out_Years.Length - 1;

            int Year1_Split = int.Parse(Work_Out_Years[1]);
            int Year2_Split = 0;
            int Year3_Split = 0;

            string[] All_Years = new string[3];

            //incrementally search values in modules columns in program table where Profram ID = Program ID
            string[] Year1_Core_Modules = Data_Storage.Increment_Search("Module_", 1, Year1_Split, "Programs", "ProgramID", ProgramID.ToString());
            // turn 2d array into string
            string Year1_Core_modules_String = Make_Array_Single_String(Year1_Core_Modules, 0);
            All_Years[0] = Year1_Core_modules_String;
            // add values to column 

            Data_Storage.Enroll_Prog(Year1_Core_modules_String, StudentID,1);

            if (Length >= 2)
            {
                Year2_Split = int.Parse(Work_Out_Years[2]);
                string[] Year2_Core_Modules = Data_Storage.Increment_Search("Module_", Year1_Split + 1, Year2_Split, "Programs", "ProgramID", ProgramID.ToString());
                string Year2_Core_modules_String = Make_Array_Single_String(Year2_Core_Modules, 0);
                All_Years[1] = Year2_Core_modules_String;
                Data_Storage.Enroll_Prog(Year2_Core_modules_String, StudentID, 2);
            }
            if (Length >= 3)
            {
                Year3_Split = int.Parse(Work_Out_Years[3]);
                string[] Year3_Core_Modules =Data_Storage.Increment_Search("Module_", Year2_Split + 1, Year3_Split, "Programs", "ProgramID", ProgramID.ToString());
                string Year3_Core_modules_String = Make_Array_Single_String(Year3_Core_Modules, 0);
                All_Years[2] = Year3_Core_modules_String;
                Data_Storage.Enroll_Prog(Year3_Core_modules_String, StudentID, 3);
            }
            return All_Years;
        }

        ///enrolls the student onto each individual module based on their program selection (Only core modules are enrolled onto, optionals are not)
        public void EnrollOnto_AllCoreModules(string User_ID, string Program_Enrolled, string User_Name, string Passworde, string Department, string Cohort)
        {
            // first try and get all modules assigned to a program
            string[] ArrayData = Data_Storage.Get_Entire_Row("Programs", "Program_Title", Program_Enrolled);
            string Program_Length = ArrayData[2];

            // get amount of core and optional modules
            string[] TempArray = ArrayData[3].Split(' ');
            int Core_Modules = int.Parse(TempArray[0]);
            TempArray = ArrayData[4].Split(' ');
            int Optional_Modules = int.Parse(TempArray[0]);

            string[] Module_Names = new string[Core_Modules];

            // enroll onto each core module in module table 
            for (int i = 0; i < Core_Modules; i++)
            {
                //Get Module name
                string Module_Name = ArrayData[5 + i];
                Module_Names[i] = Module_Name;

                //Get Module ID
                string ModuleID = Data_Storage.Get_Module_ID(Module_Name);

                // Get students already Enrolled
             
                // Add new user ID to those already enrolled (item 0 contains all data)
                string students = Data_Storage.Get_Students_Enrolled(ModuleID) + ", " + Cohort + User_ID;

                // Update students enrolled column
                Data_Storage.AddStudent_to_enrolled(students, ModuleID);
            }

            // get the amount of core / optional modules in each year
            string Years_SPlit = ArrayData[3];
            string[] All_Modules_Split = SplitCore_Modules_intoYears(Module_Names, ArrayData[0], Years_SPlit, Cohort + User_ID);

            string[] Year_1_Core = All_Modules_Split[0].Split(',');

            string[] Year_2_Core = new string[1];
            string[] Year_3_Core = new string[1];

            // if null then only 1 year course or 2 year course
            if (All_Modules_Split[1] != null)
            { Year_2_Core = All_Modules_Split[1].Split(','); }
            if (All_Modules_Split[2] != null)
            { Year_3_Core = All_Modules_Split[2].Split(','); }

            //create student 
            Data_Storage.Create_Student( Cohort,  User_ID,  User_Name,  Passworde,  Program_Enrolled,  Department,  Program_Length, Year_1_Core,  Year_2_Core, Year_3_Core, All_Modules_Split);

            // DO this again as it adds the name of the modules to the years {this must be done once the user is already exisiting as it uses update}
            All_Modules_Split = SplitCore_Modules_intoYears(Module_Names, ArrayData[0], Years_SPlit, Cohort + User_ID);
        }



        public void CreateNewProgram(float similarity, string Values, string Columns)
        {
            if (similarity < 80)
            {
                Data_Storage.CreateNewProgram(Values, Columns);
            }
        }


        public void UpdateLinkedModule(string StudentID,string Original_module_ID, int Grade)
        {        
            // get linked assignmets name and pos
            string[] Linked_Module_name = Data_Storage.Get_Linked_Module_Name(Original_module_ID);

            // turn name into ID
            string ModuleID = Data_Storage.Get_Module_ID(Linked_Module_name[0].Trim());

            string [] Students_data = Data_Storage.Get_Student_Data(StudentID);

            string[] Modules_1 = Students_data[8].Split(','); // year 1
            string[] Modules_2 = null; // year 2
            string[] Modules_3 = null; // year 3

            int YearPos = 0;
            int Pos = 0;
            bool a = Array.Exists(Modules_1, element => element == Linked_Module_name[0]);
            if (a)
            {
                YearPos = 1;
                Pos = Array.IndexOf(Modules_1, Linked_Module_name[0]);
            }

            bool b;
            bool c;
            
            if (Students_data[9] != "" && !a)
            {
                Modules_2 = Students_data[9].Split(',');
                b = Array.Exists(Modules_2, element => element == Linked_Module_name[0]);
                if (b)
                {
                  YearPos = 2;
                  Pos = Array.IndexOf(Modules_2, Linked_Module_name[0]);
                }
            }
            if (Students_data[10] != "" && !a)
            {
                Modules_3 = Students_data[10].Split(',');
                c = Array.Exists(Modules_3, element => element == Linked_Module_name[0]);
                if (c)
                {
                    YearPos = 3;
                    Pos = Array.IndexOf(Modules_3, Linked_Module_name[0]);
                }
            }

            int arraypos = 12 + ((YearPos - 1)  * 6) + (Pos * 2);

            string Current_Grades = Students_data[arraypos];
            string[] Grades_array = Current_Grades.Replace('[', ' ').Replace(']', ' ').Split(',');


            

            if (Linked_Module_name.Length == 2)// then we have an assignment specifier (0,1,2)
            {
                Grades_array[int.Parse(Linked_Module_name[1])] = Grade.ToString();
            }
            else // assignment 4
            {
                Grades_array[3] = Grade.ToString();
            }

            // get weights so we can calculate the new final grade for the other module
            string [] assignment_Data = Get_Assesment_Data(ModuleID);     
            string[] Weights = Remove_Commans(assignment_Data[1]);


            int Final_grade = 0;
            for (int d = 0; d < Weights.Length; d++)
            {
                Final_grade += (int) (float.Parse(Weights[d])/100 * float.Parse(Grades_array[d]));
            }

            Grades_array[4] = Final_grade.ToString();

            string grades_str = string.Format("[{0},{1},{2},{3},{4}]", Grades_array[0], Grades_array[1], Grades_array[2], Grades_array[3], Grades_array[4].Trim());

            int Position = ((YearPos - 1) * 6) + (Pos+ 1);

            Data_Storage.UpdateStudentGrades(StudentID,"Grade_"+ Position, grades_str);
        }


        // depricated code 
        //-------------------------------------------------------------------------------------------------------

        //public Student Create_User_OBJ(Student Student_MemeberLogged_in, string[] User_Data = null)
        //{
        //    bool not_Done = true; // true when we have completed populating the user class
        //    string[] Modules;

        //    int Program_length = Student_MemeberLogged_in.Get_Student_Program().Get_Program_Length();
        //    for (int i = 0; i < Program_length; i++)
        //    {
        //        int Year_int = i + 1; //the year we are working with 
        //        Year Current_Year = Student_MemeberLogged_in.Get_Year(Year_int);
        //        if (Current_Year != null)
        //        {
        //            while (not_Done)
        //            {
        //                Modules = Current_Year.Get_Modules();

        //                // set up bools 
        //                bool Year1_set = false;
        //                bool Year2_set = false;
        //                bool Year3_set = false;

        //                // get the year objects for the years 
        //                Year one = Student_MemeberLogged_in.Get_Year(1);
        //                Year two = Student_MemeberLogged_in.Get_Year(2);
        //                Year three = Student_MemeberLogged_in.Get_Year(3);

        //                // only set the bool to true if value is uninitialized and we need to work with that year
        //                if (one != null)
        //                {
        //                    if (!one.Get_yearFully_Set() && Year_int == 1)
        //                    { Year1_set = true; }
        //                }
        //                if (two != null)
        //                {
        //                    if (!two.Get_yearFully_Set() && Year_int == 2)
        //                    { Year2_set = true; }
        //                }
        //                if (three != null)
        //                {
        //                    if (!three.Get_yearFully_Set() && Year_int == 3)
        //                    { Year3_set = true; }
        //                }


        //                // if we havent already loaded the most recent values in
        //                if (Year1_set || Year2_set || Year3_set)
        //                {
        //                    for (int j = 0; j < Modules.Length; j++)
        //                    {
        //                        string[] ModuleID = Select_From_Where_Equals("ModuleID", "Modules", "ModuleTitle", Modules[j]);

        //                        //add all module data to the user class
        //                        Student_MemeberLogged_in = Add_Module_to_USer_Class(Student_MemeberLogged_in, ModuleID[0], j, Year_int);
        //                    }
        //                }
        //                else
        //                {
        //                    not_Done = false; // no more years/modules left to add, finished
        //                }
        //            }
        //        }
        //    }
        //    return Student_MemeberLogged_in;
        //}



        // method adds a module to the user class
        //public Student Add_Module_to_USer_Class(Student Student_MemeberLogged_in, string ModuleID, int module_no, int Year)
        //{
        //    string[] Start_end = Start_end_modules(Year, Student_MemeberLogged_in);

        //    int Module_Start = int.Parse(Start_end[0]);
        //    int Module_End = int.Parse(Start_end[1]);
        //    string[] Years_Modules = null;
        //    for (int i = 0; i < 6; i++)
        //    {
        //        Years_Modules[i] = Start_end[2 + i];
        //    }

        //    // get the generic assesment data for the module 
        //    string[] Module_Data = Get_Assesment_Data(ModuleID);

        //    //get titles
        //    string[] Titles = Module_Data[0].Split(',');
        //    Module_Data = Module_Data[1].Split(',');

        //    //get grades
        //    string[] Grades = Increment_Search("Grade_", Module_Start, Module_End, "Students", "StudentID", Student_MemeberLogged_in.Get_ID().ToString());
        //    Grades = Grades[module_no].Replace("[", "").Replace("]", "").Split(',');

        //    //get weights 
        //    string[] weights = Module_Data;

        //    //format titles and weights togehter
        //    string[,] Titles_weights = new string[Titles.Length, 2];
        //    for (int i = 0; i < Titles.Length; i++)
        //    {
        //        Titles_weights[i, 0] = Titles[i];
        //        Titles_weights[i, 1] = weights[i];
        //    }

        //    // add to class
        //    switch (Year)
        //    {
        //        case 1:
        //            Student_MemeberLogged_in.Year1.Set_Module(Years_Modules[module_no], ModuleID, Titles_weights, Grades);
        //            break;
        //        case 2:
        //            Student_MemeberLogged_in.Year2.Set_Module(Years_Modules[module_no], ModuleID, Titles_weights, Grades);
        //            break;
        //        case 3:
        //            Student_MemeberLogged_in.Year3.Set_Module(Years_Modules[module_no], ModuleID, Titles_weights, Grades);
        //            break;
        //        default:
        //            break;
        //    }
        //    return Student_MemeberLogged_in;
        //}



        //private string[] Start_end_modules(int Year, Student Student_MemeberLogged_in)
        //{
        //    string[] Start_end = new string[8];
        //    int Module_Start = 0;
        //    int Module_End = 0;
        //    string[] Years_Modules;
        //    switch (Year)
        //    {
        //        case 1:
        //            Years_Modules = Student_MemeberLogged_in.Year1.Get_Modules();
        //            Module_Start = 1;
        //            Module_End = 6;
        //            break;
        //        case 2:
        //            Years_Modules = Student_MemeberLogged_in.Year2.Get_Modules();
        //            Module_Start = 7;
        //            Module_End = 12;
        //            break;
        //        case 3:
        //            Years_Modules = Student_MemeberLogged_in.Year3.Get_Modules();
        //            Module_Start = 13;
        //            Module_End = 18;
        //            break;
        //        default:
        //            Years_Modules = new string[] { "Null", "Nuller" };
        //            break;
        //    }
        //    Start_end[0] = Module_Start.ToString();
        //    Start_end[1] = Module_End.ToString();
        //    for (int i = 0; i < Years_Modules.Length; i++)
        //    {
        //        Start_end[2 + i] = Years_Modules[i];
        //    }
        //    return Start_end;
        //}




        /// <summary>
        /// Write a given module to the database
        /// </summary>
        //public void WriteModuleToDatabase(string Module_COmmand)
        //{
        //    Data_Storage.Write_to_dataBase(Module_COmmand);
        // }





        /// <summary>
        /// Search the database for empty fields
        /// </summary> 
        //public int Probe_For_EMpty_Field(int Range_Start, int Range_End, string Column_Keyword, string Table_Name, string Criteria_1, string Criteria_2)
        //{
        //    int Position = Data_Storage.Probe_For_EMpty_Field(Range_Start, Range_End, Column_Keyword, Table_Name, Criteria_1, Criteria_2);
        //    return Position;
        //}




    }
}
