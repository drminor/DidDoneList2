using DidDoneListApp.ViewModels;
using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiClientLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DidDoneListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Splash : ContentPage
	{
        CancellationTokenSource _cancellationTS;

        public Splash ()
		{
			InitializeComponent ();
		}

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new RandomIntegerView(new RandomIntegerViewModel(new RandomGenerator())));

            //CustomerListWithDetailVM ustomerListVM = GetCustomerVM();
            //await Navigation.PushAsync(new CustomersPage(customerListVM));

            //CustomerListWithDetailVM customerListVM = GetCustomerVM();

            //await Navigation.PushAsync(new CustomersPage(customerListVM));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            CustomerListWithDetailVM customerListVM = GetCustomerVM();

            await Navigation.PushAsync(new CustomersPage(customerListVM));
            //Navigation.NavigationStack.LoadFromXaml(typeof(CustomersPage));
        }

        private CustomerListWithDetailVM GetCustomerVM()
        {
            _cancellationTS = new CancellationTokenSource();

            DAL dal = GetDAL();

            Task<IEnumerable<Customers>> fetchCustomerListTask = dal.GetCustomerListAsync(_cancellationTS.Token);

            CustomerListWithDetailVM customerListVM = new CustomerListWithDetailVM(fetchCustomerListTask, _cancellationTS);

            return customerListVM;
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