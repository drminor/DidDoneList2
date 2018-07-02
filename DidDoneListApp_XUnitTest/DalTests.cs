using DidDoneListApp;
using DidDoneListModels;
using System.Collections.Generic;
using WebApiClientLib;


// TODO: Instead of using inheritance, use dependency injection.
namespace DidDoneListApp_XUnitTest
{
    public class DalTests
    {
        private DAL _dal = null;
        protected DAL GetDataAccessLayer()
        {
            if (_dal == null)
            {
                EndPointDetails endPointDetails = GetEndPointDetails();
                _dal = new DAL(endPointDetails);
            }
            return _dal;
        }

        public const string BASE_URI_HOST = "192.168.1.125";
        public const int BASE_URI_PORT_NUMBER = 8080;

        private EndPointDetails _endPointDetails = null;
        protected EndPointDetails GetEndPointDetails()
        {
            if (_endPointDetails == null)
            {
                Dictionary<string, string> serviceMap = new Dictionary<string, string>
                {
                    { nameof(Customers), "api/Customers" },
                    { nameof(CustomerSite), "api/CustomerSites" }
                };

                _endPointDetails = new EndPointDetails(serviceMap, BASE_URI_HOST, BASE_URI_PORT_NUMBER);
            }

            return _endPointDetails;
        }

    }
}
