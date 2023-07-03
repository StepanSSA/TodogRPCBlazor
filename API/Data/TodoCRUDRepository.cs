using Core;
using Core.Exeptions;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal class TodoCRUDRepository : ITodoCRUDRepository
    {

        private readonly TodoDbContext _contenxt;

        public TodoCRUDRepository(TodoDbContext contenxt)
        {
            _contenxt=contenxt;
        }

        public Guid AddSubtask(Subtask subtask)
        {   
            if(subtask.Id == Guid.Empty)
                subtask.Id = Guid.NewGuid();
            _contenxt.Subtasks.Add(subtask);

			_contenxt.SaveChanges();
			return subtask.Id;
        }

        public async Task<Guid> AddSubtaskAsync(Subtask subtask)
        {
            if (subtask.Id == Guid.Empty)
                subtask.Id = Guid.NewGuid();
            await _contenxt.Subtasks.AddAsync(subtask);

			await _contenxt.SaveChangesAsync();
			return subtask.Id;
        }

        public Guid AddTodo(Todo todo)
        {
            if (todo.Id == Guid.Empty)
                todo.Id = Guid.NewGuid();
            _contenxt.Todo.Add(todo);

			_contenxt.SaveChanges();
			return todo.Id;
        }

        public async Task<Guid> AddTodoAsync(Todo todo)
        {
            if (todo.Id == Guid.Empty)
                todo.Id = Guid.NewGuid();
            await _contenxt.Todo.AddAsync(todo);

            await _contenxt.SaveChangesAsync();
            return todo.Id;
        }

        public void DeleteSubtask(Guid id)
        {
            var subtask = _contenxt.Subtasks.Where(s => s.Id == id).FirstOrDefault() 
                ?? throw new NotFoundExeption(nameof(Subtask), id);
            _contenxt.Subtasks.Remove(subtask);

            _contenxt.SaveChanges();
        }

        public async Task DeleteSubtaskAsync(Guid id)
        {
            var subtask = await _contenxt.Subtasks.Where(s => s.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundExeption(nameof(Subtask), id);
            _contenxt.Subtasks.Remove(subtask);

            await _contenxt.SaveChangesAsync();
        }

        public void DeleteTodo(Guid id)
        {
            var todo = _contenxt.Todo.Where(t => t.Id == id).FirstOrDefault()
                ?? throw new NotFoundExeption(nameof(Todo), id);
            _contenxt.Todo.Remove(todo);

            _contenxt.SaveChanges();
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var todo = await _contenxt.Todo.Where(t => t.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundExeption(nameof(Todo), id);
            _contenxt.Todo.Remove(todo);

            await _contenxt.SaveChangesAsync();
        }

        public List<Todo> GetAll()
        {
            var todo = _contenxt.Todo.Include(s => s.Subtasks).ToList() ?? throw new NotFoundExeption(nameof(Todo), "all");
            return todo;
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            var todo = await _contenxt.Todo.Include(s => s.Subtasks).ToListAsync() ?? throw new NotFoundExeption(nameof(Todo), "all");
            return todo;
        }

        public Todo GetTodo(Guid id)
        {
            return _contenxt.Todo.Where(t => t.Id == id).FirstOrDefault() 
                ?? throw new NotFoundExeption(nameof(Todo), id);
        }

        public async Task<Todo> GetTodoAsync(Guid id)
        {
            return await _contenxt.Todo.Where(t => t.Id == id).FirstOrDefaultAsync()
               ?? throw new NotFoundExeption(nameof(Todo), id);
        }

        public void SetComplited(Guid id)
        {
            var todo = _contenxt.Todo.Where(t => t.Id == id).FirstOrDefault()
                ?? throw new NotFoundExeption(nameof(Todo), id);
            todo.Complited = true;
            _contenxt.SaveChanges();
        }

        public async Task SetComplitedAsync(Guid id)
        {
            var todo = await _contenxt.Todo.Where(t => t.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundExeption(nameof(Todo), id);
            todo.Complited = true;
            await _contenxt.SaveChangesAsync();
        }

        public void SetSubtaskComplited(Guid id)
        {
            var subtask = _contenxt.Subtasks.Where(t => t.Id == id).FirstOrDefault()
                ?? throw new NotFoundExeption(nameof(Subtask), id);
            subtask.Complited = true;
            _contenxt.SaveChanges();
        }

        public async Task SetSubtaskComplitedAsync(Guid id)
        {
            var subtask = await _contenxt.Subtasks.Where(t => t.Id == id).FirstOrDefaultAsync()
               ?? throw new NotFoundExeption(nameof(Subtask), id);
            subtask.Complited = true;
            await _contenxt.SaveChangesAsync();
        }

        public void UnsetComplited(Guid id)
        {
            var todo = _contenxt.Todo.Where(t => t.Id == id).FirstOrDefault()
                ?? throw new NotFoundExeption(nameof(Todo), id);
            todo.Complited = false;
            _contenxt.SaveChanges();
        }

        public async Task UnsetComplitedAsync(Guid id)
        {
            var todo = await _contenxt.Todo.Where(t => t.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundExeption(nameof(Todo), id);
            todo.Complited = false;
            await _contenxt.SaveChangesAsync();
        }

        public void UnsetSubtaskComplited(Guid id)
        {
            var subtask = _contenxt.Subtasks.Where(t => t.Id == id).FirstOrDefault()
                ?? throw new NotFoundExeption(nameof(Subtask), id);
            subtask.Complited = false;
            _contenxt.SaveChanges();
        }

        public async Task UnsetSubtaskComplitedAsync(Guid id)
        {
            var subtask = await _contenxt.Subtasks.Where(t => t.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundExeption(nameof(Subtask), id);
            subtask.Complited = false;
            await _contenxt.SaveChangesAsync();
        }
    }
}
