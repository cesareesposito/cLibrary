using cLibrary.Helper;
using cLibrary.Models.Attributes;

namespace cLibrary.Models.Enums
{
    public enum SortDirection
    {
        [StringValue("")]
        Ascending = 0,
        [StringValue("desc")]
        Descending = 1
    }
    //[Serializable]
    //public class SortDirection : cEnum
    //{
    //    public static SortDirection Ascending = new SortDirection("", "Ascending");
    //    public static SortDirection Descending = new SortDirection("desc", "Descending");

    //    public SortDirection() { }

    //    public SortDirection(string id, string label)
    //        : base(id, label)
    //    {
    //    }
    //}
}
