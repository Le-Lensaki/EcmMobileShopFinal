using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Controllers
{
    public class OrdersPaymentController : Controller
    {
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();
        // GET: OrdersPayment
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) // Kiểm tra xem User đã đăng nhập hay chưa
            {
                var user = User.Identity as ClaimsIdentity;
                var addressClaim = user.FindFirst(ClaimTypes.StreetAddress);
                string address = addressClaim != null ? addressClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var phoneClaim = user.FindFirst(ClaimTypes.MobilePhone);
                string phone = phoneClaim != null ? phoneClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var nameClaim = user.FindFirst(ClaimTypes.Name);
                string name = nameClaim != null ? nameClaim.Value : "";
                // Thực hiện đặt hàng với thông tin từ User.Identity
                Orders(name, phone, address);
                return RedirectToAction("Cart", "Home");
            }
            tb_KHACHHANG model = new tb_KHACHHANG();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(tb_KHACHHANG model)
        {
            try
            {               
                if (ModelState.IsValid)
                {
                    Orders(model.TenKH, model.SDT, model.DiaChi);
                    return RedirectToAction("Cart", "Home");
                }
                else
                {
                    model = new tb_KHACHHANG();
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        public void Orders(string hoten,string sdt,string diachi)
        {
            List<CartItem> shoppingCart = Session["ShoppingCart"] as List<CartItem>;
            tb_KHACHHANG kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == hoten && k.SDT == sdt && k.TrangThai == true);
            if(kh == null)
            {
                kh = new tb_KHACHHANG();
                ecmMobile.tb_KHACHHANG.Add(new tb_KHACHHANG { TenKH = hoten,SDT = sdt, DiaChi = diachi, TrangThai = true });
                ecmMobile.SaveChanges();
                kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == hoten && k.SDT == sdt && k.TrangThai == true);
            }
            tb_HOADON hd = new tb_HOADON();
            hd.tb_KHACHHANG = kh;
            hd.NgayLap = DateTime.Now;
            hd.IdTinhTrangDH = 1;
            hd.DiaChiGiao = diachi;
            ecmMobile.tb_HOADON.Add(hd);
            ecmMobile.SaveChanges();
            



            foreach (CartItem item in shoppingCart)
            {
                tb_CT_SANPHAM ctsp = ecmMobile.tb_CT_SANPHAM.Single(ct => ct.IdMau == item.IdMau && ct.IdctSP == item.IdctSP);
                

                    tb_CHITIETHOADON cthd = new tb_CHITIETHOADON();
                cthd.tb_HOADON = hd;
                cthd.tb_CT_SANPHAM = ctsp;
                cthd.TenSP = item.TenSP;
                cthd.SoLuongBan = item.SoLuong;
                cthd.GiaBan = item.Gia;
                ecmMobile.tb_CHITIETHOADON.Add(cthd);

            }
            ecmMobile.SaveChanges();


            Session["ShoppingCart"] = null;
           
        }
        
        public ActionResult FormHistory()
        {
            tb_KHACHHANG kh = new tb_KHACHHANG();
            if (User.Identity.IsAuthenticated) // Kiểm tra xem User đã đăng nhập hay chưa
            {
                var user = User.Identity as ClaimsIdentity;
                var addressClaim = user.FindFirst(ClaimTypes.StreetAddress);
                string address = addressClaim != null ? addressClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var phoneClaim = user.FindFirst(ClaimTypes.MobilePhone);
                string phone = phoneClaim != null ? phoneClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var nameClaim = user.FindFirst(ClaimTypes.Name);
                string name = nameClaim != null ? nameClaim.Value : "";

                kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == name && k.SDT == phone && k.TrangThai == true);

                return RedirectToAction("HistoryOrders", "OrdersPayment", new { idkh = kh.IdKH });
            }
            return View(kh);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> FormHistory(tb_KHACHHANG model)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    tb_KHACHHANG kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == model.TenKH && k.SDT == model.SDT && k.TrangThai == true);
                    return RedirectToAction("HistoryOrders", "OrdersPayment",new { idkh = kh.IdKH});

                }else
                {
                    model = new tb_KHACHHANG();
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        public ActionResult HistoryOrders(int idkh)
        {
            if(idkh == 0)
            {
                return RedirectToAction("Index", "Home");
            }    
            List<tb_HOADON> hd = ecmMobile.tb_HOADON.Where(p => p.IdKH == idkh).ToList();

            return View(hd);
        }

        public ActionResult Details(int ordersID)
        {
            List<tb_CHITIETHOADON> cthd = ecmMobile.tb_CHITIETHOADON.Where(p => p.IdHD == ordersID).ToList();


            return View(cthd);
        }

        public ActionResult Feedback(int IdctHD)
        {
            tb_FEEDBACK a = new tb_FEEDBACK();
            a.IdctHD = IdctHD;
            return View(a);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Feedback(tb_FEEDBACK model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    model.NgayFB = DateTime.Now;
                    ecmMobile.tb_FEEDBACK.Add(model);
                    ecmMobile.SaveChanges();
                    tb_CHITIETHOADON a = ecmMobile.tb_CHITIETHOADON.SingleOrDefault(mc => mc.IdctHD == model.IdctHD);

                    return RedirectToAction("Details", "OrdersPayment",new { ordersID = a.IdHD});
                }
                else
                {
                    model = new tb_FEEDBACK();
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
    }
}