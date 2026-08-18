using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.DAO.FileStorage
{
	// Token: 0x02000073 RID: 115
	public interface IFilesStorageDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002C1 RID: 705
		BasicFileInfo GetFileInfo(FileIdentifier fileId);

		// Token: 0x060002C2 RID: 706
		Task<BasicFileInfo> GetFileInfoAsync(FileIdentifier fileId);

		// Token: 0x060002C3 RID: 707
		FileIdentifier AddFileInfo(BasicFileInfo fileInfo);

		// Token: 0x060002C4 RID: 708
		Task<FileIdentifier> AddFileInfoAsync(BasicFileInfo fileInfo);

		// Token: 0x060002C5 RID: 709
		BasicFileInfo GetTempFileInfo(FileIdentifier fileId);

		// Token: 0x060002C6 RID: 710
		Task<BasicFileInfo> GetTempFileInfoAsync(FileIdentifier fileId);

		// Token: 0x060002C7 RID: 711
		FileIdentifier AddTempFileInfo(BasicFileInfo fileInfo);

		// Token: 0x060002C8 RID: 712
		Task<FileIdentifier> AddTempFileInfoAsync(BasicFileInfo fileInfo);

		// Token: 0x060002C9 RID: 713
		void DeleteFileInfo(FileIdentifier fileId);

		// Token: 0x060002CA RID: 714
		void DeleteTempFileInfo(FileIdentifier fileId);

		// Token: 0x060002CB RID: 715
		Task DeleteFileInfoAsync(FileIdentifier fileId);

		// Token: 0x060002CC RID: 716
		Task DeleteTempFileInfoAsync(FileIdentifier fileId);
	}
}
