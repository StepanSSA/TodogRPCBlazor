using BlazorTodoClient.Models;
using static BlazorTodoClient.Pages.Todo;

namespace BlazorTodoClient
{
	public class TodoService
	{
        private readonly Todos.TodosClient _client;

		public TodoService(Todos.TodosClient Client)
        {
            _client = Client;
        }

		public async Task<List<DropItem>> LoadData()
		{
			var result = await _client.GetTodosAsync(new GetTodosRequest() { });
			var tasks = new List<DropItem>();
			var index = 0;
			foreach (var item in result.Todos)
			{
				tasks.Add(new DropItem()
				{
					Complited = item.Complited,
					id = item.Id,
					CompletionDate = item.CompletionDate.ToDateTime(),
					CreationDate = item.CreationDate.ToDateTime(),
					Description = item.Description,
					Name = item.Name,
				});

				if (tasks[index].CompletionDate?.Year == 1970)
					tasks[index].CompletionDate = null;

				if (tasks[index].Complited)
					tasks[index].Selector = "2";
				else
					tasks[index].Selector = "1";

				tasks[index].Subtasks.AddRange(item.Subtasks);

				index++;
			}

			return tasks;
		}

		public async Task<string> SaveTodo(string name, string description)
		{
			var result = await _client.AddTodoAsync(new AddTodoRequest()
			{
				Description = description,
				Name = name
			});

			return result.Id;
		}

		public async Task<string> SaveSubtask(SubtaskMessage subtask, string todoId)
		{
			var result = await _client.AddSubtaskAsync(new AddSubtaskRequest()
			{
				Complited = subtask.Complited,
				Name = subtask.Name,
				TodoId = todoId
			});
			return result.Id;
		}

		public async Task<bool> DeleteTodo(string id)
		{
			var result = await _client.DeleteTodoAsync(new DeleteTodoRequest() { Id = id });
			return result.Complited;
		}
		public async Task<bool> DeleteSubtask(string todoId, string subtaskId)
		{
			var result = await _client.DeleteSubtaskAsync(new DeleteSubtaskRequest() { Id = subtaskId });
			return result.Complited;
		}

		public async Task<bool> SetSubtaskComplited(string subtaskId)
		{
			var result = await _client.SetSubtaskComplitedAsync(new SetSubtaskComplitedRequest() { Id = subtaskId });
			return result.Complited;
		}

		public async Task<bool> UnsetSubtaskComplited(string subtaskId)
		{
			var result = await _client.UnsetSubtaskComplitedAsync(new UnsetSubtaskComplitedRequest() { Id = subtaskId });
			return result.Complited;
		}

		public async Task<bool> SetTodoComplited(string todoId)
		{
			var result = await _client.SetTodoComplitedAsync(new SetTodoComplitedRequest() { Id = todoId });
			return result.Complited;
		}

		public async Task<bool> UnsetTodoComplited(string todoId)
		{
			var result = await _client.UnsetTodoComplitedAsync(new UnsetTodoComplitedRequest() { Id = todoId });
			return result.Complited;
		}
	}
}
