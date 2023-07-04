using Core.Exeptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcTodoService.Middleware
{
	public class ExeptionInterceptor : Interceptor
	{
		private readonly ILogger<ExeptionInterceptor> _logger;

		public ExeptionInterceptor(ILogger<ExeptionInterceptor> logger)
		{
			_logger=logger;
		}

		public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>
			(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
		{
			try
			{
				return await continuation(request, context);
			}
			catch (NotFoundExeption ex)
			{
				var httpContext = context.GetHttpContext();
				httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
				
				throw new RpcException(Status.DefaultCancelled,ex.Message);
			}
		}
	}
}
