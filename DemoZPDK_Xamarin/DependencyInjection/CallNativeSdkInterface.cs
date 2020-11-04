using System;

namespace DemoZPDK_Xamarin
{
    public interface CallNativeSdkInterface
    {
        void sendIntent(string zpTransToken);
        //object receiveIntent(object data);
    }
}
