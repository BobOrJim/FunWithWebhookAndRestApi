using System.Collections.ObjectModel;
using API.Models;

namespace API.Repository
{
    public class WebhookRepository : IWebhookRepository
    {
        public static List<WebhookSubscription> WebhookSubscriptionInMemoryDb { get; set; }  = new List<WebhookSubscription>();


        public WebhookRepository()
        {
            //WebhookSubscription asdf = new() { WebhookUri = "asdf", Secret = "", Header = new Dictionary<string, string>
            //    {
            //        ["key1"] = "value1",
            //        ["key2"] = "value2",
            //        ["key3"] = "value3"
            //    }
            //};
            //WebhookSubscriptionInMemoryDb.Add(asdf);
        }


        public ReadOnlyCollection<WebhookSubscription> GetSubscribers() => WebhookSubscriptionInMemoryDb.AsReadOnly();


        public void AddSubscriber(WebhookSubscription webhookSubscriber)
        {
            if (webhookSubscriber == null)
            {
                throw new ArgumentNullException(nameof(webhookSubscriber));
            }
            WebhookSubscriptionInMemoryDb.Add(webhookSubscriber);
        }


    }
}





