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
    [Route("api/categories")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CategoriesDTO>> Get()
        {
            var categories = await context.Categories.ToListAsync();
            return mapper.Map<List<CategoriesDTO>>(categories);

        }

        [HttpGet("{id:int}", Name ="getCategory")]
        public async Task<ActionResult<CategoriesDTO>> Get(int id)
        {
            var existCategory = await context.Categories.AnyAsync(x => x.Id == id);
            if(!existCategory)
            {
                return NotFound();
            }

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<CategoriesDTO>(category);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<CategoriesDTO>>> Get([FromRoute]string name)
        {
            var existeCategory = await context.Categories.AnyAsync(x => x.Name == name);

            if (!existeCategory)
            {
                return NotFound();
            }
            var category = await context.Categories.Where(x => x.Name.Contains(name)).ToListAsync();
            return mapper.Map<List<CategoriesDTO>>(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriesCreateDTO categoriesCreateDTO)
        {
            var existeCategory = await context.Categories.AnyAsync(x => x.Name == categoriesCreateDTO.Name);

            if (existeCategory)
            {
                return BadRequest($"Ya existe una categoria con el nombre {categoriesCreateDTO.Name}");
            }

            var category = mapper.Map<Categories>(categoriesCreateDTO);

            context.Add(category);
            await context.SaveChangesAsync();

            var categoryDTO = mapper.Map<Categories>(category);

            return CreatedAtRoute("getCategory", new { id = category.Id }, categoriesCreateDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriesCreateDTO categoriesCreateDTO)
        {
            var existeCategory = await context.Categories.AnyAsync(x => x.Id == id);

            if (!existeCategory)
            {
                return NotFound();
            }

            var category = mapper.Map<Categories>(categoriesCreateDTO);
            category.Id = id;

            context.Update(category);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeCategory = await context.Categories.AnyAsync(x => x.Id == id);

            if (!existeCategory)
            {
                return NotFound();
            }

            context.Remove(new Categories() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
