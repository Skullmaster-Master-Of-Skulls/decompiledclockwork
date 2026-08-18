using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.Files.FileUpload;
using TechnoPro.Common.Core.TempFiles;
using TechnoPro.Common.ICore.TempFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000095 RID: 149
	public class TempFileServiceManager : ITempFile, IService
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00018B5C File Offset: 0x00016D5C
		public void DeleteOldTempFiles(DeleteOldTempFilesReq Request)
		{
			ITempFileManager tempFileManager = new TempFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			tempFileManager.DeleteOldTempFiles();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018B8C File Offset: 0x00016D8C
		public AddNewTempFileResp AddNewTempFile(AddNewTempFileReq Request)
		{
			ITempFileManager tempFileManager = new TempFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			int newTempFileId = tempFileManager.AddNewTempFile(Request.Context.ToDomainObject(), Request.FileToUpload.ToDomainObject());
			return new AddNewTempFileResp
			{
				NewTempFileId = newTempFileId
			};
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00018BE0 File Offset: 0x00016DE0
		[DebuggerStepThrough]
		public Task<AddNewTempFileResp> AddNewTempFileAsync(AddNewTempFileReq Request)
		{
			TempFileServiceManager.<AddNewTempFileAsync>d__2 <AddNewTempFileAsync>d__ = new TempFileServiceManager.<AddNewTempFileAsync>d__2();
			<AddNewTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AddNewTempFileResp>.Create();
			<AddNewTempFileAsync>d__.<>4__this = this;
			<AddNewTempFileAsync>d__.Request = Request;
			<AddNewTempFileAsync>d__.<>1__state = -1;
			<AddNewTempFileAsync>d__.<>t__builder.Start<TempFileServiceManager.<AddNewTempFileAsync>d__2>(ref <AddNewTempFileAsync>d__);
			return <AddNewTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00018C2C File Offset: 0x00016E2C
		public DownloadTempFileResp DownloadTempFile(DownloadTempFileReq Request)
		{
			ITempFileManager tempFileManager = new TempFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			BinaryFile binaryFile = tempFileManager.DownloadTempFile(Request.Context.ToDomainObject(), Request.TempFileId);
			return new DownloadTempFileResp
			{
				TempFile = ((binaryFile == null) ? null : binaryFile.ToDTO())
			};
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00018C88 File Offset: 0x00016E88
		[DebuggerStepThrough]
		public Task<DownloadTempFileResp> DownloadTempFileAsync(DownloadTempFileReq Request)
		{
			TempFileServiceManager.<DownloadTempFileAsync>d__4 <DownloadTempFileAsync>d__ = new TempFileServiceManager.<DownloadTempFileAsync>d__4();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadTempFileResp>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.Request = Request;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<TempFileServiceManager.<DownloadTempFileAsync>d__4>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00018CD4 File Offset: 0x00016ED4
		public void DeleteTempFiles(DeleteTempFilesReq Request)
		{
			ITempFileManager tempFileManager = new TempFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			tempFileManager.DeleteTempFiles(Request.Context.ToDomainObject());
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018D0C File Offset: 0x00016F0C
		public void DeleteTempFile(DeleteTempFileReq Request)
		{
			ITempFileManager tempFileManager = new TempFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			tempFileManager.DeleteTempFile(Request.Context.ToDomainObject(), Request.TempFileId);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00018D4C File Offset: 0x00016F4C
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(DeleteTempFileReq Request)
		{
			TempFileServiceManager.<DeleteTempFileAsync>d__7 <DeleteTempFileAsync>d__ = new TempFileServiceManager.<DeleteTempFileAsync>d__7();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.Request = Request;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<TempFileServiceManager.<DeleteTempFileAsync>d__7>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00018D98 File Offset: 0x00016F98
		public CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp CopyTempFilesToInstructorExamUploadAndDeleteTempFile(CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq Request)
		{
			ITempFileManager tempFileManager = new TempFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			int[] newExamFileIds = tempFileManager.CopyTempFilesToInstructorExamUploadAndDeleteTempFile(Request.Context.ToDomainObject(), Request.ExamId, (Request.WhoEnteredPersonId < 1) ? Request.WhoAmI : Request.WhoEnteredPersonId, Request.Description);
			return new CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp
			{
				NewExamFileIds = newExamFileIds
			};
		}
	}
}
