using DidDoneListApp;
using DidDoneListModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DidDoneListApp_XUnitTest
{
    public class CustomersTests : DalTests
    {
        public const int WAIT_TIME = 10000; // 10 seconds

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
        public void TestGetCustomer()
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

    }


}
