namespace cLibrary.Models
{
    public class DataSource<T>
    {
        public DataSource()
        {
            Items = new List<T>();
        }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
