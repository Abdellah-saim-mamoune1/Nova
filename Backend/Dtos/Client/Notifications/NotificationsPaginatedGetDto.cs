namespace bankApI.BusinessLayer.Dto_s.ClientDto_s
{
    public class NotificationsPaginatedGetDto
    {
       public List<NotificationsGetDto>? Notifications { get; set; }
       public int TotalPages { get; set; }
    }


    public class NotificationsGetDto
    {
        public int Id { get; set; }
        public string Notification { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public bool IsViewed { get; set; }
    }



}
