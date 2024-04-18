using HotelManagement.Application.UseCases.CheckIns.DoCheckIn;
using HotelManagement.Communication.Responses;
using HotelManagement.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly Context _ctx;

        public CheckInController(Context context)
        {
            _ctx = context;
        }

        [HttpPost]
        [Route("{hospedeId}")]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult CheckIn([FromRoute] int hospedeId)
        {
            try
            {
                var useCase = new DoHospedeCheckInUseCase(_ctx);

                var response = useCase.Execute(hospedeId);

                return Created(string.Empty, response);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
