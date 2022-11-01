using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        MyContext myContext;

        public DepartmentController(MyContext myContext) 
        {
            this.myContext = myContext;
        }

        //GET ALL
        public IActionResult Index()
        {
            var data = (from dept in myContext.Departments
                        join div in myContext.Divisions on dept.DivisionId equals div.Id
                        select new DepartmentViewModel() { Id = dept.Id, DepartmentName = dept.Name, DivisionName = div.Name }).ToList();

            return View(data);
        }

        //GET BY ID
        public IActionResult Details(int id)
        {
            //var data = (from dept in myContext.Departments
            //           join div in myContext.Divisions on dept.DivisionId equals div.Id
            //           where dept.Id == id
            //           select new DepartmentViewModel() { Id = dept.Id, DepartmentName = dept.Name, DivisionName = div.Name }).SingleOrDefault();
            
            var data = myContext.Departments
                .Include(x => x.Division)
                .SingleOrDefault();
            DepartmentViewModel departmentViewModel = new DepartmentViewModel()
            {
                Id = data.Id,
                DepartmentName = data.Name,
                DivisionName = data.Division.Name
            };

            return View(departmentViewModel);
        }

        //INSERT - GET POST
        public IActionResult Create()
        {
            //var department = new DepartmentViewModel();
            //department.Divisions = new SelectList(myContext.Divisions, nameof(Division.Id), nameof(Division.Name));
            //get data disini
            // ex: dropdown data didapat dari database
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {            
            var department = (from div in myContext.Divisions
                            where div.Name == departmentViewModel.DivisionName
                            select new Department(){ Name = departmentViewModel.DepartmentName, DivisionId = div.Id}).SingleOrDefault();
            myContext.Departments.Add(department);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Department");
            return View();
        }

        // UPDATE - GET POST
        public IActionResult Edit(int id)
        {
            var data = (from dept in myContext.Departments
                        join div in myContext.Divisions on dept.DivisionId equals div.Id
                        where dept.Id == id
                        select new DepartmentViewModel() { Id = dept.Id, DepartmentName = dept.Name, DivisionName = div.Name }).SingleOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentViewModel departmentViewModel)
        {
            var department = (from div in myContext.Divisions
                              where div.Name == departmentViewModel.DivisionName
                              select new Department() { Name = departmentViewModel.DepartmentName, DivisionId = div.Id }).SingleOrDefault();
            
            var data = myContext.Departments.Find(departmentViewModel.Id);
            if (data != null)
            {
                data.Name = department.Name;
                data.DivisionId = department.DivisionId;                
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Department");
            }
            return View();
        }

        // DELETE - GET POST
        public IActionResult Delete(int id)
        {
            var data = (from dept in myContext.Departments
                        join div in myContext.Divisions on dept.DivisionId equals div.Id
                        where dept.Id == id
                        select new DepartmentViewModel() { Id = dept.Id, DepartmentName = dept.Name, DivisionName = div.Name }).SingleOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentViewModel departmentViewModel)
        {
            var department = myContext.Departments.SingleOrDefault(x => x.Id == departmentViewModel.Id);
            myContext.Departments.Remove(department);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Department");
            return View();
        }
    }
}
