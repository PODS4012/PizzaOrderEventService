using Microsoft.AspNetCore.Mvc;
using PizzaOrderEventService.Models;
using PizzaOrderEventService.Services;

namespace PizzaOrderEventService.Controllers;

[Route("/events")]
public class EventFeedController : ControllerBase
{
    private readonly IEventStore eventStore;

    public EventFeedController(IEventStore eventStore) =>
        this.eventStore = eventStore;

    [HttpGet("")]
    public ActionResult<PizzaOrderEvent[]> Get([FromQuery] long start, [FromQuery] long end = long.MaxValue) =>
        this.eventStore.GetEvents(start, end).ToArray();

    [HttpPost("")]
    public IActionResult Post([FromBody] RaiseEventRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.EventName) || request.Content == null)
        {
            return BadRequest("Invalid data");
        }

        this.eventStore.Raise(request.EventName, request.Content);
        return Ok();
    }

    public class RaiseEventRequest
    {
        public string EventName { get; set; }
        public object Content { get; set; }
    }
}
