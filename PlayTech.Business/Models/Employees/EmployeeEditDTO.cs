using PlayTech.Shared.DTOs;

namespace PlayTech.Business.Models.Employees
{
    public class EmployeeEditDTO : BaseDTO
    {
        public int? DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
    }
}
