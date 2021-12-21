using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.DTOs
{
    public class CupBoardExpire
    {
        public List<CupBoardDTO> Expired { get; set; }
        public List<CupBoardDTO> Expire5Days { get; set; }
        public List<CupBoardDTO> Expire30Days { get; set; }
        public ProductsDTO Product { get; set; }
        public StatusDTO Status { get; set; }

    }

}
