namespace ProgrammingLanguages.middleware_test
{
    public class UserLoginMonitoringMiddleware
    {
        //DI容器
        private readonly RequestDelegate _next;

        public UserLoginMonitoringMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)//InvokeAsync:叫用HttpContext檢視元件。
        {
            var user = context.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                Console.WriteLine($"用戶已登入：{user.Identity.Name}");
            }
            else
            {
                Console.WriteLine("用戶未登入");
            }
            await _next(context);
        }
    }
}
