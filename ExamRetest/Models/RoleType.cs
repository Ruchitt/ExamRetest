﻿using System.ComponentModel.DataAnnotations;

namespace ExamRetest.Models
{
    public class RoleType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
		public List<RoleType> Roles { get; set; }

	}
}
