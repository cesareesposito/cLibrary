using cLibrary.Helper;

namespace cLibrary.Models.Enums
{
    //public enum SortDirection
    //{
    //    Ascending = 0,
    //    Descending = 1
    //}
    public class SortDirection : cEnum
    {
        public static SortDirection Ascending = new SortDirection("", "Ascending");
        public static SortDirection Descending = new SortDirection("desc", "Descending");

        public SortDirection() { }

        public SortDirection(string id, string label)
            : base(id, label)
        {
        }
    }
}
