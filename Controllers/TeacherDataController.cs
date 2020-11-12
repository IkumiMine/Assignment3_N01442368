using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3_N01442368.Models;
using MySql.Data.MySqlClient;

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
        [Route("api/TeacherData/ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers";

            //Gather Result Set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row the Result Set
            while (ResultSet.Read())
            {
                //Access column information by DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
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
        /// <example>GET: api/TeacherData/FindTeacher/{teacherid}</example>
        /// <return>
        /// information of a teacher (first name, last name, teacher ID, hire date, salary, class, class date)
        /// </return>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        //[Route("api/TeacherData/FindTeacger/{TeacherFname}")]
        public Teacher Findteacher(int? TeacherId)
        //public Teacher Findteacher(string? TeacherFname)
        {
            Teacher TeacherInfo = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT teachers.teacherid, teacherfname, teacherlname, DATE_FORMAT(hiredate,'%M %d %Y') AS hiredate, salary, classname, DATE_FORMAT(startdate, '%M %d %Y') AS startdate, DATE_FORMAT(finishdate, '%M %d %Y') AS finishdate FROM teachers JOIN classes ON teachers.teacherid = classes.teacherid WHERE teachers.teacherid =" + TeacherId;
            //cmd.CommandText = "SELECT teachers.teacherid, teacherfname, teacherlname, DATE_FORMAT(hiredate,'%M %d %Y') AS hiredate, salary, classname, DATE_FORMAT(startdate, '%M %d %Y') AS startdate, DATE_FORMAT(finishdate, '%M %d %Y') AS finishdate FROM teachers JOIN classes ON teachers.teacherid = classes.teacherid WHERE teachers.teacherfname =" + TeacherFname;

            //Gather Result Set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row the Result Set
            while (ResultSet.Read())
            {
                //Access column information by DB column name as an index
                //int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                decimal Salary = (decimal)ResultSet["salary"];
                string ClassName = ResultSet["classname"].ToString();
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();


                //assign the column information to the fields created in Teacher.cs
                //TeacherInfo.TeacherId = TeacherId;
                TeacherInfo.TeacherFname = TeacherFname;
                TeacherInfo.TeacherLname = TeacherLname;
                TeacherInfo.HireDate = HireDate;
                TeacherInfo.Salary = Salary;
                TeacherInfo.ClassName = ClassName;
                TeacherInfo.StartDate = StartDate;
                TeacherInfo.FinishDate = FinishDate;

            }

            //Close the connection between the MySQL database and the web server 
            Connection.Close();

            //Return the teacher information
            return TeacherInfo;
        }
    }
}
