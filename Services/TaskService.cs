using TaskPro.Models;
using TaskPro.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
namespace TaskPro.Services;

public class TaskService : ITaskService{
    private List<TaskPro.Models.Task> tasks=new List<TaskPro.Models.Task>();
    private string fileName;

    public TaskService(/*IWebHostEnvinronment webHost*/){
       this.fileName=Path.Combine(/*webHost.ContentRootPath,*/"wwwroot","data","tasks.json");
       using(var jsonFile=File.OpenText(fileName)){
        // tasks=new List<TaskPro.Models.Task>();
        tasks=JsonSerializer.Deserialize<List<TaskPro.Models.Task>>(jsonFile.ReadToEnd(),
        new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        });
       }
    }

    private void saveToFile()
    {
        File.WriteAllText(fileName,JsonSerializer.Serialize(tasks));
    }
    
    public  List<TaskPro.Models.Task> GetAll()=>tasks;

    public  TaskPro.Models.Task GetById(int id){
      return tasks.FirstOrDefault(t=>t.Id==id);
    }

    public  int Add(TaskPro.Models.Task task){
        if(tasks.Count==0)
            task.Id=1;
        else
            task.Id=tasks.Max(task=>task.Id)+1;
        tasks.Add(task);
        saveToFile();
        return task.Id;
    }

    public  bool Delete(int id){
        var taskToDel=GetById(id);
        if(taskToDel==null)
            return false;
        var ind=tasks.IndexOf(taskToDel);
        if(ind==-1)
            return false;
        tasks.RemoveAt(ind);
        saveToFile();
        return true;
    }

    public  bool Update(int id ,TaskPro.Models.Task task){
        if(id!=task.Id)
            return false;
        var taskToUpdate=GetById(id);
        if(taskToUpdate==null)
            return false;
        var ind=tasks.IndexOf(taskToUpdate);
        if(ind==-1)
            return false;
        tasks[ind]=task;
        saveToFile();
        return true;
    }
}

public static class TaskUtils
{
    public static void AddTaskList(this IServiceCollection services){
        services.AddSingleton<ITaskService,TaskService>();
    }
}
