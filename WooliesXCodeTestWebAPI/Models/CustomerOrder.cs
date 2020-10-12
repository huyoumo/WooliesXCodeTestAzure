using System.Collections.Generic;

namespace WooliesXCodeTestWebAPI.Models
{
    public class CustomerOrder
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
