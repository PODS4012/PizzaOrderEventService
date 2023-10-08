using PizzaOrderEventService.Models;

namespace PizzaOrderEventService.Services;

public class EventStore : IEventStore
{
    private static long currentSequenceNumber = 0;
    private static readonly IList<PizzaOrderEvent> Database = new List<PizzaOrderEvent>();

    public IEnumerable<PizzaOrderEvent> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber) =>
        Database.Where(e => e.SequenceNumber >= firstEventSequenceNumber && e.SequenceNumber <= lastEventSequenceNumber)
            .OrderBy(e => e.SequenceNumber);

    public void Raise(string eventName, object content)
    {
        var seqNumber = Interlocked.Increment(ref currentSequenceNumber);
        Database.Add(new PizzaOrderEvent(seqNumber, DateTimeOffset.UtcNow, eventName, content));
    }
}
