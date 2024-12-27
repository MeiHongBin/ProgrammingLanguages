namespace ProgrammingLanguages.middleware_test
{
    public class HeaderValidationMiddleware
    {
        //DI容器
        private readonly RequestDelegate _next;

        public HeaderValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)//InvokeAsync:叫用HttpContext檢視元件。
        {
            if (!context.Request.Headers.ContainsKey("X-Custom-Header"))//檢查回傳標頭是否為X-Custom-Header
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Missing X-Custom-Header");
                return;
            }

            // 繼續處理請求
            await _next(context);
        }
    }
}
