
namespace bankApI.BusinessLayer.Dto_s
{
    public class UpdateClientDto
    {
       
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }
       
        public string personalEmail { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
    }
}
