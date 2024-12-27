namespace ProgrammingLanguages.middleware_test
{
    public class RequireRunning_time
    {
    }
    public class RequestTimingMiddleware
    {
        //DI容器
        //私人 唯讀 處理http請求
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)//InvokeAsync:叫用HttpContext檢視元件。
        {
            var startTime = DateTime.UtcNow;

            // 繼續執行後續的 Middleware 或處理請求
            await _next(context);

            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;

            //context.Request.Method 請求方法
            //context.Request.Path 請求路徑
            Console.WriteLine($"Request [{context.Request.Method}] {context.Request.Path} took {duration.TotalMilliseconds} ms");
        }
    }

}
