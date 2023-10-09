namespace PizzaOrderEventService.Models;

public record PizzaOrderEvent(long SequenceNumber, DateTimeOffset OccuredAt, string Name, PizzaOrderContent Content);
