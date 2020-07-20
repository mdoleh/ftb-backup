using ftb_backup.Parsers;
using ftb_backup.Utils;
using System;
using System.IO;

namespace ftb_backup
{
    class App
    {
        private IArchiver _archiver;
        private ILogger _logger;
        private Config _config;

        public App(IArchiver archiver, ILogger logger, IJSONParser<Config> jsonParser)
        {
            _archiver = archiver;
            _logger = logger;
            _config = jsonParser.ParseFile(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));
        }

        public void Run()
        {
            FileData archiveFile = archiveDirectory(_config.World);
            try
            {
                moveToBackupLocation(archiveFile);
            } 
            catch (Exception ex)
            {
                File.Delete(archiveFile.filePath);
                _logger.Log("Error detected, archive deleted");
                _logger.Log(ex);
            }
        }

        private FileData archiveDirectory(string directory)
        {
            string archiveName = DateTime.Now.ToString();
            return _archiver.CompressDirectory(directory, archiveName);
        }

        private void moveToBackupLocation(FileData archiveFile)
        {
            _logger.Log($"Moving archive:\n{archiveFile.filePath}\nto backup folder:\n{_config.Backup}\n....");
            File.Move(archiveFile.filePath, Path.Combine(_config.Backup, archiveFile.fileName));
            _logger.Log("Completed Backup!");
        }
    }
}
