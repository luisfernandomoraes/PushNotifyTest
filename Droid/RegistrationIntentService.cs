using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PushNotifyTest.Droid
{
    [Service(Exported = false)]
    class RegistrationIntentService : IntentService
    {
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info(nameof(RegistrationIntentService), "Calling InstanceID.GetToken");
                lock (locker)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    var token = instanceID.GetToken(
                        "912851590439", GoogleCloudMessaging.InstanceIdScope, null);

                    Log.Info(nameof(RegistrationIntentService), "GCM Registration Token: " + token);
                    SendRegistrationToAppServer(token);
                    Subscribe(token);
                }
            }
            catch (Exception e)
            {
                Log.Debug(nameof(RegistrationIntentService), "Failed to get a registration token");
                return;
            }
        }

        void SendRegistrationToAppServer(string token)
        {
            // Add custom implementation here as needed.
        }

        static void Subscribe(string token)
        {
            var pubSub = GcmPubSub.GetInstance(MainActivity.AppMainContext);
            pubSub.Subscribe(token, "/topics/global", null);
        }
    }
}