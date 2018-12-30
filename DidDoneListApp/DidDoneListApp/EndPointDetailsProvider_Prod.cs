using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClientLib;
using Xamarin.Forms;

namespace DidDoneListApp
{
    public class EndPointDetailsProvider_Prod : IEndPointDetailsProvider
    {
        private EndPointDetails _endPointDetails = null;

        //public const string BASE_URI_HOST = "172.20.10.2"; // Sprint Celluar Hotspot
        //public const string BASE_URI_HOST = "192.168.1.125"; // Apartment WiFi
        public const string BASE_URI_HOST = "localhost"; // universal 
        public const string BASE_URI_HOST_ANDROID_DEV = "10.0.2.2"; // universal 

        public const int BASE_URI_PORT_NUMBER = 8080;


        public EndPointDetails EndPointDetails
        {
            get
            {
                if (_endPointDetails == null)
                {
                    Dictionary<string, string> serviceMap = new Dictionary<string, string>
                {
                    { nameof(Customers), "api/Customers" },
                    { nameof(CustomerSite), "api/CustomerSites" }
                };

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        _endPointDetails = new EndPointDetails(serviceMap, BASE_URI_HOST_ANDROID_DEV, BASE_URI_PORT_NUMBER);
                    }
                    else
                    {
                        _endPointDetails = new EndPointDetails(serviceMap, BASE_URI_HOST, BASE_URI_PORT_NUMBER);
                    }
                }

                return _endPointDetails;
            }
        }

    }
}
