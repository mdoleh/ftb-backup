using ftb_backup.Parsers;
using ftb_backup.Utils;

namespace ftb_backup
{
    class MainApp
    {
        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            new App(
                new Archiver(logger),
                logger,
                new JSONParser<Config>()
            ).Run();
        }
    }
}
