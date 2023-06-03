namespace ECHO.DB.Models
{
    public partial class Procedure
    {
        public int id { get; set; }
        public string? name { get; set; }
        public byte[]? file_content { get; set; }
        public string? file_name { get; set; }
        public string? file_extension { get; set; }
    }
}
