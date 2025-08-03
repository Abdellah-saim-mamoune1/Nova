using bankApI.BusinessLayer.Dto_s;

namespace bankApI.Dto_s.Employee
{
    public class GetPaginatedClientsInfoDto
    {

        public List<GetClientInfoDto>? Clients { get; set; }

        public int TotalPages { get; set; }

    }
}
