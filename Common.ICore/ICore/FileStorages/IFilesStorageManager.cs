using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.ICore.FileStorages
{
	// Token: 0x0200008E RID: 142
	public interface IFilesStorageManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003F5 RID: 1013
		StreamingFile DownloadLargeFile(FileIdentifier fileId);

		// Token: 0x060003F6 RID: 1014
		Task<StreamingFile> DownloadLargeFileAsync(FileIdentifier fileId);

		// Token: 0x060003F7 RID: 1015
		BasicFileInfo UploadLargeFile(StreamingFile file);

		// Token: 0x060003F8 RID: 1016
		Task<BasicFileInfo> UploadLargeFileAsync(StreamingFile file);

		// Token: 0x060003F9 RID: 1017
		StreamingFile DownloadLargeTempFile(FileIdentifier fileId);

		// Token: 0x060003FA RID: 1018
		Task<StreamingFile> DownloadLargeTempFileAsync(FileIdentifier fileId);

		// Token: 0x060003FB RID: 1019
		BasicFileInfo UploadLargeTempFile(StreamingFile file);

		// Token: 0x060003FC RID: 1020
		Task<BasicFileInfo> UploadLargeTempFileAsync(StreamingFile file);

		// Token: 0x060003FD RID: 1021
		InMemoryFile DownloadFile(FileIdentifier fileId);

		// Token: 0x060003FE RID: 1022
		Task<InMemoryFile> DownloadFileAsync(FileIdentifier fileId);

		// Token: 0x060003FF RID: 1023
		BasicFileInfo UploadFile(InMemoryFile file);

		// Token: 0x06000400 RID: 1024
		Task<BasicFileInfo> UploadFileAsync(InMemoryFile file);

		// Token: 0x06000401 RID: 1025
		InMemoryFile DownloadTempFile(FileIdentifier fileId);

		// Token: 0x06000402 RID: 1026
		Task<InMemoryFile> DownloadTempFileAsync(FileIdentifier fileId);

		// Token: 0x06000403 RID: 1027
		BasicFileInfo UploadTempFile(InMemoryFile file);

		// Token: 0x06000404 RID: 1028
		Task<BasicFileInfo> UploadTempFileAsync(InMemoryFile file);

		// Token: 0x06000405 RID: 1029
		void DeleteFile(FileIdentifier fileId);

		// Token: 0x06000406 RID: 1030
		Task DeleteFileAsync(FileIdentifier fileId);

		// Token: 0x06000407 RID: 1031
		void DeleteTempFile(FileIdentifier fileId);

		// Token: 0x06000408 RID: 1032
		Task DeleteTempFileAsync(FileIdentifier fileId);

		// Token: 0x06000409 RID: 1033
		void DeleteTempFilesOlderThan(DateTimeOffset date);

		// Token: 0x0600040A RID: 1034
		Task DeleteTempFilesOlderThanAsync(DateTimeOffset date);

		// Token: 0x0600040B RID: 1035
		void MoveTempFileToPersistentStorage(FileIdentifier fileId);

		// Token: 0x0600040C RID: 1036
		Task MoveTempFileToPersistentStorageAsync(FileIdentifier fileId);
	}
}
