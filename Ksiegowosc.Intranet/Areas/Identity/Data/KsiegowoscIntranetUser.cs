using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ksiegowosc.Intranet.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the KsiegowoscIntranetUser class
    public class KsiegowoscIntranetUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName="nvarchar(100)")]
        public string Imie { get; set; }
        [Column(TypeName="nvarchar(100)")]
        [PersonalData]
        public string Nazwisko { get; set; }
    }
}
