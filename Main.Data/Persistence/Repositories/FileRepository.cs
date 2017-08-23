using System;
using System.Collections.Generic;
using System.Linq;
using Main.Data.Core.Domain;
using Main.Data.Core.Repositories;
using Main.Data.Persistence.Entities;
using Microsoft.Extensions.Logging;

namespace Main.Data.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {

        private readonly FileContext _context;

        private readonly ILogger _logger;

        public FileRepository(FileContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("FileRepository");
        }

        public IEnumerable<FileDescriptionShort> AddFileDescriptions(FileResult fileResult)
        {
            List<string> filenames = new List<string>();
            for (int i = 0; i < fileResult.FileNames.Count(); i++)
            {


                int index = fileResult.FileNames[i].LastIndexOf("\\", StringComparison.Ordinal);
                var shortName = fileResult.FileNames[i].Substring(index + 1);

                var fileDescription = new FileDescription
                {
                    ContentType = fileResult.ContentTypes[i],
                    FileName = shortName,
                    CreatedTimestamp = fileResult.CreatedTimestamp,
                    UpdatedTimestamp = fileResult.UpdatedTimestamp,
                    Description = fileResult.Description
                };

                filenames.Add(fileResult.FileNames[i]);
                _context.FileDescriptions.Add(fileDescription);
            }

            _context.SaveChanges();
            return GetNewFiles(filenames);
        }

        private IEnumerable<FileDescriptionShort> GetNewFiles(List<string> filenames)
        {
            IEnumerable<FileDescription> x = _context.FileDescriptions.Where(r => filenames.Contains(r.FileName));
            return x.Select(t => new FileDescriptionShort { Id = t.Id, Description = t.Description });
        }

        public IEnumerable<FileDescriptionShort> GetAllFiles()
        {
            return _context.FileDescriptions.Select(
                    t => new FileDescriptionShort { Name = t.FileName, Id = t.Id, Description = t.Description });
        }

        public bool AddFile(File file)
        {
            _context.Files.Add(file);
            return _context.SaveChanges()>0;
        }

        public File GetFile(int id)
        {
            return _context.Files.Single(t => t.Id == id);
        }

        public FileDescription GetFileDescription(int id)
        {
            return _context.FileDescriptions.Single(t => t.Id == id);
        }
    }
}
