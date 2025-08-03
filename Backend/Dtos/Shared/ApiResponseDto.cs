namespace bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string? Title { get; set; }
        public T? Data { get; set; }
        public IEnumerable<ValidationErrorsDto>? Errors { get; set; }
    }
}
