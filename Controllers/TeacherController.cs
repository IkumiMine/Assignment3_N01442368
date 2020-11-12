using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01442368.Models;

//#nullable enable

//Code References
//Professor Christine Bittle
//BlogProject:retrieved from https://github.com/christinebittle/BlogProject_2 on November 12, 2020 for building MVP to provide teachers' data from MySql database using WebAPI and MVC. 
//SeasonApp: retrieved from https://github.com/christinebittle/SeasonApp on November 12,2020 for building a search interface to find a teacher

namespace Assignment3_N01442368.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            List<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        //POST: /Teacher/Show/{TeacherId}
        [HttpPost]
        public ActionResult Show(int? TeacherId)
        //public ActionResult Show(string TeacherFname)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher TeacherInfo = controller.Findteacher(TeacherId);
            return View(TeacherInfo);
        }

    }
}