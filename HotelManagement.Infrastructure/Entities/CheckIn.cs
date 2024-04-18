using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Infrastructure.Entities
{
    public class CheckIn
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public int Hospede_Id { get; set; }
        [ForeignKey("Hospede_Id")]
        public TabHospedes tabHospedes { get; set; } = default!;
    }
}
