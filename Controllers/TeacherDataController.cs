using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3_N01442368.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;

//#nullable enable 


namespace Assignment3_N01442368.Controllers
{
    public class TeacherDataController : ApiController
    {
        //the database context class allows to access MySQL database.
        private SchoolDbContext School = new SchoolDbContext();

        //This controller will connect to the teachers table of the school database.
        ///<summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <return>
        /// A list of teachers (first names and last names)
        /// </return>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers WHERE LOWER(teacherfname) LIKE LOWER(@SearchKey) OR LOWER(teacherlname) LIKE LOWER(@SearchKey) OR LOWER(CONCAT(teacherfname, ' ', teacherlname)) LIKE LOWER(@SearchKey)";

            cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row the Result Set
            while (ResultSet.Read())
            {
                //Access column information by DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                //optional to add the following data
                /*string HireDate = ResultSet["hiredate"].ToString();
                decimal Salary = (decimal)ResultSet["salary"];
                string ClassName = ResultSet["classname"].ToString();
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();*/

                //assign the column information to the fields created in Teacher.cs
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                //optional to add the following data
                /*NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
                NewTeacher.ClassName = ClassName;
                NewTeacher.StartDate = StartDate;
                NewTeacher.FinishDate = FinishDate;*/

                //Add the teacher information to the list
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL database and the web server 
            Connection.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        ///<summary>
        /// Returns teachers'information in the system 
        /// Some of codes are commented that is my attempt that I tried to find a teacher by their first name and it did't work
        /// </summary>
        /// <example>GET: api/TeacherData/FindTeacher/1</example>
        /// <return>
        /// information of a teacher (first name, last name, teacher ID, hire date, salary, class, class date)
        /// </return>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers LEFT JOIN classes ON teachers.teacherid = classes.teacherid WHERE teachers.teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row the Result Set
            while (ResultSet.Read())
            {
                //Access column information by DB column name as an index
                int TeacherId =  Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                /*DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string ClassName = ResultSet["classname"].ToString();
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate =(DateTime)ResultSet["finishdate"];*/


                //assign the column information to the fields created in Teacher.cs
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                /*NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
                NewTeacher.ClassName = ClassName;
                NewTeacher.StartDate = StartDate;
                NewTeacher.FinishDate = FinishDate;*/
            }

            //Close the connection between the MySQL database and the web server 
            Connection.Close();

            //Return the teacher information
            return NewTeacher;
        }

        ///<summary>
        ///Deletes an teacher from the connected MySQL database if the ID of that teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/6</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "DELETE FROM teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Connection.Close();

        }

        ///<summary>
        ///Adds an teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields to the columns of the teacher's table. Non-Deterministic.</param>
        /// <exmaple>POST api/TeacherData/AddTeacher</exmaple>
        [HttpPost]
        [EnableCors(origins:"*", methods:"*", headers:"*")]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Server side validation not working
            if (NewTeacher.TeacherFname == "" && NewTeacher.TeacherFname == null)
            {
                Debug.WriteLine("test");
            }

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);
            Debug.WriteLine(NewTeacher.TeacherId);

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname) VALUES (@TeacherFname, @TeacherLname)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Connection.Close();

        }
    }
}
