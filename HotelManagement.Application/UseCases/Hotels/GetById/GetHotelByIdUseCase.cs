using HotelManagement.Communication.Responses;
using HotelManagement.Exceptions;
using HotelManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.UseCases.Hotels.GetById
{
    public class GetHotelByIdUseCase
    {
        private readonly Context _ctx;

        public GetHotelByIdUseCase(Context context)
        {
            _ctx = context;
        }

        public ResponseHotelJson Execute(int id)
        {
            var entity = _ctx.tabHotel.Include(ev => ev.tabHospedes).FirstOrDefault(ev => ev.Id == id);
            if (entity is null)
                throw new NotFoundException("An hotel with this id dont exist.");

            return new ResponseHotelJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Details = entity.Details,
                MaximumGuests = entity.Maximum_guests,
                GuestsAmount = entity.tabHospedes.Count(),
            };
        }
    }
}
