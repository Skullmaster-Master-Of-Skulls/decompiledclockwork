using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000823 RID: 2083
	internal class StandardDiskBuildResultCache : DiskBuildResultCache
	{
		// Token: 0x0600639F RID: 25503 RVA: 0x0015D1A4 File Offset: 0x0015B3A4
		internal StandardDiskBuildResultCache(string cacheDir) : base(cacheDir)
		{
			base.EnsureDiskCacheDirectoryCreated();
			this.FindSatelliteDirectories();
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x0015D1B9 File Offset: 0x0015B3B9
		private string GetSpecialFilesCombinedHashFileName()
		{
			return BuildManager.WebHashFilePath;
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0015D1C0 File Offset: 0x0015B3C0
		internal Tuple<long, long> GetPreservedSpecialFilesCombinedHash()
		{
			string specialFilesCombinedHashFileName = this.GetSpecialFilesCombinedHashFileName();
			return StandardDiskBuildResultCache.GetPreservedSpecialFilesCombinedHash(specialFilesCombinedHashFileName);
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x0015D1DC File Offset: 0x0015B3DC
		internal static Tuple<long, long> GetPreservedSpecialFilesCombinedHash(string fileName)
		{
			if (!FileUtil.FileExists(fileName))
			{
				return Tuple.Create<long, long>(0L, 0L);
			}
			try
			{
				string[] array = Util.StringFromFile(fileName).Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				long item;
				long item2;
				if (array.Length == 2 && long.TryParse(array[0], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out item) && long.TryParse(array[1], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out item2))
				{
					return Tuple.Create<long, long>(item, item2);
				}
			}
			catch
			{
			}
			return Tuple.Create<long, long>(0L, 0L);
		}

		// Token: 0x060063A3 RID: 25507 RVA: 0x0015D270 File Offset: 0x0015B470
		internal void SavePreservedSpecialFilesCombinedHash(Tuple<long, long> hash)
		{
			string specialFilesCombinedHashFileName = this.GetSpecialFilesCombinedHashFileName();
			StandardDiskBuildResultCache.SavePreservedSpecialFilesCombinedHash(specialFilesCombinedHashFileName, hash);
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x0015D28C File Offset: 0x0015B48C
		internal static void SavePreservedSpecialFilesCombinedHash(string hashFilePath, Tuple<long, long> hash)
		{
			string directoryName = Path.GetDirectoryName(hashFilePath);
			if (!FileUtil.DirectoryExists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(hashFilePath, false, Encoding.UTF8))
			{
				streamWriter.Write(hash.Item1.ToString("x", CultureInfo.InvariantCulture));
				streamWriter.Write(';');
				streamWriter.Write(hash.Item2.ToString("x", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x0015D31C File Offset: 0x0015B51C
		private void FindSatelliteDirectories()
		{
			string[] directories = Directory.GetDirectories(this._cacheDir);
			foreach (string text in directories)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				if (!(fileNameWithoutExtension == "assembly") && !(fileNameWithoutExtension == "hash") && Util.IsCultureName(fileNameWithoutExtension))
				{
					if (StandardDiskBuildResultCache._satelliteDirectories == null)
					{
						StandardDiskBuildResultCache._satelliteDirectories = new ArrayList();
					}
					StandardDiskBuildResultCache._satelliteDirectories.Add(Path.Combine(this._cacheDir, text));
				}
			}
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x0015D3A0 File Offset: 0x0015B5A0
		internal static void RemoveSatelliteAssemblies(string baseAssemblyName)
		{
			if (StandardDiskBuildResultCache._satelliteDirectories == null)
			{
				return;
			}
			string path = baseAssemblyName + ".resources";
			foreach (object obj in StandardDiskBuildResultCache._satelliteDirectories)
			{
				string path2 = (string)obj;
				string str = Path.Combine(path2, path);
				Util.DeleteFileIfExistsNoException(str + ".dll");
				Util.DeleteFileIfExistsNoException(str + ".pdb");
			}
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x0015D430 File Offset: 0x0015B630
		internal void RemoveOldTempFiles()
		{
			this.RemoveCodegenResourceDir();
			string text = this._cacheDir + "\\";
			foreach (object obj in ((IEnumerable)FileEnumerator.Create(text)))
			{
				FileData fileData = (FileData)obj;
				if (!fileData.IsDirectory)
				{
					string extension = Path.GetExtension(fileData.Name);
					if (!(extension == ".dll") && !(extension == ".pdb") && !(extension == ".web") && !(extension == ".ccu") && !(extension == ".prof") && !(extension == ".compiled"))
					{
						if (extension != ".delete")
						{
							int num = fileData.Name.LastIndexOf('.');
							if (num > 0)
							{
								string text2 = fileData.Name.Substring(0, num);
								int num2 = text2.LastIndexOf('.');
								if (num2 > 0)
								{
									text2 = text2.Substring(0, num2);
								}
								if (FileUtil.FileExists(text + text2 + ".dll"))
								{
									continue;
								}
								if (FileUtil.FileExists(text + "App_Web_" + text2 + ".dll"))
								{
									continue;
								}
							}
							try
							{
								File.Delete(fileData.FullName);
							}
							catch
							{
							}
						}
						else
						{
							DiskBuildResultCache.CheckAndRemoveDotDeleteFile(new FileInfo(fileData.FullName));
						}
					}
				}
			}
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x0015D5E0 File Offset: 0x0015B7E0
		private void RemoveCodegenResourceDir()
		{
			string codegenResourceDir = BuildManager.CodegenResourceDir;
			if (Directory.Exists(codegenResourceDir))
			{
				try
				{
					Directory.Delete(codegenResourceDir, true);
				}
				catch
				{
				}
			}
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x0015D618 File Offset: 0x0015B818
		internal void RemoveAllCodegenFiles()
		{
			this.RemoveCodegenResourceDir();
			foreach (object obj in ((IEnumerable)FileEnumerator.Create(this._cacheDir)))
			{
				FileData fileData = (FileData)obj;
				if (fileData.IsDirectory)
				{
					if (fileData.Name == "assembly" || fileData.Name == "hash" || StringUtil.StringStartsWith(fileData.Name, "Sources_"))
					{
						continue;
					}
					try
					{
						this.DeleteFilesInDirectory(fileData.FullName);
						continue;
					}
					catch
					{
						continue;
					}
				}
				DiskBuildResultCache.TryDeleteFile(fileData.FullName);
			}
			AppDomainSetup setupInformation = Thread.GetDomain().SetupInformation;
			UnsafeNativeMethods.DeleteShadowCache(setupInformation.CachePath, setupInformation.ApplicationName);
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x0015D6FC File Offset: 0x0015B8FC
		internal void DeleteFilesInDirectory(string path)
		{
			foreach (object obj in ((IEnumerable)FileEnumerator.Create(path)))
			{
				FileData fileData = (FileData)obj;
				if (fileData.IsDirectory)
				{
					Directory.Delete(fileData.FullName, true);
				}
				else
				{
					Util.RemoveOrRenameFile(fileData.FullName);
				}
			}
		}

		// Token: 0x04003396 RID: 13206
		private const string fusionCacheDirectoryName = "assembly";

		// Token: 0x04003397 RID: 13207
		private const string webHashDirectoryName = "hash";

		// Token: 0x04003398 RID: 13208
		private static ArrayList _satelliteDirectories;
	}
}
