using System;
using Android.Content;
using DemoZPDK_Xamarin.Droid;
using VN.Zalopay.Sdk;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppLinks;

[assembly: Xamarin.Forms.Dependency(typeof(CallNativeSdkDroid))]
namespace DemoZPDK_Xamarin.Droid
{
    public class CallNativeSdkDroid : CallNativeSdkInterface
    {
        public void sendIntent(string zpTransToken)
        {

            Console.WriteLine(">>> CallNativeSdkDroid:sendIntent ---> " + zpTransToken);
            Console.WriteLine(">>>> " + MainActivity.getCurrentActivity().ToString());


            AndroidAppLinks.Init(MainActivity.getCurrentActivity());
            try
            {
                //Intent intent = new Intent(Intent.ActionSend);
                //intent.AddFlags(ActivityFlags.NewTask);
                ////intent.AddFlags(ActivityFlags.SingleTop);
                //intent.PutExtra("zptranstoken", zpTransToken);
                ////StartActivityForResult(intent, 0);
                //Forms.Context.StartActivity(intent);
                //var pm = Android.App.Application.Context.PackageManager;
                //var uri = Android.Net.Uri.Parse("vn.com.vng.zalopay.sbmc");
                //var intent = new Intent(Intent.ActionView, uri);
                //Xamarin.Forms.Forms.Context.StartActivity(intent);
                //MainActivity.getCurrentContext().StartActivity(intent);

                PayOrderListener payOrderListener = new PayOrderListener();
                ZaloPaySDK.Instance.PayOrder(MainActivity.getCurrentActivity(), zpTransToken ?? string.Empty, "demozpdk://xamarinapp", payOrderListener);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception >>> " + e.ToString());
            }
        }
    }
}
