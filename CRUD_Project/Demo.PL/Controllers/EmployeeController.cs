using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork/*IEmployeeRepository employeeRepo*/ /*, IDepartmentRepository departmentRepo*/)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_employeeRepo = employeeRepo;
            //_departmentRepo = departmentRepo;
        }


        
        public IActionResult Index( string SearchInp)
        {
            ////Binding Through View's Dictionary: Tranfer Data from Action To view
            //1. ViewData
            //2. ViewBag

            //ViewData["Message"] = "Hello ViewData";
            //ViewBag.Message = "Hello ViewBag";

            var Employees = Enumerable.Empty<Employee>();

            if(string.IsNullOrEmpty(SearchInp) )
            {
                Employees = _unitOfWork.EmployeeRepository.GetAll();
         
            }
            else
            {
                Employees = _unitOfWork.EmployeeRepository.SearchByName(SearchInp.ToLower()); 
            }
            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);

            return View(mappedEmps);
        }

        public IActionResult Create()
        {
      

            return View();
        }


        [HttpPost]
        public IActionResult Create(EmployeeViewModel newEmployee)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
               newEmployee.ImageName =  DocumentSettings.UploadFile(newEmployee.Image, "images");
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(newEmployee);

                _unitOfWork.EmployeeRepository.Add(mappedEmp);
                  var Count = _unitOfWork.Complete();

                // 3. TempData

                if (Count > 0)

                    TempData["Message"] = "Department is Created Successfully";


                else

                    TempData["Message"] = " OOPS ...Department is not Created :(";

                _unitOfWork.Complete();
                
                return RedirectToAction(nameof(Index));

            }
            return View(newEmployee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest(); //400

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            var mappedemp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee == null)
                return NotFound();

            return View(viewName, mappedemp);

        }

        public IActionResult Edit(int? id)
        {
        
            return Details(id, "Edit");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute]int id, EmployeeViewModel empVM)
        {
            var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

            if (id != empVM.Id)
                return BadRequest();


            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    // 1.Log Exception
                    //2.Friend Message
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(empVM);
        }


        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel Employee)
        {
            var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(Employee);
            if (id != Employee.Id)
                return BadRequest();
            try
            {
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                    DocumentSettings.DeleteFile(Employee.ImageName, "images");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Employee);

            }

        }

    }
}
