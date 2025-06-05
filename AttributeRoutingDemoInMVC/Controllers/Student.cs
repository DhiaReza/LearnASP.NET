using AttributeRoutingDemoInMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttributeRoutingDemoInMVC.Controllers
{
    public class Student : Controller
    {
        static List<Models.Student> students = new List<Models.Student>()
        {
            new Models.Student() { Id = 1, Name = "Pranaya" },
            new Models.Student() { Id = 2, Name = "Priyanka" },
            new Models.Student() { Id = 3, Name = "Anurag" },
            new Models.Student() { Id = 4, Name = "Sambit" }
        };

        [HttpGet]
        public ActionResult GetAllStudents()
        {
            return View(students);
        }

        [HttpGet]
        public ActionResult GetStudentByID(int studentID)
        {
            Models.Student studentDetails = students.FirstOrDefault(s => s.Id == studentID);
            return View(studentDetails);
        }

        [HttpGet]
        public ActionResult GetStudentCourses(int studentID)
        {
            List<string> CourseList = new List<string>();

            if (studentID == 1)
                CourseList = new List<string>() { "ASP.NET", "C#.NET", "SQL Server" };
            else if (studentID == 2)
                CourseList = new List<string>() { "ASP.NET MVC", "C#.NET", "ADO.NET" };
            else if (studentID == 3)
                CourseList = new List<string>() { "ASP.NET WEB API", "C#.NET", "Entity Framework" };
            else
                CourseList = new List<string>() { "Bootstrap", "jQuery", "AngularJs" };

            ViewBag.CourseList = CourseList;

            return View();
        }
    }
}