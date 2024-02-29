using apiSample.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using Repository.Interfaces;
using Repository.Interfaces.Implement;

namespace apiSample.Controllers
{
    public class bookController : Controller
    {
        private readonly IbookRepo ibookRepo;
        private readonly IMapper mapper;

        public bookController(IbookRepo ibookRepo, IMapper mapper)
        {
            this.ibookRepo = ibookRepo;
            this.mapper = mapper;
        }


        [HttpPost("postBook")]
        public async Task<ActionResult<book>> addBook([FromBody] bookDto dto)
        {
            var bok = mapper.Map<book>(dto);
            var buk= await ibookRepo.CreateAsync(bok);
            return Ok(buk);
        }

        [HttpGet("GetBooks")]
        public async Task<ActionResult<IEnumerable<book>>> getAllBooks()
        {
            var books = await ibookRepo.GetAllAsync();
            return Ok(books);
        }

    }
}
