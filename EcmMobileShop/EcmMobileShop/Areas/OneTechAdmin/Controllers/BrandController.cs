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
    public class BrandController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Brand
        public ActionResult Index()
        {
            return View(db.tb_HANGSP.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_HANGSP tb_HANGSP = db.tb_HANGSP.Find(id);
            if (tb_HANGSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_HANGSP);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Brand/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdHangSP,TenHangSP,TrangThai")] tb_HANGSP tb_HANGSP)
        {
            if (ModelState.IsValid)
            {
                db.tb_HANGSP.Add(tb_HANGSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_HANGSP);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_HANGSP tb_HANGSP = db.tb_HANGSP.Find(id);
            if (tb_HANGSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_HANGSP);
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdHangSP,TenHangSP,TrangThai")] tb_HANGSP tb_HANGSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_HANGSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_HANGSP);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_HANGSP tb_HANGSP = db.tb_HANGSP.Find(id);
            if (tb_HANGSP == null)
            {
                return HttpNotFound();
            }
            return View(tb_HANGSP);
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tb_HANGSP tb_HANGSP = db.tb_HANGSP.Find(id);
            db.tb_HANGSP.Remove(tb_HANGSP);
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
