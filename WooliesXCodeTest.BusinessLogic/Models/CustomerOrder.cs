using System.Collections.Generic;

namespace WooliesXCodeTest.BusinessLogic.Models
{
    public class CustomerOrder
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
