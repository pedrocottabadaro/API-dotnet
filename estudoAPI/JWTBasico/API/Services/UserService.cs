using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            try
            {
                return await _context.Users.ToListAsync();

            }
            catch (System.Exception)
            {

                throw;
            }


        }

        public async Task<ActionResult<User>> GetUser(int id)
        {

            try
            {
                var user = await _context.Users.FindAsync(id);
                return user;
            }
            catch (System.Exception)
            {

                throw;
            }


        }
    }
}