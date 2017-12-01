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
            _ctx = ctx;
        }

        [Route("start")]
        [HttpPost]
        public ContentResult Start()
        {
            try
            {
                _ctx.Database.EnsureDeleted();
                _ctx.Database.EnsureCreated();
                _ctx.Live.Add(new Tulostaulu {
                    GamePeriod = 1,
                    HomeHitter = 1,
                    AwayHitter = 1,
                    HomeLastHitter = 9,
                    AwayLastHitter = 9,
                    InningJoker = 3,
                    PeriodInning = 1,
                    InningTurn = 'A',
                    InningInsideTeam = Request.Form["inningInsideTeam"]
                });
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = 500, Content=ex.Message };
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
                taulu.HomeHitter = int.Parse(Request.Form["homeHitter"]);
                taulu.HomeLastHitter = int.Parse(Request.Form["homeLastHitter"]);
                taulu.AwayHitter = int.Parse(Request.Form["awayHitter"]);
                taulu.AwayLastHitter = int.Parse(Request.Form["homeLastHitter"]);
                taulu.PeriodInning = int.Parse(Request.Form["periodInning"]);
                taulu.InningTurn = char.Parse(Request.Form["inningTurn"]);
                taulu.InningInsideTeam = Request.Form["inningInsideTeam"]; 
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = 500, Content = ex.Message };
            }
            _ctx.SaveChanges();
            return new ContentResult { StatusCode = 200 };
        }

        [Route("inningchange")]
        [HttpGet]
        public ContentResult InningChange()
        {
            Tulostaulu taulu = _ctx.Live.Last();
            bool GamePeriodChanged = false;

            // Tallenna tilanne undoa varten
            History undo = new History {
                AwayHitter = taulu.AwayHitter,
                AwayLastHitter= taulu.AwayLastHitter,
                AwayRuns = taulu.AwayRuns,
                AwayWins = taulu.AwayWins,
                HomeHitter = taulu.HomeHitter,
                HomeLastHitter = taulu.HomeLastHitter,
                HomeRuns = taulu.HomeRuns,
                HomeWins = taulu.HomeWins,
                GamePeriod = taulu.GamePeriod,
                PeriodInning = taulu.PeriodInning,
                InningInsideTeam = taulu.InningInsideTeam,
                InningJoker = taulu.InningJoker,
                InningStrikes = taulu.InningStrikes,
                InningTurn = taulu.InningTurn
            };
            _ctx.History.Add(undo);

            // Reset Palot ja Jokerit
            taulu.InningStrikes = 0;
            taulu.InningJoker = 3;

            // Vaihtuuko vuoropari
            if (taulu.InningTurn == 'L')
            {
                _ctx.Score.Add(new Score
                {
                    AwayRuns = taulu.AwayRuns,
                    HomeRuns = taulu.HomeRuns,
                    GamePeriod = taulu.GamePeriod,
                    PeriodInning = taulu.PeriodInning
                });
                taulu.HomeRuns = 0;
                taulu.AwayRuns = 0;
                taulu.PeriodInning += 1;
                taulu.InningTurn = 'A';
                // Vaihtuuko jakso
                if (taulu.PeriodInning > 4)
                {
                    // Score from DB
                    int AwayScore = _ctx.Score.Where(x => x.GamePeriod == taulu.GamePeriod).Sum(x => x.AwayRuns);
                    int HomeScore = _ctx.Score.Where(x => x.GamePeriod == taulu.GamePeriod).Sum(x => x.HomeRuns);
                    // Score added before SaveChanges()
                    AwayScore += _ctx.Score.Local.Where(x => x.GamePeriod == taulu.GamePeriod).Sum(x => x.AwayRuns);
                    HomeScore += _ctx.Score.Local.Where(x => x.GamePeriod == taulu.GamePeriod).Sum(x => x.HomeRuns);
                    if (HomeScore != AwayScore)
                    {
                        if (HomeScore > AwayScore)
                        {
                            taulu.HomeWins += 1;
                        }
                        else
                        {
                            taulu.AwayWins += 1;
                        }
                    }
                    taulu.PeriodInning = 1;
                    taulu.GamePeriod += 1;
                    GamePeriodChanged = true;
                }
            }
            else
            {
                taulu.InningTurn = 'L';
            }

            // Vaihda koti / vieras joukkueen välillä
            if (GamePeriodChanged)
            {
                taulu.AwayHitter = 1;
                taulu.AwayLastHitter = 9;
                taulu.HomeHitter = 1;
                taulu.HomeLastHitter = 9;
            }
            else
            {
                if (taulu.InningInsideTeam == "home")
                {
                    taulu.InningInsideTeam = "away";

                    // Laske viimeinen lyöjä vuoropariin
                    if(taulu.PeriodInning != 1)
                        taulu.AwayLastHitter = taulu.AwayHitter - 1;
                    if (taulu.AwayLastHitter < 1)
                        taulu.AwayLastHitter = 9;
                }
                else
                {
                    taulu.InningInsideTeam = "home";

                    // Laske viimeinen lyöjä vuoropariin
                    if(taulu.PeriodInning != 1)
                        taulu.HomeLastHitter = taulu.HomeHitter - 1;
                    if (taulu.HomeLastHitter < 1)
                        taulu.HomeLastHitter = 9;

                }
            }

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                return new ContentResult { StatusCode = 500, Content = ex.Message };
            }

            return new ContentResult { StatusCode = 200 };
        }

        [Route("status")]
        [HttpGet]
        public JsonResult GetStatus()
        {
            return Json(_ctx.Live.Last());
        }

        [Route("score")]
        [HttpGet]
        public JsonResult GetScore()
        {
            return Json(_ctx.Score);
        }

        [Route("undo")]
        [HttpGet]
        public ContentResult DoUndo()
        {
            try
            {
                History undo = _ctx.History.Last();
                _ctx.History.Remove(undo);
                if (undo.InningTurn == 'L')
                {
                    _ctx.Score.Remove(_ctx.Score.Last());
                }
                _ctx.Live.Add(new Tulostaulu {
                    AwayHitter = undo.AwayHitter,
                    AwayLastHitter = undo.AwayLastHitter,
                    AwayRuns = undo.AwayRuns,
                    AwayWins = undo.AwayWins,
                    HomeHitter = undo.HomeHitter,
                    HomeLastHitter = undo.HomeLastHitter,
                    HomeRuns = undo.HomeRuns,
                    HomeWins = undo.HomeWins,
                    GamePeriod = undo.GamePeriod,
                    PeriodInning = undo.PeriodInning,
                    InningInsideTeam = undo.InningInsideTeam,
                    InningJoker = undo.InningJoker,
                    InningStrikes = undo.InningStrikes,
                    InningTurn = undo.InningTurn
                });
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = 500, Content = ex.Message };
            }
            return new ContentResult { StatusCode = 200 };
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
            if (taulu.InningInsideTeam == "home")
            {
                return Json(
                        $"a{taulu.HomeRuns}" +
                        $"b{taulu.AwayRuns}" +
                        $"c{taulu.InningStrikes}" +
                        $"d{taulu.HomeWins}" +
                        $"e{taulu.AwayWins}" +
                        $"f{taulu.GamePeriod}" +
                        $"g{taulu.InningJoker}" +
                        $"h{taulu.HomeHitter}" +
                        $"i{taulu.HomeLastHitter}" +
                        $"j{taulu.PeriodInning}{taulu.InningTurn}"
                    );
            }
            else
            {
                return Json(
                        $"a{taulu.HomeRuns}" +
                        $"b{taulu.AwayRuns}" +
                        $"c{taulu.InningStrikes}" +
                        $"d{taulu.HomeWins}" +
                        $"e{taulu.AwayWins}" +
                        $"f{taulu.GamePeriod}" +
                        $"g{taulu.InningJoker}" +
                        $"h{taulu.AwayHitter}" +
                        $"i{taulu.AwayLastHitter}" +
                        $"j{taulu.PeriodInning}{taulu.InningTurn}"
                    );
            }
        }
    }
}
