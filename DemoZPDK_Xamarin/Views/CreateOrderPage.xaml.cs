using System;
using DemoZPDK_Xamarin;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using DemoZPDK_Xamarin.Models;
using DemoZPDK_Xaramin_V2.Helper;
using DemoZPDK_Xamarin.Helper;

namespace DemoZPDK_Xamarin.Views
{
    public partial class CreateOrderPage : ContentPage
    {
        public Order Order { get; set; }

        public CreateOrderPage()
        {
            InitializeComponent();

            Title = "Create Order";
            string appTransId = ZaloPayHelper.GenTransID();
            Order = new Order
            {
                AppId = Int32.Parse(Constants.APP_ID),
                AppUser = "Demo",
                AppTransId = appTransId,
                AppTime = Util.GetTimeStamp(),
                Amount = 10000,
                Description = "[Demp ZPDK Xamarin] Thanh toan don hang " + appTransId,
                Item = "[]",
                EmbedData = "{}",
                Mac = "",
                ReturnCode = "",
                ReturnMessage = "",
                OrderUrl = "",
                SubReturnCode = "",
                SubReturnMessage = "",
                Status = "PENDING", 
                ZpTransToken = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            MessagingCenter.Send(this, "CreateOrder", Order);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
