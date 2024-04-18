namespace HotelManagement.Communication.Responses
{
    public class ResponseHospedeJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CheckedInAt { get; set; }
    }
}
