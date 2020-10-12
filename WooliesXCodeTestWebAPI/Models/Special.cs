using System.Collections.Generic;

namespace WooliesXCodeTestWebAPI.Models
{
    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}
