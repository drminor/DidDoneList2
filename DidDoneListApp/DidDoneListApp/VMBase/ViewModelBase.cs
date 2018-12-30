using System;
using System.Collections.Generic;
using System.Text;

/// <remarks>
/// This is based on the ViewModelBase described in https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/
/// </remarks>


namespace DidDoneListApp.VMBase
{
    public abstract class ViewModelBase : BindableBaseWI //ExtendedBindableObject
    {
        //protected readonly IDialogService DialogService;
        //protected readonly INavigationService NavigationService;

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set { SetProperty(nameof(IsBusy), ref _isBusy, value); }
        }

        public ViewModelBase()
        {
            //DialogService = ViewModelLocator.Resolve<IDialogService>();
            //NavigationService = ViewModelLocator.Resolve<INavigationService>();

            //var settingsService = ViewModelLocator.Resolve<ISettingsService>();

            //GlobalSetting.Instance.BaseIdentityEndpoint = settingsService.IdentityEndpointBase;
            //GlobalSetting.Instance.BaseGatewayShoppingEndpoint = settingsService.GatewayShoppingEndpointBase;
            //GlobalSetting.Instance.BaseGatewayMarketingEndpoint = settingsService.GatewayMarketingEndpointBase;
        }

        //public virtual Task InitializeAsync(object navigationData)
        //{
        //    return Task.FromResult(false);
        //}
    }
}
