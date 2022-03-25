using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace templatemvc.Models
{
    public class usermodel
    {
        public int id { get; set; }
       
        public string username { get; set; }
        public string email { get; set; }
        [required]

        public string pass { get; set; }
        [required]

        public string name { get; set; }
    }
}