using System;

namespace Domain
{
    public class ExceptionLogDomain
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string ErrorText { get; set; }
        public string StackTrace { get; set; }
        public DateTime Datetime { get; set; }
    }
}
