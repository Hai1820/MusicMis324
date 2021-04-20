using MusicStore.EntityContext;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize]
    public class OrderHistoryController : Controller
    {
        // GET: OrderHistory
        MusicStoreEntities storeDB = new MusicStoreEntities();

       
        public ActionResult Index()
        {
            ViewBag.AtOrderHistory = true;
            var orderHistory = storeDB.Orders.Where(order => order.Username == User.Identity.Name).OrderByDescending(order => order.OrderDate); ;
            return View(orderHistory);
        }

        public ActionResult Details(int id)
        {
            if (id == null) return Redirect("/Home/Index");
            var orderDetails = storeDB.OrderDetails.Where(od => od.OrderId == id);
            return View(orderDetails);
        }
    }
}