using AutoMapper;
using HeartHealth.API.ViewModels;
using HeartHealth.Application.Features.BloodPressureMeasurement.Commands;
using HeartHealth.Application.Features.BloodPressureMeasurement.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeartHealth.API.Controllers
{
    [Route("api/bloodpressure")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;
        public MeasurementsController(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id:guid}", Name = "GetMeasurementById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetBloodPressureMeasurementByIdQuery { Id = id };
            var response = await _mediatr.Send(request);

            if (response.WasSuccessful == false)
            {
                return BadRequest(response.ValidationErrors);
            }

            return Ok(_mapper.Map<MeasurementVM>(response));
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostBloodPressure(BloodPressureVM viewModel)
        {
            var request = _mapper.Map<AddBloodPressureMeasurementCommand>(viewModel);
            var response = await _mediatr.Send(request);

            if (response.WasSuccessful == false)
            {
                return BadRequest(response.ValidationErrors);
            }

            var url = Url.Link("GetMeasurementById", new { id = response.Id});
            return Created(url, _mapper.Map<MeasurementVM>(response));
        }

        [HttpGet]
        [Route("{start}/{end}", Name = "GetMeasurementsInDateRange")]
        public async Task<ActionResult> GetByDateRange(string start, string end)
        {
            var request = new GetBloodPressureMeasurementByDateRangeQuery();
            try
            {
                request.Start = DateTime.Parse(start);
                request.End = DateTime.Parse(end);
            }
            catch (ArgumentException)
            {
                return BadRequest("Start and end date are required.");
            }
            catch (FormatException)
            {
                return BadRequest("Format dates as mm/dd/yyyy");
            }
            
            var response = await _mediatr.Send(request);
            return Ok(response.Measurements);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var request = new DeleteBloodPressureMeasurementByIdCommand { Id = id };
            var response = await _mediatr.Send(request);
            
            if (response.WasSuccessful == false)
            {
                return BadRequest(response.ValidationErrors);
            }

            return Ok();
        }
    }
}
