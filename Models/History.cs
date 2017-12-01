using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TulostauluCore.Models
{
    public class History
    {
        public int Id { get; set; }
        public int GamePeriod { get; set; }
        public int PeriodInning { get; set; }
        public char InningTurn { get; set; }
        public string InningInsideTeam { get; set; }
        public int InningStrikes { get; set; }
        public int InningJoker { get; set; }
        public int HomeWins { get; set; }
        public int HomeRuns { get; set; }
        public int HomeHitter { get; set; }
        public int HomeLastHitter { get; set; }
        public int AwayWins { get; set; }
        public int AwayRuns { get; set; }
        public int AwayHitter { get; set; }
        public int AwayLastHitter { get; set; }
    }
}
