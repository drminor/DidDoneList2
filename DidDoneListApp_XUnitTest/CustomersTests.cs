using DidDoneListApp;
using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiClientLib;
using Xunit;

namespace DidDoneListApp_XUnitTest
{
    public class CustomersTests
    {
        public const int WAIT_TIME = 10000; // 10 seconds

        [Fact]
        public void TestCreateCustomer()
        {
            DAL dal = GetDAL();

            Customers record = new Customers
            {
                CustomerId = 0,
                FirstName = "New",
                LastName = "Test",
                StreetAddress = "1 North St.",
                City = "Raleigh",
                StateId = 1,
                Zip = "27800"
            };

            Task<Uri> task = dal.CreateCustomerAsync(record);

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
        public void TestGetCustomer()
        {
            DAL dal = GetDAL();

            int id = 2;

            Customers result = GetCustomer(id, dal);

            // Now try the same operation again.
            result = GetCustomer(id, dal);
        }

        private Customers GetCustomer(int id, DAL dal)
        {
            Customers result = null;

            Task<Customers> task = dal.GetCustomerAsync(id);

            if (task.Wait(WAIT_TIME))
            {
                if (task.IsCompletedSuccessfully)
                {
                    result = task.Result;
                    System.Diagnostics.Debug.WriteLine("Sucessfully retreived Customer record.");

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

        [Fact]
        public void TestGetAllCustomers()
        {
            DAL dal = GetDAL();

            IEnumerable<Customers> list = null;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task<IEnumerable<Customers>> task = dal.GetCustomerListAsync(cancellationTokenSource.Token);

            if (task.Wait(WAIT_TIME))
            {
                if (task.IsCompletedSuccessfully)
                {
                    list = task.Result;
                    System.Diagnostics.Debug.WriteLine("Sucessfully retreived Customer record.");

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

        private DAL _dal;
        private DAL GetDAL()
        {
            if (_dal == null)
            {
                IEndPointDetailsProvider endPointDetailsProvider = new EndPointDetailsProvider_Prod();
                EndPointDetails endPointDetails = endPointDetailsProvider.EndPointDetails;

                IHttpClientProvider httpClientProvider = new StandardHttpClientProvider(endPointDetails.BaseUri);

                IDalProvider dalProvider = new DalProvider(endPointDetails, httpClientProvider);

                _dal = dalProvider.DAL;
            }

            return _dal;
        }

    }
}
