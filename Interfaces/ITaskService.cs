using TaskPro.Models;
namespace TaskPro.Interfaces;

public interface ITaskService{

    List<TaskPro.Models.Task> GetAll();

    TaskPro.Models.Task GetById(int id);
    
    int Add(TaskPro.Models.Task task);

    bool Delete(int id);

    bool Update(int id ,TaskPro.Models.Task task);
}