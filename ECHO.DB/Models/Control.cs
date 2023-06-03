namespace ECHO.DB.Models
{
    public partial class Control
    {
        public int id { get; set; }
        public string user_id { get; set; } = null!;
        public DateTime date { get; set; }
        public int control_type_id { get; set; }
        public string? value { get; set; }
    }
}
