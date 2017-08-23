using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Main.Data.Core.Domain;

namespace Main.Web.Features.Admin.Models
{
    public class LogsModel
    {
        public List<Log> Logs { get; set; }
        [Display(Name = "Clear Date")]
        [DataType(DataType.Date)]
        public DateTime ClearLogsDate { get; set; }
    }
}
