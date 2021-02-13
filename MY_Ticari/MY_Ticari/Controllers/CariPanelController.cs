using MY_Ticari.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MY_Ticari.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x=>x.Alici==mail).ToList();
            ViewBag.m = mail;

            var mailId = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.mid = mailId;

            var toplamSatis = c.SatisHarekets.Where(x => x.CariID == mailId).Count();
            ViewBag.top = toplamSatis;

            var toplamTutar = c.SatisHarekets.Where(x => x.CariID == mailId).Sum(y => y.ToplamTutar);
            ViewBag.tutar = toplamTutar;

            var toplamUrun = c.SatisHarekets.Where(x => x.CariID == mailId).Sum(y => y.Adet);
            ViewBag.urun = toplamUrun;

            var adSoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adSoyad;

            

            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.CariID == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x=>x.Alici==mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;

            return View(degerler);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {

            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidenSayisi;

            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            var mail = (string)Session["CariMail"];
            p.Gonderici = mail;
            c.Mesajlars.Add(p);
            c.SaveChanges();

            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
                k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(degerler);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");

        }
        public PartialViewResult Partial1()
        {

            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
           
            return PartialView("Partial1", caribul);
        }
        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cariler p)
        {
            var cari = c.Carilers.Find(p.CariID);
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariSifre = p.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}