using System;

using Android.Views;
using Android.Widget;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Xamarin.Forms;
using VN.Zalopay.Sdk;
using VN.Zalopay.Sdk.Listeners;
using Java.Interop;

namespace DemoZPDK_Xamarin.Droid
{
    [Activity(Label = "DemoZPDK_Xamarin",
        Icon = "@mipmap/icon", Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
        LaunchMode = LaunchMode.SingleTop)
    ]
    [IntentFilter(new[] { Intent.ActionView }, DataScheme = "demozpdk", DataHost = "xamarinapp")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public static Activity getCurrentActivity()
        {
            //return Xamarin.Forms.Forms.Context as MainActivity;
            return (MainActivity)Forms.Context;
        }

        public static Context getCurrentContext()
        {
            return Forms.Context;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // -------------------------------------------------------------------------------------------------------------------------------------
            // Config appid and env
            ZaloPaySDK.Init(120987, VN.Zalopay.Sdk.Environment.Sandbox);
            // -------------------------------------------------------------------------------------------------------------------------------------
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            // -------------------------------------------------------------------------------------------------------------------------------------
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, "PayOrder", (sender, zpTransToken) =>
            {

                PayOrderListener payOrderListener = new PayOrderListener();
                // Define URI schema in AndroidManifest.xml
                ZaloPaySDK.Instance.PayOrder(this, zpTransToken ?? string.Empty, "demozpdk://xamarinapp", payOrderListener);

                //Intent intent = new Intent(this.ApplicationContext, typeof(PayOrderListener));
                //intent.AddFlags(ActivityFlags.NewTask);
                ////intent.AddFlags(ActivityFlags.SingleTop);
                //intent.PutExtra("zptranstoken", zpTransToken);
                ////StartActivityForResult(intent, 0);
                //StartActivity(intent);
            });
            // -------------------------------------------------------------------------------------------------------------------------------------

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            MessagingCenter.Send(Xamarin.Forms.Application.Current, "Redirect", this.Intent?.GetStringExtra("zptranstoken"));

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            ZaloPaySDK.Instance.OnResult(intent);

        }

    }
}