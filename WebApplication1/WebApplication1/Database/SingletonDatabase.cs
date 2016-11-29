using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Database
{
    public class SingletonDatabase
    {
        private static Database _instance;



        // Constructor is 'protected'

        protected SingletonDatabase() 

        {

        }



        public static Database Instance()

        {

            // Uses lazy initialization.

            // Note: this is not thread safe.

            if (_instance == null)

            {

                _instance = new Database();

            }



            return _instance;

        }

    }
}
