using System.Collections.Generic;
using Main.Data.Core.Domain;

namespace Main.Web.Features.Admin.Models
{
    public class ConfigsModel
    {
        public List<Config> Configs { get; set; }
        public Config SelectedConfig { get; set; }
    }
}
