using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushNotifyTest.NativeCallsInterfaces
{
    public interface IPushNotification
    {
        bool IsPlayServicesAvailable();

        string GetToken();
    }
}
