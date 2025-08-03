namespace bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s
{
    public class ServiceResponseDto<T>
    {
        public int Status { get; set; }
        public T? Data { get; set; }
    }
}
