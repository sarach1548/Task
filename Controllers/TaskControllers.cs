using Microsoft.AspNetCore.Mvc;
using TaskPro.Models;
using TaskPro.Services;

namespace TaskPro.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskControllers : ControllerBase
{
    [HttpGet]
    public ActionResult<List<TaskPro.Models.Task>> Get()=>TaskService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<TaskPro.Models.Task> Get(int id){
        var task=TaskService.GetById(id);
        if(task==null)
            return NotFound();
        return task;
    }

    [HttpPost]
    public ActionResult Post(TaskPro.Models.Task task){
        var newTask=TaskService.Add(task);
        return CreatedAtAction("post"
        ,new {id=newTask},TaskService.GetById(newTask));
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id ,TaskPro.Models.Task task){
        var ans =TaskService.Update(id,task);
        if(!ans)
            return BadRequest();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id){
        var ans =TaskService.Delete(id);
        if(!ans)
            return BadRequest();
        return NoContent();
    }
}
