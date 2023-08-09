using AutoMapper;
using DAL;
using Lab_AutoMapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_AutoMapper.Controllers
{
    public class DepartmentController : Controller
    {

        AppDbContext _db; 
        IMapper _mapper; 
        public DepartmentController(AppDbContext db, IMapper mapper) 
        { 
            _db = db; 
            _mapper = mapper; 
        }
        public IActionResult Index() 
        { 
            var depts = _db.Departments.Select(p => p).ToList(); 
            var model = _mapper.Map<IEnumerable<DepartmentViewModel>>(depts); 
            return View(model); 
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel model)
        {
            ModelState.Remove("DepartmentId");
            if (ModelState.IsValid)
            {
                Department department = new Department
                {
                    Name = model.Name
                };
                _db.Departments.Add(department);


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            Department data = _db.Departments.Find(id);
            if (data != null)
            {
                model = new DepartmentViewModel
                {
                    DepartmentId = data.DepartmentId,


                    Name = data.Name
                };
            }
            return View("Create", model);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department
                {
                    DepartmentId = model.DepartmentId,
                    Name = model.Name
                };
                _db.Departments.Update(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            Department model = _db.Departments.Find(id);
            if (model != null)
            {
                _db.Departments.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
