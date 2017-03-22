using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Models
{
   public class ExceptionClassManager
    {
        public string exceptionClass { get; set; }
        public int errorCode { get; set; }
        public bool clientError { get; set; }
        public string userMessage { get; set; }
        public string exceptionMessage { get; set; }
        public List<string> parameters { get; set; }
    }
}
