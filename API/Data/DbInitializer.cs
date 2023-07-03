

using Domains;

namespace Data
{
    public class DbInitializer
    {
        public static void Initialize(TodoDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            seed(dbContext);
        }

        private static void seed(TodoDbContext dbContext) 
        {
            if (dbContext.Todo.Any()) return;

            var todo = new Todo() 
            {
                Id = Guid.NewGuid(),
                Name = "First",
                Complited = true,
                CreationDate = DateTime.UtcNow,
                Description = "Description",
                Subtasks = new List<Subtask>(),
                СompletionDate = DateTime.UtcNow,
            };

            var subtask = new Subtask()
            {
                Complited = true,
                Name = "1",
                Id = Guid.NewGuid(),
                Todo = todo,
            };
            todo.Subtasks.Add(subtask);

            dbContext.Todo.Add(todo);
            dbContext.Subtasks.Add(subtask);
            
            dbContext.SaveChanges();
        }
    }
}
