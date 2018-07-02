using System;
using Xunit;

using DidDoneListApp;
using System.Threading.Tasks;
using DidDoneListModels;

namespace DidDoneListApp_XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreateCustomer()
        {
            DAL x = new DAL();
            Task<Uri> task = x.TestCreateCustomerAsync();

            if(task.Wait(1000))
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
                System.Diagnostics.Debug.WriteLine("Stoped waiting after 1 sec.");
            }

        }


        [Fact]
        public void TestGetCustomer2()
        {
            DAL x = new DAL();

            string id = "2";
            Task<Customers> task = x.TestGetCustomerAsync(id);

            if (task.Wait(1000))
            {
                if (task.IsCompletedSuccessfully)
                {
                    Customers answer = task.Result;
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
                System.Diagnostics.Debug.WriteLine("Stoped waiting after 1 sec.");
            }

        }

    }
}
