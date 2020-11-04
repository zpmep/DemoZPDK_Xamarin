using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DemoZPDK_Xamarin.Models;
using DemoZPDK_Xamarin.Views;
using System.Collections.Generic;
using DemoZPDK_Xamarin.Services;

namespace DemoZPDK_Xamarin.ViewModels
{
    public class ListOrderViewModel : BaseViewModel
    {
        public List<Order> Orders { get; set; }
        public Command DeleteCommand { get; set; }
        public Command LoadOrdersCommand { get; set; }
        public INavigation Navigation;

        public ListOrderViewModel()
        {
            Title = "History";
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
            DeleteCommand = new Command(async (order) => await ExecuteDeleteCommand((Order)order));

            MessagingCenter.Subscribe<CreateOrderPage, Order>(this, "CreateOrder", async (obj, order) =>
            {
                var newOrder = order as Order;
                IsBusy = true;
                try
                {
                    await ZaloPayService.CreateOrder(newOrder);
                }
                catch { }
                finally
                {
                    IsBusy = false;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new OrderDetailPage(new OrderDetailViewModel(order)));
                }
            });
        }

        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                Orders.Clear();
                Orders = await App.Database.GetItemsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteDeleteCommand(Order order)
        {
            await App.Database.DeleteItemAsync(order);
        }
    }
}
