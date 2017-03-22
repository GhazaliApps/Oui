using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Models.SignUp
{

    public class Rootobject
    {
        public Group[] groups { get; set; }
    }

    public class Group
    {
        public DateTime createdOn { get; set; }
        public DateTime lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string groupName { get; set; }
        public object area { get; set; }
        public object siteName { get; set; }
        public string country { get; set; }
        public string benefits { get; set; }
        public Line[] lines { get; set; }
        public object freeProduct { get; set; }
        public float? latitude { get; set; }
        public float? longitude { get; set; }
        public string address { get; set; }
        public string imageChecksum { get; set; }
    }

    public class Line
    {
        public int id { get; set; }
        public string name { get; set; }
        public float? latitude { get; set; }
        public float? longitude { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public int attachedUsersCount { get; set; }
    }

}
