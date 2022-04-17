using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaplePuck.Notifications
{
    public class FCMClient : IFCMClient
    {
        public FCMClient()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "StaplePuck.Notifications.firebase-adminsdk.json";

            var stream = assembly.GetManifestResourceStream(resourceName);
            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromStream(stream)
            });
        }

        public async Task<bool> SendNotification(Message message)
        {
            var fireMessage = new MulticastMessage
            {
                Tokens = message.registration_ids,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = message.notification.title,
                    Body = message.notification.text
                    //ImageUrl = "https://www.staplepuck.com/img/StaplePuckLogo.fea5ec08.png"
                }
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(fireMessage);
            // Response is a message ID string.
            Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
            if (response.FailureCount > 0)
            {
                Console.WriteLine($"Error: failed to send to {response.FailureCount}");
                foreach (var item in response.Responses.Where(x => !x.IsSuccess))
                {
                    Console.WriteLine($"Error code {item?.Exception?.MessagingErrorCode} Message: {item?.Exception?.Message}");
                }
            }

            return true;
        }
    }
}
