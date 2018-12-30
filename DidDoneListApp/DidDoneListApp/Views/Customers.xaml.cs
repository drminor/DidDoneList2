using DidDoneListApp.ViewModels;
using System;
using WebApiClientLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DidDoneListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomersPage : ContentPage
	{
		public CustomersPage (CustomerListWithDetailVM customerListWithDetailVM )
		{
            BindingContext = customerListWithDetailVM;
			InitializeComponent ();
            //this.lstBoxBookSelector.Focus();
        }

        public CustomersPage()
        {
            //throw new NotSupportedException($"Using the parameterless constructor of the {nameof(CustomersPage)} class is not supported.");
            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RandomIntegerView(new RandomIntegerViewModel(new RandomGenerator())));

        }
    }
}