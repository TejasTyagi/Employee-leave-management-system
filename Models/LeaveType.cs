//noahb
//description: class for LeaveType model
namespace Employee_leave_management_system.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; }
        public double AccrualRatePerMonth { get; set; }
    }
}
