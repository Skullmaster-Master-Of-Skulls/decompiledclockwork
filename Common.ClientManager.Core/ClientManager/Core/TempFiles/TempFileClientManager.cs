using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;
using TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.TempFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.TempFiles
{
	// Token: 0x02000010 RID: 16
	public class TempFileClientManager : ITempFileClientManager, IWebService
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003F90 File Offset: 0x00002190
		public void DeleteOldTempFiles()
		{
			DeleteOldTempFilesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteOldTempFilesReq>();
			ClientServiceFactory.GetClientInstance<ITempFile>().DeleteOldTempFiles(request);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003FB8 File Offset: 0x000021B8
		public int AddNewTempFile(TempFileContextDTO context, BinaryFileDTO fileToUpload)
		{
			AddNewTempFileReq addNewTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddNewTempFileReq>();
			addNewTempFileReq.Context = context;
			addNewTempFileReq.FileToUpload = fileToUpload;
			return ClientServiceFactory.GetClientInstance<ITempFile>().AddNewTempFile(addNewTempFileReq).NewTempFileId;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003FF8 File Offset: 0x000021F8
		[DebuggerStepThrough]
		public Task<int> AddNewTempFileAsync(TempFileContextDTO context, BinaryFileDTO fileToUpload)
		{
			TempFileClientManager.<AddNewTempFileAsync>d__2 <AddNewTempFileAsync>d__ = new TempFileClientManager.<AddNewTempFileAsync>d__2();
			<AddNewTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<AddNewTempFileAsync>d__.<>4__this = this;
			<AddNewTempFileAsync>d__.context = context;
			<AddNewTempFileAsync>d__.fileToUpload = fileToUpload;
			<AddNewTempFileAsync>d__.<>1__state = -1;
			<AddNewTempFileAsync>d__.<>t__builder.Start<TempFileClientManager.<AddNewTempFileAsync>d__2>(ref <AddNewTempFileAsync>d__);
			return <AddNewTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000404C File Offset: 0x0000224C
		public BinaryFileDTO DownloadTempFile(TempFileContextDTO context, int tempFileId)
		{
			DownloadTempFileReq downloadTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DownloadTempFileReq>();
			downloadTempFileReq.Context = context;
			downloadTempFileReq.TempFileId = tempFileId;
			return ClientServiceFactory.GetClientInstance<ITempFile>().DownloadTempFile(downloadTempFileReq).TempFile;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000408C File Offset: 0x0000228C
		[DebuggerStepThrough]
		public Task<BinaryFileDTO> DownloadTempFileAsync(TempFileContextDTO context, int tempFileId)
		{
			TempFileClientManager.<DownloadTempFileAsync>d__4 <DownloadTempFileAsync>d__ = new TempFileClientManager.<DownloadTempFileAsync>d__4();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFileDTO>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.context = context;
			<DownloadTempFileAsync>d__.tempFileId = tempFileId;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<TempFileClientManager.<DownloadTempFileAsync>d__4>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000040E0 File Offset: 0x000022E0
		public void DeleteTempFiles(TempFileContextDTO context)
		{
			DeleteTempFilesReq deleteTempFilesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTempFilesReq>();
			deleteTempFilesReq.Context = context;
			ClientServiceFactory.GetClientInstance<ITempFile>().DeleteTempFiles(deleteTempFilesReq);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004110 File Offset: 0x00002310
		public void DeleteTempFile(TempFileContextDTO context, int tempFileId)
		{
			DeleteTempFileReq deleteTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTempFileReq>();
			deleteTempFileReq.Context = context;
			deleteTempFileReq.TempFileId = tempFileId;
			ClientServiceFactory.GetClientInstance<ITempFile>().DeleteTempFile(deleteTempFileReq);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004148 File Offset: 0x00002348
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(TempFileContextDTO context, int tempFileId)
		{
			TempFileClientManager.<DeleteTempFileAsync>d__7 <DeleteTempFileAsync>d__ = new TempFileClientManager.<DeleteTempFileAsync>d__7();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.context = context;
			<DeleteTempFileAsync>d__.tempFileId = tempFileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<TempFileClientManager.<DeleteTempFileAsync>d__7>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000419C File Offset: 0x0000239C
		public int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContextDTO context, int examId, int whoEntered, string description)
		{
			CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq copyTempFilesToInstructorExamUploadAndDeleteTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq>();
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.Context = context;
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.ExamId = examId;
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.WhoEnteredPersonId = whoEntered;
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.Description = description;
			return ClientServiceFactory.GetClientInstance<ITempFile>().CopyTempFilesToInstructorExamUploadAndDeleteTempFile(copyTempFilesToInstructorExamUploadAndDeleteTempFileReq).NewExamFileIds;
		}
	}
}
