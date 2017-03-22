using OuiHop.Models.SignUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Models
{
    public class AppManager
    {
        public ObservableCollection<Prediction> GAddress { get; set; } = new ObservableCollection<Prediction>();
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public string userMode { get ; set; }
        public facebookUser fbUser;
    }
}
