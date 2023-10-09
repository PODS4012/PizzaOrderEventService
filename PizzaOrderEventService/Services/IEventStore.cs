using PizzaOrderEventService.Models;

namespace PizzaOrderEventService.Services;

public interface IEventStore
{
    IEnumerable<PizzaOrderEvent> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
    void Raise(string eventName, PizzaOrderContent content);
}
