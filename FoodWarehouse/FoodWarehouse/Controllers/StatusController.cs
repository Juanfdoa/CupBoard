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
    [Route("api/status")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StatusController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public StatusController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<StatusDTO>> Get()
        {
            var status = await context.Status.ToListAsync();
            return mapper.Map<List<StatusDTO>>(status);
        }

        [HttpGet("{id:int}", Name ="getStatus")]
        public async Task<ActionResult<StatusDTO>> Get(int id)
        {
            var existeStatus = await context.Status.AnyAsync(x => x.Id == id);
            if (!existeStatus)
            {
                return NotFound();
            }

            var status = await context.Status.Include(x => x.CupBoards).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<StatusDTO>(status);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StatusCreateDTO statusCreateDTO)
        {
            var existeStatus = await context.Status.AnyAsync(x => x.Name == statusCreateDTO.Name);
            if (existeStatus)
            {
                return BadRequest($"Ya existe un estado con este nombre{statusCreateDTO.Name}");
            }

            var status = mapper.Map<status>(statusCreateDTO);

            context.Add(status);
            await context.SaveChangesAsync();

            var statusDTO = mapper.Map<StatusDTO>(status);

            return CreatedAtRoute("getStatus", new { id = status.Id }, statusDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, StatusCreateDTO statusCreateDTO)
        {
            var existeStatus = await context.Status.AnyAsync(x => x.Id == id);
            if (!existeStatus)
            {
                return NotFound();
            }

            var status = mapper.Map<status>(statusCreateDTO);
            status.Id = id;

            context.Update(status);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeStatus = await context.Status.AnyAsync(x => x.Id == id);
            if (!existeStatus)
            {
                return NotFound();
            }

            context.Remove(new status() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
