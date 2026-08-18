using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.TempFiles;
using TechnoPro.Common.DAO.TempFiles;
using TechnoPro.Common.ICore.TempFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Files.FileUpload;

namespace TechnoPro.Common.Core.TempFiles
{
	// Token: 0x02000035 RID: 53
	public class TempFileManager : ITempFileManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000BB51 File Offset: 0x00009D51
		public TempFileManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this._tempFileDao = new TempFileDAO(opContext);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000BB6F File Offset: 0x00009D6F
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000BB77 File Offset: 0x00009D77
		public OperationContext OpContext { get; set; }

		// Token: 0x06000218 RID: 536 RVA: 0x0000BB80 File Offset: 0x00009D80
		public void DeleteOldTempFiles()
		{
			DateTime minDateToKeep = DateTime.Now.AddHours(-24.0);
			this._tempFileDao.DeleteOldTempFiles(minDateToKeep);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public int AddNewTempFile(TempFileContext context, BinaryFile fileToUpload)
		{
			return this._tempFileDao.AddNewTempFile(context, fileToUpload);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		[DebuggerStepThrough]
		public Task<int> AddNewTempFileAsync(TempFileContext context, BinaryFile fileToUpload)
		{
			TempFileManager.<AddNewTempFileAsync>d__8 <AddNewTempFileAsync>d__ = new TempFileManager.<AddNewTempFileAsync>d__8();
			<AddNewTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<AddNewTempFileAsync>d__.<>4__this = this;
			<AddNewTempFileAsync>d__.context = context;
			<AddNewTempFileAsync>d__.fileToUpload = fileToUpload;
			<AddNewTempFileAsync>d__.<>1__state = -1;
			<AddNewTempFileAsync>d__.<>t__builder.Start<TempFileManager.<AddNewTempFileAsync>d__8>(ref <AddNewTempFileAsync>d__);
			return <AddNewTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000BC28 File Offset: 0x00009E28
		public BinaryFile DownloadTempFile(TempFileContext context, int tempFileId)
		{
			return this._tempFileDao.GetTempFile(context, tempFileId);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000BC48 File Offset: 0x00009E48
		[DebuggerStepThrough]
		public Task<BinaryFile> DownloadTempFileAsync(TempFileContext context, int tempFileId)
		{
			TempFileManager.<DownloadTempFileAsync>d__10 <DownloadTempFileAsync>d__ = new TempFileManager.<DownloadTempFileAsync>d__10();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.context = context;
			<DownloadTempFileAsync>d__.tempFileId = tempFileId;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<TempFileManager.<DownloadTempFileAsync>d__10>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000BC9A File Offset: 0x00009E9A
		public void DeleteTempFiles(TempFileContext context)
		{
			this._tempFileDao.DeleteTempFiles(context);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000BCAA File Offset: 0x00009EAA
		public void DeleteTempFile(TempFileContext context, int tempFileId)
		{
			this._tempFileDao.DeleteTempFile(context, tempFileId);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000BCBC File Offset: 0x00009EBC
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(TempFileContext context, int tempFileId)
		{
			TempFileManager.<DeleteTempFileAsync>d__13 <DeleteTempFileAsync>d__ = new TempFileManager.<DeleteTempFileAsync>d__13();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.context = context;
			<DeleteTempFileAsync>d__.tempFileId = tempFileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<TempFileManager.<DeleteTempFileAsync>d__13>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BD10 File Offset: 0x00009F10
		public int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContext context, int examId, int whoEntered, string description)
		{
			return this._tempFileDao.CopyTempFilesToInstructorExamUploadAndDeleteTempFile(context, examId, whoEntered, description);
		}

		// Token: 0x0400006D RID: 109
		private readonly ITempFileDAO _tempFileDao;
	}
}
