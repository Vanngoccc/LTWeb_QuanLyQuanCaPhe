using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace QuanLyQuanCaPhe.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MyConnectionString")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
    }
}