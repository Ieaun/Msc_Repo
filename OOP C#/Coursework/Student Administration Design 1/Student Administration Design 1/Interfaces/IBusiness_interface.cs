using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb; //Database
using System.Data;

namespace Student_Administration_Design_1
{
    interface IBusiness
    {
        #region Module Values

        //------------------User Values------------------------------------
        /// Unenroll all students from a module when given a ModulesID
        //void Unenroll_all_Students(string ModuleID);

        /// Unenroll all students from all modules in database
        void Unenroll_all_Students_all_Modules(string[] ModuleIDs);

        ///get all fields relating to a 1 student when given his ID
        //string[] Get_Student_Data(string UserID);

        /// update the students grades in a module
        //void UpdateStudentGrades(string StudentID, string ModuleID, string Grade);

        /// gets the modules a student is enrolled on
        //string[] WhatModulesis_thisStudent_EnrolledOn(string year_, string studentID);

        /// Get the students modules for that year
        //String[] Get_Modules_for_This_Year(int Year, string StudentID);

        ///Create a new staff memeber in the datastorage
       // void Create_Staff_member(string User_Name, string Login_ID, string Password);

        ///UPDATE the students enrolled status on the given module list for a given year
        //void EnrollStudent_ontoModules(string[] Module_List, int Year, string User_ID);

        /// get all the grades for a selected student from the datastorage
        string[] Get_Student_Module_Grades(int ModuleID, int StudentID);

        ///enrolls the student onto each individual module based on their program selection (Only core modules are enrolled onto, optionals are not)
        void EnrollOnto_AllCoreModules(string User_ID, string Program_Enrolled, string User_Name, string Passworde, string Department, string Cohort);
        #endregion

        #region Program Values

        //------------------Program Values------------------------------------
        /// reformats all the Data into individual variables and then inserts these values into the datastorage saving the program
        string[] Dissassemble_Program_Overview(string Program_Title, string Program_ID, string Lenght_Of_program, string Data);

        #endregion

        #region Module Values

        //------------------Modules Values------------------------------------     
        /// gets the assesment information of a module ,returns assesment titles, weighting ,*does not return student data only general module data
        string[] Get_Assesment_Data(string Module_ID);

        #endregion

        #region Functions
        //----------------Functions----------------------------------------
        /// create the student class with basic data (still needs module assesment data)
        Student Create_Student(string StudentID, string[] Student_Data = null);

        /// get the average for the year
        string[] DO_Year_Average(Student Student_MemeberLogged_in, int Year__);

        /// Returns a random number after being given a minimum and maximum range
        int GiveMe_ARandomNumber(int MinRange, int MaxRange);

        /// make sure we have 6 modules and they are split corretly (cant have 6 core modules and also optional modules)
        bool Check_ModuleList_follows_rules(int Optional, int Core);

        /// take an array and turn that array into a string "element1,element2..."
        string Make_Array_Single_String(string[] Array, int startFrom);

        /// compares this program to other programs and calculates a simliarity score, returns the similarity score and program it is most similar to
        string[] Is_Program_Unique(string[] Module_List);

        /// Checks to see if ID meets naming rules 
        bool Meets_required_Rules(string TableName, string ColName, string Text, int Required_length);

        /// checks if grade is within 0< grade <= 100
        int Check_Grades(int Grade);

        /// calcualates the final grade when given all other grades and their weights
        int Calculate_final_grade(float[] Grades, float[] weights);

        /// Determine classification based on the final grade, type = "Module" or "Program"
        string DetermineClassifiation(float[] Grades, float[] weights,int Grade, string Type);

        /// Overload which is called from  DetermineClassifiation(int,string) which contains the logic that determines classification
        string DetermineClassifiation(int Grade, int Pass_Mark, int Distinction_mark, int fail_mark = 0);
        #endregion

        Student student_MemeberLogged_in { get; set; }
        Staff staff_Member_Logged_in { get; set; }
    }
}
