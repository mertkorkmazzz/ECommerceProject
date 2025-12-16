using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOf;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOf , IMapper mapper)
        {
            this._unitOf = unitOf;
            this._mapper = mapper;
        }




        public async Task CreateAsync(UserCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("email boş olamaz");


            var user = _mapper.Map<User>(dto);

            await _unitOf.Repository<User>().AddAsync(user);
            await _unitOf.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _unitOf.Repository<User>().GetByIdAsync(id);

            if(user == null)
                throw new Exception("kullanıcı bulunamadı");

            _unitOf.Repository<User>().Delete(user);
            await _unitOf.SaveAsync();
        }

        public async Task<List<UserListDto>> GetAllAsync()
        {
            var users = await _unitOf.Repository<User>().GetAllAsync();
            return _mapper.Map<List<UserListDto>>(users);
        }

        public async Task<UserListDto> GetByIdAsync(int id)
        {
           var user = await _unitOf.Repository<User>().GetByIdAsync(id);

            return user == null ? null : _mapper.Map<UserListDto>(user);
        }

        public async Task UpdateAync(UserUpdateDto dto)
        {
            var user = await _unitOf.Repository<User>().GetByIdAsync(dto.Id);

            if(user == null)
                throw new Exception("kullanıcı bulunamadı");


            _mapper.Map(dto, user);


            _unitOf.Repository<User>().Update(user);
            await _unitOf.SaveAsync();
        }
    }
}
