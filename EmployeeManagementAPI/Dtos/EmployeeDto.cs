namespace EmployeeManagementAPI.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public DepartmentDto Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
