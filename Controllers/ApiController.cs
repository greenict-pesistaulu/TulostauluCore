using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TulostauluCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TulostauluCore.Controllers
{
    [Route("api/")]
    public class ApiController : Controller
    {
        TulostauluContext _ctx;

        public ApiController(TulostauluContext ctx)
        {
            ctx.Database.EnsureCreated();
            _ctx = ctx;
        }

        [Route("start")]
        [HttpGet]
        public ContentResult Start()
        {
            try
            {
                _ctx.Live.Add(new Tulostaulu());
                _ctx.SaveChanges();
            }
            catch (Exception)
            {
                return new ContentResult { StatusCode = 500 };
            }
            return new ContentResult { StatusCode = 200 };
        }

        [Route("update")]
        [HttpPost]
        public ContentResult Update()
        {
            Tulostaulu taulu = _ctx.Live.Last();
            try
            {
                taulu.HomeRuns = int.Parse(Request.Form["homeRuns"]);
                taulu.AwayRuns = int.Parse(Request.Form["awayRuns"]);
                taulu.InningStrikes = int.Parse(Request.Form["inningStrikes"]);
                taulu.HomeWins = int.Parse(Request.Form["homeWins"]);
                taulu.AwayWins = int.Parse(Request.Form["awayWins"]);
                taulu.GamePeriod = int.Parse(Request.Form["gamePeriod"]);
                taulu.InningJoker = int.Parse(Request.Form["inningJoker"]);
                taulu.InningHitter = int.Parse(Request.Form["inningHitter"]);
                taulu.InningLastHitter = int.Parse(Request.Form["inningLastHitter"]);
                taulu.PeriodInning = int.Parse(Request.Form["periodInning"]);
            }
            catch (Exception)
            {
                return new ContentResult { StatusCode = 500 };
            }
            _ctx.SaveChanges();
            return new ContentResult { StatusCode = 200 };
        }

        [Route("status")]
        [HttpGet]
        public JsonResult GetStatus()
        {
            return Json(_ctx.Live.Last());
        }

        [Route("serial")]
        [HttpGet]
        public JsonResult GetSerial()
        {
            /*
             * a - Kotijoukkueen tulos/juoksut. 2 digittiä. Max. esitettävä numero "99"
             * b - Vierasjuokkueen tulos/juoksut. 2 digittiä. Max. esitettävä numero "99"
             * c - Palot. 1 Digitti. Max. esitettävä numero "9"
             * d - Kotijuokkueen jaksovoitot. 1 digitti. . Max. esitettävä numero "9"
             * e - Vierasjuokkueen jaksovoitot. 1 digitti. Max. esitettävä numero "9"
             * f - Pelattava jakso. 1 digitti. Max. esitettävä numero "9"
             * g - Vuorossa viellä käyttettävissä olevien jokerien lukumäärä.1 digitti. Max. esitettävä numero "9"
             * h - Kyseisellä hetkellä lyöntivuorossa olevan pelaajan numero. 1 digitti. Max. esitettävä numero "9"
             * i - Vuoron viimeinen lyöjä. 1 digitti. Max. esitettävä numero "9"
             * j - Tällähetkellä pelattava vuoropari. 2 digittiä. Max. esitettävä numero "9 + "A" ja "L" ykköset digitissä"
             */
            Tulostaulu taulu = _ctx.Live.Last();
            return Json(
                    $"a{taulu.HomeRuns}" +
                    $"b{taulu.AwayRuns}" +
                    $"c{taulu.InningStrikes}" +
                    $"d{taulu.HomeWins}" +
                    $"e{taulu.AwayWins}" +
                    $"f{taulu.GamePeriod}" +
                    $"g{taulu.InningJoker}" +
                    $"h{taulu.InningHitter}" +
                    $"i{taulu.InningLastHitter}" +
                    $"j{taulu.PeriodInning}"
                );
        }
    }
}
