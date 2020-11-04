using System;
using SQLite;
namespace DemoZPDK_Xamarin.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int AppId { get; set; }
        public string AppUser { get; set; }
        public string AppTransId { get; set; }
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string Item { get; set; }
        public string EmbedData { get; set; }
        public string Mac { get; set; }
        public string BankCode { get; set; }

        public string Status { get; set; }
        public string ZpTransToken { get; set; }
        public string OrderUrl { get; set; }

        /// <summary>
        /// / DEBUG PARAMS
        /// </summary>
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string SubReturnCode { get; set; }
        public string SubReturnMessage { get; set; }
    }
}
