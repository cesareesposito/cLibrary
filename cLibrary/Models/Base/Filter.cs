﻿using cLibrary.Models.Enums;

namespace cLibrary.Models.Base
{
    public abstract class Filter
    {
        public string SearchText { get; set; }
        public int? PageSize { get; set; }
        public int Skip { get; set; }
        public string SortField { get; set; }
        public SortDirection SortOrder { get; set; } = SortDirection.Descending;
        public bool CountTotal { get; set; } = true;
    }    
}