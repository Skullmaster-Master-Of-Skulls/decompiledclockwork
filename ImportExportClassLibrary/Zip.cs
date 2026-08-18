using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ImportExportClassLibrary
{
	// Token: 0x02000049 RID: 73
	public class Zip
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0001E630 File Offset: 0x0001D630
		public static void ZipUp(string inputFile)
		{
			string text = inputFile;
			string extension = Path.GetExtension(text);
			if (extension.Length > 0)
			{
				text = text.Substring(0, text.Length - extension.Length);
			}
			text += ".zip";
			Zip.ZipUp(inputFile, text);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001E678 File Offset: 0x0001D678
		public static void ZipUp(string inputFile, string outputFile)
		{
			string text = Path.GetExtension(inputFile).ToLower();
			if (text.CompareTo(".zip") == 0)
			{
				File.Copy(inputFile, outputFile);
				return;
			}
			if (File.Exists(outputFile))
			{
				File.Delete(outputFile);
			}
			ZipFile zipFile = ZipFile.Create(outputFile);
			zipFile.BeginUpdate();
			zipFile.Add(inputFile);
			zipFile.CommitUpdate();
			zipFile.Close();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001E6D4 File Offset: 0x0001D6D4
		public static void expandFolder(string zipFile, string baseFolder)
		{
			if (!Directory.Exists(baseFolder))
			{
				Directory.CreateDirectory(baseFolder);
			}
			FileStream fileStream = File.OpenRead(zipFile);
			ZipInputStream zipInputStream = new ZipInputStream(fileStream);
			for (ZipEntry nextEntry = zipInputStream.GetNextEntry(); nextEntry != null; nextEntry = zipInputStream.GetNextEntry())
			{
				if (nextEntry.IsDirectory)
				{
					Directory.CreateDirectory(baseFolder + "\\" + nextEntry.Name);
				}
				else if (nextEntry.IsFile)
				{
					if (!Directory.Exists(baseFolder + Path.GetDirectoryName(nextEntry.Name)))
					{
						Directory.CreateDirectory(baseFolder + Path.GetDirectoryName(nextEntry.Name));
					}
					FileStream fileStream2 = File.Create(baseFolder + "\\" + nextEntry.Name);
					byte[] buffer = new byte[nextEntry.Size];
					int num = 0;
					for (;;)
					{
						int num2 = zipInputStream.Read(buffer, (int)Math.Min(nextEntry.Size, (long)(num * 2048)), (int)Math.Min(nextEntry.Size - (long)((int)Math.Min(nextEntry.Size, (long)(num * 2048))), 2048L));
						if (num2 <= 0)
						{
							break;
						}
						fileStream2.Write(buffer, (int)Math.Min(nextEntry.Size, (long)(num * 2048)), num2);
						num++;
					}
					fileStream2.Close();
				}
			}
			zipInputStream.Close();
			fileStream.Close();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001E82C File Offset: 0x0001D82C
		public static string UnZip(string SrcFile)
		{
			int num = 255;
			FileStream fileStream = new FileStream(SrcFile, FileMode.Open, FileAccess.Read);
			ZipInputStream zipInputStream = new ZipInputStream(fileStream);
			ZipEntry nextEntry = zipInputStream.GetNextEntry();
			string text = nextEntry.Name;
			if (!File.Exists(text))
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

		// Token: 0x060002E9 RID: 745 RVA: 0x0001E8C4 File Offset: 0x0001D8C4
		public static void UnZip(string zipFile, string outFile)
		{
			ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFile));
			ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				FileStream fileStream = File.Create(outFile);
				long num = nextEntry.Size;
				byte[] array = new byte[num];
				for (;;)
				{
					num = (long)zipInputStream.Read(array, 0, array.Length);
					if (num <= 0L)
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
