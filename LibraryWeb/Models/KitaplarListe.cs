using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWeb.Models
{
    public class KitaplarListe
    {
        public int ID { get; set; }
        [Required (ErrorMessage =" * Zorunlu Alan")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter giriniz")]
        public string KitapAdi { get; set; }
        [Required(ErrorMessage = " * Zorunlu Alan")]
        [StringLength(40, ErrorMessage = "En fazla 40 karakter giriniz")]
        public string Yazar { get; set; }
        public int? SayfaNo { get; set; }
        [StringLength(30, ErrorMessage = "En fazla 30 karakter giriniz")]
        public string  Turu { get; set; }
        [StringLength(25, ErrorMessage = "En fazla 25 karakter giriniz")]
        public string Yayinevi { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AldigimTarih { get; set; }
        public bool? Okundu { get; set; }

    }
}