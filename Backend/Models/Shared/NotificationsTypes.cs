using System.ComponentModel.DataAnnotations;


namespace bankApI.Models.ClientXEmployeeModels
{
    public class NotificationsTypes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable< Notification >? Notifications { get; set; }
    }
}
