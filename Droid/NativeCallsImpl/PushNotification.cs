using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PushNotifyTest.Droid.NativeCallsImpl;
using Android.Gms.Common;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotification))]
namespace PushNotifyTest.Droid.NativeCallsImpl
{
    public class PushNotification : PushNotifyTest.NativeCallsInterfaces.IPushNotification
    {
        static object locker = new object();
        public  string GetToken()
        {
            var token = string.Empty;
           var thread =  new System.Threading.Thread(() =>
            {
                try
                {
                    lock (locker)
                    {
                        var instanceID = InstanceID.GetInstance(MainActivity.AppMainContext);
                        token = instanceID.GetToken(
                            "912851590439", GoogleCloudMessaging.InstanceIdScope, null);

                        //Subscrevendo 
                        if (!string.IsNullOrEmpty(token))
                            Subscribe(token);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            });
            thread.Start();
            thread.Join(1000);

          

            return token;
        }
         
        
        public bool IsPlayServicesAvailable()
        {
            string msgText;
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(MainActivity.AppMainContext);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText = "Sorry, this device is not supported";
                    //Finish();
                }
                return false;
            }
            else
            {
                msgText = "Google Play Services is available.";
                return true;
            }
        }

        static void Subscribe(string token)
        {
            var pubSub = GcmPubSub.GetInstance(MainActivity.AppMainContext);
            pubSub.Subscribe(token, "/topics/global", null);
        }
    }
}