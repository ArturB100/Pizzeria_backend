using Pizzeria.Model;

namespace Pizzeria.Dto
{
    
    public record NewUserDtoReq (string FirstName, string LastName, string Phone, string Email, string Password, string PasswordConfirm);   
    //public record UserShowDataDto (string FirstName, string LastName, string Phone, string Email, Address Address);
    public record UpdateUserDataDto (string FirstName, string LastName, string Phone);
    public class UserShowDataDto
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
