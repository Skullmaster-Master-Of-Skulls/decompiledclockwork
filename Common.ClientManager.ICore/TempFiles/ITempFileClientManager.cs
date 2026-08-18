using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.TempFiles
{
	// Token: 0x0200000D RID: 13
	public interface ITempFileClientManager : IWebService
	{
		// Token: 0x0600004D RID: 77
		void DeleteOldTempFiles();

		// Token: 0x0600004E RID: 78
		int AddNewTempFile(TempFileContextDTO context, BinaryFileDTO fileToUpload);

		// Token: 0x0600004F RID: 79
		Task<int> AddNewTempFileAsync(TempFileContextDTO context, BinaryFileDTO fileToUpload);

		// Token: 0x06000050 RID: 80
		BinaryFileDTO DownloadTempFile(TempFileContextDTO context, int tempFileId);

		// Token: 0x06000051 RID: 81
		Task<BinaryFileDTO> DownloadTempFileAsync(TempFileContextDTO context, int tempFileId);

		// Token: 0x06000052 RID: 82
		void DeleteTempFiles(TempFileContextDTO context);

		// Token: 0x06000053 RID: 83
		void DeleteTempFile(TempFileContextDTO context, int tempFileId);

		// Token: 0x06000054 RID: 84
		Task DeleteTempFileAsync(TempFileContextDTO context, int tempFileId);

		// Token: 0x06000055 RID: 85
		int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContextDTO context, int examId, int whoEntered, string description);
	}
}
