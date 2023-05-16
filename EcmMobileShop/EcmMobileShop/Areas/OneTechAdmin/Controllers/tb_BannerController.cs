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
    public class tb_BannerController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_Banner
        public ActionResult Index()
        {
            return View(db.tb_Banner.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_Banner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Banner tb_Banner = db.tb_Banner.Find(id);
            if (tb_Banner == null)
            {
                return HttpNotFound();
            }
            return View(tb_Banner);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_Banner/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/tb_Banner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdBanner,NgayHH,ImagePathDetail,TinhTrang")] tb_Banner tb_Banner)
        {
            if (ModelState.IsValid)
            {
                db.tb_Banner.Add(tb_Banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Banner);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_Banner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Banner tb_Banner = db.tb_Banner.Find(id);
            if (tb_Banner == null)
            {
                return HttpNotFound();
            }
            return View(tb_Banner);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/tb_Banner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdBanner,NgayHH,ImagePathDetail,TinhTrang")] tb_Banner tb_Banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Banner);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/tb_Banner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Banner tb_Banner = db.tb_Banner.Find(id);
            if (tb_Banner == null)
            {
                return HttpNotFound();
            }
            return View(tb_Banner);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/tb_Banner/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_Banner tb_Banner = db.tb_Banner.Find(id);
            db.tb_Banner.Remove(tb_Banner);
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
