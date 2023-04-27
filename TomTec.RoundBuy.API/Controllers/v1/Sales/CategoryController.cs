using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1.Sales
{
    [Route("v1/categories")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost("")]
        [Authorize(Roles = "manager")]
        public IActionResult CratePaymmentMethod([FromBody] CategoryDto categoryDto)
        {
            var category = _categoryRepository.Create(categoryDto.ToModel());
            return Created(ResponseMessage.Success, category);
        }

        [HttpGet("")]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.Get();
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = categories,
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.Get(id);
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = category,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = categoryDto.ToModel();
            category.Id = id;
            _categoryRepository.Update(category);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }
    }
}
