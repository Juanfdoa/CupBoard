using AutoMapper;
using FoodWarehouse.DTOs;
using FoodWarehouse.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.Controllers
{
    [ApiController]
    [Route("api/cupboard")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CupBoardController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CupBoardController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CupBoardDTO>> Get()
        {
            var cupboard = await context.CupBoard.Include(x => x.Product).ToListAsync();
            return mapper.Map<List<CupBoardDTO>>(cupboard);
        }

        [HttpGet("{id:int}", Name ="getCupBoard")]
        public async Task<ActionResult<CupBoardDTO>> Get(int id)
        {
            var existCupBoard = await context.CupBoard.AnyAsync(x => x.Id == id);
            if (!existCupBoard)
            {
                return NotFound();
            }

            var cupBoard = await context.CupBoard.Include(x => x.Product).Include(y => y.Status).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<CupBoardDTO>(cupBoard);
        }

        [HttpGet("expire")]
        public async Task<ActionResult<CupBoardExpire>> Expired ()
        {
            var today = DateTime.Now;
            var days5 = today.AddDays(5);
            var days30 = today.AddDays(30);

            var expired = await context.CupBoard.Where(x => x.ExpireDate < today).Include(x => x.Product).ToListAsync();

            var expire5Days = await context.CupBoard.Where(x => x.ExpireDate < days5 && x.ExpireDate >= DateTime.Now).ToListAsync();

            var espire30Days = await context.CupBoard.Where(x => x.ExpireDate < days30 && x.ExpireDate >= DateTime.Now).ToListAsync();

            var resultado = new CupBoardExpire();
            resultado.Expired = mapper.Map<List<CupBoardDTO>>(expired);
            resultado.Expire5Days = mapper.Map<List<CupBoardDTO>>(expire5Days);
            resultado.Expire30Days = mapper.Map<List<CupBoardDTO>>(espire30Days);
            return resultado;

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CupBoardCreateDTO cupBoardCreateDTO)
        {
            var cupBoard = mapper.Map<CupBoard>(cupBoardCreateDTO);

            context.Add(cupBoard);
            await context.SaveChangesAsync();

            var cupBoardDTO = mapper.Map<CupBoard>(cupBoard);
            return CreatedAtRoute("getCupBoard", new { id = cupBoard.Id }, cupBoardCreateDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CupBoardCreateDTO cupBoardCreateDTO)
        {
            var existCupBoard = await context.CupBoard.AnyAsync(x => x.Id == id);
            if (!existCupBoard)
            {
                return BadRequest();
            }

            var cupBoard = mapper.Map<CupBoard>(cupBoardCreateDTO);
            cupBoard.Id = id;

            context.Update(cupBoard);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existCupBoard = await context.CupBoard.AnyAsync(x => x.Id == id);
            if (!existCupBoard)
            {
                return NotFound();
            }

            context.Remove(new CupBoard() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
