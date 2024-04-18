namespace HotelManagement.Infrastructure.Entities
{
    public class TabHospedes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Hotel_Id { get; set; }
        public DateTime Created_At { get; set; }
        public CheckIn? CheckIn { get; set; }
    }
}
