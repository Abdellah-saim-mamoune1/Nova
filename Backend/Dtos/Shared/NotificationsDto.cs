
namespace bankApI.BusinessLayer.Dto_s
{
    public class NotificationsDto
    {

        public string Title { get; set; } = string.Empty;
       
        public string Body { get; set; } = string.Empty;

        public int Type { get; set; }

        public string Account { get; set; } = string.Empty;
    }
}
