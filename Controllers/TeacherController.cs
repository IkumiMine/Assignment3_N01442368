using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01442368.Models;
using System.Diagnostics;

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
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            List<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            Debug.WriteLine(id);

            return View(NewTeacher);
        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            //Identify if the method is running
            Debug.WriteLine(id);

            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET: /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }

        //GET: /Teacher/AjaxAdd
        public ActionResult AjaxAdd()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname)
        {
            //Identify if this method is running
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
           

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


    }
}