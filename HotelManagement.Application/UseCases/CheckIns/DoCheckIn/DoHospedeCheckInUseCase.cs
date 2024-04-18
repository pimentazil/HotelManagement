using HotelManagement.Communication.Responses;
using HotelManagement.Exceptions;
using HotelManagement.Infrastructure;
using HotelManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.UseCases.CheckIns.DoCheckIn
{
    public class DoHospedeCheckInUseCase
    {
        private readonly Context _ctx;

        public DoHospedeCheckInUseCase(Context context)
        {
            _ctx = context;
        }

        public bool Execute(int hospedeId)
        {
            Validate(hospedeId);

            var entity = new CheckIn
            {
                Hospede_Id = hospedeId,
                Created_at = DateTime.UtcNow,
            };

            _ctx.CheckIn.Add(entity);
            _ctx.SaveChanges();

            return true;
        }

        private void Validate(int hospedeId)
        {
            var existHospede = _ctx.tabHospedes.Any(hospede => hospede.Id == hospedeId);
            if (existHospede == false)
            {
                throw new NotFoundException("The hospede whit this Id was not found");
            }

            var existCheckin = _ctx.CheckIn.Any(ch => ch.Hospede_Id == hospedeId);
            if (existCheckin)
            {
                throw new ConflictException("Hospede can not do checking twice in the same event");
            }
        }
    }
}
