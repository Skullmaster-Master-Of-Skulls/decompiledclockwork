using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Files.FileUpload;

namespace TechnoPro.Common.ICore.TempFiles
{
	// Token: 0x0200002A RID: 42
	public interface ITempFileManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600012B RID: 299
		void DeleteOldTempFiles();

		// Token: 0x0600012C RID: 300
		int AddNewTempFile(TempFileContext context, BinaryFile fileToUpload);

		// Token: 0x0600012D RID: 301
		Task<int> AddNewTempFileAsync(TempFileContext context, BinaryFile fileToUpload);

		// Token: 0x0600012E RID: 302
		BinaryFile DownloadTempFile(TempFileContext context, int tempFileId);

		// Token: 0x0600012F RID: 303
		Task<BinaryFile> DownloadTempFileAsync(TempFileContext context, int tempFileId);

		// Token: 0x06000130 RID: 304
		void DeleteTempFiles(TempFileContext context);

		// Token: 0x06000131 RID: 305
		void DeleteTempFile(TempFileContext context, int tempFileId);

		// Token: 0x06000132 RID: 306
		Task DeleteTempFileAsync(TempFileContext context, int tempFileId);

		// Token: 0x06000133 RID: 307
		int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContext context, int examId, int whoEntered, string description);
	}
}
