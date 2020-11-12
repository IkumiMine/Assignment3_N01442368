using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Assignment3_N01442368.Models
{
    public class SchoolDbContext
    {
        //Only the SchoolDbContext can access them.
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //To connect to the database.
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }

        //The method to get the database.
        ///<summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <return>A MySqlConnection Object</return>
        public MySqlConnection AccessDatabase()
        {
            //instantiate the MySqlConnection Class to create an object
            //the object is a specific connection to school database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }


    }
}