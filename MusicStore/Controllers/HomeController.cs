using MusicStore.EntityContext;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.ViewModels;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private String AlbumSessionKey = "AlbumId";
        MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.IsAtHome = true;
            // Get most popular albums
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ViewedAlbums = GetViewedAlbums(5);
            homeViewModel.RandomAlbums = GetRandomAlbums(5);
            homeViewModel.TopSellingAlbums = GetTopSellingAlbums(5);
            return View(homeViewModel);
        }

        public List<Album> GetViewedAlbums(int cnt)
        {
            var id = Session[AlbumSessionKey];

            var viewedAlbums = new List<Album>();
            if (id != null)
            {
                var indexString = id.ToString().Split(',');
                for(int i = indexString.Count() - 1; i >= 0; i--)
                {
                    var index = indexString.ElementAt(i);
                    if (cnt == 0) break;
                    viewedAlbums.Add(storeDB.Albums.Find(Convert.ToInt32(index)));
                    cnt--;
                }
            }
            return viewedAlbums;
        }
        public List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        public List<Album> GetRandomAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Albums.Take(count).ToList();
        }

        public PartialViewResult RightContent()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ViewedAlbums = GetViewedAlbums(5);
            homeViewModel.RandomAlbums = GetRandomAlbums(5);
            homeViewModel.TopSellingAlbums = GetTopSellingAlbums(5);
            return PartialView(homeViewModel);
        }

    }
}