using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Administration_Design_1.Layers
{
    class Student_admin_API
    {

        //Data storage layer
        public DataStorage_Class Data_Storage = null;
        public void InitDataStoragelayer()
        {
            Data_Storage = new DataStorage_Class();
        }

        //Business logic Layer
        public Business_Logic Business_layer = null;
        public void InitBusinesslayer()
        {
            Business_layer = new Business_Logic(Data_Storage);
        }

        //constructor (init the DS and BL layers)
        public Student_admin_API()
        {
            InitDataStoragelayer(); // create an instance of datastorage class
            InitBusinesslayer(); // pass datastorage instance to business layer
            //InitUILayer();
        }

        public Business_Logic GetBusiness_layer()
        {
            return Business_layer;
        }

        public DataStorage_Class GetDataStorage()
        {
            return Data_Storage;
        }
    }
}
