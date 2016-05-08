using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XYZEve05MVC.Models;

namespace XYZEve05MVC.Controllers
{
    public class ProductController : Controller
    {
        IBusinessShopping ib = new BusinessShopping();
        // GET: Product
        public ActionResult Index(int CatID)
        {
            SessionFacade.CATID = CatID.ToString();
            List<Product> products = ib.getProductsByCatID(CatID);
            return View(products);
        }

        public ActionResult Detail(int Id)
        {
            Product product = ib.getPoductByProductId(Id);
            ViewData["ProductId"] = product.ProductId;
            ViewData["ProductLDesc"] = product.ProdectLDesc;
            ViewData["Price"] = product.Price.ToString();
            ViewData["ProductSDesc"] = product.ProductSDesc;
            ViewData["ProductImage"] = product.ProductImage;
            return View();
        }
        
        
        public ActionResult AddToCart(Models.CartItem item)
        {
            ShoppingCart myCart = new ShoppingCart();
            if (SessionFacade.MyCart == null)
                SessionFacade.MyCart = myCart;
            else
                myCart = SessionFacade.MyCart;

            CartItem row = item;
            CartItem testrow = new CartItem();
            //  if item exists in shopping cart, remove first, then add
            //  with an updated total count
            int nTotalItem = myCart.list.Count;

            for (int nItem = 0; nItem < nTotalItem; nItem++)
            {
                testrow = (CartItem)myCart.list[nItem];
                if (testrow.ProductId == item.ProductId)
                {
                    int qty = testrow.Quantity;
                    qty += row.Quantity;
                    testrow.Quantity = qty;
                    testrow.TotalAmount = testrow.Quantity * testrow.Price;
                    myCart.list.RemoveAt(nItem);
                    row = testrow;
                    break;
                }
            }
            myCart.list.Add(row);
            myCart.TotalQty = nTotalItem;
            TempData["msg"] = row.Quantity + " items of " + row.ProductSDesc + " added to your Shopping Cart";
            return RedirectToAction("Detail", new { Id = item.ProductId });
        }

        
        public ActionResult ViewCart()
        {

            ShoppingCart myCart = new ShoppingCart();
            if (SessionFacade.MyCart == null)
                SessionFacade.MyCart = myCart;
            else
                myCart = SessionFacade.MyCart;
            myCart.items.Clear();
            int nTotalItem = myCart.list.Count;
            myCart.TotalQty = nTotalItem;
            double Total = 0;
            for(int nItem =0; nItem < nTotalItem; nItem++)
            {
                CartItem Item = (CartItem)myCart.list[nItem];
                Item.TotalAmount = Item.Quantity * Item.Price;
                
                myCart.items.Add(Item);
                Total += Item.TotalAmount;
            }
            myCart.TotalCost = Total;
            return View(myCart);
        }

        
        public ActionResult ContinueShopping()
        {
            if(SessionFacade.CATID != null)
            {
                string catID = SessionFacade.CATID;
                return RedirectToAction("Index", new { CatID = catID });
            }else
            {
                return RedirectToAction("Index", new { CatID = 10 });
            }
        }

        public ActionResult Update(FormCollection fc)
        {
            string[] quantities = fc.GetValues("quantity");
            ShoppingCart myCart = SessionFacade.MyCart;
            for (int i = 0; i < myCart.TotalQty; i++)
            {
                ((CartItem)myCart.list[i]).Quantity = Convert.ToInt32(quantities[i]);
            }
            return RedirectToAction("ViewCart");
        }



    }
}