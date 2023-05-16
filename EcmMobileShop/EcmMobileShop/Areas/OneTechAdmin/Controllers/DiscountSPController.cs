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
    public class DiscountSPController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountSP
        public ActionResult Index()
        {
            var tb_DISCOUNTSP = db.tb_DISCOUNTSP.Include(t => t.tb_SANPHAM);
            return View(tb_DISCOUNTSP.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountSP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_DISCOUNTSP tb_DISCOUNTSP = db.tb_DISCOUNTSP.Find(id);
            if (tb_DISCOUNTSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_DISCOUNTSP);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountSP/Create
        public ActionResult Create()
        {
            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/DiscountSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdDCSP,IdSP,GiaTri,NgayHH,TinhTrang")] tb_DISCOUNTSP tb_DISCOUNTSP)
        {
            if (ModelState.IsValid)
            {
                db.tb_DISCOUNTSP.Add(tb_DISCOUNTSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP", tb_DISCOUNTSP.IdSP);
            return View(tb_DISCOUNTSP);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountSP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_DISCOUNTSP tb_DISCOUNTSP = db.tb_DISCOUNTSP.Find(id);
            if (tb_DISCOUNTSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP", tb_DISCOUNTSP.IdSP);
            return View(tb_DISCOUNTSP);
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/DiscountSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdDCSP,IdSP,GiaTri,NgayHH,TinhTrang")] tb_DISCOUNTSP tb_DISCOUNTSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_DISCOUNTSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSP = new SelectList(db.tb_SANPHAM, "IdSP", "TenSP", tb_DISCOUNTSP.IdSP);
            return View(tb_DISCOUNTSP);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountSP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_DISCOUNTSP tb_DISCOUNTSP = db.tb_DISCOUNTSP.Find(id);
            if (tb_DISCOUNTSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_DISCOUNTSP);
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/DiscountSP/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_DISCOUNTSP tb_DISCOUNTSP = db.tb_DISCOUNTSP.Find(id);
            db.tb_DISCOUNTSP.Remove(tb_DISCOUNTSP);
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
