using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorldReact.Models.GENRE;

namespace HelloWorldReact.Controllers
{
    public class GenreController : Controller
    {
        public ActionResult Index()
        {
            var obj = new GENRE_BUS();
            var model = obj.GetGenreList("");
            return View(model);
        }
    }
}