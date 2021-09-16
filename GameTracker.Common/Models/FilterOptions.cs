using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace GameTracker.Common.Models
{
    public class FilterOptions
    {
        public string SearchTerm { get; set; }

        public SortOrder Order { get; set; }
    }
}
