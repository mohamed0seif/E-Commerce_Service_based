﻿using Khadamati.BLL;
using Khadamati.BLL.DTO.Category;
using Khadamati.BLL.Managers.Category;
using Microsoft.AspNetCore.Mvc;

namespace Khadamati.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManger _manger;
        public CategoryController(ICategoryManger manger)
        {
            _manger = manger;
        }

        [HttpGet]
        public ActionResult<List<CategoryReadDTO>> GetAll()
        {
            return _manger.GetAll();
        }
        [HttpGet]
        [Route("GetByID")]
        public ActionResult<CategoryReadDTO> GetByID(int ID)
        {
            return _manger.Getbyid(ID);
        }

        [HttpPost]
        public ActionResult Add(CategoryAddDTO category)
        {
            var newID = _manger.Add(category);
            return Ok("Request Created Successfully");
        }

        [HttpPut]
        public ActionResult Update(CategoryUpdateDTO request)
        {
            var isFound = _manger.Update(request);
            if (!isFound) { return NotFound(); }
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var isFound = _manger.Remove(id);
            if (!isFound) { return NotFound(); }
            return NoContent();
        }
    }
}
