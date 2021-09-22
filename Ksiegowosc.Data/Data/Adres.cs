using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegowosc.Data.Data
{
    public class Adres
    {
        [Key]
        public int IdAdresu { get; set; }
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
        public virtual ICollection<Kontrachent> Kontrachent { get; set; }
    }
}
