using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MY_Ticari.Models.Siniflar;

namespace MY_Ticari.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index()
        {
            UrunDetayListesi udl = new UrunDetayListesi();
            udl.Deger1 = c.Uruns.Where(x => x.UrunID == 1).ToList();
            udl.Deger2 = c.Detays.Where(y => y.DetayID == 1).ToList();
            //var degerler = c.Uruns.Where(x => x.UrunID == 1).ToList();

            return View(udl);
        }
    }
}