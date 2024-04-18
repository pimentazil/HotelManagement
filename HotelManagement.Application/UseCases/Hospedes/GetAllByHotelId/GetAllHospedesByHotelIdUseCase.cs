using HotelManagement.Communication.Responses;
using HotelManagement.Exceptions;
using HotelManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.UseCases.Hospedes.GetAllByHotelId
{
    public class GetAllHospedesByHotelIdUseCase
    {
        private readonly Context _ctx;

        public GetAllHospedesByHotelIdUseCase(Context context)
        {
            _ctx = context;
        }

        public ResponseAllHospedesJson Execute(int hotelId)
        {
            var entity = _ctx.tabHotel.Include(ev => ev.tabHospedes).ThenInclude(hospedes => hospedes.CheckIn).FirstOrDefault(ev => ev.Id == hotelId);
            if (entity is null)
                throw new NotFoundException("An event with this id dont exist.");

            return new ResponseAllHospedesJson
            {
                tabHospedes = entity.tabHospedes.Select(hospede => new ResponseHospedeJson
                {
                    Id = hospede.Id,
                    Name = hospede.Name,
                    Email = hospede.Email,
                    CreatedAt = hospede.Created_At,
                    CheckedInAt = hospede.CheckIn?.Created_at
                }).ToList()
            };
        }
    }
}
