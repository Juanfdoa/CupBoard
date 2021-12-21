using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.DTOs
{
    public class CategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductsDTO> Products { get; set; }

    }
}
