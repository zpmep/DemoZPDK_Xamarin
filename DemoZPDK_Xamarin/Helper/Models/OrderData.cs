using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DemoZPDK_Xaramin_V2.Helper.Crypto;
using Newtonsoft.Json;
using DemoZPDK_Xaramin_V2;
using DemoZPDK_Xamarin;

namespace DemoZPDK_Xaramin_V2.Helper.Models
{
    public class OrderData
    {
        public string Appid { get; set; }
        public string Apptransid { get; set; }
        public long Apptime { get; set; }
        public string Appuser { get; set; }
        public string Item { get; set; }
        public string Embeddata { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string Bankcode { get; set; }
        public string Mac { get; set; }
        
        public OrderData(long amount, string description = "", string bankcode = "", object embeddata = null, object item = null, string appuser = "")
        {
            Appid = Constants.APP_ID;
            Apptransid = ZaloPayHelper.GenTransID();
            Apptime = TimeHelper.GetTimeStamp();
            Appuser = appuser;
            Amount = amount;
            Bankcode = bankcode;
            Description = description;
            Embeddata = JsonConvert.SerializeObject(embeddata);
            Item = JsonConvert.SerializeObject(item);
            Mac = ComputeMac();
        }

        public virtual string GetMacData()
        {
            return Appid + "|" + Apptransid + "|" + Appuser + "|" + Amount + "|" + Apptime + "|" + Embeddata + "|" + Item;
        }

        public string ComputeMac()
        {
            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, Constants.APP_ID, GetMacData());
        }
    }
}