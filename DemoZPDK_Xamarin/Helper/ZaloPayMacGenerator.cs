using System.Collections.Generic;
using DemoZPDK_Xaramin_V2.Helper.Crypto;
using System.Configuration;
using DemoZPDK_Xamarin;
using DemoZPDK_Xamarin.Models;

namespace DemoZPDK_Xaramin_V2.Helper
{
    public class ZaloPayMacGenerator
    {
        public static string Compute(string data, string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                key = Constants.KEY1;
            }

            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key, data);
        }

        private static string CreateOrderMacData(Dictionary<string, string> order)
        {
            return order["app_id"] + "|" + order["app_trans_id"] + "|" + order["app_user"] + "|" + order["amount"]
              + "|" + order["app_time"] + "|" + order["embed_data"] + "|" + order["item"];
        }

        public static string CreateOrder(Dictionary<string, string> order)
        {
            return Compute(CreateOrderMacData(order));
        }

        public static string GetOrderStatus(Dictionary<string, string> data)
        {
            return Compute(data["app_id"] + "|" + data["app_trans_id"] + "|" + Constants.KEY1);
        }

        public static string Redirect(Dictionary<string, object> data)
        {
            return Compute(data["appid"] + "|" + data["apptransid"] + "|" + data["pmcid"] + "|" + data["bankcode"]
                + "|" + data["amount"] + "|" + data["discountamount"] + "|" + data["status"]);
        }

        public static string CalculateOrderMac(Order order)
        {
            string macString = order.AppId.ToString() + "|" + order.AppTransId + "|" + order.AppUser + "|" +
                order.Amount.ToString() + "|" + order.AppTime.ToString() + "|" + order.EmbedData + "|" + order.Item;
            return Compute(macString);
        }
    }
}