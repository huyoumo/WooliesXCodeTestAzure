using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WooliesXCodeTestWebAPI.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WooliesXCodeTestWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        #region Public
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IEnumerable<Product>> GET(string sortOption)
        {
            var products = await GetAllProducts();

            switch (sortOption) 
            {
                case SortOptionEnum.Low:
                    var sortedLow = SortLow(products);
                    return sortedLow;
                case SortOptionEnum.High:
                    var sortedHigh = SortHigh(products);
                    return sortedHigh;
                case SortOptionEnum.Ascending:
                    var sortAscending = SortAscending(products);
                    return sortAscending;
                case SortOptionEnum.Descending:
                    var sortDescending = SortDescending(products);
                    return sortDescending;
                case SortOptionEnum.Recommended:
                    var sortRecommended = SortRecommended(await GetCustomerOrders());
                    return sortRecommended;
            }
            return null;
        }
        #endregion

        #region Private

        private IEnumerable<Product> SortLow(IEnumerable<Product> products) 
        {
            return products.ToList().OrderBy(p => p.Price);
        }

        private IEnumerable<Product> SortHigh(IEnumerable<Product> products)
        {
            return products.ToList().OrderByDescending(p => p.Price);
        }

        private IEnumerable<Product> SortAscending(IEnumerable<Product> products)
        {
            return products.ToList().OrderBy(p => p.Name);
        }

        private IEnumerable<Product> SortDescending(IEnumerable<Product> products)
        {
            return products.ToList().OrderByDescending(p => p.Name);
        }

        private async Task<List<Product>> GetAllProducts() 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync(_productApiUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonString = responseMessage.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    var products = JsonConvert.DeserializeObject<List<Product>>(jsonString.Result);
                    return products;
                }
            }
            return null;
        }

        
        private async Task<List<CustomerOrder>> GetCustomerOrders()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync(_customerOrderApiUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonString = responseMessage.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    var customerOrders = JsonConvert.DeserializeObject<List<CustomerOrder>>(jsonString.Result);
                    return customerOrders;
                }
            }
            return null;
        }

        private IEnumerable<Product> SortRecommended(IEnumerable<CustomerOrder> customerOrders) 
        {
            var result = customerOrders
                .SelectMany(co => co.Products)
                .GroupBy(p => p.Name)
                .Select(group => new Product {
                    Name = group.Key,
                    Price = group.First().Price,
                    Quantity = group.Sum(g => g.Quantity)
                })
                .OrderByDescending(p => p.Quantity)
                .ToList();
            return result;
        }

        private static string _customerOrderApiUrl = _baseAddress + "shopperHistory?token=bce3fad2-c52f-4247-82f7-836a77192814";
        private static string _productApiUrl = _baseAddress + "products?token=bce3fad2-c52f-4247-82f7-836a77192814";
        private static string _baseAddress = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/";
        #endregion
    }
}
