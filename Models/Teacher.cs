using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_N01442368.Models
{
    public class Teacher
    {
        //The following fields define a Teacher
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public DateTime HireDate;
        public decimal Salary;
        public string ClassName;
        public DateTime StartDate;
        public DateTime FinishDate;

        public Teacher() { }

    }
}