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

        public void CompressFolder(string folderPath, string archiveName)
        {
            SevenZipCompressor compressor = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.Zip,
                PreserveDirectoryRoot = true
            };
            _logger.Log($"Compressing {folderPath}");
            compressor.CompressDirectory(folderPath, archiveName);
            _logger.Log(string.Format($"Created archive {archiveName}"));
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
