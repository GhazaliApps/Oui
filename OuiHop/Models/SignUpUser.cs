using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Models
{
    public  class SignUpUser
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }
        public DateTime birthDate { get; set; }
        public string country { get; set; }
        public string mode { get; set; }
        public string sponsorCode { get; set; }
        public string accountType { get; set; }
        public string newsLetter { get; set; }
        public string externalId { get; set; }
        public string accessToken { get; set; }
        public byte [] Image { get; set; }
        public string carMake { get; set; }
        public string carModel { get; set; }
        public string carColor { get; set; }
        public string carPlateNumber { get; set; }
        public string businessCar { get; set; }
        public string groupId { get; set; }
        public string hwLineIds { get; set; }
        public string whLineIds { get; set; }
    }
}
