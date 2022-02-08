using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(DataContext context,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerData)
        {
            try
            {
                if(await UserExists(registerData.Username))return BadRequest("User Exists");

                using var hmac = new HMACSHA512();
                var newUser= new User{
                    Username = registerData.Username,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerData.Password)),
                    PasswordSalt = hmac.Key,
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return new UserDTO{
                    Username = newUser.Username,
                    Token = _tokenService.CreateToken(newUser)
                };


            }
            catch (System.Exception)
            {
                
                return BadRequest("A problem has occured");
            }
        

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginData)
        {
            try
            {
                var existingUser = await _context.Users.SingleOrDefaultAsync(userDB=>userDB.Username == loginData.Username.ToLower());
                if(existingUser==null)return BadRequest("User not found");
                using var hmac = new HMACSHA512(existingUser.PasswordSalt);
                var hashLogin=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginData.Password));

                for (int i = 0; i < hashLogin.Length; i++)
                {
                    if(hashLogin[i]!=existingUser.PasswordHash[i])return BadRequest("Wrong Password or Username");
                }

                return new UserDTO{
                    Username = loginData.Username,
                    Token = _tokenService.CreateToken(existingUser)
                };
            }
            catch (System.Exception)
            {
                return BadRequest("An Error has occured");
            }
        
           

        }
        private async Task<Boolean> UserExists(string username) => await _context.Users.AnyAsync(userDB => userDB.Username == username.ToLower());
    }
}