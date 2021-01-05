using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SahaCloudManager.Models
{
    public class SahaContext : DbContext
    {
        public SahaContext() : base("SahaCloudManger_db")
        {
        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}