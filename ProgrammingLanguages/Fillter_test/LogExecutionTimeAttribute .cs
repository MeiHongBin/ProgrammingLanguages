using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ProgrammingLanguages.Fillter_test
{
    public class LogExecutionTimeAttribute : ActionFilterAttribute
    {
        private Stopwatch? _stopwatch;

        //OnActionExecuting:叫用動作方法前呼叫
        //ActionExecutingContext可參考:https://ithelp.ithome.com.tw/articles/10245775
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }
        //OnActionExecuting:叫用動作方法後呼叫
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            Console.WriteLine($"Action executed in {_stopwatch.ElapsedMilliseconds} ms");
        }
    }

}
