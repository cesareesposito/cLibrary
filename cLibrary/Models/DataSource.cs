namespace cLibrary.Models
{
    public class DataTable<T>
    {
        public DataTable()
        {
            Items = new List<T>();
        }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
