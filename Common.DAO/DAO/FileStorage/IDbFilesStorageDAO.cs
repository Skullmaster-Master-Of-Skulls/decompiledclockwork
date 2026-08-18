using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.DAO.FileStorage
{
	// Token: 0x02000074 RID: 116
	public interface IDbFilesStorageDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002CD RID: 717
		StreamingFile DownloadLargeFile(FileIdentifier fileId);

		// Token: 0x060002CE RID: 718
		Task<StreamingFile> DownloadLargeFileAsync(FileIdentifier fileId);

		// Token: 0x060002CF RID: 719
		BasicFileInfo UploadLargeFile(StreamingFile file);

		// Token: 0x060002D0 RID: 720
		Task<BasicFileInfo> UploadLargeFileAsync(StreamingFile file);

		// Token: 0x060002D1 RID: 721
		StreamingFile DownloadLargeTempFile(FileIdentifier fileId);

		// Token: 0x060002D2 RID: 722
		Task<StreamingFile> DownloadLargeTempFileAsync(FileIdentifier fileId);

		// Token: 0x060002D3 RID: 723
		BasicFileInfo UploadLargeTempFile(StreamingFile file);

		// Token: 0x060002D4 RID: 724
		Task<BasicFileInfo> UploadLargeTempFileAsync(StreamingFile file);

		// Token: 0x060002D5 RID: 725
		InMemoryFile DownloadFile(FileIdentifier fileId);

		// Token: 0x060002D6 RID: 726
		Task<InMemoryFile> DownloadFileAsync(FileIdentifier fileId);

		// Token: 0x060002D7 RID: 727
		BasicFileInfo UploadFile(InMemoryFile file);

		// Token: 0x060002D8 RID: 728
		Task<BasicFileInfo> UploadFileAsync(InMemoryFile file);

		// Token: 0x060002D9 RID: 729
		InMemoryFile DownloadTempFile(FileIdentifier fileId);

		// Token: 0x060002DA RID: 730
		Task<InMemoryFile> DownloadTempFileAsync(FileIdentifier fileId);

		// Token: 0x060002DB RID: 731
		BasicFileInfo UploadTempFile(InMemoryFile file);

		// Token: 0x060002DC RID: 732
		Task<BasicFileInfo> UploadTempFileAsync(InMemoryFile file);

		// Token: 0x060002DD RID: 733
		void DeleteFile(FileIdentifier fileId);

		// Token: 0x060002DE RID: 734
		Task DeleteFileAsync(FileIdentifier fileId);

		// Token: 0x060002DF RID: 735
		void DeleteTempFile(FileIdentifier fileId);

		// Token: 0x060002E0 RID: 736
		Task DeleteTempFileAsync(FileIdentifier fileId);

		// Token: 0x060002E1 RID: 737
		void DeleteTempFilesOlderThan(DateTimeOffset date);

		// Token: 0x060002E2 RID: 738
		Task DeleteTempFilesOlderThanAsync(DateTimeOffset date);

		// Token: 0x060002E3 RID: 739
		void MoveTempFileToPersistentStorage(FileIdentifier fileId);

		// Token: 0x060002E4 RID: 740
		Task MoveTempFileToPersistentStorageAsync(FileIdentifier fileId);
	}
}
