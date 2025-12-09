using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryNew.Filters
{
    // This filter runs for every controller action.
    // It implements IActionFilter to intercept action execution.
    public class GlobalActionFilter : IActionFilter
    {
        private readonly ILogger<GlobalActionFilter> _logger;

        public GlobalActionFilter(ILogger<GlobalActionFilter> logger)
        {
            _logger = logger;
        }

        // Called BEFORE the action method executes.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Logs the name of the action being invoked.
            _logger.LogInformation("Creating Action: {ActionName}", context.ActionDescriptor.DisplayName);
        }

        // Called AFTER the action method executes.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Logs that the action has completed.
            _logger.LogInformation("Executed Action: {ActionName}", context.ActionDescriptor.DisplayName);
        }
    }
}
