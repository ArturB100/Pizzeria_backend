using AutoMapper;
using Pizzeria.Data;
using Pizzeria.Dto;
using Pizzeria.Model;

namespace Pizzeria.Services
{
    public class AddressService
    {
        private readonly PizzeriaContext _context;
        private readonly IMapper _mapper;
        public AddressService(PizzeriaContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public Address AddAddressIfNotExists (NewAddressDto dto)
        {

            Address mappedAddress = _mapper.Map<Address>(dto);

            Address? addressInDb = FindTheSameAddress(mappedAddress);
            if (addressInDb != null)
            {
                return addressInDb;
            } 

            _context.Address.Add(mappedAddress);
            _context.SaveChanges();
            return mappedAddress;

        }
        private Address? FindTheSameAddress (Address address)
        {
            return _context.Address.FirstOrDefault(a => a.Equals(address));
        }
        private bool CheckIfAddressExists (Address address)
        {
            return FindTheSameAddress(address) != null;
        }

        

    }
}
