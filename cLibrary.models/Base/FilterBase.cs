using cLibrary.models.Enums;

namespace cLibrary.models.Base
{
    public abstract class FilterBase
    {
        public string SearchText { get; set; }
        public int? PageSize { get; set; }
        public int Skip { get; set; }
        public string SortField { get; set; }
        public SortDirection SortOrder { get; set; } = SortDirection.Descending;
        public bool CountTotal { get; set; } = true;
    }    
}
