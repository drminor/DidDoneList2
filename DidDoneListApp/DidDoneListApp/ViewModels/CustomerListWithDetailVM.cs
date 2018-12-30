using DidDoneListApp.Models;
using DidDoneListApp.VMBase;
using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace DidDoneListApp.ViewModels
{
    public class CustomerListWithDetailVM : ViewModelBase
    {
        #region Private Properties

        private readonly CancellationTokenSource _cancellationTS;
        private ObservableCollection<Customer> _customerList = new ObservableCollection<Customer>();
        private Customer _selectedCustomer;

        #endregion

        #region Constructor

        public CustomerListWithDetailVM(Task<IEnumerable<Customers>> task, CancellationTokenSource cancellationTS)
        {
            _cancellationTS = cancellationTS;
            task.ContinueWith(LoadCustomerList);
        }

        private void LoadCustomerList(Task<IEnumerable<Customers>> task)
        {
            if (task.IsCompleted)
            {
                if (task.IsFaulted)
                {
                    string errMessage = $"An error occured while fetching the list of customers." +
                        $" The error message is {task.Exception.Message}.";

                    throw new InvalidOperationException(errMessage, task.Exception);
                }

                if (!task.IsCanceled)
                {
                    IEnumerable<Customers> lst = task.Result;

                    CustomerList = ProcessCustomers(lst);
                    SelectedCustomer = CustomerList[0];
                    System.Diagnostics.Debug.WriteLine("Completed ProcessCustomers.");
                }
            }
        }

        private ObservableCollection<Customer> ProcessCustomers(IEnumerable<Customers> lst)
        {
            //System.Diagnostics.Debug.WriteLine($"Processing the Customer List @: {System.DateTime.Now}. ");

            ObservableCollection<Customer> result = new ObservableCollection<Customer>();

            if (lst != null)
            {
                foreach (Customers br in lst)
                {
                    if (_cancellationTS?.IsCancellationRequested == true)
                    {
                        return null;
                    }

                    Customer customer = new Customer
                    {
                        Id = br.CustomerId,
                        FirstName = br.FirstName,
                        LastName = br.LastName
                    };
                    result.Add(customer);
                }
            }

            return result;
        }

        #endregion

        #region Public Properties

        public ObservableCollection<Customer> CustomerList
        {
            get => _customerList;
            set { SetProperty(nameof(CustomerList), ref _customerList, value); }
        }


        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { SetProperty(nameof(SelectedCustomer), ref _selectedCustomer, value); }
        }

        #endregion
    }
}
