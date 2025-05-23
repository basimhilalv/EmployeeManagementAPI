﻿namespace EmployeeManagementAPI.Dtos
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}
