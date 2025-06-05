using Microsoft.AspNetCore.Mvc;

namespace AttributeRoutingDemoInMVC.Models
{
    public class Student : Controller
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
