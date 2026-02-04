using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ApiLegacyHost;

public class RouteInfoService
{
    private readonly IActionContextAccessor _actionContextAccessor;

    public RouteInfoService(IActionContextAccessor actionContextAccessor)
    {
        _actionContextAccessor = actionContextAccessor;
    }

    public string GetCurrentRouteInfo()
    {
        var actionContext = _actionContextAccessor.ActionContext;
        if (actionContext == null)
        {
            return "No action context available";
        }

        var actionName = actionContext.ActionDescriptor.DisplayName;
        var routeData = actionContext.RouteData;
        
        return $"Action: {actionName}, Route: {string.Join(", ", routeData.Values.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
    }
}
