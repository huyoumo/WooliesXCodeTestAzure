using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WooliesXCodeTestWebAPI.Models;

namespace WooliesXCodeTestWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        #region Public

        [HttpPost]
        public decimal Post([FromBody] Trolley model)
        {
            var allProducts = model.Products;
            var availableQuantities = model.Quantities;
            var allSpecials = model.Specials;

            var productList = GetProductStatistics(allProducts, availableQuantities);
            var totalNotInSpecial = GetTotalForNotInSpecial(productList, allSpecials);
            var totalInSpecial = GetTotalInSpecial(allSpecials);
            return totalInSpecial + totalNotInSpecial;
        }

        #endregion

        #region Private

        private List<Product> GetProductStatistics(List<Product> products, List<ProductQuantity> quantities) 
        {
            var productList = (from p in products
                              join q in quantities on p.Name equals q.Name
                              select new Product ()
                              {
                                  Name = p.Name,
                                  Price = p.Price,
                                  Quantity = q.Quantity
                              }).ToList();
                              
            return productList;
        }

        private decimal GetTotalInSpecial(List<Special> allSpecials) 
        {
            var total = allSpecials.Sum(s => s.Total);
            return total;
        }

        private decimal GetTotalForNotInSpecial(List<Product> allProducts, List<Special> allSpecials) 
        {
            var allProductInSpecial = allSpecials
                .SelectMany(s => s.Quantities)
                .Select(p => new Product()
                    {
                        Name = p.Name,
                        Quantity = p.Quantity
                    }
                )
                .ToList();

            var total = (decimal) 0.00;
            foreach (var p in allProducts)
            {
                var matchedProduct = allProductInSpecial.FirstOrDefault(ps => ps.Name == p.Name);
                if (matchedProduct != null)
                {
                    var quantityInSpecial = matchedProduct.Quantity;
                    if (quantityInSpecial <= p.Quantity)
                    {
                        total += p.Price * (p.Quantity - quantityInSpecial);
                    }
                    else
                    {
                        throw new Exception(string.Format("Quantity {0} for Product {1} in special exceeds available quantity {2}",
                            matchedProduct.Quantity, p.Name, p.Quantity));
                    }
                }
                else 
                {
                    total += p.Price * p.Quantity;
                }
            }

            return total;
        }

        #endregion
    }
}
