using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb; //Database
using System.Data;

namespace Student_Administration_Design_1
{
    interface IDataStorage_Interface
    {
        #region Module Values
        //------------------Module Values------------------------------------      
        /// Save all module data about a module to the data storage
        string CreateNewModule(string Title, string Module_Code, string[,] Number_of_Assesments_and_weighting, string Linked);

        /// return all module IDs for a staff member
        string[] Get_all_moduleIDs();

        ///Get a module ID when given a module title
        string Get_Module_ID(string ModuleTitle);

        /// Get IDs that were in there already and add the new user to it
        string GetStudents_Enrolled_Module(string Selected_Item);

        /// Get the students enrolled in a specific module
        string[] GetStudent(string Module_Name);

        /// get the linked assingments in the datastorage
        string Get_linked_assignments(string Module_ID);

        /// get all values assosicated with a given module ID
        string[] Get_all_Module_data(string Module_ID);
        #endregion

        #region Program Values
        //------------------Program Values------------------------------------
        /// return all program titles
        string[] Get_Program_List();

        /// return all program titles
        string[] Get_ProgramID_List();

        ///Get an entire programs values
        string[] Get_Entire_Program(string ProgramID);

        ///get Program ID when given Program Title
        string GetProgramID(string Program);

        ///get Program Lenght when given Program ID
        int GetProgramLength(string ProgramID);

        ///save program to datastorage
        void CreateNewProgram(string Values, string Columns);
        #endregion

        #region User Values
        //------------------User Values------------------------------------
        ///get all fields relating to a 1 student when given his ID
        string[] Get_Student_Data(string UserID);

        /// gets the modules a student is enrolled on
        string[] WhatModulesis_thisStudent_EnrolledOn(string year_, string studentID);

        /// update the students grades in a module
        void UpdateStudentGrades(string StudentID, string ModuleID, string Grade);

        /// Enroll the student on a single module
        void EnrollOntoModule(string UserID, string Module_title);

        ///Get a users password
        string Get_password(string UserID);

        ///UPDATE the students enrolled status on the given module list for a given year
        void EnrollStudent_ontoModules(string Module_string, int Year, string User_ID);

        ///save staff memeber in datastorage
        void Create_Staff_member(string User_Name, string Login_ID, string Password);

        ///save student in datastorage
        void Create_Student(string Cohort, string User_ID, string User_Name, string Password, string Program_Enrolled, string Department, string Program_Length, string[] Year_1_Core, string[] Year_2_Core, string[] Year_3_Core, string[] All_Modules_Split);
        #endregion

        #region General
        //-----------------General ------------------------------------------
        /// Return an entire column from a table given the name of the column and the table name
        string[] Get_Entire_Column(string Column_Name, string Table_Name);

        /// Return an entire row from a table given the name of the row, the identifiying column and the table name
        string[] Get_Entire_Row(string Table_Name, string Identifinging_Coloumn, string Identifinging_Value);

        /// check if a feild exists
        string[] FieldExists(string Table, string Column, string Value);

        /// Update a modules "Students enrolled" coloumn to indicate the student is taking that module
        void Enroll_onto_module(string StudentID, string ModuleName, int Pos_EmptyCOl, int Year_Compensation);
      
        ///Clear data in the data structure (used for testing) 
        void Clear_field(string Column_Name, string TableName, string Compair_Coloumn, string SimilarString);
        #endregion
    }
}
