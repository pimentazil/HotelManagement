using HotelManagement.Communication.Requests;
using HotelManagement.Communication.Responses;
using HotelManagement.Exceptions;
using HotelManagement.Infrastructure;
using HotelManagement.Infrastructure.Entities;

namespace HotelManagement.Application.UseCases.Hotels.Register
{
    public class RegisterHotelUseCase
    {

        private readonly Context _ctx;

        public RegisterHotelUseCase(Context context)
        {
            _ctx = context;
        }
        public ResponseRegisteredJson adicionarHotel(RequestHotelJson request)
        {
            Validate(request);

            var hotel = new TabHotel()
            {
                Name = request.Name,
                Details = request.Details,
                Maximum_guests = request.MaximumGuests,
            };

            _ctx.tabHotel.Add(hotel);
            _ctx.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = hotel.Id
            };

        }

        private void Validate(RequestHotelJson request)
        {
            if (request.MaximumGuests <= 0)
            {
                throw new ErrorOrValidationException("The Maximum guests is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOrValidationException("The name is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOrValidationException("The details details is invalid.");
            }
        }
    }
}
