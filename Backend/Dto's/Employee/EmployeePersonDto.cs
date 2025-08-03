namespace bankApI.BusinessLayer.Dto_s
{
    public class EmployeePersonDto
    {
       public required  PersonDto Person { get; set; }
       public required  EmployeeDto Employee { get; set; }
       public required  EmployeeAccountDto EmployeeAccount { get; set; }

    }
}
