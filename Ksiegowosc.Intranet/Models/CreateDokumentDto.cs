using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class CreateDokumentDto
    {
        public string NazwaDokumentu { get; set; }
        public IFormFile Dokument { get; set; }
    }
}
