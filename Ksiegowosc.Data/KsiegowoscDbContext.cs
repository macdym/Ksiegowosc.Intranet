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
        public DbSet<Kontrachent> Kontrachenci { get; set; }
        public DbSet<Dokument> Dokumenty { get; set; }
        public DbSet<Adres> Adres { get; set; }
        public DbSet<DokumentKontrachenta> DokumentyKontrachenta { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }    
}
