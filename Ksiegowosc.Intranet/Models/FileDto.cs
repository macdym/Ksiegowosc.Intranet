using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class FileDto
    {
        public byte[] fileBytes { get; set; }
        public string fileName { get; set; }
        public Stream fileStream { get; set; }
    }
}
