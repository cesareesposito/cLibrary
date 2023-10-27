using cLibrary.Enums;

namespace cLibrary.Models
{
    [Serializable]
    public abstract class Filter
    {
        public string? SearchText { get; set; }
        public int? PageSize { get; set; }
        public int Skip { get; set; } = 0;
        public string? SortField { get; set; }
        public cSortDirection SortOrder { get; set; } = cSortDirection.Descending;
        public bool CountTotal { get; set; } = true;

        public static TFilter CreateFilter<TFilter, TViewModel>(TViewModel viewModel) where TFilter : Filter, new()
        {
            var filter = new TFilter();

            var filterProperties = typeof(TFilter).GetProperties();
            var viewModelProperties = typeof(TViewModel).GetProperties();

            foreach (var filterProperty in filterProperties)
            {
                var viewModelProperty = viewModelProperties.FirstOrDefault(p => p.Name == filterProperty.Name);
                if (viewModelProperty != null)
                {
                    var value = viewModelProperty.GetValue(viewModel);
                    filterProperty.SetValue(filter, value);
                }
            }

            return filter;
        }
    }    
}
