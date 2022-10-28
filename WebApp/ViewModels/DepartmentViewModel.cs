using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionName { get; set; }
        //public SelectList Divisions { get; set; }
    }
}
