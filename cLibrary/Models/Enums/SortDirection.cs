using cLibrary.Attributes;

namespace cLibrary.Models.Enums
{
    public enum SortDirection
    {
        [StringValue("")]
        Ascending = 0,
        [StringValue("desc")]
        Descending = 1
    }    
}
