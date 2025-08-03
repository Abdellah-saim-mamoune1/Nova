
namespace bankApI.BusinessLayer.Dto_s
{
    public class GetClientInfoDto
    {
       
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
       
        public string LastName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public string PersonalEmail { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }

    }
}
