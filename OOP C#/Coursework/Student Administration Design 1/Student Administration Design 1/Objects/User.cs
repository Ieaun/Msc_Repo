using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Student_Administration_Design_1
{
    class User : Description
    {
        protected UserType Type_ofUser;
        public enum UserType
        {
            Student,
            Staff
        }

        // Get methods
        public int Get_User_Type()        
        {
          if (Type_ofUser == UserType.Staff) {return 0; } else { return 1; }          
        }

    }
}
