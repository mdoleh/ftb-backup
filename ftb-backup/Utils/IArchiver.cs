namespace ftb_backup.Utils
{
    public interface IArchiver
    {
        bool ExtractArchive(string filePath, string extractDirectory);
    }
}
