using System;
using Xamarin.Forms;
using DemoZPDK_Xamarin.Models;
using DemoZPDK_Xamarin.ViewModels;

namespace DemoZPDK_Xamarin.Views
{
    public partial class ListOrderPage : ContentPage
    {

        ListOrderViewModel viewModel;
        public ListOrderPage()
        {
            InitializeComponent();
            Title = "History";
            BindingContext = viewModel = new ListOrderViewModel();
            viewModel.Navigation = Navigation;
        }

        async void OnOrderSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var order = (Order)layout.BindingContext;
            await Navigation.PushAsync(new OrderDetailPage(new OrderDetailViewModel(order)));
        }

        async void CreateOrder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateOrderPage()));
        }

        protected override async void OnAppearing()
        {

            /// NOTE: NEED TO IMPLEMENT SCHEDULED JOB IN YOUR BACKEND SERVICE TO QUERY YOUR ORDER STATUS AND FETCH THEM EVERY OPEN APP INSTANCE 
            viewModel.Orders = await App.Database.GetItemsAsync();
            OrderCollectionViews.ItemsSource = viewModel.Orders;
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var order = (Order)layout.BindingContext;
            await App.Database.DeleteItemAsync(order);
            OnAppearing();
        }
    }
}
