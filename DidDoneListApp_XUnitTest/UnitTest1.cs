using System;
using Xunit;

using DidDoneListApp;
using System.Threading.Tasks;
using DidDoneListModels;
using WebApiClientLib;
using System.Collections.Generic;

namespace DidDoneListApp_XUnitTest
{
    public class UnitTest1
    {
        public const string BASE_URI_HOST = "192.168.1.125";
        public const int BASE_URI_PORT_NUMBER = 8080;

        public const int WAIT_TIME = 10000;


        [Fact]
        public void TestCreateCustomer()
        {
            DAL dal = GetDataAccessLayer();

            Task<Uri> task = dal.TestCreateCustomerAsync();

            if(task.Wait(WAIT_TIME))
            {
                if (task.IsCompletedSuccessfully)
                {
                    Uri answer = task.Result;
                }
                else if (task.IsCanceled)
                {
                    System.Diagnostics.Debug.WriteLine("Was cancelled.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Was faulted.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Stoped waiting after {WAIT_TIME / 1000} seconds.");
            }
        }

        [Fact]
        public void TestGetCustomer2()
        {
            DAL dal = GetDataAccessLayer();

            string id = "2";

            Customers result = GetCustomer(id, dal);

            // Now try the same operation again.
            result = GetCustomer(id, dal);
        }

        private Customers GetCustomer(string id, DAL dal)
        {
            Customers result = null;

            Task<Customers> task = dal.TestGetCustomerAsync(id);

            if (task.Wait(WAIT_TIME))
            {
                if (task.IsCompletedSuccessfully)
                {
                    result = task.Result;
                    System.Diagnostics.Debug.WriteLine("Sucessfull retreived Customer record.");

                }
                else if (task.IsCanceled)
                {
                    System.Diagnostics.Debug.WriteLine("Was cancelled.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Was faulted.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Stoped waiting after {WAIT_TIME / 1000} seconds.");
            }

            return result;
        }

        private DAL GetDataAccessLayer()
        {
            EndPointDetails endPointDetails = GetEndPointDetails();
            DAL result = new DAL(endPointDetails);
            return result;
        }

        private EndPointDetails GetEndPointDetails()
        {
            Dictionary<string, string> serviceMap = new Dictionary<string, string>
            {
                { "Customers", "api/Customers" }
            };

            EndPointDetails result = new EndPointDetails(serviceMap, BASE_URI_HOST, BASE_URI_PORT_NUMBER);

            return result;
        }

    }


}
