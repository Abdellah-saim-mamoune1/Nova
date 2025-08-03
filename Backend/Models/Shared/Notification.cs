using bankApI.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public string Body { get; set; } = string.Empty;
        [ForeignKey("NotificationsTypes")]
        public int TypeId { get; set; }
        public IEnumerable<ClientXNotifications>? clientXNotifications { get; set; }
        public NotificationsTypes? types { get; set; }

    }
}
