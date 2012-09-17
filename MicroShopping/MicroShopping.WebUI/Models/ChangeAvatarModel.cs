using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroShopping.WebUI.Models
{
    public class ChangeAvatarModel
    {
        public int UserId { get; set; }
        public string CurrentAvatarUrl { get; set; }
        public string NewAvatarUrl { get; set; }
    }
}