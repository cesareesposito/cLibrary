namespace cLibrary.Models
{
    public class DataSource<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
