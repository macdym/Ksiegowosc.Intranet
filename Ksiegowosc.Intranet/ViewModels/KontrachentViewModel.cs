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
        public PagedList<KontrachentDto> Kontrachenci { get; set; }
    }
}
