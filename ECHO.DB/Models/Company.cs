namespace ECHO.DB.Models
{
    public partial class Company
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public bool is_delete { get; set; }
    }
}
