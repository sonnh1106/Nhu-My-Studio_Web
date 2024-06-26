﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NM.Studio.API.Controllers.Base;
using NM.Studio.Domain.CQRS.Commands.Events;
using NM.Studio.Domain.CQRS.Queries.Events;
using NM.Studio.Domain.Models.Messages;
using NM.Studio.Domain.Models.Events;
using NM.Studio.Domain.Results.Messages;
using NM.Studio.Domain.Results.Events;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NM.Studio.API.Controllers.Events
{
    [Route("api/event")]
    [Authorize]
    public class EventController : BaseController
    {
        public EventController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("get-event-list")]
        public async Task<IActionResult> GetAll([FromBody] EventGetAllQuery eventGetAllQuery)
        {
            MessageResults<EventResult> messageResult = await _mediator.Send(eventGetAllQuery);
            return Ok(messageResult);
        }

        // GET api/<EventController>/5
        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] EventGetByIdQuery eventGetByIdQuery)
        {
            MessageResult<EventResult> messageResult = await _mediator.Send(eventGetByIdQuery);
            return Ok(messageResult);
        }

        // POST api/<EventController>
        [HttpPost("create-event")]
        public async Task<IActionResult> Create([FromBody] EventCreateCommand eventCreateCommand)
        {
            MessageView<EventView> messageView = await _mediator.Send(eventCreateCommand);
            return Ok(messageView);
        }

        // PUT api/<EventController>/5
        [HttpPost("update-event")]
        public async Task<IActionResult> Update([FromBody] EventUpdateCommand eventUpdateCommand)
        {
            MessageView<EventView> messageView = await _mediator.Send(eventUpdateCommand);
            return Ok(messageView);
        }


        // DELETE api/<EventController>/5
        [HttpPost("delete-event")]
        public async Task<IActionResult> Delete([FromBody] EventDeleteCommand eventDeleteCommand)
        {
            MessageView<EventView> messageView = await _mediator.Send(eventDeleteCommand);
            return Ok(messageView);
        }
    }
}
