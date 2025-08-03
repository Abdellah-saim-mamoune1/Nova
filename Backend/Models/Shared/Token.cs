using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Token
    {
        [Key]
        public int Id { get; set; }
        public string RefreshToken { get; set; }=string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Account? Account { get; set; }
        public EmployeeAccount? EmployeeAccount { get; set; }

    }
}
