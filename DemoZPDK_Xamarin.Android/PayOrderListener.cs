using System;
using VN.Zalopay.Sdk;
using VN.Zalopay.Sdk.Listeners;
using Java.Interop;
using Xamarin.Forms;

namespace DemoZPDK_Xamarin.Droid
{

    public class PayOrderListener : Java.Lang.Object, IPayOrderListener
    {
        public void OnPaymentCanceled(string p0, string p1)
        {
            Console.WriteLine("OnPaymentCanceled");

            MessagingCenter.Send(Xamarin.Forms.Application.Current, "Redirect", "OnPaymentCanceled");
        }

        public void OnPaymentError(ZaloPayError p0, string p1, string p2)
        {
            Console.WriteLine("OnPaymentError");

            MessagingCenter.Send(Xamarin.Forms.Application.Current, "Redirect", "OnPaymentCanceled");

        }

        public void OnPaymentSucceeded(string p0, string p1, string p2)
        {
            Console.WriteLine("OnPaymentSucceeded");

            MessagingCenter.Send(Xamarin.Forms.Application.Current, "Redirect", "OnPaymentCanceled");
        }
    }
}
