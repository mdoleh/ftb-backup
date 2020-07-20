namespace ftb_backup.Utils
{
    public interface IArchiver
    {
        string CompressDirectory(string folderPath, string archiveName);
        bool ExtractArchive(string filePath, string extractDirectory);
    }
}
