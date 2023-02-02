using ExamRetest.Data;
using ExamRetest.Models;
using ExamRetest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamRetest.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _db;

		public EmployeeController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
        {
            return View();
        }

		//GET
		public IActionResult Upsert(int? id)
		{
			EmployeeVM employeeVM = new()
			{
				employee = new(),
				roleList = _db.roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList()
			};
			if (id == 0 || id == null)
			{
				return View(employeeVM);
			}
			else
			{
				employeeVM.employee = _db.employees.Find(id);
				return View(employeeVM);
			}

		}

		[HttpPost]
		public IActionResult Upsert(EmployeeVM obj,int? id, int[]? selectedIds)
		{
			if (id == 0 || id == null)
			{
				_db.employees.Add(obj.employee);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				_db.employees.Update(obj.employee);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
		}

		public IActionResult Delete(int id)
		{
			if (ModelState.IsValid)
			{
				var employeefromdb = _db.employees.Find(id);
				_db.employees.Remove(employeefromdb);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}


		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var categorylist = _db.employees.ToList();
			var modifiedCategoryList = categorylist.Select(c => new { c.EmployeeId, c.Name, c.Description, IsActive = c.IsActive ? "Active" : "InActive", c.CreatedOn, c.ModifiedOn });
			return Json(new { data = modifiedCategoryList }); ;
		}
		#endregion
	}
}
