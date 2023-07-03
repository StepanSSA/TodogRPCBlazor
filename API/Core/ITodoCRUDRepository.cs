using Domains;

namespace Core
{
    public interface ITodoCRUDRepository
    {
        Guid AddTodo(Todo todo);
        Task<Guid> AddTodoAsync(Todo todo);
        Todo GetTodo(Guid id);
        Task<Todo> GetTodoAsync(Guid id);
        void SetComplited(Guid id);
        Task SetComplitedAsync(Guid id);
        void UnsetComplited(Guid id);
        Task UnsetComplitedAsync(Guid id);
        Guid AddSubtask(Subtask subtask);
        Task<Guid> AddSubtaskAsync(Subtask subtask);
        void SetSubtaskComplited(Guid id);
        Task SetSubtaskComplitedAsync(Guid id);
        void UnsetSubtaskComplited(Guid id);
        Task UnsetSubtaskComplitedAsync(Guid id);
        void DeleteSubtask(Guid id);
        Task DeleteSubtaskAsync(Guid id);
        void DeleteTodo(Guid id);
        Task DeleteTodoAsync(Guid id);
        List<Todo> GetAll();
        Task<List<Todo>> GetAllAsync();
    }
}
