using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
using SevenZip.Compression.LZMA;
using TechnoPro.Common.Compression.Entity;

namespace TechnoPro.Common.Compression
{
	// Token: 0x02000002 RID: 2
	public static class CompressDataAdapter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static CompressionBinaryFile UncompressFirstLevelFile(CompressionBinaryFile compressedFile, string filename)
		{
			using (MemoryStream memoryStream = new MemoryStream(compressedFile.FileBytes))
			{
				using (Ionic.Zip.ZipFile zipFile = Ionic.Zip.ZipFile.Read(memoryStream))
				{
					foreach (Ionic.Zip.ZipEntry zipEntry in zipFile)
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							zipEntry.Extract(memoryStream2);
							byte[] fileBytes = memoryStream2.ToArray();
							bool flag = zipEntry.FileName.Equals(filename, StringComparison.OrdinalIgnoreCase);
							if (flag)
							{
								return new CompressionBinaryFile
								{
									FileBytes = fileBytes,
									FileName = zipEntry.FileName
								};
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002150 File Offset: 0x00000350
		public static IList<CompressionBinaryFile> UncompressFirstLevelFiles(CompressionBinaryFile compressedFile)
		{
			List<CompressionBinaryFile> list = new List<CompressionBinaryFile>();
			using (MemoryStream memoryStream = new MemoryStream(compressedFile.FileBytes))
			{
				using (Ionic.Zip.ZipFile zipFile = Ionic.Zip.ZipFile.Read(memoryStream))
				{
					foreach (Ionic.Zip.ZipEntry zipEntry in zipFile)
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							zipEntry.Extract(memoryStream2);
							byte[] fileBytes = memoryStream2.ToArray();
							list.Add(new CompressionBinaryFile
							{
								FileBytes = fileBytes,
								FileName = zipEntry.FileName
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002248 File Offset: 0x00000448
		public static CompressionBinaryFile CompressFile(CompressionBinaryFile uncompressedFile)
		{
			bool flag = uncompressedFile == null;
			CompressionBinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = CompressDataAdapter.CompressFiles(new List<CompressionBinaryFile>
				{
					uncompressedFile
				});
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002278 File Offset: 0x00000478
		public static CompressionBinaryFile CompressFiles(IList<CompressionBinaryFile> uncompressedFiles)
		{
			bool flag = uncompressedFiles == null || uncompressedFiles.Count < 1;
			CompressionBinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = uncompressedFiles[0].FileName;
				text = Path.GetFileNameWithoutExtension(text) + ".zip";
				result = CompressDataAdapter.CompressFiles(uncompressedFiles, text);
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022C8 File Offset: 0x000004C8
		public static CompressionBinaryFile CompressFiles(IList<CompressionBinaryFile> uncompressedFiles, string fn)
		{
			CompressionBinaryFile result;
			using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile())
			{
				foreach (CompressionBinaryFile compressionBinaryFile in uncompressedFiles)
				{
					zipFile.AddEntry(compressionBinaryFile.FileName, compressionBinaryFile.FileBytes);
				}
				byte[] fileBytes;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					zipFile.Save(memoryStream);
					fileBytes = memoryStream.ToArray();
				}
				result = new CompressionBinaryFile
				{
					FileBytes = fileBytes,
					FileName = fn
				};
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002390 File Offset: 0x00000590
		public static byte[] CompressTo7Z(this byte[] data)
		{
			return SevenZipHelper.Compress(data);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023A8 File Offset: 0x000005A8
		public static byte[] DecompressFrom7Z(this byte[] cData)
		{
			return SevenZipHelper.Decompress(cData);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023C0 File Offset: 0x000005C0
		public static void ZipUp(string inputFile)
		{
			string text = inputFile;
			string extension = Path.GetExtension(text);
			bool flag = extension.Length > 0;
			if (flag)
			{
				text = text.Substring(0, text.Length - extension.Length);
			}
			text += ".zip";
			CompressDataAdapter.ZipUp(inputFile, text);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002410 File Offset: 0x00000610
		public static void ZipUp(string inputFile, string outputFile)
		{
			string text = Path.GetExtension(inputFile).ToLower();
			bool flag = text.CompareTo(".zip") == 0;
			if (flag)
			{
				File.Copy(inputFile, outputFile);
			}
			else
			{
				bool flag2 = File.Exists(outputFile);
				if (flag2)
				{
					File.Delete(outputFile);
				}
				ICSharpCode.SharpZipLib.Zip.ZipFile zipFile = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(outputFile);
				zipFile.BeginUpdate();
				zipFile.Add(inputFile);
				zipFile.CommitUpdate();
				zipFile.Close();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002480 File Offset: 0x00000680
		public static void expandFolder(string zipFile, string baseFolder)
		{
			bool flag = !Directory.Exists(baseFolder);
			if (flag)
			{
				Directory.CreateDirectory(baseFolder);
			}
			FileStream fileStream = File.OpenRead(zipFile);
			ICSharpCode.SharpZipLib.Zip.ZipInputStream zipInputStream = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(fileStream);
			for (ICSharpCode.SharpZipLib.Zip.ZipEntry nextEntry = zipInputStream.GetNextEntry(); nextEntry != null; nextEntry = zipInputStream.GetNextEntry())
			{
				bool isDirectory = nextEntry.IsDirectory;
				if (isDirectory)
				{
					Directory.CreateDirectory(baseFolder + "\\" + nextEntry.Name);
				}
				else
				{
					bool isFile = nextEntry.IsFile;
					if (isFile)
					{
						bool flag2 = !Directory.Exists(baseFolder + Path.GetDirectoryName(nextEntry.Name));
						if (flag2)
						{
							Directory.CreateDirectory(baseFolder + "\\" + Path.GetDirectoryName(nextEntry.Name));
						}
						FileStream fileStream2 = File.Create(baseFolder + "\\" + nextEntry.Name);
						byte[] buffer = new byte[nextEntry.Size];
						int num = 0;
						for (;;)
						{
							int num2 = zipInputStream.Read(buffer, (int)Math.Min(nextEntry.Size, (long)(num * 2048)), (int)Math.Min(nextEntry.Size - (long)((int)Math.Min(nextEntry.Size, (long)(num * 2048))), 2048L));
							bool flag3 = num2 > 0;
							if (!flag3)
							{
								break;
							}
							fileStream2.Write(buffer, (int)Math.Min(nextEntry.Size, (long)(num * 2048)), num2);
							num++;
						}
						fileStream2.Close();
					}
				}
			}
			zipInputStream.Close();
			fileStream.Close();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002624 File Offset: 0x00000824
		public static string UnZip(string SrcFile)
		{
			int num = 255;
			FileStream fileStream = new FileStream(SrcFile, FileMode.Open, FileAccess.Read);
			ICSharpCode.SharpZipLib.Zip.ZipInputStream zipInputStream = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(fileStream);
			ICSharpCode.SharpZipLib.Zip.ZipEntry nextEntry = zipInputStream.GetNextEntry();
			string text = nextEntry.Name;
			bool flag = !File.Exists(text);
			if (flag)
			{
				text = Path.Combine(Path.GetDirectoryName(SrcFile), Path.GetFileName(text));
			}
			FileStream fileStream2 = new FileStream(text, FileMode.Create, FileAccess.Write);
			byte[] array = new byte[num];
			int num2;
			do
			{
				num2 = zipInputStream.Read(array, 0, array.Length);
				fileStream2.Write(array, 0, num2);
			}
			while (num2 > 0);
			zipInputStream.Close();
			fileStream2.Close();
			fileStream.Close();
			return text;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026D8 File Offset: 0x000008D8
		public static void UnZip(string zipFile, string outFile)
		{
			ICSharpCode.SharpZipLib.Zip.ZipInputStream zipInputStream = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(zipFile));
			ICSharpCode.SharpZipLib.Zip.ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				FileStream fileStream = File.Create(outFile);
				long num = nextEntry.Size;
				byte[] array = new byte[num];
				for (;;)
				{
					num = (long)zipInputStream.Read(array, 0, array.Length);
					bool flag = num > 0L;
					if (!flag)
					{
						break;
					}
					fileStream.Write(array, 0, (int)num);
				}
				fileStream.Close();
			}
		}
	}
}
