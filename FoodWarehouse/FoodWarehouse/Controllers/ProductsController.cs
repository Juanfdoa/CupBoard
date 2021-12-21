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
    [Route("api/products")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ProductsDTO>> Get()
        {
            var products = await context.Products.Include(x => x.Category).ToListAsync();
            return mapper.Map<List<ProductsDTO>>(products);
        }

        [HttpGet("{id:int}", Name ="getProduct")]
        public async Task<ActionResult<ProductsDTO>> Get(int id)
        {
            var existProduct = await context.Products.AnyAsync(x => x.Id == id);
            if (!existProduct)
            {
                return NotFound();
            }

            var product = await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ProductsDTO>(product);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<ProductsDTO>>> Get(string name)
        {
            var existProduct = await context.Products.AnyAsync(x => x.Name == name);
            if (!existProduct)
            {
                return NotFound();
            }

            var product = await context.Products.Where(x => x.Name.Contains(name)).ToListAsync();
            return mapper.Map<List<ProductsDTO>>(product);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductCreateDTO productCreateDTO)
        {
            var existProduct = await context.Products.AnyAsync(x => x.Name == productCreateDTO.Name);
            if (existProduct)
            {
                return BadRequest($"Ya existe un producto con el nombre {productCreateDTO.Name}");
            }

            var product = mapper.Map<Products>(productCreateDTO);

            context.Add(product);
            await context.SaveChangesAsync();

            var productDTO = mapper.Map<Products>(product);
            return CreatedAtRoute("getProduct", new { id = product.Id }, productCreateDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductCreateDTO productCreateDTO)
        {
            var existProduct = await context.Products.AnyAsync(x => x.Id == id);
            if (!existProduct)
            {
                return NotFound();
            }

            var product = mapper.Map<Products>(productCreateDTO);
            product.Id = id;

            context.Update(product);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existProduct = await context.Products.AnyAsync(x => x.Id == id);
            if (!existProduct)
            {
                return NotFound();
            }

            context.Remove(new Products() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
