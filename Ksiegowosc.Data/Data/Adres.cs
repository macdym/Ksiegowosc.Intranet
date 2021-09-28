using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksiegowosc.Data.Data
{
    public class Adres
    {
        [Key]
        public int IdAdresu { get; set; }
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
        public virtual ICollection<Kontrahent> Kontrahent { get; set; }
    }
}
