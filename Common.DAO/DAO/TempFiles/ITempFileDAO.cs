using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Files.FileUpload;

namespace TechnoPro.Common.DAO.TempFiles
{
	// Token: 0x02000023 RID: 35
	public interface ITempFileDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000080 RID: 128
		void DeleteOldTempFiles(DateTime minDateToKeep);

		// Token: 0x06000081 RID: 129
		int AddNewTempFile(TempFileContext context, BinaryFile fileToUpload);

		// Token: 0x06000082 RID: 130
		Task<int> AddNewTempFileAsync(TempFileContext context, BinaryFile fileToUpload);

		// Token: 0x06000083 RID: 131
		BinaryFile GetTempFile(TempFileContext context, int tempFileId);

		// Token: 0x06000084 RID: 132
		Task<BinaryFile> GetTempFileAsync(TempFileContext context, int tempFileId);

		// Token: 0x06000085 RID: 133
		void DeleteTempFiles(TempFileContext context);

		// Token: 0x06000086 RID: 134
		void DeleteTempFile(TempFileContext context, int tempFileId);

		// Token: 0x06000087 RID: 135
		Task DeleteTempFileAsync(TempFileContext context, int tempFileId);

		// Token: 0x06000088 RID: 136
		int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContext context, int examId, int whoEntered, string description);
	}
}
