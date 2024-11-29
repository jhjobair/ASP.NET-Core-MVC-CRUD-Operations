using ASPNETMVCCRUD.Data;
using AspNetMvcCrud2.Models;
using AspNetMvcCrud2.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AspNetMvcCrud2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbContext mVCDemoDbContext;

        public EmployeeController(MVCDemoDbContext mVCDemoDbContext)
        {
            this.mVCDemoDbContext = mVCDemoDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employee = mVCDemoDbContext.Employees.ToList();
            return View(employee);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel addEmployeeViewModel)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeViewModel.Name,
                Email = addEmployeeViewModel.Email,
                Salary = addEmployeeViewModel.Salary,
                Department = addEmployeeViewModel.Department,
                DateOfBirth = addEmployeeViewModel.DateOfBirth,

            };

            mVCDemoDbContext.Employees.Add(employee);
            mVCDemoDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            UpdateEmployeeViewModel viewModel = new UpdateEmployeeViewModel();
            var employee = mVCDemoDbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {

                viewModel.Name = employee.Name;
                viewModel.Email = employee.Email;
                viewModel.Salary = employee.Salary;
                viewModel.Department = employee.Department;
                viewModel.DateOfBirth = employee.DateOfBirth;
                viewModel.Id = employee.Id;
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(UpdateEmployeeViewModel model)
        {
            var employee = mVCDemoDbContext.Employees.Find(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
                mVCDemoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Delete(UpdateEmployeeViewModel model) 
        {
            var employee = mVCDemoDbContext.Employees.Find(model.Id);
            if (employee != null) 
            {
            mVCDemoDbContext.Employees.Remove(employee);
                mVCDemoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
