using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgreSqlDotnetCore.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        // GET: api/<DeliveryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeliveryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DeliveryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DeliveryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DeliveryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        //[ActionName("GetStoreProducts")]
        //[HttpGet("{store_id}")]
        //[EnableCors("ApiPolicy")]

        //[Authorize]
        //public async Task<ActionResult<string>> GetStoreProducts(int store_id, int? category_id)
        //{
        //    var StoreProduct = from p in _context.Product
        //                       where p.CategoryId == category_id
        //                       join sp in _context.ProductsStores
        //                       on p.ProductId equals sp.ProductId
        //                       where sp.StoreId == store_id
        //                       select new { productName = p.ProductName, p.ProductId, p.Category.Name, price = sp.StorePrice, discountedprice = sp.StoreDiscountedPrice, p.MainImage, p.CategoryId, p.ShortDescription, sp.StoreId, p.OrderNumber, p.NumberOfSold };
        //    //return await _context.Product.ToListAsync();

        //    var JsonStoreProducts = JsonConvert.SerializeObject(StoreProduct);

        //    return JsonStoreProducts;
        //}

        [ActionName("slots")]
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<string>> GetProductDetail(int id)
        {

            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("http://178.62.41.5/v1/graphql", UriKind.Absolute),
            };
            var graphQLClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());
            string QueryText = "query vendor_menu_products{vendors(where: { id: { _eq: 1} }) {delivery_fee,delivery_range,feature_image,id,latitude,longitude,logo,maximum_delivery_time,minimum_delivery_time,minimum_order,name,phone_number,rating,slug,address,vendor_open_times {id,date,dayTitle,endTime,startTime}menu_vendors {menu {created_at,id,image,name,parent_id,menu_products(limit: 10, offset: 0, where: { menu: { firstLevelMenuId: { _eq: 1} } }) {product {created_at,description,id,image,isInternetSale,listPrice,name,salePrice,shortDescription,updated_at filtertag_products {filtertag {id,name,isFilter}}}}}}}filtertag_user(where: { userId: { _eq: 1} }) {filtertag {id,name,isFilter}}}";
            var msg = new GraphQLRequest
            {
                //Query = "query {getClient(condition){clientName, clientID}}"
                Query = QueryText,
                //x-hasura-admin-secret = Test.1234


            };
            var graphQLResponse = await graphQLClient.SendQueryAsync<dynamic>(msg).ConfigureAwait(false);
            string resultx = graphQLResponse.Data.ToString();
            //var JsonUpCategories = JsonConvert.SerializeObject(resultx);
            //string result = JsonUpCategories;
            return resultx;
        }


        //[ActionName("productDetail")]
        //[HttpGet("{id}")]
        ////[Authorize]
        //public async Task<ActionResult<string>> GetProductDetail(int id)
        //{

        //    var graphQLOptions = new GraphQLHttpClientOptions
        //    {
        //        EndPoint = new Uri("http://167.99.196.105/v1/graphql", UriKind.Absolute),
        //    };
        //    var graphQLClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());
        //    string QueryText = "query{product_store(where:{store_id:{ _eq: " + id + "}}){product{id,title, weight, slug, price, type, image, categories{ category{ id,slug,title}}}}}";
        //    var msg = new GraphQLRequest
        //    {
        //        //Query = "query {getClient(condition){clientName, clientID}}"
        //        Query = QueryText
        //    };
        //    var graphQLResponse = await graphQLClient.SendQueryAsync<dynamic>(msg).ConfigureAwait(false);
        //    string resultx = graphQLResponse.Data.ToString();
        //    //var JsonUpCategories = JsonConvert.SerializeObject(resultx);
        //    //string result = JsonUpCategories;
        //    return resultx;
        //}







    }
}









