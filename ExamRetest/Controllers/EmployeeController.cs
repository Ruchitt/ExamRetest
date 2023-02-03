using ExamRetest.Data;
using ExamRetest.Models;
using ExamRetest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

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
				var roleids = _db.emproles.Where(r => r.EmployeeId == id).Select(a => a.RoleId).ToList();
				employeeVM.roleids = roleids;
				employeeVM.employee = _db.employees.Find(id);
				return View(employeeVM);
			}

		}

		[HttpPost]
		public IActionResult Upsert(EmployeeVM obj, int[]? selectedIds)
		{
			if (obj.employee.EmployeeId == 0 || obj.employee.EmployeeId == null)
			{
				_db.employees.Add(obj.employee);
				_db.SaveChanges();
				int eid = obj.employee.EmployeeId;
				foreach(int roleId in selectedIds)
				{
					if(!_db.emproles.Any(r=>r.EmployeeId == eid && r.RoleId == roleId))
					{
						EmployeeeRoles employeeeRole = new EmployeeeRoles
						{
							EmployeeId = eid,
							RoleId = roleId
						};
						_db.Add(employeeeRole);
						_db.SaveChanges();
					}
				}
                return RedirectToAction("Index");
            }
            else
			{
				
				if (selectedIds != null)
				{
					var eid = obj.employee.EmployeeId;
					var emprolesid = _db.emproles.Where(x => x.EmployeeId == eid);
					_db.emproles.RemoveRange(emprolesid);
					foreach (int roleId in selectedIds)
					{
						//if (!_db.emproles.Any(r => r.EmployeeId == eid && r.RoleId == roleId))
						//{
							EmployeeeRoles employeeeRole = new EmployeeeRoles
							{
								EmployeeId = eid,
								RoleId = roleId
							};
							_db.Add(employeeeRole);
							_db.SaveChanges();

						//}
					}

				}
				_db.employees.Update(obj.employee);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}
		}

		public IActionResult Delete(int id)
		{
			if (ModelState.IsValid)
			{
				var emprolesid = _db.emproles.Where(x => x.EmployeeId == id);
				_db.emproles.RemoveRange(emprolesid);
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
			var rolelist = _db.roles.ToList();
            var employeelist = _db.employees.ToList();
            var modifiedCategoryList = employeelist.Select(c => new { c.EmployeeId, c.Name, c.Description,c.Salary, IsActive = c.IsActive ? "Active" : "InActive", c.CreatedOn, c.ModifiedOn });

			return Json(new { data = modifiedCategoryList }); ;
		}
		#endregion
	}










//var rolenames = from e in _db.employees
			//				join er in _db.emproles on e.EmployeeId equals er.EmployeeId
			//				join r in _db.roles on er.RoleId equals r.Id
			//				select new EmployeeVM
			//				{
			//					rolenames = new List<RoleType>{r}
			//	};
}
