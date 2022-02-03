using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CarroController : BaseApiController
    {
        public DataContext _context { get; }
        public CarroController(DataContext context)
        {
            _context = context;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> Get(int id)
        {
            return await _context.Carros.FindAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<Carro>>> GetTodos()
        {
            return await _context.Carros.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<RegisterCarroDTO>>> Create(RegisterCarroDTO registerCarro)
        {
            if(await checkCarExists(registerCarro.Nome))return BadRequest("Car already exists");
            var carro = new Carro
            {
                Nome = registerCarro.Nome,
                Modelo = registerCarro.Modelo,
                Cor = registerCarro.Cor

            };
            _context.Carros.Add(carro);
            await _context.SaveChangesAsync();
            return Ok(await _context.Carros.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<Carro>> Update(Carro updateCarro)
        {
            var dbCarro = await _context.Carros.FindAsync(updateCarro.Id);
            if (dbCarro == null) return BadRequest("Not Found");

            
            dbCarro.Nome = updateCarro.Nome;
            dbCarro.Modelo = updateCarro.Modelo;
            dbCarro.Cor = updateCarro.Cor;

            await _context.SaveChangesAsync();
            return Ok(dbCarro);

        }


        [HttpDelete]
        public async Task<ActionResult<Carro>> Delete(int id)
        {

            var dbCarro = await _context.Carros.FindAsync(id);
            if (dbCarro == null) return BadRequest("Not Found");

            _context.Carros.Remove(dbCarro);
            await _context.SaveChangesAsync();

            return Ok(await _context.Carros.ToListAsync());

        }


        private async Task<Boolean> checkCarExists(string nomeCarro) => await _context.Carros.AnyAsync(carroDB => carroDB.Nome == nomeCarro);
    }
}