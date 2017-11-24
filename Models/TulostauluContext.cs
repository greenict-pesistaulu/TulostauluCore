using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TulostauluCore.Models
{
    public class TulostauluContext : DbContext
    {
        public TulostauluContext(DbContextOptions<TulostauluContext> options) : base(options)
        {

        }

        public DbSet<Tulostaulu> Live { get; set; }
    }
}
