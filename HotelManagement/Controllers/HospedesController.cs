using HotelManagement.Application.UseCases.Hospedes.GetAllByHotelId;
using HotelManagement.Application.UseCases.Hotels.RegisterHospede;
using HotelManagement.Communication.Requests;
using HotelManagement.Communication.Responses;
using HotelManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospedesController : ControllerBase
    {
        private readonly Context _ctx;

        public HospedesController(Context context)
        {
            _ctx = context;
        }

        [HttpPost]
        [Route("{hotelId}/register")]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult Register([FromRoute] int hotelId, [FromBody] RequestRegisterHotelJson request)
        {
            try
            {
                var useCase = new RegisterHospedeOnHotelUseCase(_ctx);
                var response = useCase.Execute(hotelId, request);

                return Created(string.Empty, response);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { detalhes = ex.Message });
            }
        }


        [HttpGet]
        [Route("{hotelId}")]
        [ProducesResponseType(typeof(ResponseAllHospedesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromRoute] int hotelId)
        {
            try
            {
                var useCase = new GetAllHospedesByHotelIdUseCase(_ctx);
                var response = useCase.Execute(hotelId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { detalhes = ex.Message });
            }
        }
    }
}
