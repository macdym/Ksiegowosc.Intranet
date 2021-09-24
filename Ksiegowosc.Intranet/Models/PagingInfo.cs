using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class PagingInfo
    {
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }
}
