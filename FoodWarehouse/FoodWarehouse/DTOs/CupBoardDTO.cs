using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.DTOs
{
    public class CupBoardDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Quantity { get; set; }
        public int StatusId { get; set; }
        public ProductsDTO Product { get; set; }
        public StatusDTO Status { get; set; }
    }
}
