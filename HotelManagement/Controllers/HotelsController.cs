using HotelManagement.Application.UseCases.Hotels.GetById;
using HotelManagement.Application.UseCases.Hotels.Register;
using HotelManagement.Communication.Requests;
using HotelManagement.Communication.Responses;
using HotelManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly Context _ctx;

        public HotelsController(Context context)
        {
            _ctx = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult adicionarHotel([FromBody] RequestHotelJson request)
        {
            try
            {
                var hotelService = new RegisterHotelUseCase(_ctx);
                var sucesso = hotelService.adicionarHotel(request);

                return Ok(sucesso);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { mensagem = "An error occurred while processing a request.", detalhes = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseHotelJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseHotelJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var hotelService = new GetHotelByIdUseCase(_ctx);
                return Ok(hotelService.Execute(id));
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}

