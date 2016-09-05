using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PushNotifyTest.Droid.NativeCallsImpl;
using Gcm.Client;


[assembly: Permission(Name = "com.softecsul.scannprice.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.softecsul.scannprice.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.SEND")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace PushNotifyTest.Droid
{
    [Activity(Label = "PushNotifyTest.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Context AppMainContext;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            AppMainContext = ApplicationContext;

            var pushNotification = new PushNotification();
            var thread = System.Threading.Thread.CurrentThread;

            RegisterInGcm();

            /*if (pushNotification.IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);

                /*var intent1 = new Intent(this, typeof(MyGcmListenerService));
                StartService(intent1);
                var token = pushNotification.GetToken();
            }*/

            LoadApplication(new App());
        }

        private void RegisterInGcm()
        {
            GcmClient.CheckDevice(AppMainContext);
            GcmClient.CheckManifest(AppMainContext);
            GcmClient.Register(AppMainContext, "/topics/global");
            
        }
    }
}

