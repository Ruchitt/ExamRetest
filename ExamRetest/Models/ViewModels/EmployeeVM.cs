using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamRetest.Models.ViewModels
{
	public class EmployeeVM
	{
		public EmployeeDetail employee { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> roleList { get; set; }
		public List<int> roleids { get; set; }
        public List<RoleType> rolenames { get; set; }

    }
}
