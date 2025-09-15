using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestDate={RequestDate}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken > TimeSpan.FromSeconds(3))
            {
                logger.LogWarning("[SLOW] Handle Request={Request} - Response={Response} - TimeTaken={TimeTaken}",
                    typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.Seconds);
            }
            logger.LogInformation("[END] Handle Request={Request} - Response={Response} - TimeTaken={TimeTaken}",
                typeof(TRequest).Name, typeof(TResponse).Name, timeTaken);

            return response;
        }
    }
}
