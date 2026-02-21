using bankApI.BusinessLayer.Dto_s;

namespace bankApI.db_samples
{
    public static  class Sample
    {

        //Notification Types
        public static List<string> Notificationtypes = new List<string> { "Deposite", "Withdraw", "Transfer Fund", "Message", "Warning" };


        //Clinets

        public static List<PersonClientSetDto> Clients = new List<PersonClientSetDto> {
        new PersonClientSetDto
        {
            Person = new PersonDto
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = "Male",
                BirthDate = new DateOnly(1998, 5, 14),
                Email = "john.doe@email.com",
                Address = "123 Main Street, New York",
                PhoneNumber = "5551234567"
            },
            Account = new AccountDto
            {
                PassWord = "John@123",
                Balance = 1500.75
            }
        },

      new PersonClientSetDto
        {
            Person = new PersonDto
            {
                FirstName = "Sara",
                LastName = "Smith",
                Gender = "Female",
                BirthDate = new DateOnly(2001, 11, 3),
                Email = "sara.smith@email.com",
                Address = "45 Sunset Blvd, Los Angeles",
                PhoneNumber = "5559876543"
            },
            Account = new AccountDto
            {
                PassWord = "Sara@456",
                Balance = 3200.00
            }
        },

       new PersonClientSetDto
        {
            Person = new PersonDto
            {
                FirstName = "Ali",
                LastName = "Khan",
                Gender = "Male",
                BirthDate = new DateOnly(1995, 2, 20),
                Email = "ali.khan@email.com",
                Address = "78 Green Road, Chicago",
                PhoneNumber = "5552223344"
            },
            Account = new AccountDto
            {
                PassWord = "Ali@789",
                Balance = 500.25
            }
        } 
        };

        //Employee Types
        public static List<string> Employeetypes = new List<string>{ "Admin","Seller" };

        //Employees
        public static List<EmployeePersonDto> Employees=new List<EmployeePersonDto> {
         new EmployeePersonDto
        {
            Person = new PersonDto
            {
                FirstName = "Michael",
                LastName = "Brown",
                Gender = "Male",
                BirthDate = new DateOnly(1990, 7, 10),
                Email = "michael.brown@bank.com",
                Address = "12 Business Ave, Dallas",
                PhoneNumber = "5554445566"
            },
            Employee = new EmployeeDto
            {
                TypeId = 1
            },
            EmployeeAccount = new EmployeeAccountDto
            {
                Password = "Admin@123"
            }
        },

        new EmployeePersonDto
        {
            Person = new PersonDto
            {
                FirstName = "Linda",
                LastName = "Wilson",
                Gender = "Female",
                BirthDate = new DateOnly(1993, 9, 25),
                Email = "linda.wilson@bank.com",
                Address = "88 Corporate St, Miami",
                PhoneNumber = "5557778899"
            },
            Employee = new EmployeeDto
            {
                TypeId = 2
            },
            EmployeeAccount = new EmployeeAccountDto
            {
                Password = "Manager@456"
            }
        },

        new EmployeePersonDto
        {
            Person = new PersonDto
            {
                FirstName = "David",
                LastName = "Clark",
                Gender = "Male",
                BirthDate = new DateOnly(1988, 3, 18),
                Email = "david.clark@bank.com",
                Address = "101 Finance Rd, Seattle",
                PhoneNumber = "5551112233"
            },
            Employee = new EmployeeDto
            {
                TypeId = 1
            },
            EmployeeAccount = new EmployeeAccountDto
            {
                Password = "Staff@789"
            }
        }
        };

        public static List<string> TransactionsTypes = new List<string> { "Deposit", "Withdrawal","Transfer" };


    }
}
