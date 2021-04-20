using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicStore.Models;

namespace MusicStore.ViewModels
{
    public class HomeViewModel
    {
        public List<Album> RandomAlbums { get; set; }
        public List<Album> TopSellingAlbums { get; set; }
        public List<Album> ViewedAlbums { get; set; }
    }
}