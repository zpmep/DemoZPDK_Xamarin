using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using DemoZPDK_Xaramin_V2.Helper.Crypto;
using DemoZPDK_Xaramin_V2.Helper.Models;
using DemoZPDK_Xaramin_V2.Helper.Extension;
using System.Net.Http;
using DemoZPDK_Xamarin;
using Newtonsoft.Json;
using DemoZPDK_Xamarin.Models;

namespace DemoZPDK_Xaramin_V2.Helper
{
    public class ZaloPayHelper
    {
        private static long uid = TimeHelper.GetTimeStamp();
        private static readonly HttpClient httpClient = new HttpClient();

        public static bool VerifyCallback(string data, string requestMac)
        {
            try
            {
                string mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, Constants.KEY1, data);

                return requestMac.Equals(mac);
            } catch
            {
                return false;
            }
        }

        public static string GenTransID()
        {
            return DateTime.Now.ToString("yyMMdd") + "_" + (++uid); 
        }


        public static Task<Dictionary<string, object>> CreateOrder(Dictionary<string, string> orderData)
        {
            return HttpHelper.PostFormAsync(Constants.EndpointUri+"/v2/create", orderData);
        }

        public static Task<Dictionary<string, object>> CreateOrder(OrderData orderData)
        {
            return CreateOrder(orderData.AsParams());
        }


        public static Task<Dictionary<string, object>> GetOrderStatus(string appTransId)
        {
            var data = new Dictionary<string, string>();

            Console.WriteLine(Constants.APP_ID);
            data.Add("app_id", Constants.APP_ID);
            data.Add("app_trans_id", appTransId);
            data.Add("mac", ZaloPayMacGenerator.GetOrderStatus(data));

            Console.WriteLine(data.ToString());
            return HttpHelper.PostFormAsync(Constants.EndpointUri + "/v2/query", data);
        }

    }
}