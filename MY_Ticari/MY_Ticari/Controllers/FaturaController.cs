using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MY_Ticari.Models.Siniflar;

namespace MY_Ticari.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar p)
        {
            c.Faturalars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir",fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar p)
        {
            var fat = c.Faturalars.Find(p.FaturaID);
            fat.FaturaSeriNo = p.FaturaSeriNo;
            fat.FaturaSiraNo = p.FaturaSiraNo;
            fat.VergiDairesi = p.VergiDairesi;
            fat.Tarih = p.Tarih;
            fat.Saat = p.Saat;
            fat.TeslimEden = p.TeslimEden;
            fat.TeslimAlan = p.TeslimAlan;
            fat.Toplam = p.Toplam;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.FaturaID == id).ToList();

            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {


            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}