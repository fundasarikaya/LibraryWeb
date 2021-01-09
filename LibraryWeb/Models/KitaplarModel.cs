using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace LibraryWeb.Models
{
    public class KitaplarModel
    {
        //arama kısmında neye gore aranacak alanların belirtilmesi ve sayfalama icin tutulacak sayfa numarası modeli oluştur
        public string KitapAdi { get; set; }
        public string Yazar { get; set; }
        public int? Page { get; set; }
        public IPagedList<KitaplarListe> Kitaplar { get; set; }
        public KitaplarListe KitapList { get; set; }
    }
}