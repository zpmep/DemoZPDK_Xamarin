using System;
using System.Threading.Tasks;
using DemoZPDK_Xamarin.Models;
using DemoZPDK_Xaramin_V2.Helper;
using Xamarin.Forms;

namespace DemoZPDK_Xamarin.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public Order Order { get; set; }
        public INavigation Navigation;
        public OrderDetailViewModel(Order order = null)
        {
            Title = "Order detail";
            Order = order;
            if(Order.Status != "PENDING")
            {
                IsFinished = true;
            }

            // -------------------------------------------------------------------------

            MessagingCenter.Instance.Subscribe<Application, string>(Application.Current, "Redirect", async (sender, result) =>
            {
                IsBusy = true;
                try
                {

                    /// NOTE: Need to query to your backend service and then your backend will check the ZaloPay API to get the order status
                    var response = await ZaloPayHelper.GetOrderStatus(Order.AppTransId);
                    int returnCode = int.Parse(response["return_code"].ToString());
                    if (returnCode == 1)
                    {
                        Order.Status = "SUCCESS";
                        IsFinished = true;
                    }
                    else
                    {
                        IsFinished = true;
                        Order.Status = "FAILED";
                    }
                    await App.Database.SaveItemAsync(Order);

                }
                catch (Exception e)
                {
                    Console.WriteLine(">> EXCEPTION: " + e.ToString());
                    Order.Status = "FAILED";
                    IsFinished = true;
                }
                finally
                {
                    IsBusy = false;
                    await Navigation.PopAsync();
                }

            });


            // -------------------------------------------------------------------------
        }
    }
}
