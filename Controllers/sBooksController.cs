using apiSample.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using Repository.Interfaces;

namespace apiSample.Controllers
{
    public class sBooksController : Controller
    {
        private readonly IsBookRepo isBookRepo;
        private readonly IMapper mapper;

        public sBooksController(IsBookRepo isBookRepo, IMapper mapper)
        {
            this.isBookRepo = isBookRepo;
            this.mapper = mapper;
        }

        [HttpGet("getStuBooks")]
        public async Task<ActionResult<IEnumerable<sBooks>>> GetStuBooks()
        {
            var res = await isBookRepo.GetAllAsync();
            return Ok(res);
        }

        [HttpPost("addBook")]
        public async Task<ActionResult<sBooks>> AddsBooks([FromBody] sbookDto sdto)
        {
            try
            {
                var stBook = mapper.Map<sBooks>(sdto);
                var isexist = await isBookRepo.isExist(stBook);
                if (isexist == false)
                {
                    var res = await isBookRepo.CreateAsync(stBook);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Book is already assigned to student");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateSbooks")]
        public async Task<ActionResult<sBooks>> update(sBooks sBooks)
        {
            try
            {
                var res = isBookRepo.UpdateAsync(sBooks);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
