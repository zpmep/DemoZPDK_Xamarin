using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

using Foundation;
using UIKit;
using NativeLibrary;

namespace DemoZPDK_Xamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        private const int PAYMENTCOMPLETE = 1;
        private const int PAYMENTERROR = -1;
        private const int PAYMENTCANCELED = 4;


        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            ZaloPaySDK.SharedInstance().InitWithAppId( int.Parse(Constants.APP_ID), "demozpdk://xamarinapp", ZPZPIEnvironment.Sandbox);
            ZPPaymentDelegateImplement zPPaymentDelegate = new ZPPaymentDelegateImplement();

            // -------------------------------------------------------------------------
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, "PayOrder", (sender, zpTransToken) => {
                ZaloPaySDK.SharedInstance().PaymentDelegate = zPPaymentDelegate;
                ZaloPaySDK.SharedInstance()?.PayOrder(zpTransToken);
            });
            // -------------------------------------------------------------------------

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
            Console.WriteLine("ONACTIVZATE");
            MessagingCenter.Send(Xamarin.Forms.Application.Current, "Redirect", "redirect");
        }

    }

    public class ZPPaymentDelegateImplement : ZPPaymentDelegate
    {

        public override void PaymentDidSucceeded(string transactionId, string zpTranstoken, string appTransId)
        {
            Console.WriteLine("PaymentDidSucceeded");
        }

        public override void PaymentDidCanceled(string zpTranstoken, string appTransId)
        {
            Console.WriteLine("void PaymentDidCanceled(string zpTranstoken, string appTransId)");
        }

        public override void PaymentDidError(ZPPaymentErrorCode errorCode, string zpTranstoken, string appTransId)
        {
            Console.WriteLine("void PaymentDidError(ZPPaymentErrorCode errorCode, string zpTranstoken, string appTransId)");
        }
    }
}
