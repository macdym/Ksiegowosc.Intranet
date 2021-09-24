using Ksiegowosc.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Ksiegowosc.Intranet.ViewModels
{
    public class KontrachentViewModel
    {
        public IPagedList<KontrachentDto> Kontrachenci { get; set; }
        public KontrachentDto KontrachentDto { get; set; }
    }
}
