using FoodWarehouse.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string BarCode { get; set; }

       
    }
}
