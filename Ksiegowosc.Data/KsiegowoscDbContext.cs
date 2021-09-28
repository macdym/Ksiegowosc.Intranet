using Ksiegowosc.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegowosc.Data
{
    public class KsiegowoscDbContext : DbContext
    {
        public KsiegowoscDbContext(DbContextOptions<KsiegowoscDbContext> options):base(options)
        {
        }
        public DbSet<Kontrahent> Kontrahenci { get; set; }
        public DbSet<Dokument> Dokumenty { get; set; }
        public DbSet<Adres> Adres { get; set; }
        public DbSet<DokumentKontrahenta> DokumentyKontrahenta { get; set; }
    }    
}
