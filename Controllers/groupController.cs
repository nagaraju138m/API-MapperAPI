using apiSample.Models;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Interfaces.Implement;
using System.Text.RegularExpressions;

namespace apiSample.Controllers
{
    public class groupController : Controller
    {
        private readonly IgroupRepo igroupRepo;

        public groupController(IgroupRepo igroupRepo)
        {
            this.igroupRepo = igroupRepo;
        }

        [HttpGet("getgroups")]
        public async Task<ActionResult<IEnumerable<group>>> getGroups()
        {
            var groups = await igroupRepo.GetAllAsync(); 
            return Ok(groups);
        }

        [HttpPost("postGroup")]
        public async Task<ActionResult<group>> addGroup([FromBody] group group)
        {
            var grop = await igroupRepo.CreateAsync(group);
            return Ok(grop);
        }
        [HttpPut("updateGroup")]
        public async Task<ActionResult<group>> updategroup([FromBody] group group)
        {
            try
            {
                await igroupRepo.UpdateAsync(group);
                return Ok(group);
            }
            catch(Exception ex) 
            {
                return BadRequest($"the id is not available {ex.Message}");
            }
        }

        [HttpPost("getByGroupId")]
        public async Task<ActionResult<IEnumerable<Student>>> getbyGroupid(int id)
        {
            try
            {
                var res= await igroupRepo.getbyGroupid(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest("not found");
            }
        }
    }
}


