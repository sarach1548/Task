using TaskPro.Models;
namespace TaskPro.Services;

public static class TaskService{
    private static List<TaskPro.Models.Task> tasks;

    static TaskService(){
        tasks=new List<TaskPro.Models.Task>
        {
            new TaskPro.Models.Task{Id=1,Name="home work",isDone=false},
            new TaskPro.Models.Task{Id=2,Name="wash dishes",isDone=true},
            new TaskPro.Models.Task{Id=3,Name="read",isDone=true}
        };
    }

    public static List<TaskPro.Models.Task> GetAll()=>tasks;

    public static TaskPro.Models.Task GetById(int id){
      return tasks.FirstOrDefault(t=>t.Id==id);
    }

    public static int Add(TaskPro.Models.Task task){
        if(tasks.Count==0)
            task.Id=1;
        else
            task.Id=tasks.Max(task=>task.Id)+1;
        tasks.Add(task);
        return task.Id;
    }

    public static bool Delete(int id){
        var taskToDel=GetById(id);
        if(taskToDel==null)
            return false;
        var ind=tasks.IndexOf(taskToDel);
        if(ind==-1)
            return false;
        tasks.RemoveAt(ind);
        return true;
    }

    public static bool Update(int id ,TaskPro.Models.Task task){
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