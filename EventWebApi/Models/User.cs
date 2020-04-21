namespace EventWebApi.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public Company Company { get; set; }
        public Address Address { get; set; }

    }
}