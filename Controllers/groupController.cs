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


FileDTO file = new FileDTO();
if (taskUploadData != null && (taskUploadData.TaskId ?? 0) != 0)
{
    var taskData = await _taskServices.GetTaskbyId(taskUploadData.TaskId);
    string folderName = (type == 1) ? taskData.TaskId + "_" + taskData.Title + "/TaskData" : (type == 2) ? taskData.TaskId + "_" + taskData.Title + "/TraningData" : (type == 3) ? taskData.TaskId + "_" + taskData.Title + "/InstructionData" : "";
    if (type == 1)
    {
        foreach (var upload in taskUploadData.Files)
        {
            file = upload != null ? await _taskDataContainer.UploadTaskData(upload, folderName) : new FileDTO();
            taskUploadData.TaskFileName = file.FileName;
            taskUploadData.TaskFileBlobUrl = file.FileBlobUrl;
            taskUploadData.Id = 0;
            var dataModel = ContextMapper.TaskUploadData(taskUploadData);
            if (dataModel != null)
            {
                taskUploadData.Id = await _taskServices.AddUploadeData(dataModel);
                if (taskUploadData.Id != 0)
                {
                    clientResponse.Status = taskUploadData.ResultStatus = 1;
                    clientResponse.Message = "Task Data Uploaded Successfully";
                }
                else
                {
                    clientResponse.Status = taskUploadData.ResultStatus = 2;
                    clientResponse.Message = "Error! while creating record.";
                }
            }
            taskUploads.Add(taskUploadData);   