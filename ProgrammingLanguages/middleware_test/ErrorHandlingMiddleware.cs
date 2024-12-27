namespace ProgrammingLanguages.middleware_test
{
    public class ErrorHandlingMiddleware
    {
        //DI容器
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)//InvokeAsync:叫用HttpContext檢視元件。
        {
            try//嘗試跳至下一個管道
            {
                await _next(context);
            }
            catch (Exception ex)//擲出例外
            {
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
