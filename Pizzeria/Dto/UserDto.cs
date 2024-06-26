namespace Pizzeria.Dto
{
    
    public record NewUserDtoReq (string FirstName, string LastName, string Phone, string Email, string Password, string PasswordConfirm);    
}
