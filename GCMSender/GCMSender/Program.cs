using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GCMSender
{
    class Program
    {
        public const string API_KEY = "AIzaSyC97S7_u2Ohe1Uc4QyrHibqs1DsT6YLXIY";
        public const string MESSAGE = "Hello, ScannPrice!";

        static void Main()
        {
            var jGcmData = new JObject();
            var jData = new JObject(); 

            jData.Add("message", MESSAGE);
            //jGcmData.Add("to", "el_e5Qe6sI8:APA91bGrN6RL9B2k6goDfoS1_YZFfJrG0xeXSphu_lhLj3ncUF6xJCzBGJa9E3SCWsXPfNH-R6X7NMSD5OvSjNHbmGmNkcGXdb5kBXg7zA1-VM1o69FTB5iIPzvq8f0BInDUx-lF6y86");
            jGcmData.Add("to", "/topics/global");
            jGcmData.Add("data", jData);

            var url = new Uri("https://gcm-http.googleapis.com/gcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + API_KEY);

                    using (var stringContent = new StringContent(jGcmData.ToString(), Encoding.Default, "application/json"))
                    {
                        Task.WaitAll(client.PostAsync(url, stringContent)
                                .ContinueWith(response =>
                                {
                                    Debug.WriteLine(response);
                                    Console.WriteLine("Message sent: check the client device notification tray.");
                                }));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to send GCM message:");
                Console.Error.WriteLine(e.StackTrace);
            }
        }
    }
}

