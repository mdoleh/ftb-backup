using ftb_backup.Parsers;
using ftb_backup.Utils;
using System;

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
            _config = jsonParser.ParseFile($"{Environment.CurrentDirectory}/config.json");
        }

        public void Run()
        {

        }
    }
}
