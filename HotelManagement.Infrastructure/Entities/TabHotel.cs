using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Infrastructure.Entities
{
    public class TabHotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Maximum_guests { get; set; }
        [ForeignKey("Hotel_Id")]
        public List<TabHospedes> tabHospedes { get; set; } 
    }
}
