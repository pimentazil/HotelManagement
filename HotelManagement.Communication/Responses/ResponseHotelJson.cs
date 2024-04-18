namespace HotelManagement.Communication.Responses
{
    public class ResponseHotelJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int MaximumGuests { get; set; }
        public int GuestsAmount { get; set; }

    }
}
