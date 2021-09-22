using Ksiegowosc.Data;
using Ksiegowosc.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet
{
    public class KsiegowoscIntranetSeeder
    {
        private readonly KsiegowoscDbContext _dbContext;

        public KsiegowoscIntranetSeeder(KsiegowoscDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Kontrachenci.Any())
                {
                    var kontrachenci = GetKontrachenci();
                    _dbContext.Kontrachenci.AddRange(kontrachenci);
                    _dbContext.SaveChanges();
                }
            }
        }
        #region GetKontrachenci
        private IEnumerable<Kontrachent> GetKontrachenci()
        {
            var result = new List<Kontrachent>()
            {
                new Kontrachent()
                {
                    NipLubPesel="12345678901",
                    Nazwa="Firma 1",
                    SkrotNazwy="F1",
                    PlatnikVat=true,
                    Dostawca = true,
                    Odbiorca = false,
                    Zalezny = true,
                    Adres = new Adres()
                    {
                        Ulica="Ulica 1",
                        Miasto = "Miasto 1",
                        KodPocztowy = "Kodpocztowy1"
                    }
                },
                new Kontrachent()
                {
                    NipLubPesel="12345678901",
                    Nazwa="Firma 2",
                    SkrotNazwy="F2",
                    PlatnikVat=true,
                    Dostawca = true,
                    Odbiorca = false,
                    Zalezny = true,
                    Adres = new Adres()
                    {
                        Ulica="Ulica 2",
                        Miasto = "Miasto 2",
                        KodPocztowy = "Kodpocztowy2"
                    }
                },
                new Kontrachent()
                {
                    NipLubPesel="12345678901",
                    Nazwa="Firma 3",
                    SkrotNazwy="F3",
                    PlatnikVat=true,
                    Dostawca = false,
                    Odbiorca = true,
                    Zalezny = true,
                    Adres = new Adres()
                    {
                        Ulica="Ulica 3",
                        Miasto = "Miasto 3",
                        KodPocztowy = "Kodpocztowy3"
                    }
                },
                new Kontrachent()
                {
                    NipLubPesel="10987654321",
                    Nazwa="Osoba 1",
                    SkrotNazwy="O1",
                    PlatnikVat=false,
                    Dostawca = false,
                    Odbiorca = true,
                    Zalezny = false,
                    Adres = new Adres()
                    {
                        Ulica="Ulica 4",
                        Miasto = "Miasto 4",
                        KodPocztowy = "Kodpocztowy4"
                    }
                },
            };
            return result;
        }
        #endregion
    }
}
