using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.controllers
{
    //[Route("Home")]
    [Route("Sepide/[controller]/[action]")]
    public class HomeController : Controller
    {

        // This process called constructor injection resdonly prevents accidently assing another value to it
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("~/Home")]
        [Route("~/")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Index")] //we dont need action here bec we mentioned it as token above the controller
        public ViewResult Index()
        {
            //return _employeeRepository.GetEmployee(1).Name;
            var model = _employeeRepository.getAllEmployees();
            return View(model);
        }

        //ObjectResult used in web API
        //public ObjectResult Details()
        //public JsonResult Details()
        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(2);
            // we dont want to return an object to be shown in MVC we specify view method ourselves its not aAPI app its web application
            // so we use view method to disply info in return
            //return new ObjectResult(model);
            //return Json(model);

            //to just show a specific view file not default details file
            //return View("test");

            // To specify a specific file in specific folder we need an absolute path with file extension  
            // when we myViews in in project folder
            //return View("~/MyViews/test.cshtml");

            // To specify a specific file in specific folder we can use reletive path with file extension 
            // when MyVieews foldr is in views filder in reletive path we dont need extrension
            //return View("../MyViews/test");

            ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details";
            return View();
        }

        
        public ViewResult DetailsViewBag()
        {
            Employee model = _employeeRepository.GetEmployee(2);
            // "Employee" and "PageTitle" are string keys

            //in view bag we use dynamic properties . and the name of dynamic property .Employee and .PageTitle
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";
            return View();
        }



        public ViewResult DetailsStronglyTyped()
        {
            Employee model = _employeeRepository.GetEmployee(2);
            // "Employee" and "PageTitle" are string keys

            //in view bag we use dynamic properties . and the name of dynamic property .Employee and .PageTitle
            ViewBag.PageTitle = "Employee Details";
            return View(model);
        }



        [Route("{id?}")]
        public ViewResult DetailsModelView(int? id)
        {
            HomeDetailsViewModel homeDetailesViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };

            return View("~/Views/Home/DetailsModelView.cshtml", homeDetailesViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid) {
                string uniqueFileName = ProceesUploadedFile(model);
                /*If we have many photoes
                 * 
                 * if(model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uplaodFol = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" +  photo.FileName;
                        string filePath = Path.Combine(uplaodFol, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    
                }*/
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("DetailsModelView", new { id = newEmployee.Id });
            }

            return View(); //View retun ViewResult RedirectToAction RedirectToActionResult both implement from IActionResult
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExinstingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.PhotoPath = model.ExinstingPhotoPath;
                if (model.Photo != null)
                {
                    employee.PhotoPath = ProceesUploadedFile(model);
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }

            return View(); //View retun ViewResult RedirectToAction RedirectToActionResult both implement from IActionResult
        }

        private string ProceesUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uplaodFol = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uplaodFol, uniqueFileName);
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

            }

            return uniqueFileName;
        }
    }
}
