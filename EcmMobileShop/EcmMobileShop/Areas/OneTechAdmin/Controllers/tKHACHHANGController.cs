using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcmMobileShop.Models;
using FireSharp.Interfaces;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class tKHACHHANGController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();
        private static string ApiKey = "AIzaSyChvF1RW2IL-N7gbgf_5LHAyOSWT5WK4w0";
        private static string Bucket = "ecmmobileshop-default-rtdb.firebaseio.com";
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();

        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "PtNahcVALDcleR71F1jFTbj4j8AM4RED2PamGprQ",

            BasePath = "https://ecmmobileshop-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;

        // GET: OneTechAdmin/tKHACHHANG
        public ActionResult Index()
        {
            return View(db.tb_KHACHHANG.ToList());
        }

        // GET: OneTechAdmin/tKHACHHANG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KHACHHANG tb_KHACHHANG = db.tb_KHACHHANG.Find(id);
            if (tb_KHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_KHACHHANG);
        }

        // GET: OneTechAdmin/tKHACHHANG/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OneTechAdmin/tKHACHHANG/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdKH,TenKH,DiaChi,SDT,TrangThai")] tb_KHACHHANG tb_KHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.tb_KHACHHANG.Add(tb_KHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_KHACHHANG);
        }

        // GET: OneTechAdmin/tKHACHHANG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KHACHHANG tb_KHACHHANG = db.tb_KHACHHANG.Find(id);
            if (tb_KHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_KHACHHANG);
        }

        // POST: OneTechAdmin/tKHACHHANG/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdKH,TenKH,DiaChi,SDT,TrangThai")] tb_KHACHHANG tb_KHACHHANG)
        {
            if (ModelState.IsValid)
            {
                // Khởi tạo client
                IFirebaseClient client = new FireSharp.FirebaseClient(config);

                var response = client.Get("Khachhang/");
                Dictionary<string, SignUpModel> data = response.ResultAs<Dictionary<string, SignUpModel>>();
                SignUpModel customer = data.Values.FirstOrDefault(x => x.SDT == tb_KHACHHANG.SDT);

                if (customer != null)
                {
                    customer.SDT = tb_KHACHHANG.SDT;
                    customer.Diachi = tb_KHACHHANG.DiaChi;
                    customer.Name = tb_KHACHHANG.TenKH;
                    var res = client.Update("Khachhang/" + customer.id, customer);
                }


                db.Entry(tb_KHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_KHACHHANG);
        }

        // GET: OneTechAdmin/tKHACHHANG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KHACHHANG tb_KHACHHANG = db.tb_KHACHHANG.Find(id);
            if (tb_KHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_KHACHHANG);
        }

        // POST: OneTechAdmin/tKHACHHANG/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_KHACHHANG tb_KHACHHANG = db.tb_KHACHHANG.Find(id);
            db.tb_KHACHHANG.Remove(tb_KHACHHANG);
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
        [Authorize(Roles = "Admin")]
        public ActionResult GetUser()
        {
            // Lấy danh sách đơn hàng mới từ cơ sở dữ liệu
            var newUser = db.tb_KHACHHANG.Where(o => o.TrangThai == true).ToList();

            return Content(newUser.Count.ToString());
        }
    }
}
