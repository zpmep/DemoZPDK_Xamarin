using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DemoZPDK_Xaramin_V2.Helper.Crypto;
using DemoZPDK_Xamarin;

namespace DemoZPDK_Xaramin_V2.Helper.Models
{
    public class RefundData
    {
        public string Appid { get; set; }
        public string Zptransid { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public long Timestamp { get; set; }
        public string Mrefundid { get; set; }
        public string Mac { get; set; }

        public RefundData(long amount, string zptransid, string description = "")
        {
            Appid = Constants.APP_ID;
            Zptransid = zptransid;
            Amount = amount;
            Description = description;
            Mrefundid = ZaloPayHelper.GenTransID();
            Timestamp = TimeHelper.GetTimeStamp();
            Mac = ComputeMac();
        }

        public string GetMacData()
        {
            return Appid + "|" + Zptransid + "|" + Amount + "|" + Description + "|" + Timestamp;
        }

        public string ComputeMac()
        {
            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, Constants.KEY2, GetMacData());
        }
    }
}