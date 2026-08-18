using System;
using TechnoPro.Common.DAO;
using TechnoPro.Common.DAO.Impl;
using TechnoPro.Common.DAO.Impl.FileStorages;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core
{
	// Token: 0x0200001E RID: 30
	public class FileStorageManager : IFileStorageManager
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005FC4 File Offset: 0x000041C4
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00005FCC File Offset: 0x000041CC
		public IFileStorageDAO FileStorageDAO { get; set; }

		// Token: 0x060000ED RID: 237 RVA: 0x00005FD5 File Offset: 0x000041D5
		public FileStorageManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.FileStorageDAO = new FileStorageDAO(opContext);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005FF4 File Offset: 0x000041F4
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00005FFC File Offset: 0x000041FC
		public OperationContext OpContext { get; set; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00006008 File Offset: 0x00004208
		public FileStructure LoadFile(string fileType, int addrSize, string clientVersion, string customerId)
		{
			FileType fileType2 = FileStorageManager.GetFileType(fileType, this.OpContext);
			return this.FileStorageDAO.LoadFile(fileType2, addrSize, clientVersion);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006038 File Offset: 0x00004238
		public FileStructure LoadFile(string fileType, eAddressSize addrSize, string clientVersion, string customerId)
		{
			return this.LoadFile(fileType, (int)addrSize, clientVersion, customerId);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006058 File Offset: 0x00004258
		public void SaveFile(FileStructure fs)
		{
			FileType fileType = FileStorageManager.GetFileType(fs.FileType.Title, this.OpContext);
			fs.FileType = fileType;
			this.FileStorageDAO.SaveFile(fs);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006094 File Offset: 0x00004294
		public FileVersionResp GetFileVersion(string fileType, int addrSize)
		{
			FileType fileType2 = FileStorageManager.GetFileType(fileType, this.OpContext);
			return this.FileStorageDAO.GetFileVersion(fileType2, addrSize);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000060C0 File Offset: 0x000042C0
		internal static FileType GetFileType(string fileType, OperationContext opContext)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[fileType];
			bool flag = obj != null;
			FileType fileType2;
			if (flag)
			{
				fileType2 = (obj as FileType);
			}
			else
			{
				fileType2 = UpdateFileTypeFactory.GetFileType(fileType);
				bool flag2 = fileType2 == null;
				if (flag2)
				{
					FileTypeDAO fileTypeDAO = new FileTypeDAO(opContext);
					fileType2 = fileTypeDAO.GetFileType(fileType);
				}
				cacheStorageManager[fileType] = fileType2;
			}
			return fileType2;
		}
	}
}
