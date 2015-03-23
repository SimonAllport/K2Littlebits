using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2Littlebits
{
    public class LittleBit
    {
        const string MY_DEVICE_ID = "00e04c036776";
        const string MY_TOKEN = "2a38b392605703094d623791422cd03fe6e753a6033e9814aecf3efda8ca3737";
        const string MY_TEST_API = "http://c-63-az9jd5pqb.cloudapp.net/K2littlebits/API/InboundMessages/Post";
       


        static Client client = Client.Authenticate(MY_TOKEN);

        public static void BlinkMe()
        {
            Blink();

        }

        public static void Monitor()
        {
            CreateSubscription();
        }

        public static void StopMonitoring()
        {
            DeleteSubscription();
        }

        static void Blink()
        {
            int i = 0;
            while (i < 3)
            {
                ActivateLed();
                System.Threading.Thread.Sleep(500);
                DeactivateLed();
                System.Threading.Thread.Sleep(500);
                i += 1;
            }
        }

        static void ActivateLed()
        {
            SetDeviceOutputLevel(100, -1);
        }

        static void DeactivateLed()
        {
            SetDeviceOutputLevel(0, -1);
        }

        static void SetDeviceOutputLevel(int level = 100, int duration = -1)
        {
            client.SetOutput(new DeviceOutputRequest
            {
                DeviceId = MY_DEVICE_ID,
                DurationInMilliseconds = duration,
                Percent = level
            });
        }
        static void CreateSubscription()
        {
            client.CreateSubscription(new CreateSubscriberRequest
            {
                PublisherId = MY_DEVICE_ID,
                SubscriberId = MY_TEST_API,
                PublisherEvents = new string[] {
                        "amplitude:delta:ignite"
                    }
            });
        }

        static void DeleteSubscription()
        {
            client.DeleteSubscription(new PublisherSubscriberRequest
            {
                PublisherId = MY_DEVICE_ID,
                SubscriberId = MY_TEST_API
            });
        }

    }
}
