namespace cLibrary.models.Base
{
    public abstract class FilterBase
    {
        public string SearchText { get; set; }
        public int? PageSize { get; set; }
        public int Skip { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; } = 1;
        public bool CountTotal { get; set; } = true;
    }    
}
