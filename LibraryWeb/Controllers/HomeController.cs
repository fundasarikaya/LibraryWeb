using LibraryWeb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LibraryWeb.Controllers
{
    public class HomeController : Controller
    {
        MyLibraryEntities db;
        public ActionResult Index(KitaplarModel model)
        {
            //modelden gelen page e gore deger atadık eger bos ise 1 degerini atadık
            int pageIndex = model.Page ?? 1;
            db = new MyLibraryEntities();
            model.Kitaplar = (from kitap in db.Mylibrary.Where(x =>
              (String.IsNullOrEmpty(model.KitapAdi) || x.KitapAdi.Contains(model.KitapAdi)) &&
              (String.IsNullOrEmpty(model.Yazar) || x.Yazar.Contains(model.Yazar))).OrderBy(x => x.ID)
                              select new KitaplarListe
                              {
                                  KitapAdi=kitap.KitapAdi,
                                  Yazar=kitap.Yazar,
                                  Yayinevi=kitap.Yayinevi,
                                  SayfaNo=kitap.SayfaNo,
                                  AldigimTarih=kitap.AldigimTarih,
                                  Turu=kitap.Turu,
                                  ID=kitap.ID,
                                  Okundu=kitap.Okundu
                                  
                                  
                                  
                              }).OrderByDescending(x=>x.ID ).ToPagedList(pageIndex,5);
            //istek ajx üzerinden gelirse yanı sayfanın yenilenmesine gerek yoksa yani ilk kez acılmamışsa
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Kitaplar",model);
            }
            //sayfa ilk kez acılıyorsa yani istek ajax degilse
            else
            {
                return View(model);
            }
            //return View(GetKitaplar());
        }
       
       
        #region Ekle
        public ActionResult Ekle()
        {
            KitaplarListe model = new KitaplarListe();
            return View(model);
        }
        //parametredeki modeli degiitidim
        [HttpPost]
        public JsonResult Ekle(KitaplarModel model, string KitapOkundu)
        {
            JsonResultModel jmodel=new JsonResultModel();
            try
            {
                MyLibraryEntities db = new MyLibraryEntities();
                //Mylibrary kitaplik = new Mylibrary();
                Mylibrary kitaplik = null;
                if (model.KitapList.ID > 0)
                {
                    kitaplik = db.Mylibrary.FirstOrDefault(x => x.ID == model.KitapList.ID);
                }
                else
                {
                    kitaplik = new Mylibrary();
                }
                kitaplik.KitapAdi = model.KitapList.KitapAdi;
                kitaplik.Yazar = model.KitapList.Yazar;
                kitaplik.SayfaNo = model.KitapList.SayfaNo;
                kitaplik.Yayinevi = model.KitapList.Yayinevi;
                kitaplik.Turu = model.KitapList.Turu;
                kitaplik.Okundu = KitapOkundu == "E" ? true : false;
                kitaplik.AldigimTarih =model.KitapList.AldigimTarih;
                if (model.KitapList.ID == 0)
                {
                    db.Mylibrary.Add(kitaplik);
                }
                //db.Mylibrary.Add(kitaplik);
                db.SaveChanges();
                jmodel.IsSuccess = true;
                jmodel.UserMessage = "İşlem Başarılı.";
                
               
            }
            catch (Exception ex)
            {
                jmodel.IsSuccess = false;
                jmodel.UserMessage = "Hata:" + ex.Message;
            }
            return Json(jmodel, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region sil
        [HttpPost]
        public string Sil(int id)
        {
            try
            {
                db = new MyLibraryEntities();
                var kitap = db.Mylibrary.FirstOrDefault(x => x.ID == id);
                db.Mylibrary.Remove(kitap);
                db.SaveChanges();
                //sectigin satıları try catch icine almak icin ks tuslarına bas try sec
                return "1";
            }
            catch 
            {
                return "-1";
            }
        }
        #endregion
      [HttpPost]
      public JsonResult KitapIDGetir(int id)
        {
            db = new MyLibraryEntities();
            Mylibrary kitap = db.Mylibrary.FirstOrDefault(x => x.ID == id);
            KitaplarListe model = new KitaplarListe();
           
            model.KitapAdi = kitap.KitapAdi;
            model.Yazar = kitap.Yazar;
            model.SayfaNo = kitap.SayfaNo;
            model.Turu = kitap.Turu;
            model.Yayinevi = kitap.Yayinevi;
            model.AldigimTarih =kitap.AldigimTarih;
            model.Okundu = kitap.Okundu;
            model.ID = kitap.ID;
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}