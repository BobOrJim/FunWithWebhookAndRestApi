using System.Collections.ObjectModel;
using API.Models;


namespace API.Repository
{
    public interface IWebhookRepository
    {

        ReadOnlyCollection<WebhookSubscription> GetSubscribers();

        void AddSubscriber(WebhookSubscription webhookSubscriber);

    }
}


