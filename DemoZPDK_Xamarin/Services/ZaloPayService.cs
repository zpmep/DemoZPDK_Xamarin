using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoZPDK_Xamarin.Models;
using DemoZPDK_Xaramin_V2.Helper;
using Xamarin.Forms;

namespace DemoZPDK_Xamarin.Services
{
    public class ZaloPayService
    {
        readonly List<Order> orders;

        public ZaloPayService()
        {
            orders = new List<Order>();
        }


        public static async Task<int> CreateOrder(Order order)
        {
            order.Mac = ZaloPayMacGenerator.CalculateOrderMac(order);

            Dictionary<string, string> orderData = new Dictionary<string, string>();
            orderData.Add("app_id", order.AppId.ToString());
            orderData.Add("app_user", order.AppUser);
            orderData.Add("app_time", order.AppTime.ToString());
            orderData.Add("app_trans_id", order.AppTransId);
            orderData.Add("description", order.Description);
            orderData.Add("amount", order.Amount.ToString());
            orderData.Add("item", order.Item);
            orderData.Add("embed_data", order.EmbedData);
            orderData.Add("mac", order.Mac);

            try
            {
                var orderResponse = await ZaloPayHelper.CreateOrder(orderData);
                if (orderResponse.Count > 0)
                {
                    //Console.WriteLine(string.Join(";", orderResponse.Select(x => x.Key + "=" + x.Value).ToArray()));
                    Order updateOrder = await App.Database.GetItemAsync(order.ID);
                    order.ReturnCode = orderResponse["return_code"].ToString();
                    order.ReturnMessage = orderResponse["return_message"].ToString();
                    order.SubReturnCode = orderResponse["sub_return_code"].ToString();
                    order.SubReturnMessage = orderResponse["sub_return_message"].ToString();
                    order.OrderUrl = orderResponse["order_url"].ToString();
                    order.ZpTransToken = orderResponse["zp_trans_token"].ToString();
                    await App.Database.SaveItemAsync(order);
                }
                else
                {
                    order.Status = "FAILED";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> EXCEPTION!");
                Console.WriteLine(ex);
                order.Status = "FAILED";
            }
            finally
            {
                Console.WriteLine(order.OrderUrl);
            }

            return await App.Database.SaveItemAsync(order);
        }
        

        public static async Task<int> UpdateOrder(Order order)
        {
            return await App.Database.SaveItemAsync(order);
        }


        public async Task<Order> GetOrder(int id)
        {
            return await Task.FromResult(orders.FirstOrDefault(s => s.ID == id));
        }

        public static async Task<Order> GetOrderByZpTransToken(string zpTransToken)
        {
            return await App.Database.GetItemAsyncByAppTransId(zpTransToken);
        }

        public async Task<IEnumerable<Order>> ListOrder(bool forceRefresh = false)
        {
            return await App.Database.GetItemsAsync();
            //return await Task.FromResult(orders);
        }
    }
}
