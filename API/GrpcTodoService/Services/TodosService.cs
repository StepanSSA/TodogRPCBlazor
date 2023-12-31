﻿using Core;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Domains;

namespace GrpcTodoService.Services
{
    public class TodosService : Todos.TodosBase
    {
        private readonly ILogger<TodosService> _logger;
        private readonly ITodoCRUDRepository _repository;

        public TodosService(ILogger<TodosService> logger, ITodoCRUDRepository todoCRUDRepository)
        {
            _logger = logger;
            _repository = todoCRUDRepository;
        }

        public override async Task<AddSubtaskReply> AddSubtask(AddSubtaskRequest request, ServerCallContext context)
        {
            var id = await _repository.AddSubtaskAsync(new Subtask()
            {
                Complited = request.Complited,
                Name = request.Name,
                Todo = _repository.GetTodo(new Guid(request.TodoId)),
            });

            return new AddSubtaskReply() {Id = id.ToString() };
        }

        public override async Task<AddTodoReply> AddTodo(AddTodoRequest request, ServerCallContext context)
        {
            var id = await _repository.AddTodoAsync(new Todo()
            {
                Complited = false,
                Name = request.Name,
                Description = request.Description,
                CreationDate = DateTime.UtcNow,
            });

            return new AddTodoReply() { Id = id.ToString() };
        }

        public override async Task<ComplitedReply> SetSubtaskComplited(SetSubtaskComplitedRequest request, ServerCallContext context)
        {
            await _repository.SetSubtaskComplitedAsync(new Guid(request.Id));
            return new ComplitedReply() { Complited = true };
        }

        public override async Task<GetTodosReply> GetTodos(GetTodosRequest request, ServerCallContext context)
        {
            return await CreateTodosReply(request);
        }

        public override async Task<ComplitedReply> SetTodoComplited(SetTodoComplitedRequest request, ServerCallContext context)
        {
            await _repository.SetComplitedAsync(new Guid(request.Id));
            return new ComplitedReply() { Complited = true };
        }

        public override async Task<ComplitedReply> UnsetSubtaskComplited(UnsetSubtaskComplitedRequest request, ServerCallContext context)
        {
            await _repository.UnsetSubtaskComplitedAsync(new Guid(request.Id));
            return new ComplitedReply() { Complited = true };
        }

        public override async Task<ComplitedReply> UnsetTodoComplited(UnsetTodoComplitedRequest request, ServerCallContext context)
        {
            await _repository.UnsetComplitedAsync(new Guid(request.Id));
            return new ComplitedReply() { Complited = true };
        }

        public override async Task<ComplitedReply> DeleteSubtask(DeleteSubtaskRequest request, ServerCallContext context)
        {
            await _repository.DeleteSubtaskAsync(new Guid(request.Id));
            return new ComplitedReply() { Complited = true };
        }

        public override async Task<ComplitedReply> DeleteTodo(DeleteTodoRequest request, ServerCallContext context)
        {
            await _repository.DeleteTodoAsync(new Guid(request.Id));
            return new ComplitedReply() { Complited = true };
        }

        private async Task<GetTodosReply> CreateTodosReply(GetTodosRequest request)
        {
            var data = await _repository.GetAllAsync();
            var reply = new GetTodosReply();
            var date = new Timestamp();

            for (int i = 0; i < data.Count; i++)
            {
				var subtaskReply = new List<SubtaskMessage>();
				foreach (var subtask in data[i].Subtasks)
				{
					subtaskReply.Add(new SubtaskMessage()
					{
						Complited = subtask.Complited,
						Id = subtask.Id.ToString(),
						Name = subtask.Name,
					});
				}

				if (data[i].СompletionDate.Date != new DateTime(0001, 01, 01))
					date = Timestamp.FromDateTime(data[i].СompletionDate);

				reply.Todos.Add(new TodosMessage()
				{
					Id = data[i].Id.ToString(),
					Complited = data[i].Complited,
					Description = data[i].Description,
					Name = data[i].Name,
					CompletionDate = date,
					CreationDate = Timestamp.FromDateTime(data[i].CreationDate),
				});
				reply.Todos[i].Subtasks.AddRange(subtaskReply);
			}

            return reply;
        }
    }
}
