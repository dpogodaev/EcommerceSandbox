namespace EcommerceSandbox.WebMvc.Components;

public static class WebMvcConfig
{
    public static void ConfigWebMvc(this IServiceCollection services)
    {
        services.AddMvc(x => x.EnableEndpointRouting = false);
    }
}