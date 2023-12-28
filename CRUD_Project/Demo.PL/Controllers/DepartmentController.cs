using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{

    //Inheritance : DepartmentController is a Controller
    // Composition : DepartmentController is a DepartmentRepository
    [Authorize]
    public class DepartmentController : Controller

    {
        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork /*IDepartmentRepository departmentRepositor*/)
        {

            //_departmentRepo = departmentRepository;
            _unitOfWork = unitOfWork;
  
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department newDepartment)
        {
            if (ModelState.IsValid) //Server Side Validation
            {

                _unitOfWork.DepartmentRepository.Add(newDepartment);
                 var Count =_unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(newDepartment);
        }

        public IActionResult Details(int? id , string viewName = "Details")
        {
             if(id == null)
                return BadRequest(); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if(department == null)
                return NotFound();//404

            return View(viewName,department);

        }

        public IActionResult Edit(int? id)
        {
            //if(id is null)
            //    return BadRequest();
            //var department = _departmentRepo.Get(id.Value);

            //if (department == null)
            //    return NotFound();

            //return View(department);
            return Details(id , "Edit");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit( [FromRoute]int id ,Department newDepartment)
        {

            if(id != newDepartment.Id)
                return BadRequest();


            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(newDepartment);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    // 1.Log Exception
                    //2.Friend Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                  
                }

            }
            return View(newDepartment);
        }


        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch(System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);

            }
         
        }




    }
}
