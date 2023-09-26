using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogAppMVC.Models
{
    [Table("BlogInfo")]
    public class BlogInfo
    {
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string BlogUrl { get; set; }
        public string EmpEmailId { get; set; }
    }
}