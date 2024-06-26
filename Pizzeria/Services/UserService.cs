using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Dto;
using Pizzeria.Model;
using System.Diagnostics;


namespace Pizzeria.Services
{
    public class UserService
    {
        private readonly PizzeriaContext _context;
        private readonly IMapper _mapper;

        public UserService(PizzeriaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User GetUserById(int id)
        {
            User user = _context.User.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public User Authenticate (string email, string password)
        {
            return _context.User.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool CheckIfEmailExists (string email)
        {
            return _context.User.FirstOrDefault(u => u.Email == email) != null;
        }
        public OperationResult RegisterNewUser (NewUserDtoReq newUserDto)
        {
            OperationResult result = new OperationResult();
            if (CheckIfEmailExists(newUserDto.Email))
            {
                result.Success = false;
                result.Errors.Add(new FieldError() { FieldKey = "email", ErrorMsg = "Podany email jest zajety" });
            }

            if (newUserDto.Password != newUserDto.PasswordConfirm) 
            {
                result.Success = false;
                result.Errors.Add(new FieldError() { FieldKey = "passwordConfirm", ErrorMsg = "Hasła sie nie zgadzaja" });
            }

            if (result.Success)
            {
                User user = _mapper.Map<User>(newUserDto);
                _context.Add(user);
                _context.SaveChanges();
            }

            return result;
        }

        public void AssignAddressToUser (Address address, User user)
        {
            user.Address = address;
            _context.SaveChanges();
        }

        public Address? GetAddressOfUser (User user)
        {
            return _context.User
                .Include(u => u.Address)
                .FirstOrDefault(u  => u.Id == user.Id)
                .Address;   
        }
      
    }
}
