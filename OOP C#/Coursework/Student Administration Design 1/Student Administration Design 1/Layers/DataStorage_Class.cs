using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb; //Database
using System.Data;
using System.Windows.Forms; // message boxes
using Student_Administration_Design_1.Layers;

namespace Student_Administration_Design_1
{
    class DataStorage_Class : IDataStorage_Interface
    {      
        //readonly string DataBase_Path = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =\\adir.hull.ac.uk\home\532\532631\Desktop\Svn msc\OOP C#\Coursework\Student Administration Design 1\Student Administration Design 1\Students3.mdb;OLE DB Services = -1";
        string DataBase_Path = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\SVN 2019\ieaun\MSC\OOP C#\Coursework\Student Administration Design 1\Student Administration Design 1\Students3.mdb;OLE DB Services = -1";

        public OleDbConnection con;

        public DataStorage_Class()
        {
            this.con =  new OleDbConnection(@DataBase_Path);
        }


        //--------------------------------------------------------------------------------------------
        //Modules values
        //---------------------------------------------------------------------------------------------
        public string CreateNewModule(string Title, string Module_Code, string[,] Number_of_Assesments_and_weighting, string Linked)
        {
            string Assignment_Length = (Number_of_Assesments_and_weighting.Length / 2).ToString();   //number of assignments = half this array
            string Assignment_1_Title = Number_of_Assesments_and_weighting[0, 0],
                   Assignment_2_Title = Number_of_Assesments_and_weighting[1, 0],
                   Assignment_3_Title = Number_of_Assesments_and_weighting[2, 0],
                   Assignment_4_Title = Number_of_Assesments_and_weighting[3, 0],
                   Assignment_1_Weight = Number_of_Assesments_and_weighting[0, 1],
                   Assignment_2_Weight = Number_of_Assesments_and_weighting[1, 1],
                   Assignment_3_Weight = Number_of_Assesments_and_weighting[2, 1],
                   Assignment_4_Weight = Number_of_Assesments_and_weighting[3, 1];


            string Command = string.Format("INSERT INTO Modules (ModuleID,ModuleTitle,Number_of_Assesments,Title_of_assesment1" +
                ",Weighting1,Title_of_assesment2,Weighting2,Title_of_assesment3,Weighting3,Title_of_assesment4,Weighting4,Linked_Assignments) VALUES('{0}','{1}','{2}','{3}','{4}'," +
                "'{5}','{6}','{7}','{8}','{9}','{10}','{11}')", Module_Code, Title, Assignment_Length, Assignment_1_Title, Assignment_1_Weight, Assignment_2_Title, Assignment_2_Weight, Assignment_3_Title, Assignment_3_Weight, Assignment_4_Title, Assignment_4_Weight, Linked);
            Write_to_dataBase(Command);
            Module NewModule = new Module(Title, Module_Code, Number_of_Assesments_and_weighting);

            return NewModule.ToString();
        }


        /// <summary>
        /// Makes sure that ID is unique 
        /// </summary>
        public bool IsPrimaryKey_Unique(string TableName, string Primarykey, string col_name)
        {
            bool isUnique = true; //intialize as true
            string[] PrimaryKeys = Get_Entire_Column(col_name, TableName); // get all primary keys
            for (int i = 0; i < PrimaryKeys.Length; i++)  //for each primary key
            {
                if (PrimaryKeys[i] == Primarykey)
                {
                    isUnique = false;
                }
            }
            return isUnique;
        }

       


        /// <summary>
        /// return all module titles for a staff member
        /// </summary>
        public string[] Get_all_modulesTitles()
        {
            string[] ModuleTitles = Get_Entire_Column("ModuleTitle", "Modules");
            return ModuleTitles;
        }

        public void SetDatastoragePath(string Path)
        {
            DataBase_Path = Path;
        }


        /// <summary>
        /// return all module IDs for a staff member
        /// </summary>
        public string[] Get_all_moduleIDs()
        {
            string[] ModuleTitles = Get_Entire_Column("ModuleID", "Modules");
            return ModuleTitles;
        }

        /// <summary>
        /// Get a module ID when given a module title
        /// </summary>
        public string Get_Module_ID(string ModuleTitle)
        {
            string[] temp = Select_From_Where_Equals("ModuleID", "Modules", "ModuleTitle", ModuleTitle);
            string ModuleID = temp[0];
            return ModuleID;
        }

        /// <summary>
        /// Get IDs that were in there already and add the new user to it
        /// </summary>
        public string GetStudents_Enrolled_Module(string Selected_Item)
        {
            string Students = "";
            string[] Temp = Select_From_Where_Equals("Students_Enrolled", "Modules", "ModuleTitle", Selected_Item);
            if (Temp.Length < 1)
            { Students = Temp[0]; }
            return Students;
        }

        /// <summary>
        /// Get the students enrolled in a specific module
        /// </summary>
        public string[] GetStudent(string Module_Name)
        {
            string[] Students = Select_From_Where_Equals("Students_Enrolled", "Modules", "ModuleID", Module_Name);

            if (Students[0] != null) // returned nothing
            {
                string Temp = Students[0];
                Students = Temp.Split(',');
            }
            else
            { Students = null; }

            return Students;
        }

        /// <summary>
        /// get the linked assingments in the datastorage
        /// </summary>
        public string Get_linked_assignments(string Module_ID)
        {
            string[] Temp = Select_From_Where_Equals("Linked_Assignments", "Modules", "ModuleID", Module_ID);
            string Linked_Assignment = Temp[0];
            return Linked_Assignment;
        }

        /// <summary>
        /// get all values assosicated with a given module ID
        /// </summary>
        public string[] Get_all_Module_data(string Module_ID)
        {
            string[] Program_Values = Get_Entire_Row("Modules", "ModuleID", Module_ID);
            return Program_Values;
        }


        //--------------------------------------------------------------------------------------------
        //Program values
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// return all program titles
        /// </summary>
        public string[] Get_Program_List()
        {     
            string[] Programs = Get_Entire_Column("Program_Title", "Programs");
            return Programs;
        }

        /// <summary>
        ///Get an entire programs values
        /// </summary>
        public string[] Get_Entire_Program(string ProgramID)
        {
            string[] Program_Values = Get_Entire_Row("Programs", "ProgramID", ProgramID);
            return Program_Values;
        }

        ///get Program ID when given Program Title
        public string GetProgramID(string Program)
        {
            string[] temp = Select_From_Where_Equals("ProgramID", "Programs", "Program_Title", Program);
            string ID = temp[0];
            return ID;
        }

        //get Program Lenght when given Program ID
        public int GetProgramLength(string ProgramID)
        {
            string[] temp = Select_From_Where_Equals("Program_Length", "Programs", "ProgramID", ProgramID);
            string Program_length = temp[0];
            return int.Parse(Program_length);
        }


        //--------------------------------------------------------------------------------------------
        //Student values
        //---------------------------------------------------------------------------------------------
        ///get all fields relating to a 1 student when given his ID
        public string[] Get_Student_Data(string UserID)
        {
            //check exists, if so get all data 
            string[] StudentData = FieldExists("Students", "StudentID", UserID);
            return StudentData;
        }

        /// <summary>
        /// gets the modules a student is enrolled on
        /// </summary>
        public string[] WhatModulesis_thisStudent_EnrolledOn(string year_, string studentID)
        {
            year_ = "Year" + year_;
            string[] Modules = Select_From_Where_Equals(year_, "Students", "StudentID", studentID);
            return Modules;
        }

        /// <summary>
        /// update the students grades in a module
        /// </summary>
        public void UpdateStudentGrades(string StudentID, string ModuleID, string Grade)
        {
            string Command = string.Format("Update Students Set {0}= '{1}' where StudentID = '{2}'; ", ModuleID, Grade, StudentID);
            Write_to_dataBase(Command);
        }


        /// <summary>
        /// Enroll the student on a single module
        /// </summary>
        public void EnrollOntoModule(string UserID, string Module_title)
        {
            //UPDATE Module DATABASE
            string Command = string.Format("UPDATE Modules SET Students_Enrolled = '{0}' where ModuleTitle= '{1}'; ", UserID, Module_title);
            Write_to_dataBase(Command);
        }

        ///Get a users password
        public string Get_password(string UserID)
        {
            string Password = "";
            string[] Temp = Select_From_Where_Equals("User_Password", "Students", "StudentID", UserID);
            Password = Temp[0];

            return Password;
        }   

        public string[] Get_Modules_for_This_Year(int Year, string StudentID)
        {
            string Search_String = "Year" + Year.ToString();
            string[] Modules = Select_From_Where_Equals(Search_String, "Students", "StudentID", StudentID);
            Modules = Modules[0].Split(',');
            return Modules;
        }

        public string Get_Students_module(int Module_Pos, int StudentID)
        {
            string Search = "Module_" + Module_Pos.ToString();
            string[] Results = Select_From_Where_Equals(Search, "Students", "StudentID", StudentID.ToString());
            string Module = Results[0].Trim();
            return Module;
        }

        public string Get_Students_Grade(int Module_Pos, int StudentID)
        {
            string Search = "Grade_" + Module_Pos.ToString();
            string[] Results = Select_From_Where_Equals(Search, "Students", "StudentID", StudentID.ToString());
            string Grade = Results[0].Trim();
            return Grade;
        }

        public string Get_Students_Enrolled(string ModuleID)
        {
            string[] Temp = Select_From_Where_Equals("Students_Enrolled", "Modules", "ModuleID", ModuleID);
            string students = Temp[0].Trim();
            return students;
        }


        public void CreateNewProgram(string Values, string Columns)
        {
            string command = string.Format("INSERT INTO Programs ({0}) VALUES ({1})", Columns, Values);
            Write_to_dataBase(command);
        }

        public void Enroll_Prog(string Year1_Core_modules_String, string StudentID, int Year)
        {
            string Command = string.Format("UPDATE Students SET Year{2}= '{0}' where StudentID= '{1}'; ", Year1_Core_modules_String, StudentID, Year);
            Write_to_dataBase(Command);
        }

        public void AddStudent_to_enrolled(string students, string ModuleID)
        {
            string Command = string.Format("Update Modules Set Students_Enrolled= '{0}' where ModuleID = '{1}'; ", students, ModuleID);
            Write_to_dataBase(Command);
        }

        public string Get_Module_Title(int ModuleID)
        {
            string[] Temp = Select_From_Where_Equals("ModuleTitle", "Modules", "ModuleID", ModuleID.ToString());
            string Title = Temp[0];
            return Title;
        }

        /// <summary>
        /// UPDATE the students enrolled status on the given module list for a given year
        /// </summary>
        public void EnrollStudent_ontoModules(string Module_string, int Year, string User_ID)
        {
            string Command = string.Format("UPDATE Students SET Year{0} = '{1}' where StudentID= '{2}'; ", Year.ToString(), Module_string, User_ID);
            Write_to_dataBase(Command);
        }

        //--------------------------------------------------------------------------------------------
        //Staff values
        //---------------------------------------------------------------------------------------------

        public void Create_Staff_member(string User_Name, string Login_ID, string Password)
        {
            // format sql command
            string Columns = "StudentID, StudentName, User_Password, User_Type";
            string Values = Login_ID + "','" + User_Name + "','" + Password + "','" + "Staff";
            string Command = string.Format("INSERT INTO Students({0})", Columns);
            Command += string.Format(" VALUES('{0}');", Values);

            // add user into the database    
            // insert the new values into the Student database
            Write_to_dataBase(Command);
        }



        //return the path used by the database
        public string Get_DB_Path() {return DataBase_Path;}

        //--------------------------------------------------------------------------------------------
        //General values
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// Read database
        /// </summary>
        private DataTable Read_Database(string Command)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(Command);//Command contains the command string e.g("select ModuleTitle, ModuleID from Modules")
                cmd.Connection = con;  //Pass connection object
                OleDbDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
                con.Close();

                return dt;

            }
            catch (Exception exc)
            {
                MessageBox.Show("Unable to use database, " + exc.ToString(), "Database error in API");
                return dt;
            }

        }

        /// Return an entire column from a table given the name of the column and the table name
        public string[] Get_Entire_Column(string Column_Name, string Table_Name)
        {
            DataTable Data = Read_Database(string.Format("select {0} from {1}", Column_Name, Table_Name));
            string[] holder_Temp = new string[1];
            string[] Column_Data = new string[Data.Rows.Count];

            for (int i = 0; i < Data.Rows.Count; i++)
            {
                holder_Temp = Data.Rows[i].ItemArray.Select(x => x.ToString()).ToArray(); // get value in row
                Column_Data[i] = holder_Temp[0].ToString(); // add value to the array
            }

            return Column_Data;
        }

        /// <summary>
        /// Return an entire row from a table given the name of the row, the identifiying column and the table name
        /// </summary>
        public string[] Get_Entire_Row(string Table_Name, string Identifinging_Coloumn, string Identifinging_Value)
        {
            string[] ArrayData;
            try
            {
                DataTable Data = Read_Database(string.Format("Select * from {0} where {1} = '{2}';", Table_Name, Identifinging_Coloumn, Identifinging_Value));
                string[] Row_Data = new string[Data.Rows.Count];
                ArrayData = Data.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            }
            catch { ArrayData = null; }

            return ArrayData;
        }

        /// <summary>
        /// check to see if a field exisits and if it matches the given value in the datastorage
        /// </summary>
        public string [] FieldExists(string Table, string Column, string Value)
        {
            string[] exists = null;           
            string[] Values = Get_Entire_Row(Table, Column, Value);
            if (Values != null)
            {
                if (Values.Length > 0)
                {
                    exists = Values;
                }
            }
            return exists;
        }


        /// <summary>
        /// get list of all program IDs
        /// </summary>
        public string[] Get_ProgramID_List()
        {
            string[] programID = Get_Entire_Column("ProgramID", "Programs");
            return programID;
        }

        /// <summary>
        /// Unenroll all students from a module when given a ModulesID
        /// </summary>
        public void Unenroll_all_Students(string ModuleID)
        {
            Clear_field("Students_Enrolled", "Modules", "ModuleID", ModuleID);
        }

        ///save student in datastorage
        public void Create_Student(string Cohort, string User_ID ,string User_Name,string Password,string Program_Enrolled,string Department,string Program_Length, string [] Year_1_Core, string[] Year_2_Core, string[] Year_3_Core,string [] All_Modules_Split)
        {
            //set column strings in Student table 
            string Columns = "StudentID, StudentName, User_Password, Program, Department, CoHort, Length_of_program, User_Type";
            string Values = Cohort + User_ID + "','" + User_Name + "','" + Password + "','" + Program_Enrolled + "','" + Department + "','" + Cohort + "','" + Program_Length + "','" + "Student";

            // add modules to their positions in the student table
            for (int i = 1; i <= Year_1_Core.Length; i++)
            {
                Columns += ", " + "Module_" + i.ToString() + ", " + "Grade_" + i.ToString();
                Values += "', '" + Year_1_Core[i - 1].Trim() + "', '" + " [0,0,0,0,0] ";
            }

            if (All_Modules_Split[1] != null)
            {
                for (int i = 7; i <= Year_2_Core.Length + 6; i++)
                {
                    Columns += ", " + "Module_" + i.ToString() + ", " + "Grade_" + i.ToString();
                    Values += "', '" + Year_2_Core[i - 7].Trim() + "', '" + " [0,0,0,0,0] ";
                }
            }

            if (All_Modules_Split[2] != null)
            {
                for (int i = 13; i <= Year_3_Core.Length + 12; i++)
                {
                    Columns += ", " + "Module_" + i.ToString() + ", " + "Grade_" + i.ToString();
                    Values += "', '" + Year_3_Core[i - 13].Trim() + "', '" + " [0,0,0,0,0] ";
                }
            }

            // add user into the database         
            string Command_2 = string.Format("INSERT INTO Students({0})", Columns);
            Command_2 += string.Format(" VALUES('{0}');", Values);
            // insert the new values into the Student database
            Write_to_dataBase(Command_2);
        }


        /// Write data to the data_Storage
        private void Write_to_dataBase(string Command)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(Command);//Command contains the command string e.g("select ModuleTitle, ModuleID from Modules")
                cmd.Connection = con;  //Pass connection object

                OleDbDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(dr);
                con.Close();

            }
            catch (Exception exe)
            {
                MessageBox.Show("Unable to use database, Exception " + exe, "Open Database error");
            }

        }

        /// <summary>
        /// Update a modules "Students enrolled" coloumn to indicate the student is taking that module
        /// </summary>
        public void Enroll_onto_module(string StudentID, string ModuleName, int Pos_EmptyCOl, int Year_Compensation)
        {
            string Command = string.Format("UPDATE Students SET Module_{0} = '{1}' where StudentID= '{2}'; ", Pos_EmptyCOl, ModuleName, StudentID);
            Write_to_dataBase(Command);

            Command = string.Format("UPDATE Students SET Grade_{0} = '{1}' where StudentID= '{2}'; ", Pos_EmptyCOl, "[0,0,0,0,0]", StudentID);
            Write_to_dataBase(Command);
        }


        public int Probe_For_EMpty_Field(int Range_Start, int Range_End, string Column_Keyword, string Table_Name, string Criteria_1, string Criteria_2)
        {
            int Position = 01;
            for (int i = Range_Start; i <= Range_End; i++)
            {
                string Key_Word = Column_Keyword + i.ToString();
                string[] Temp = Select_From_Where_Equals(Key_Word, Table_Name, Criteria_1, Criteria_2);
                if (Temp[0] == "")
                {
                    Position = i;
                    i = 20; // break out 
                }
            }

            return Position;
        }


        /// <summary>
        /// Takes a string and creates multiple versions of it.
        /// The different versions are just incremented values of the Text
        /// e.g Assignment_ -> Assignment_1  -> Assignment_2  -> Assignment_3
        /// take this new value and search in the database for coloumns that match it
        /// return the field in the database where coloumn = Numbered_Strings[i]
        /// </summary>
        public string[] Increment_Search(string Text, int Start_From, int Until, string Table, string Specifier, string Value)
        {

            int This_Many_Times = Until - Start_From + 1;

            string[] Results = new string[This_Many_Times];
            string[] Temp;
            int LoopCount = 0;
            int Ns_Counter = 0;
            int Index_Counter = 0;

            string[] Numbered_Strings = new string[This_Many_Times];
            for (int i = Start_From; LoopCount < This_Many_Times; i++)
            {
                Numbered_Strings[Ns_Counter] = Text + i.ToString();
                Ns_Counter++;

                // Take new coloumn string and search for its value in the database
                Temp = Select_From_Where_Equals(Numbered_Strings[LoopCount], Table, Specifier, Value);
                if (Temp[0] != "")
                {
                    Results[Index_Counter] = Temp[0];
                    Index_Counter++;
                }
                LoopCount++;
            }
            return Results;
        }


        /// <summary>
        /// select {Coulumn name} from {table name} where {column} = '{value}';
        /// </summary>
        private string[] Select_From_Where_Equals(string Column_Name, string Table_Name, string Compair_Coloumn, string Similar)
        {
            DataTable Data = Read_Database(string.Format("select {0} from {1} where {2} = '{3}';", Column_Name, Table_Name, Compair_Coloumn, Similar));
            string[] holder_Temp = new string[1];
            string[] Column_Data = new string[Data.Rows.Count + 1];

            for (int i = 0; i < Data.Rows.Count; i++)
            {
                holder_Temp = Data.Rows[i].ItemArray.Select(x => x.ToString()).ToArray(); // get value in row
                Column_Data[i] = holder_Temp[0].ToString(); // add value to the array

                if (holder_Temp.Length > 1)
                {
                    //this never happens except for when logging in and in need of cohort
                    Column_Data[i + 1] = holder_Temp[1].ToString();
                }
            }
            return Column_Data;
        }

 

        /// delete all data in a field
        public void Clear_field(string Column_Name, string TableName, string Compair_Coloumn, string SimilarString)
        {
            string Command = string.Format("UPDATE {0} SET {1} = '' Where {2} = '{3}' ;", TableName, Column_Name, Compair_Coloumn, SimilarString);
            Write_to_dataBase(Command);
        }

        public string [] Get_Linked_Module_Name(string Original_module_ID)
        {
            string[] Module_Data = Get_all_Module_data(Original_module_ID);
            string Linked_Module_name = Module_Data[12].Replace("[","").Replace("]","");
            string[] Linked_Module = Linked_Module_name.Split(',');
            return Linked_Module;
        }

        public string[,] Get_all_Linked_Modules()
        {
            DataTable Data = Read_Database("Select * from Modules where Linked_Assignments != 'None';");
            string[,] Linked_Modules = new string[1, 1];
            return Linked_Modules;
        }

        public bool DoesThisModuleHaveLinkedAssignment(string ModuleID)
        {
            bool hasLinked = false;
            string[] Module_Data = Get_all_Module_data(ModuleID);
            if (Module_Data[12] != "None" && Module_Data[12] != "")
            {
                hasLinked = true;
            }
            return hasLinked;
        }

        public void BindToLinkedModule(string Name_of_new_module, string ModuleID_of_Linked)
        {
            string Command = string.Format("Update Modules SET Linked_Assignments = '[{0}]' Where {1} = '{2}'", Name_of_new_module,"ModuleID", ModuleID_of_Linked);
            Write_to_dataBase(Command);
        }
       
    }
}
