using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MY_Ticari.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int PersonelID { get; set; }


        [Display(Name ="Personel Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelAd { get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelSoyad { get; set; }

        [Display(Name = "Görsel")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersoelGorsel { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

        public int DepartmanID { get; set; }
        public virtual Departman Departman { get; set; }
    }
}