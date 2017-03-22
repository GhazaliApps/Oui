using OuiHop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using System.Globalization;

namespace OuiHop.Controllers.Operations
{
    public class BaseOperation
    {
        public string Name { get; set; }
        public HttpClient OperationWebClient { get; set; }
        public static Dictionary<string, List<IOperationObserver>> AllObservers { get; set; }
        public string ServerLink { get; set; }
        public string OperationLink { get; set; }
        public string Method { get; set; }
        public string Response { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public static string lang { get; set; }


        public BaseOperation()
        {
            if (AllObservers == null)
                AllObservers = new Dictionary<string, List<IOperationObserver>>();

            Success = false;
            Message = "";

            ServerLink = "http://213.246.61.49/weel-web/api/";

            OperationWebClient = new HttpClient();
            OperationWebClient.DefaultRequestHeaders.Add("Api-Version", "1.0");
            OperationWebClient.DefaultRequestHeaders.Add("App-Version","1.0");
            OperationWebClient.DefaultRequestHeaders.Add("User-Agent","Windows Phone");
            var cultureName = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            CultureInfo currentUIInfo = CultureInfo.CurrentUICulture;

            if (cultureName.ToString() == "en-US")
                OperationWebClient.DefaultRequestHeaders.Add("User-Lang","EN");
            else if (cultureName.ToString() == "fr-FR")
                OperationWebClient.DefaultRequestHeaders.Add("User-Lang", "FR");
            else
                OperationWebClient.DefaultRequestHeaders.Add("User-Lang", "FR");

            string UniqueDeviceID = DeviceInfo.Instance.Id;
            OperationWebClient.DefaultRequestHeaders.Add("Device-UniqueId",UniqueDeviceID);
            OperationWebClient.DefaultRequestHeaders.Add("Timezone-Format","UTC");

        }


        // implementing subject interface
        public static void Subscribe(IOperationObserver observer, BaseOperation operation)
        {
            if (!AllObservers.Keys.Contains(operation.Name))
                AllObservers.Add(operation.Name, new List<IOperationObserver>());

            if (!AllObservers[operation.Name].Contains(observer))
                AllObservers[operation.Name].Add(observer);

        }

        public static void UnSubscribe(IOperationObserver observer, BaseOperation operation)
        {
            if (AllObservers.Keys.Contains(operation.Name))
                AllObservers[operation.Name].Remove(observer);
        }

        public void Notify()
        {
            if (AllObservers.Keys.Contains(Name))
            {
                if (AllObservers[Name] != null)
                {
                    List<IOperationObserver> observers = new List<IOperationObserver>(AllObservers[Name]);

                    foreach (IOperationObserver observer in observers)
                    {
                        observer.Update(this);
                    }
                }
            }

        }

        public virtual void DoOperation()
        {

        }


    }
}
