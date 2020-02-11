using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Administration_Design_1.Layers;

namespace Student_Administration_Design_1
{
    class Description
    {
        // API 
        public Student_admin_API Logic_API = new Student_admin_API();

        // every object that inherits this has an ID and Name (Users(staff and student),Programs,Modules)
        protected string Obj_ID = null; 
        protected string Obj_Name = null;

        /// <summary>
        /// check if derived classes follow thier rules for ID formats before setting ID,
        /// Programs/Modules inherit this ,
        /// User Classes (student) must override this to include the check for Cohort.
        /// </summary>
        public virtual bool DoesIDFollowRules(string ID,int Length)
        {
            bool Conforms = false; // if it follws rules makes this true
            if (ID.Length == Length)
            { Conforms = true; }

            return Conforms;
        }

        /// <summary>
        /// Returns objects name 
        /// </summary>
        public string Get_Name(){ return Obj_Name;}

        /// <summary>
        /// Returns objects ID
        /// </summary>
        public string Get_ID()  { return Obj_ID; }

        public bool Is_ID_Unique(string TableName, string ID, string col_name)
        {
            bool Unique = Logic_API.Data_Storage.IsPrimaryKey_Unique(TableName, ID, col_name);
            return Unique;
        }      
    }
}
