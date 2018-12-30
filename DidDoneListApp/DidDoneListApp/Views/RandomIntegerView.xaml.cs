using DidDoneListApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DidDoneListApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RandomIntegerView : ContentPage
	{
        public RandomIntegerView(RandomIntegerViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}