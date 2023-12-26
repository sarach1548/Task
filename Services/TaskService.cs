using TaskPro.Models;
using TaskPro.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TaskPro.Services;

public class TaskService : ITaskService{
    private List<TaskPro.Models.Task> tasks;

    public TaskService(){
        tasks=new List<TaskPro.Models.Task>
        {
            new TaskPro.Models.Task{Id=1,Name="home work",isDone=false},
            new TaskPro.Models.Task{Id=2,Name="wash dishes",isDone=true},
            new TaskPro.Models.Task{Id=3,Name="read",isDone=true}
        };
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
        return true;
    }
}

public static class TaskUtils
{
    public static void AddTaskList(this IServiceCollection services){
        services.AddSingleton<ITaskService,TaskService>();
    }
}
