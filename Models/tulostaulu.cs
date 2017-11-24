using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TulostauluCore.Models
{
    public class Tulostaulu
    {
        public int Id { get; set; }
        public int GamePeriod { get; set; }
	    public int PeriodInning { get; set; }
	    public int InningStrikes { get; set; }
	    public int InningHitter { get; set; }
        public int InningLastHitter { get; set; }
	    public int InningJoker { get; set; }
	    public int HomeWins { get; set; }
	    public int HomeRuns { get; set; }
	    public int AwayWins { get; set; }
	    public int AwayRuns { get; set; }
    }
}
