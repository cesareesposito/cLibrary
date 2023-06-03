namespace ECHO.DB.Models
{
    public partial class TimePeriod
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public int term_type { get; set; }
        public int value { get; set; }
    }
}
