using System;
using ClockWorkLogger;
using TechnoPro.Common.DAO;
using TechnoPro.Common.DAO.Impl.FileStorages;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.FileStorages
{
	// Token: 0x020000F2 RID: 242
	public class FileSystemStorageManager : IFileSystemStorageManager, IFileStorageManager, IBaseOperationContext<FileStorageOperationContext>
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0003C008 File Offset: 0x0003A208
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0003C010 File Offset: 0x0003A210
		public IFileSystemStorageDAO FileStorageDAO { get; set; }

		// Token: 0x06000970 RID: 2416 RVA: 0x0003C019 File Offset: 0x0003A219
		public FileSystemStorageManager(FileStorageOperationContext opContext)
		{
			this.OpContext = opContext;
			this.FileStorageDAO = new FileSystemStorageDAO(this.OpContext);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0003C040 File Offset: 0x0003A240
		public FileStructure LoadFile(string fileType, int addrSize, string clientVersion, string customerId)
		{
			FileType fileType2 = FileStorageManager.GetFileType(fileType, this.OpContext);
			string text = clientVersion;
			bool flag = false;
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = customerId + ".clientversion";
			bool flag2 = string.IsNullOrEmpty(text);
			if (flag2)
			{
				object obj = cacheStorageManager[key];
				bool flag3 = obj != null;
				if (!flag3)
				{
					return null;
				}
				text = (string)obj;
				flag = true;
				CWLogger.Logger.Debug("FileSystemStorageManager::LoadFile: Get client version value '{0}' from cache", text);
			}
			FileStructure result = this.FileStorageDAO.LoadFile(fileType2, addrSize, text);
			bool flag4 = flag;
			if (flag4)
			{
				cacheStorageManager.Remove(key);
			}
			return result;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0003C0E4 File Offset: 0x0003A2E4
		public FileStructure LoadFile(string fileType, eAddressSize addrSize, string clientVersion, string customerId)
		{
			return this.LoadFile(fileType, (int)addrSize, clientVersion, customerId);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0003C104 File Offset: 0x0003A304
		public void SaveFile(FileStructure fs)
		{
			FileType fileType = FileStorageManager.GetFileType(fs.FileType.Title, this.OpContext);
			fs.FileType = fileType;
			this.FileStorageDAO.SaveFile(fs);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0003C140 File Offset: 0x0003A340
		public FileVersionResp GetFileVersion(string fileType, int addrSize)
		{
			FileType fileType2 = FileStorageManager.GetFileType(fileType, this.OpContext);
			return this.FileStorageDAO.GetFileVersion(fileType2, addrSize);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0003C16C File Offset: 0x0003A36C
		public BinaryFile GetFile(string file)
		{
			return this.FileStorageDAO.GetFile(file);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0003C18C File Offset: 0x0003A38C
		public BinaryFile GetFile(string filename, eServerStorageSpecialFolders specialFolder)
		{
			return this.FileStorageDAO.GetFile(filename, specialFolder);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0003C1AB File Offset: 0x0003A3AB
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0003C1B3 File Offset: 0x0003A3B3
		public FileStorageOperationContext OpContext { get; set; }
	}
}
