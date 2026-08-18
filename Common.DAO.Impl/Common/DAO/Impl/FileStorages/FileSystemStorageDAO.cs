using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;

namespace TechnoPro.Common.DAO.Impl.FileStorages
{
	// Token: 0x020000CF RID: 207
	public class FileSystemStorageDAO : IFileSystemStorageDAO, IFileStorageDAO, IBaseOperationContext<FileStorageOperationContext>
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00035C18 File Offset: 0x00033E18
		public FileSystemStorageDAO(FileStorageOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00035C2C File Offset: 0x00033E2C
		public FileStructure LoadFile(FileType filetype, int addrSize, string clientVersion)
		{
			string text = this.GetFilename(filetype, addrSize);
			bool flag = !File.Exists(text);
			if (flag)
			{
				CWLogger.Logger.Error("FileSystemStorageDAO::LoadFile: File '{0}' does not exist", text);
				throw new FileNotFoundException("File does not exist", text);
			}
			string version = text.GetVersion();
			Version v = new Version(clientVersion.Trim());
			Version v2 = new Version(version.Trim());
			bool flag2 = v < v2;
			bool flag3 = !flag2;
			if (flag3)
			{
				text = this.GetSecondaryFilename(filetype, addrSize);
				version = text.GetVersion();
				CWLogger.Logger.Debug("FileSystemStorageDAO::LoadFile: Get secondary update file '{0}'", text);
			}
			else
			{
				CWLogger.Logger.Debug("FileSystemStorageDAO::LoadFile: Get primary update file '{0}'", text);
			}
			FileInfo fileInfo = new FileInfo(text);
			return new FileSystemStructure
			{
				UploadDateTime = fileInfo.LastWriteTime,
				BinaryData = File.ReadAllBytes(text),
				Version = version,
				Filename = Path.GetFileNameWithoutExtension(text),
				Extension = Path.GetExtension(text),
				AddrSize = addrSize,
				IsActive = true,
				FileType = filetype
			};
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00035D44 File Offset: 0x00033F44
		public void SaveFile(FileStructure fs)
		{
			FileSystemStructure fileSystemStructure = fs as FileSystemStructure;
			bool flag = fileSystemStructure == null;
			if (flag)
			{
				throw new ArgumentException("Argument is not of type FileSystemStructure");
			}
			fileSystemStructure.Filename = (fs.FileType.AddrSizeVersion ? string.Format("{0}.x{1}.{2}", fs.FileType.Title, fs.AddrSize, fs.Version.Replace('.', '-')) : string.Format("{0}.{1}", fs.FileType.Title, fs.Version.Replace('.', '-')));
			string path = Path.Combine(this.OpContext.ServerFileStorageFolder, string.Format("{0}.{1}", fileSystemStructure.Filename, fileSystemStructure.Extension));
			this.RemoveOldFiles(fs.FileType, fs.AddrSize);
			File.WriteAllBytes(path, fs.BinaryData);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00035E1C File Offset: 0x0003401C
		public FileVersionResp GetFileVersion(FileType filetype, int addrSize)
		{
			string filename = this.GetFilename(filetype, addrSize);
			string text = string.IsNullOrEmpty(filetype.SecondaryTitle) ? string.Empty : this.GetSecondaryFilename(filetype, addrSize);
			string fileVersion = string.IsNullOrEmpty(filename) ? string.Empty : filename.GetVersion();
			string secondaryFileVersion = string.IsNullOrEmpty(text) ? string.Empty : text.GetVersion();
			return new FileVersionResp
			{
				FileVersion = fileVersion,
				SecondaryFileVersion = secondaryFileVersion
			};
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00035E98 File Offset: 0x00034098
		private void SaveFile(string tempFilename, FileType fileType, int addSize)
		{
			try
			{
				string destFileName = Path.Combine(this.OpContext.ServerFileStorageFolder, Path.GetFileName(tempFilename));
				this.RemoveOldFiles(fileType, addSize);
				File.Move(tempFilename, destFileName);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSystemStorageDAO::SaveFile: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00035F04 File Offset: 0x00034104
		private void RemoveOldFiles(FileType fileType, int addSize)
		{
			string searchPattern = fileType.AddrSizeVersion ? string.Format("{0}.x{1}.*", fileType.Title, addSize) : string.Format("{0}.*", fileType.Title);
			string[] files = Directory.GetFiles(this.OpContext.ServerFileStorageFolder, searchPattern);
			int i = 0;
			while (i < files.Length)
			{
				string path = files[i];
				try
				{
					File.Delete(path);
				}
				catch (Exception)
				{
				}
				IL_5E:
				i++;
				continue;
				goto IL_5E;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00035F8C File Offset: 0x0003418C
		private void RemoveRecentFiles(FileStructure fs)
		{
			string searchPattern = fs.FileType.AddrSizeVersion ? string.Format("{0}.x{1}.*", fs.FileType.Title, fs.AddrSize) : string.Format("{0}.*", fs.FileType.Title);
			List<string> list = Directory.GetFiles(this.OpContext.ServerFileStorageFolder, searchPattern).ToList<string>();
			list.Sort();
			string arg = fs.FileType.AddrSizeVersion ? string.Format("{0}.x{1}.{2}", fs.FileType.Title, fs.AddrSize, fs.Version.Replace('.', '-')) : string.Format("{0}.{1}", fs.FileType.Title, fs.Version.Replace('.', '-'));
			string item = Path.Combine(this.OpContext.ServerFileStorageFolder, string.Format("{0}.{1}", arg, fs.FileType.Extension));
			int num = list.IndexOf(item);
			bool flag = num >= 0;
			if (flag)
			{
				for (int i = num + 1; i < list.Count; i++)
				{
					try
					{
						File.Delete(list[i]);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000360E4 File Offset: 0x000342E4
		private string GetFilename(FileType filetype, int addrSize)
		{
			string searchPattern = filetype.AddrSizeVersion ? string.Format("{0}.x{1}.*.{2}", filetype.Title, addrSize, filetype.Extension) : string.Format("{0}.*.{1}", filetype.Title, filetype.Extension);
			string[] files = Directory.GetFiles(this.OpContext.ServerFileStorageFolder, searchPattern);
			return files.MaxWithValue((string f) => f.GetVersion());
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0003616C File Offset: 0x0003436C
		private string GetSecondaryFilename(FileType filetype, int addrSize)
		{
			string searchPattern = filetype.AddrSizeVersion ? string.Format("{0}.x{1}.*.{2}", filetype.SecondaryTitle, addrSize, filetype.Extension) : string.Format("{0}.*.{1}", filetype.SecondaryTitle, filetype.Extension);
			string[] files = Directory.GetFiles(this.OpContext.ServerFileStorageFolder, searchPattern);
			return files.MaxWithValue((string f) => f.GetVersion());
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000361F4 File Offset: 0x000343F4
		public BinaryFile GetFile(string file)
		{
			BinaryFile result;
			try
			{
				bool flag = !File.Exists(file);
				if (flag)
				{
					CWLogger.Logger.Error("FileSystemStorageDAO::GetFile: File '{0}' does not exist", file);
					result = null;
				}
				else
				{
					FileInfo fileInfo = new FileInfo(file);
					result = new BinaryFile
					{
						ByteArray = File.ReadAllBytes(file),
						FileName = fileInfo.Name,
						FileSize = (int)fileInfo.Length
					};
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSystemStorageDAO::GetFile: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00036290 File Offset: 0x00034490
		public BinaryFile GetFile(string filename, eServerStorageSpecialFolders specialFolder)
		{
			string specialFolderPath = specialFolder.GetSpecialFolderPath(this.OpContext.ServerFileStorageFolder);
			bool flag = string.IsNullOrEmpty(specialFolderPath);
			BinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string file = Path.Combine(specialFolderPath, filename);
				result = this.GetFile(file);
			}
			return result;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000362D1 File Offset: 0x000344D1
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x000362D9 File Offset: 0x000344D9
		public FileStorageOperationContext OpContext { get; set; }
	}
}
