using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TulostauluCore.Models;
using System.Diagnostics;

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
                if (int.TryParse(Request.Form["homeRuns"], out int homeRuns))
                {
                    if (homeRuns < 0)
                        homeRuns = 0;
                    if (homeRuns > 99)
                        homeRuns = 99;
                    taulu.HomeRuns = homeRuns;
                }
                if (int.TryParse(Request.Form["awayRuns"], out int awayRuns))
                {
                    if (awayRuns < 0)
                        awayRuns = 0;
                    if (awayRuns > 99)
                        awayRuns = 99;
                    taulu.AwayRuns = awayRuns;
                }
                if (int.TryParse(Request.Form["inningStrikes"], out int inningStrikes))
                {
                    if (inningStrikes < 0)
                        inningStrikes = 0;
                    if (inningStrikes > 9)
                        inningStrikes = 9;
                    taulu.InningStrikes = inningStrikes;
                }
                if (int.TryParse(Request.Form["homeWins"], out int homeWins))
                {
                    if (homeWins < 0)
                        homeWins = 0;
                    if (homeWins > 9)
                        homeWins = 9;
                    taulu.HomeWins = homeWins;
                }
                if (int.TryParse(Request.Form["awayWins"], out int awayWins))
                {
                    if (awayWins < 0)
                        awayWins = 0;
                    if (awayWins > 9)
                        awayWins = 9;
                    taulu.AwayWins = awayWins;
                }
                if (int.TryParse(Request.Form["gamePeriod"], out int gamePeriod))
                {
                    if (gamePeriod < 1)
                        gamePeriod = 1;
                    if (gamePeriod > 9)
                        gamePeriod = 9;
                    taulu.GamePeriod = gamePeriod;
                }
                if (int.TryParse(Request.Form["inningJoker"], out int inningJoker))
                {
                    if (inningJoker < 0)
                        inningJoker = 0;
                    if (inningJoker > 9)
                        inningJoker = 9;
                    taulu.InningJoker = inningJoker;
                }
                if (int.TryParse(Request.Form["homeHitter"], out int homeHitter))
                {
                    if (homeHitter < 1)
                        homeHitter = 9;
                    if (homeHitter > 9)
                        homeHitter = 1;
                    taulu.HomeHitter = homeHitter;
                }
                if (int.TryParse(Request.Form["homeLastHitter"], out int homeLastHitter))
                {
                    if (homeLastHitter < 1)
                        homeLastHitter = 9;
                    if (homeLastHitter > 9)
                        homeLastHitter = 1;
                    taulu.HomeLastHitter = homeLastHitter;
                }
                if (int.TryParse(Request.Form["awayHitter"], out int awayHitter))
                {
                    if (awayHitter < 1)
                        awayHitter = 9;
                    if (awayHitter > 9)
                        awayHitter = 1;
                    taulu.AwayHitter = awayHitter;
                }
                if (int.TryParse(Request.Form["awayLastHitter"], out int awayLastHitter))
                {
                    if (awayLastHitter < 1)
                        awayLastHitter = 9;
                    if (awayLastHitter > 9)
                        awayLastHitter = 1;
                    taulu.AwayLastHitter = awayLastHitter;
                }
                if (int.TryParse(Request.Form["periodInning"], out int periodInning))
                {
                    if (periodInning < 1)
                        periodInning = 1;
                    if (periodInning > 9)
                        periodInning = 9;
                    taulu.PeriodInning = periodInning;
                }
                if (char.TryParse(Request.Form["inningTurn"], out char inningTurn))
                {
                    if (inningTurn == 'A' || inningTurn == 'L')
                        taulu.InningTurn = inningTurn;
                    else
                        throw new Exception($"Invalid inningTurn: {inningTurn}");
                }
                string inningInsideTeam = Request.Form["inningInsideTeam"];
                if (inningInsideTeam == "home" || inningInsideTeam == "away")
                    taulu.InningInsideTeam = inningInsideTeam;
                else
                    throw new Exception($"Invalid inningInsideTeam: {inningInsideTeam}");
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
                    taulu.HomeRuns = 0;
                    taulu.AwayRuns = 0;
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
                        $"a{taulu.HomeRuns:00}\r\n\n" +
                        $"b{taulu.AwayRuns:00}\r\n\n" +
                        $"c{taulu.InningStrikes}\r\n\n" +
                        $"d{taulu.HomeWins}\r\n\n" +
                        $"e{taulu.AwayWins}\r\n\n" +
                        $"f{taulu.GamePeriod}\r\n\n" +
                        $"g{taulu.InningJoker}\r\n\n" +
                        $"h{taulu.HomeHitter}\r\n\n" +
                        $"i{taulu.HomeLastHitter}\r\n\n" +
                        $"j{taulu.PeriodInning}.{taulu.InningTurn}\r\n\n"
                    );
            }
            else
            {
                return Json(
                        $"a{taulu.HomeRuns:00}\r\n\n" +
                        $"b{taulu.AwayRuns:00}\r\n\n" +
                        $"c{taulu.InningStrikes}\r\n\n" +
                        $"d{taulu.HomeWins}\r\n\n" +
                        $"e{taulu.AwayWins}\r\n\n" +
                        $"f{taulu.GamePeriod}\r\n\n" +
                        $"g{taulu.InningJoker}\r\n\n" +
                        $"h{taulu.AwayHitter}\r\n\n" +
                        $"i{taulu.AwayLastHitter}\r\n\n" +
                        $"j{taulu.PeriodInning}.{taulu.InningTurn}\r\n\n"
                    );
            }
        }

        [Route("halt")]
        [HttpGet]
        public ContentResult ShutdownSelf()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"shutdown now\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            try
            {
                process.Start();
                return new ContentResult { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = 500, Content = ex.Message };
            }
        }
    }
}
