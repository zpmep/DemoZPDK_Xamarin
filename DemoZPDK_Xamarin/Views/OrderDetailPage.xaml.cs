using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoZPDK_Xamarin.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace DemoZPDK_Xamarin.Views
{

    public partial class OrderDetailPage : ContentPage
    {
        OrderDetailViewModel viewModel;

        public OrderDetailPage(OrderDetailViewModel viewModel)
        {
            InitializeComponent();  
            BindingContext = this.viewModel = viewModel;
            viewModel.Navigation = Navigation;

            if (viewModel.IsFinished)
            {
                PAYBUTTON.IsVisible = false;
            }

        }

        public OrderDetailPage()
        {
            InitializeComponent();

        }

        async void Pay_Clicked(object sender, EventArgs e)
        {
            //Device.OpenUri(new System.Uri("zalopay://pay?appid=" + Constants.APP_ID + "zptranstoken=" + viewModel.Order.ZpTransToken));
            //DependencyService.Get<CallNativeSdkInterface>().sendIntent(viewModel.Order.AppTransId);
            MessagingCenter.Send(Application.Current, "PayOrder", viewModel.Order.ZpTransToken);
        }

    }
}
