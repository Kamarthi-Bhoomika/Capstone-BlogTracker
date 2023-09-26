using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogAppMVC.Data
{
    public class BlogAppMVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BlogAppMVCContext() : base("name=BlogAppMVCContext")
        {
        }

        public System.Data.Entity.DbSet<BlogAppMVC.Models.AdminInfo> AdminInfoes { get; set; }

        public System.Data.Entity.DbSet<BlogAppMVC.Models.EmpInfo> EmpInfoes { get; set; }

        public System.Data.Entity.DbSet<BlogAppMVC.Models.BlogInfo> BlogInfoes { get; set; }

        public System.Data.Entity.DbSet<BlogAppMVC.Models.LoginInfo> LoginInfoes { get; set; }
    }
}
