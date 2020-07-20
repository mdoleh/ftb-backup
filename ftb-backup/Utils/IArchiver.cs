namespace ftb_backup.Utils
{
    public interface IArchiver
    {
        FileData CompressDirectory(string folderPath, string archiveName);
        bool ExtractArchive(string filePath, string extractDirectory);
    }
}
