using System;
using System.IO;
using System.IO.Compression;

namespace NLog.Targets
{
	// Token: 0x02000188 RID: 392
	internal class ZipArchiveFileCompressor : IFileCompressor
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x0002388C File Offset: 0x00021A8C
		public void CompressFile(string fileName, string archiveFileName)
		{
			using (FileStream fileStream = new FileStream(archiveFileName, FileMode.Create))
			{
				using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
				{
					using (FileStream fileStream2 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						ZipArchiveEntry zipArchiveEntry = zipArchive.CreateEntry(Path.GetFileName(fileName));
						using (Stream stream = zipArchiveEntry.Open())
						{
							fileStream2.CopyTo(stream);
						}
					}
				}
			}
		}
	}
}
