using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicStore.Models;

namespace MusicStore.ViewModels
{
    public class AlBumViewModel
    {
        public Album Albums { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment comment { get; set; }
    }
}