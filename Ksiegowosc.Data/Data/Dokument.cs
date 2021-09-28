using System.ComponentModel.DataAnnotations;

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
