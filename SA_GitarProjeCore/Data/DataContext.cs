using Microsoft.EntityFrameworkCore;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Bodies> Bodys{ get; set; }
        public virtual DbSet<Brands> Brands{ get; set; }
        public virtual DbSet<Categories>Categories{ get; set; }
        public virtual DbSet<Cities>Citys{ get; set; }
        public virtual DbSet<Colors>Colors{ get; set; }
        public virtual DbSet<Customers>Customers{ get; set; }
        public virtual DbSet<Products>Products{ get; set; }
        public virtual DbSet<Sales>Sales{ get; set; }
        public virtual DbSet<Wires>Wires{ get; set; }
        public virtual DbSet<Users>Users{ get; set; }
    }
}
