using HotelManagement.Communication.Requests;
using HotelManagement.Communication.Responses;
using HotelManagement.Exceptions;
using HotelManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace HotelManagement.Application.UseCases.Hotels.RegisterHospede
{
    public class RegisterHospedeOnHotelUseCase
    {
        private readonly Context _ctx;

        public RegisterHospedeOnHotelUseCase(Context context)
        {
            _ctx = context;
        }
        public ResponseRegisteredJson Execute(int hotelId, RequestRegisterHotelJson request)
        {
            Validate(hotelId, request);

            var entity = new Infrastructure.Entities.TabHospedes
            {
                Email = request.Email,
                Name = request.Name,
                Hotel_Id = hotelId,
                Created_At = DateTime.UtcNow,
            };

            _ctx.tabHospedes.Add(entity);
            _ctx.SaveChanges();


            return new ResponseRegisteredJson
            {
                Id = entity.Id,
            };
        }
        private void Validate(int hotelId, RequestRegisterHotelJson request)
        {
            var hotelEntity = _ctx.tabHotel.Find(hotelId);
            if (hotelEntity is null)
                throw new NotFoundException("An event with this id dont exist.");

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOrValidationException("The name is invalid.");
            }

            var emailIsvalid = EmailIsValid(request.Email);
            if (emailIsvalid == false)
            {
                throw new ErrorOrValidationException("The email is invalid.");
            }

            var attendeeAlreadyRegistered = _ctx
                .tabHospedes
                .Any(hotel => hotel.Email.Equals(request.Email) && hotel.Hotel_Id == hotelId);

            if (attendeeAlreadyRegistered)
            {
                throw new ConflictException("You can not register twice on the same event.");
            }

            var hospedesForHotel = _ctx.tabHospedes.Count(attendee => attendee.Hotel_Id == hotelId);
            if (hospedesForHotel == hotelEntity.Maximum_guests)
            {
                throw new ErrorOrValidationException("There is no room for this event.");
            }
        }

        private bool EmailIsValid(string email)
        {
            try
            {
                new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
