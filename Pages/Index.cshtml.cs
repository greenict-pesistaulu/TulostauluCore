using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TulostauluCore.Models;

namespace TulostauluCore.Pages
{
    public class IndexModel : PageModel
    {
        TulostauluContext _ctx;

        public Tulostaulu Taulu { get; set;}

        public IndexModel(TulostauluContext ctx)
        {
            _ctx = ctx;
        }

        public void OnGet()
        {
            Taulu = _ctx.Live.Last();
        }
    }
}
