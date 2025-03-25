using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProductsProjectAPI.Data;
using ProductsProjectAPI.Logic;

namespace ProductsProjectAPI.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("GetAllProducts")]
        public ActionResult Products()
        {
            using ( MySqlConnection connection = Global.getInstance() ) 
            {
                connection.Open();
                var products = connection.Query<Product>("SELECT p.*, c.CategoryCode FROM Product p LEFT JOIN Category c ON p.CategoryID = c.CategoryId");
                connection.Close();
                return Ok(products);
            }
        }

        [HttpPost("CreateProduct")]
        public ActionResult CreateProduct(string productCode, string name, string description, string categoryId, string image, double price)
        {
            if (!Validator.IsValidCategoryCode(productCode))
            {
                return BadRequest("The ProductCode has to be in the following format: ABC123 - " +
                    "the first 3 characters have to be letters and last 3 have to be numeric characters.");
            }
            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();
                Guid guid = Guid.NewGuid();
                var results = connection.Execute($"INSERT INTO Product(`ProductId`,`ProductCode`,`Name`,`Description`,`CategoryID`,`Price`,`Image`) " +
                    $"VALUES('{guid}','{productCode}','{name}','{description}','{categoryId}','{price}','{image}')");
                if (results == 1)
                {
                    var product = connection.Query<Product>($"SELECT * FROM Product WHERE ProductId = '{guid}'");
                    if (product.Count() > 0)
                    {
                        return Ok(product.First());
                    }
                    else return this.NotFound("The Created Product is not found");
                }
                else
                {
                    return BadRequest("Failed to create Product");
                }
            }
        }

        [HttpPut("ProductUrlUpdate")]
        public ActionResult ProductURL(string urlUpdate, string productId)
        {
            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();

                var result = connection.Execute($"UDPATE Product SET Image = '{urlUpdate}' WHERE ProductId = '{productId}'");

                connection.Close();

                if (result == 1) 
                {
                    return Ok();
                }

                return this.UnprocessableEntity();
            }
        }

        [HttpGet("GetAllCategories")]
        public ActionResult Categories()
        {
            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();
                var categories = connection.Query<Category>("SELECT * FROM Category");
                connection.Close();
                return Ok(categories);
            }
        }

        [HttpPost("CreateCategory")]
        public ActionResult CreateCategory(string categoryCode, string name, bool isActive)
        {

            if (!Validator.IsValidCategoryCode(categoryCode))
            {
                return BadRequest("The CategoryCode has to be in the following format: ABC123 - the first 3 characters have to be letters and last 3 have to be numeric characters.");
            }

            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();
                Guid guid = Guid.NewGuid();
                var results = connection.Execute($"INSERT INTO Category(CategoryId,Name,CategoryCode,IsActive) VALUES('{guid}','{name}','{categoryCode}',{isActive})");

                if (results == 1)
                {
                    var category = connection.Query<Category>($"SELECT * FROM Category WHERE CategoryId = '{guid}'");
                    if (category.Count() > 0)
                    {
                        return Ok(category.First());
                    }
                    else return this.NotFound("The Created Content is not found");
                }
                else 
                {
                    return BadRequest("Failed to create Category");
                }
            }
        }

        [HttpPatch("UpdateCategory")]
        public ActionResult UpdateCategory(Guid categoryId,string categoryCode, string name, bool isActive)
        {

            if (!Validator.IsValidCategoryCode(categoryCode))
            {
                return BadRequest("The CategoryCode has to be in the following format: ABC123 - the first 3 characters have to be letters and last 3 have to be numeric characters.");
            }

            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();
                var results = connection.Execute($"Update Category SET Name = '{name}', CategoryCode = '{categoryCode}', IsActive = '{isActive}' WHERE CategoryId = '{categoryId}'");

                if (results == 1)
                {
                    var category = connection.Query<Category>($"SELECT * FROM Category WHERE CategoryId = '{categoryId}'");
                    if (category.Count() > 0)
                    {
                        return Ok(category.First());
                    }
                    else return this.NotFound("The Created Content is not found");
                }
                else
                {
                    return BadRequest("Failed to Update Category");
                }
            }
        }
    }
}
