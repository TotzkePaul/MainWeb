using System;
using Main.Core.Localization.Models;
using Microsoft.Extensions.Localization;

namespace Main.Core.Localization
{
    public class DbStringLocalizerFactory : IStringLocalizerFactory
    {
        public DbStringLocalizerFactory()
        {
            var db = new LocalizationDbContext();
            LocalizationConfig config = new LocalizationConfig();
            var cultures = config.GetCultures();
            db.AddRange(cultures);
            
            db.SaveChanges();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new DbStringLocalizer(new LocalizationDbContext());
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new DbStringLocalizer(new LocalizationDbContext());
        }
    }
}
