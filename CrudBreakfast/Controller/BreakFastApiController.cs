using CrudBreakfast.Contracts.BreakFast;
using CrudBreakfast.Models;
using CrudBreakfast.Service.Breakfast;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CrudBreakfast.Controller
{
	public class BreakFastApiController : ApiController
	{
		private readonly IBreakFastService _breakFastService;

		public BreakFastApiController(IBreakFastService breakFastService)
		{
			_breakFastService = breakFastService;
		}
		// Create breakfats
		[HttpPost("breakfast")]
		public IActionResult CreateBreakFast(CreateBreakFastReq req)
        {

            ErrorOr<BreakFast> breakfastReq = BreakFast.Create( req.Name, req.Description, req.StartDateTime, req.EndDateTime, req.Savory, req.Sweet);

            if (breakfastReq.IsError)
			{
				return Problem(breakfastReq.Errors);
			}

			var breakfast = breakfastReq.Value;
			ErrorOr<Created> createdBreakfast = _breakFastService.CreateBreakFast(breakfast);

			return createdBreakfast.Match(
				created => CreatedAtGet(breakfast),
				errors => Problem(errors)
			);
			
        }

        private IActionResult CreatedAtGet(BreakFast breakfast)
        {
            return CreatedAtAction
            (
                actionName: nameof(GetSingleBreakFast),
                routeValues: new { id = breakfast.Id },
                value: MapBreakfastResponse(breakfast)
            );
        }

        // Get Breakfast
        [HttpGet("breakfast/{id:guid}")]
		public IActionResult GetSingleBreakFast(Guid id)
		{
			ErrorOr<BreakFast> getBreakfastResult = _breakFastService.GetSingleBreakFast(id);

			return getBreakfastResult.Match(
				breakfast => Ok(MapBreakfastResponse(breakfast)),
				errors => Problem(errors)
			);
		}

		private static BreakFastResponse MapBreakfastResponse(BreakFast breakfast)
		{
			return new BreakFastResponse(
							breakfast.Id,
							breakfast.Name,
							breakfast.Description,
							breakfast.StartDateTime,
							breakfast.EndDateTime,
							breakfast.LastModifiedDateTime,
							breakfast.Savory,
							breakfast.Sweet
							);
		}

		// Update
		[HttpPut("breakfast/{id:guid}")]
		public IActionResult UpdateBreakFast(Guid id, UpsertBreakFastReq req)
		{
			ErrorOr<BreakFast> breakfastRes = BreakFast.Create(req.Name, req.Description, req.StartDateTime, req.EndDateTime,  req.Savory, req.Sweet, id);

			if (breakfastRes.IsError)
			{
				return Problem(breakfastRes.Errors);
			}

			var breakfast = breakfastRes.Value;
			ErrorOr<UpdateBreakfast> updatedRes = _breakFastService.UpdateBreakFast(id, breakfast);
			
			return updatedRes.Match(
				updated => updated.IsNewlyCreated ? CreatedAtGet(breakfast) :NoContent(),
				errors => Problem(errors)
			);
		}

		// Delete action
		[HttpDelete("breakfast/{id:guid}")]
		public IActionResult DeleteBreakFast(Guid id)
		{
			ErrorOr<Deleted> deletedBreakfast = _breakFastService.DeleteBreakFast(id);

			return deletedBreakfast.Match(
				deleted => NoContent(),
				errors => Problem(errors));
		}

		// Get all breakfasts
		[HttpGet("breakfasts")]
		public IActionResult GetAllBreakFast()
		{
			var breakfasts = _breakFastService.GetAllBreakFast();
			return Ok(breakfasts);
		}

	}
}