using BlazorTodoClient.Models;
using BlazorTodoClient.Pages;
using Grpc.Core;
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
			var result = new GetTodosReply();
			try
			{
				result = await _client.GetTodosAsync(new GetTodosRequest() { });
			}
			catch (RpcException ex)
			{
				throw ex;
			}

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
			var result = new AddTodoReply();
			try
			{
				result = await _client.AddTodoAsync(new AddTodoRequest()
				{
					Description = description,
					Name = name
				});
			}
			catch (RpcException ex)
			{
				throw ex;
			}

			return result.Id;
		}

		public async Task<string> SaveSubtask(SubtaskMessage subtask, string todoId)
		{
			var result = new AddSubtaskReply();
			try
			{
				result = await _client.AddSubtaskAsync(new AddSubtaskRequest()
				{
					Complited = subtask.Complited,
					Name = subtask.Name,
					TodoId = todoId
				});
			}
			catch (RpcException ex)
			{
				
				throw ex;
			}
			

			return result.Id;
		}

		public async Task<ComplitedReply> DeleteTodo(string id)
		{
			var result = new ComplitedReply();
			try
			{
				result = await _client.DeleteTodoAsync(new DeleteTodoRequest() { Id = id });
			}
			catch (RpcException ex)
			{
				throw ex;
			}
			
			return result;
		}
		public async Task<ComplitedReply> DeleteSubtask(string todoId, string subtaskId)
		{
			var result = new ComplitedReply();
			try
			{
				result = await _client.DeleteSubtaskAsync(new DeleteSubtaskRequest() { Id = subtaskId });
			}
			catch (RpcException ex)
			{
				throw ex;
			}
			
			return result;
		}

		public async Task<ComplitedReply> SetSubtaskComplited(string subtaskId)
		{
			var result = new ComplitedReply();
			try
			{
				result = await _client.SetSubtaskComplitedAsync(new SetSubtaskComplitedRequest() { Id = subtaskId });
			}
			catch (RpcException ex)
			{
				throw ex;
			}
			
			return result;
		}

		public async Task<ComplitedReply> UnsetSubtaskComplited(string subtaskId)
		{
			var result = new ComplitedReply();
			try
			{
				result = await _client.UnsetSubtaskComplitedAsync(new UnsetSubtaskComplitedRequest() { Id = subtaskId });
			}
			catch (RpcException ex)
			{
				throw ex;
			}

			return result;
		}

		public async Task<ComplitedReply> SetTodoComplited(string todoId)
		{
			var result = new ComplitedReply();
			try
			{
				result = await _client.SetTodoComplitedAsync(new SetTodoComplitedRequest() { Id = todoId });
			}
			catch (RpcException ex)
			{
				throw ex;
			}
			
			return result;
		}

		public async Task<ComplitedReply> UnsetTodoComplited(string todoId)
		{
			var result = new ComplitedReply();
			try
			{
				result = await _client.UnsetTodoComplitedAsync(new UnsetTodoComplitedRequest() { Id = todoId });
			}
			catch (RpcException ex)
			{
				throw ex;
			}

			return result;
		}
	}
}
