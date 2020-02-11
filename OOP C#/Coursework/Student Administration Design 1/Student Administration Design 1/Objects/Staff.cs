using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Student_Administration_Design_1
{
    class Staff : User
    {
        // holds modules and module IDs
        string[] ModuleTitles;
        string[] ModuleIDs;

        // if this is true then it means the Module arrays need to be updated 
        // first time login / new module created 
        bool Update_Modules = true;


        public Staff(int User_Type, string user_id, string user_name)
        {
            bool Conforms = DoesIDFollowRules(user_id,7);
            if (Conforms)
            {
                // ID, name, type
                Obj_ID = user_id;
                Obj_Name = user_name;
                Type_ofUser = UserType.Staff;
                //MessageBox.Show(string.Format("User [{0}] Logged in", Obj_Name), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //MessageBox.Show("ID does not follow rules", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // gets
        /// <summary>
        /// when true, tells class to update the module table
        /// </summary>
        public bool Get_Update_Modules() { return Update_Modules; }
        public string[] Get_ModuleIDs() { return ModuleIDs; }
        public string[] Get_ModuleTitles() { return ModuleTitles; }

        //sets
        /// <summary>
        /// set the hash table containing the module ids and titles
        /// </summary>
        public void Set_Modules(string[] Module_IDs, string[] Module_Titles)
        {
            ModuleIDs = Module_IDs;
            ModuleTitles = Module_Titles;
        }
        public void Set_Update_Modules(bool value) { Update_Modules = value; }

    }
}
