using MusicStore.EntityContext;
using MusicStore.Models;
using MusicStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MusicStore.Controllers
{
    
    public class StoreController : Controller
    {
        private String AlbumSessionKey = "AlbumId";
        private MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Store/
        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();
            return View(genres);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            //Include("Albums")指定返回结果要包含关联Album
            Genre example = storeDB.Genres.Include("Albums").Single(p => p.Name == genre);
            List<Album> albums = example.Albums;
            return View(example);
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["CurrentAlbumId"] = id;
            Album album = storeDB.Albums.Find(id);
            if (Session[AlbumSessionKey] != null)
            {
                var indexString = Session[AlbumSessionKey].ToString().Split(',');
                System.Diagnostics.Debug.WriteLine("Check Contain: " + !indexString.Contains(id.ToString()));
                if (!indexString.Contains(id.ToString())) Session[AlbumSessionKey] += "," + id;
            }
            else Session[AlbumSessionKey] = id;
            AlBumViewModel alBumViewModel = new AlBumViewModel();

            alBumViewModel.Albums = album;
            alBumViewModel.Comments = GetComment(id);
            System.Diagnostics.Debug.WriteLine("Session[AlbumSessionKey] in Store Controller: " + Session[AlbumSessionKey]);
            return View(alBumViewModel);
        }

        public List<Comment> GetComment(int? id)
        {
           
            var comments = storeDB.Comments.Where(comment => comment.AlbumId == id).ToList();
            comments.Reverse();
            return comments;
        }

        //[Authorize]
        //public ActionResult Create(string returnUrl)
        //{
        //   // System.Diagnostics.Debug.WriteLine("returnUrl: " + returnUrl);
        //    return View("Create");

        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Create(Comment comment)
        //{
        //    System.Diagnostics.Debug.WriteLine("Session[CurrentAlbumId]: " + Session["CurrentAlbumId"]);
        //    if (ModelState.IsValid)
        //    {
        //        comment.Username = User.Identity.Name;
        //        comment.AlbumId = Convert.ToInt32(Session["CurrentAlbumId"]);
        //        storeDB.Comments.Add(comment);
        //        storeDB.SaveChanges();
        //        return Redirect("/Store/Details/" + Session["CurrentAlbumId"]);
        //    } else {
        //        return View(comment);
        //    }
            
        //}

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(Comment comment)
        {
           // System.Diagnostics.Debug.WriteLine("Session[CurrentAlbumId]: " + Session["CurrentAlbumId"]);
            if (ModelState.IsValid)
            {
                comment.Username = User.Identity.Name;
                comment.AlbumId = Convert.ToInt32(Session["CurrentAlbumId"]);
                storeDB.Comments.Add(comment);
                storeDB.SaveChanges();
                return Redirect("/Store/Details/" + Session["CurrentAlbumId"]);
            }
            else
            {
                return Redirect("/Store/Details/" + Session["CurrentAlbumId"]);
            }

        }
        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();
            return PartialView(genres);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                storeDB.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}