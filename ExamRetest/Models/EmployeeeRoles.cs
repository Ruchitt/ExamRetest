using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamRetest.Models
{
	public class EmployeeeRoles
	{
		[Key]
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public int RoleId { get; set; }
	}
}
