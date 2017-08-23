using System;
using System.Collections.Generic;
using System.Linq;
using Main.Data.Core.Domain;
using Main.Data.Persistence;
using Main.Data.Persistence.Entities;

namespace Main.Data.Core.Repositories
{
    public class IUserLogging
    {
        private readonly ApplicationDbContext _context;
        public IUserLogging(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Log> GetLogs()
        {
            return _context.Logs
              .OrderByDescending(x => x.Timestamp)
              .ToList();
        }

        public bool AddLog(Log log)
        {
            log.Timestamp = log.Timestamp == DateTime.MinValue ? DateTime.UtcNow : log.Timestamp;
            log.Username = log.Username ?? "Unknown";
            _context.Logs.Add(log);
            return _context.SaveChanges() > 0;
        }

        public bool AddError(Exception ex, string message, string source)
        {
            Log log = new Log()
            {
                Message = message,
                ExceptionType = ex.GetType().ToString(),
                InnerExceptionType = ex.InnerException?.GetType().ToString(),
                IsHandled = true,
                Source = source,
                Logger = "Main.Data.Repositories",
                Server = Environment.MachineName

            };
            return AddLog(log);
        }

    }
}
