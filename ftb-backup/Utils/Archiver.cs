using SevenZip;
using System;
using System.IO;

namespace ftb_backup.Utils
{
    public class Archiver: IArchiver
    {
        private ILogger _logger;

        public Archiver(ILogger logger)
        {
            _logger = logger;
            SevenZipBase.SetLibraryPath(@"C:\Program Files\7-Zip\7z.dll");
        }

        public FileData CompressDirectory(string folderPath, string archiveName)
        {
            SevenZipCompressor compressor = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.Zip,
                PreserveDirectoryRoot = true,
                TempFolderPath = folderPath
            };

            _logger.Log($"Compressing {folderPath}");

            string cleanedArchiveName = $"{cleanArchiveName(archiveName)}.zip";
            compressor.CompressDirectory(folderPath, cleanedArchiveName);

            _logger.Log(string.Format($"Created archive {cleanedArchiveName}"));
            return new FileData
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), cleanedArchiveName),
                fileName = cleanedArchiveName
            };
        }

        private string cleanArchiveName(string archiveName)
        {
            string cleanedName = archiveName.Trim();
            int extensionIndex = cleanedName.IndexOf(".");
            if (extensionIndex != -1)
            {
                cleanedName = cleanedName.Substring(0, cleanedName.Length - extensionIndex - 1);
            }
            return cleanedName.Replace(":", "-").Replace("/", "-").Replace(" ", "-").Replace("\\", "-");
        }

        public bool ExtractArchive(string filePath, string extractDirectory)
        {
            using (SevenZipExtractor extr = new SevenZipExtractor(filePath))
            {
                try
                {
                    extr.FileExtractionStarted += extr_FileExtractionStarted;
                    extr.FileExists += extr_FileExists;
                    extr.ExtractionFinished += extr_ExtractionFinished;

                    extr.ExtractArchive(extractDirectory);
                }
                catch (FileNotFoundException ex)
                {
                    _logger.Log(ex);
                    return false;
                }
            }
            return true;
        }

        private void extr_FileExists(object sender, FileOverwriteEventArgs e)
        {
            _logger.Log($"Warning: {e.FileName} already exists; overwriting\r\n");
        }
        private void extr_FileExtractionStarted(object sender, FileInfoEventArgs e)
        {
            _logger.Log($"Extracting {e.FileInfo.FileName}");
        }

        private void extr_ExtractionFinished(object sender, EventArgs e)
        {
            _logger.Log("Extraction Completed");
        }
    }
}
