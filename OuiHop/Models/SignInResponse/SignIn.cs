using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Models.SignInResponse
{
    public class UserCar
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string color { get; set; }
        public string plateNumber { get; set; }
        public object photoChecksum { get; set; }
        public object businessCar { get; set; }
    }

    public class Line
    {
        public int id { get; set; }
        public string name { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object address { get; set; }
        public string country { get; set; }
        public int attachedUsersCount { get; set; }
    }

    public class UserGroup
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string groupName { get; set; }
        public object area { get; set; }
        public object siteName { get; set; }
        public string country { get; set; }
        public object benefits { get; set; }
        public List<Line> lines { get; set; }
        public object freeProduct { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object address { get; set; }
        public object imageChecksum { get; set; }
    }

    public class UserLine
    {
        public int id { get; set; }
        public string name { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object address { get; set; }
        public string country { get; set; }
        public int attachedUsersCount { get; set; }
    }

    public class HwLine
    {
        public int id { get; set; }
        public string name { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object address { get; set; }
        public string country { get; set; }
        public int attachedUsersCount { get; set; }
    }

    public class Favorite
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string address { get; set; }
        public object label { get; set; }
        public string iconName { get; set; }
    }

    public class WeeklyKm
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int kms { get; set; }
    }

    public class DailyKm
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int kms { get; set; }
    }

    public class MonthlyKm
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int kms { get; set; }
    }

    public class WeelStatistics
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public int numberOfKMsConnected { get; set; }
        public int totalDrivenMinutes { get; set; }
        public int numberOfSponsoredFriends { get; set; }
        public int numberOfTrips { get; set; }
        public int numberOfHop { get; set; }
        public int numberOfHopiz { get; set; }
        public int numberOfLotteries { get; set; }
        public List<WeeklyKm> weeklyKms { get; set; }
        public List<DailyKm> dailyKms { get; set; }
        public List<MonthlyKm> monthlyKms { get; set; }
    }

    public class LastLocation
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public int userID { get; set; }
        public object appStatus { get; set; }
    }

    public class RootObject
    {
        public string createdOn { get; set; }
        public string lastUpdatedOn { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public object password { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }
        public string birthDate { get; set; }
        public string photoChecksum { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string mode { get; set; }
        public UserCar userCar { get; set; }
        public string langCode { get; set; }
        public UserGroup userGroup { get; set; }
        public UserLine userLine { get; set; }
        public List<HwLine> hwLines { get; set; }
        public List<object> whLines { get; set; }
        public object externalId { get; set; }
        public object accessToken { get; set; }
        public object sponsorCode { get; set; }
        public object accountType { get; set; }
        public object incompleteAccount { get; set; }
        public object newPassword { get; set; }
        public object oldPassword { get; set; }
        public double averageRank { get; set; }
        public int numberOfReviews { get; set; }
        public object reviews { get; set; }
        public List<Favorite> favorites { get; set; }
        public object recentSearches { get; set; }
        public List<string> roleCodes { get; set; }
        public string authToken { get; set; }
        public int currentTripID { get; set; }
        public object driveTime { get; set; }
        public object driveDistance { get; set; }
        public bool sharePhoneNumber { get; set; }
        public int numberOfLotteryTickets { get; set; }
        public int totalKMs { get; set; }
        public string unlimitedHopsEndDate { get; set; }
        public string monthlyHopsStartDate { get; set; }
        public int monthlyHopsCount { get; set; }
        public string weelUID { get; set; }
        public WeelStatistics weelStatistics { get; set; }
        public bool markedAsDeleted { get; set; }
        public bool newsLetter { get; set; }
        public int dailyHopizCount { get; set; }
        public int dailyLotteriesCount { get; set; }
        public string currentAwardingDate { get; set; }
        public List<object> reminders { get; set; }
        public bool addedReminder { get; set; }
        public object blocked { get; set; }
        public LastLocation lastLocation { get; set; }
        public int kmsRemainingTillNextLottery { get; set; }
        public int kmsRemainingTillNext10Hopiz { get; set; }
        public int hopizCriedit { get; set; }
    }
}
