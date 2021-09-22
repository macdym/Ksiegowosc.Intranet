using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegowosc.Data.Data
{
    public class Dokument
    {
        [Key]
        public int IdDokumentu { get; set; }
        public string NazwaDokumentu { get; set; }
        public string UrlDokumentu { get; set; }
    }
}
