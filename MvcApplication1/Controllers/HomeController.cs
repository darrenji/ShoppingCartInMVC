using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            ProductsListVm productsListVm = new ProductsListVm();
            productsListVm.Products = GetAllProducts();

            return View(productsListVm);
        }

        public ActionResult CartIndex(Cart cart, string returnUrl)
        {
            return View(new CartIndexVm
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        //添加到购物车
        public ActionResult AddToCart(Cart cart, int id, string returnUrl)
        {
            Product product = GetAllProducts().Where(p => p.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("CartIndex", new {returnUrl});
        }

        //点击数量+号或点击数量-号或自己输入一个值
        [HttpPost]
        public ActionResult IncreaseOrDecreaseOne(Cart cart, int id, int quantity) 
        {
            Product product = GetAllProducts().Where(p => p.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.IncreaseOrDecreaseOne(product, quantity);
            }
            return Json(new
            {
                msg = true
            });
            //return RedirectToAction("CartIndex");
        }

        //

        //从购物车移除
        public ActionResult RemoveFromCart(Cart cart, int id, string returnUrl)
        {
            Product product = GetAllProducts().Where(p => p.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("CartIndex", new {returnUrl});
        }

        //清空购物车
        public ActionResult EmptyCart(Cart cart, string returnUrl)
        {
            cart.Clear();
            return View("Index",new ProductsListVm{Products = GetAllProducts()});
        }

        //显示购物车摘要
        public ActionResult Summary(Cart cart)
        {
            return View(cart);
        }

        private List<Product> GetAllProducts()
        {
            return new List<Product>()
            {
                new Product(){Id = 1, Description = "产品描述产品描述产品描述产品描述产品描述产品描述产品描述",ImageUrl = "/images/1.jpg",Name = "产品1",Price = 85M},
                new Product(){Id = 2, Description = "产品描述产品描述产品描述产品描述产品描述产品描述产品描述",ImageUrl = "/images/2.jpg",Name = "产品2",Price = 95M},
                new Product(){Id = 3, Description = "产品描述产品描述产品描述",ImageUrl = "/images/2.jpg",Name = "产品3",Price = 55M},
                new Product(){Id = 4, Description = "产品描述产品描述产品描述产品描述产品描述产品描述产品描述",ImageUrl = "/images/1.jpg",Name = "产品4",Price = 65M},
                new Product(){Id = 5, Description = "产品描述产品描述产品描述产品描述产品描述产品描述产品描述",ImageUrl = "/images/2.jpg",Name = "产品5",Price = 75M}
            };
        }

        //测试购物车项
        public ActionResult Demo()
        {
            return View();
        }

    }
}
