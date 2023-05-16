using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcmMobileShop.Models;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class tb_BannerSPController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_BannerSP
        public ActionResult Index()
        {
            var tb_BannerSP = db.tb_BannerSP.Include(t => t.tb_SANPHAM);
            return View(tb_BannerSP.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_BannerSP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_BannerSP tb_BannerSP = db.tb_BannerSP.Find(id);
            if (tb_BannerSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_BannerSP);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_BannerSP/Create
        public ActionResult Create()
        {
            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/tb_BannerSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdBannerSP,IdSP,NgayHH,TinhTrang,ImagePathDetail")] tb_BannerSP tb_BannerSP)
        {
            if (ModelState.IsValid)
            {
                db.tb_BannerSP.Add(tb_BannerSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP", tb_BannerSP.IdSP);
            return View(tb_BannerSP);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_BannerSP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_BannerSP tb_BannerSP = db.tb_BannerSP.Find(id);
            if (tb_BannerSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP", tb_BannerSP.IdSP);
            return View(tb_BannerSP);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/tb_BannerSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdBannerSP,IdSP,NgayHH,TinhTrang,ImagePathDetail")] tb_BannerSP tb_BannerSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_BannerSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP", tb_BannerSP.IdSP);
            return View(tb_BannerSP);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_BannerSP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_BannerSP tb_BannerSP = db.tb_BannerSP.Find(id);
            if (tb_BannerSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_BannerSP);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/tb_BannerSP/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_BannerSP tb_BannerSP = db.tb_BannerSP.Find(id);
            db.tb_BannerSP.Remove(tb_BannerSP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
