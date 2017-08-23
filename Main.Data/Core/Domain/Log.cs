using System;

namespace Main.Data.Core.Domain
{
    public class Log
    {
        public int Id { get; set; }
        public string Server { get; set; }
        public string Logger { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string InnerExceptionType { get; set; }
        public bool IsHandled { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
