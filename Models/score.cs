using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TulostauluCore.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int GamePeriod { get; set; }
        public int PeriodInning { get; set; }
        public int HomeRuns { get; set; }
        public int AwayRuns { get; set; }
    }
}
