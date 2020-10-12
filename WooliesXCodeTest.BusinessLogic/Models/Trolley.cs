using System.Collections.Generic;

namespace WooliesXCodeTest.BusinessLogic.Models
{
    public class Trolley
    {
        public List<Product> Products { get; set; }
        public List<Special> Specials { get; set; }
        public List<ProductQuantity> Quantities { get; set; }
    }
}
