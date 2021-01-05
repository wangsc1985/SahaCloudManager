using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SahaCloudManager.Models
{
    public class Setting
    {
        public Setting(string name, string value, string tag)
        {
            Name = name;
            Value = value;
            Tag = tag;
        }

        public Setting()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Tag { get; set; }
    }
}