using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Drawing;

namespace EcmMobileShop.Controllers
{
   
    public class HomeController : Controller
    {
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();

        [ChildActionOnly]
        public ActionResult _Header()
        {

            return PartialView("_Header", ecmMobile.tb_LOAISP.Where(loai => loai.TrangThai == true).ToList());
        }

        public ActionResult getLinkBannerMain()
        {
            String linkanh = "https://lh3.google.com/u/1/d/1lBP_DTjnrCq2aV0HfEhKd0272qXnjGwP=w1920-h961-iv1";

            var banner = ecmMobile.tb_Banner
                .Where(bn => bn.NgayHH >= DateTime.Now && bn.TinhTrang == true && bn.ImagePathDetail != null)
                .OrderByDescending(bn => bn.NgayHH)
                .FirstOrDefault();
            if(banner != null)
            {
                linkanh = banner.ImagePathDetail;
            }    

            return Content(linkanh);
        }

        [ChildActionOnly]
        public ActionResult _Footer()
        {
            var loaiSP = ecmMobile.tb_LOAISP.Where(loai => loai.TrangThai == true).ToList();
            var hangSP = ecmMobile.tb_HANGSP.Where(hang => hang.TrangThai == true).ToList();
            var modelCollection = new ModelCollection();
            modelCollection.AddModel(loaiSP);
            modelCollection.AddModel(hangSP);

            return PartialView("_Footer", modelCollection);
        }

        public ActionResult Index()
        {
            var loaiSP = ecmMobile.tb_LOAISP.Where(loai => loai.TrangThai == true).ToList();
            var bannerslideSP = ecmMobile.tb_BannerSP.Where(bn => bn.NgayHH >= DateTime.Now && bn.TinhTrang == true).ToList();
            var listSPMoi = ecmMobile.tb_SANPHAM.Where(sp => sp.TrangThai == true)
                                 .OrderByDescending(sp => sp.NgayNhap).ToList();

            var modelCollection = new ModelCollection();
            modelCollection.AddModel(loaiSP);
            modelCollection.AddModel(bannerslideSP);
            modelCollection.AddModel(listSPMoi);
            return View(modelCollection);
        }

        public ActionResult Shop(int? page, int? idLoai, int? idHang, string noidungtimkiem)
        {
           
            if (page == null) page = 1;

            if (idLoai != null)
            {
                var mobile = ecmMobile.tb_SANPHAM.OrderBy(b => b.IdSP).Where(p => p.IdLoaiSP == idLoai && p.TrangThai == true && p.tb_HANGSP.TrangThai == true && p.tb_LOAISP.TrangThai == true);

                int pageSize = 15;
                int pageNumber = (page ?? 1);




                return View(mobile.ToPagedList(pageNumber, pageSize));
            } if(idHang != null)
            {
                var mobile = ecmMobile.tb_SANPHAM.OrderBy(b => b.IdSP).Where(p => p.IdHangSP == idHang && p.TrangThai == true && p.tb_HANGSP.TrangThai == true && p.tb_LOAISP.TrangThai == true);

                int pageSize = 15;
                int pageNumber = (page ?? 1);


                return View(mobile.ToPagedList(pageNumber, pageSize));
            } if(noidungtimkiem != null)
            {
                var mobile = ecmMobile.tb_SANPHAM
                            .OrderBy(b => b.IdSP)
                            .Where(p => p.TenSP.ToLower().Contains(noidungtimkiem.ToLower()) && p.TrangThai == true && p.tb_HANGSP.TrangThai == true && p.tb_LOAISP.TrangThai == true);

                int pageSize = 15;
                int pageNumber = (page ?? 1);


                return View(mobile.ToPagedList(pageNumber, pageSize));
            }    
            else {
                var mobile = ecmMobile.tb_SANPHAM.OrderBy(b => b.IdSP).Where(p => p.TrangThai == true && p.tb_HANGSP.TrangThai == true && p.tb_LOAISP.TrangThai == true);
                int a = mobile.Count();

                int pageSize = 15;
                int pageNumber = (page ?? 1);


                return View(mobile.ToPagedList(pageNumber, pageSize));
            }

            
        }
        public ActionResult Product(int id)
        {
            var product = ecmMobile.tb_SANPHAM.Single(p => p.IdSP == id && p.TrangThai == true);
            List<tb_SANPHAM> tb_SANPHAMs = new List<tb_SANPHAM>();
            tb_SANPHAMs.Add(product);
            var listLoai = ecmMobile.tb_LOAISP.ToList();
            var listHang = ecmMobile.tb_HANGSP.ToList();
            var listCTSP = ecmMobile.tb_CT_SANPHAM.Where(p => p.IdSP == id).ToList();
            var listmau = ecmMobile.tb_MAUSAC.ToList();
            var dcSP = ecmMobile.tb_DISCOUNTSP.Where(dc => dc.IdSP == id).ToList();

            var modelCollection = new ModelCollection();
            modelCollection.AddModel(tb_SANPHAMs);
            modelCollection.AddModel(listLoai);
            modelCollection.AddModel(listHang);
            modelCollection.AddModel(listCTSP);
            modelCollection.AddModel(listmau);
            if(dcSP != null)
            {
                modelCollection.AddModel(dcSP);
            }    
            

            return View(modelCollection);
        }

        public ActionResult Cart()
        {
            List<CartItem> ShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if(ShoppingCart == null)
            {
                ShoppingCart = new List<CartItem>();
            }
            List<tb_MAUSAC> listmau = ecmMobile.tb_MAUSAC.ToList();
            var modelCollection = new ModelCollection();
            modelCollection.AddModel(ShoppingCart);
            modelCollection.AddModel(listmau);
            return View(modelCollection);       
        }

        public ActionResult GetCountItem()
        {
            List<CartItem> ShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (ShoppingCart == null)
            {
                ShoppingCart = new List<CartItem>();
            }
            

            return Content(ShoppingCart.Count.ToString());
        }
        public ActionResult GetMoneyItem()
        {
            List<CartItem> ShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (ShoppingCart == null)
            {
                ShoppingCart = new List<CartItem>();
            }
            double sum = 0;
            foreach (var item in ShoppingCart)
            {
                sum += (item.Gia ?? 0) * item.SoLuong;
            }

            return Content(sum.ToString());
        }
        public RedirectToRouteResult RemoveItem(int ProductId)
        {
            List<CartItem> shoppingCart = Session["ShoppingCart"] as List<CartItem>;
            CartItem DelItem = shoppingCart.FirstOrDefault(m => m.IdSP == ProductId);
            if (DelItem != null)
            {
                shoppingCart.Remove(DelItem);
            }
            return RedirectToAction("Cart", "Home");
        }

        public ActionResult AddToCart(int ProductId,string mausac, int soluong,double gia)
        {
            Cart cart = new Cart(); 
            if (Session["ShoppingCart"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["ShoppingCart"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> ShoppingCart = Session["ShoppingCart"] as List<CartItem>;  // Gán qua biến giohang dễ code


            int idmau = 0;

            if (mausac != null)
            {

                // Tách các giá trị màu Red, Green và Blue từ chuỗi
                var mau = mausac.Replace("rgb(", "").Replace(")", "").Split(',');

                // Chuyển đổi giá trị màu thành kiểu số nguyên
                int red = int.Parse(mau[0].Trim());
                int green = int.Parse(mau[1].Trim());
                int blue = int.Parse(mau[2].Trim());

                // Tạo một đối tượng Color từ các giá trị màu
                Color color = Color.FromArgb(red, green, blue);

                // Chuyển đổi màu sang mã hex
                string hexColor = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

                idmau = ecmMobile.tb_MAUSAC.Where(c => c.MaMau == hexColor).First().IdMau;
            }else
            {
                idmau = ecmMobile.tb_CT_SANPHAM.SingleOrDefault(ctsp => ctsp.IdSP == ProductId && ctsp.SoLuongSP > 0).IdMau;
            }



            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (ShoppingCart.FirstOrDefault(m => m.IdSP == ProductId && m.IdMau == idmau) == null) // ko co sp nay trong gio hang
            {
                CartItem prodouct = cart.FindProduct(ProductId,idmau);  // tim sp theo sanPhamID
                prodouct.SoLuong = soluong;
                prodouct.Gia = gia;

                ShoppingCart.Add(prodouct);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem prodouct = ShoppingCart.FirstOrDefault(m => m.IdSP == ProductId);
                prodouct.SoLuong+=soluong;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            //return RedirectToAction("Cart", "Home");
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}