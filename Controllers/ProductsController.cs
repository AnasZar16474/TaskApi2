using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.DTOs.Product;
using Task2.Models;

namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Product = await context.Products.ToListAsync();
            var DTO = Product.Adapt<IEnumerable<ProductGetAllDTO>>();
            return Ok(DTO);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductCreateDTO DTO, [FromServices] IValidator<ProductCreateDTO> validator)
        {
            var validationResults = validator.Validate(DTO);
            if (!validationResults.IsValid)
            {
                var modelState = new ModelStateDictionary();
                foreach (var item in validationResults.Errors)
                {
                    modelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
                return ValidationProblem(modelState);
            }
            var Product=DTO.Adapt<Product>();
            await context.Products.AddAsync(Product);
            await context.SaveChangesAsync();
           var view= Product.Adapt<ProductGetAllDTO>();
            return Ok(view);
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            var Product =await context.Products.FindAsync(Id);
            if (Product is null)
            {
                return NotFound();
            }
            var DTO = Product.Adapt<ProductGetAllDTO>();
            return Ok(DTO);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int Id, ProductCreateDTO DTO, [FromServices] IValidator<ProductCreateDTO> validator)
        {
            var validationResults = validator.Validate(DTO);
            if (!validationResults.IsValid)
            {
                var modelState = new ModelStateDictionary();
                foreach (var item in validationResults.Errors)
                {
                    modelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
                return ValidationProblem(modelState);
            }
            var Product =await context.Products.FindAsync(Id);
            if (Product is null)
            {
                return NotFound();
            }
            DTO.Adapt(Product);
            await context.SaveChangesAsync();
            var DTOA = Product.Adapt<ProductGetAllDTO>();
            return Ok(DTOA);
        }
        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int Id)
        {
            var Product =await context.Products.FindAsync(Id);
            if (Product is null)
            {
                return NotFound();
            }
            context.Products.Remove(Product);
           await context.SaveChangesAsync();
            return Ok("Success");

        }
    }
}
