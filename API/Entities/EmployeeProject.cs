namespace API.Entities
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int HoursWorked { get; set; }

    }
}
