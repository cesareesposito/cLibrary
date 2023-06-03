namespace ECHO.DB.Models
{
    public partial class ControlType
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public int? time_id { get; set; }
        public int? procedure_id { get; set; }
        public string ValueType { get; set; } = null!;
    }
}
