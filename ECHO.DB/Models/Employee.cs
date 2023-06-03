namespace ECHO.DB.Models
{
    public partial class Employee
    {
        public string id { get; set; } = null!;
        public string name { get; set; } = null!;
        public int company_id { get; set; }
    }
}
