using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000150 RID: 336
	internal class TempFileClientBaseProxy : ClientBase<ITempFile>, ITempFile, IService
	{
		// Token: 0x06000CD9 RID: 3289 RVA: 0x0001FF88 File Offset: 0x0001E188
		public TempFileClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0001FF93 File Offset: 0x0001E193
		public TempFileClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0001FF9F File Offset: 0x0001E19F
		public void DeleteOldTempFiles(DeleteOldTempFilesReq Request)
		{
			base.Channel.DeleteOldTempFiles(Request);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		public AddNewTempFileResp AddNewTempFile(AddNewTempFileReq Request)
		{
			return base.Channel.AddNewTempFile(Request);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		[DebuggerStepThrough]
		public Task<AddNewTempFileResp> AddNewTempFileAsync(AddNewTempFileReq Request)
		{
			TempFileClientBaseProxy.<AddNewTempFileAsync>d__4 <AddNewTempFileAsync>d__ = new TempFileClientBaseProxy.<AddNewTempFileAsync>d__4();
			<AddNewTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AddNewTempFileResp>.Create();
			<AddNewTempFileAsync>d__.<>4__this = this;
			<AddNewTempFileAsync>d__.Request = Request;
			<AddNewTempFileAsync>d__.<>1__state = -1;
			<AddNewTempFileAsync>d__.<>t__builder.Start<TempFileClientBaseProxy.<AddNewTempFileAsync>d__4>(ref <AddNewTempFileAsync>d__);
			return <AddNewTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002001C File Offset: 0x0001E21C
		public DownloadTempFileResp DownloadTempFile(DownloadTempFileReq Request)
		{
			return base.Channel.DownloadTempFile(Request);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002003C File Offset: 0x0001E23C
		[DebuggerStepThrough]
		public Task<DownloadTempFileResp> DownloadTempFileAsync(DownloadTempFileReq Request)
		{
			TempFileClientBaseProxy.<DownloadTempFileAsync>d__6 <DownloadTempFileAsync>d__ = new TempFileClientBaseProxy.<DownloadTempFileAsync>d__6();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadTempFileResp>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.Request = Request;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<TempFileClientBaseProxy.<DownloadTempFileAsync>d__6>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00020087 File Offset: 0x0001E287
		public void DeleteTempFiles(DeleteTempFilesReq Request)
		{
			base.Channel.DeleteTempFiles(Request);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00020097 File Offset: 0x0001E297
		public void DeleteTempFile(DeleteTempFileReq Request)
		{
			base.Channel.DeleteTempFile(Request);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000200A8 File Offset: 0x0001E2A8
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(DeleteTempFileReq Request)
		{
			TempFileClientBaseProxy.<DeleteTempFileAsync>d__9 <DeleteTempFileAsync>d__ = new TempFileClientBaseProxy.<DeleteTempFileAsync>d__9();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.Request = Request;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<TempFileClientBaseProxy.<DeleteTempFileAsync>d__9>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000200F4 File Offset: 0x0001E2F4
		public CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp CopyTempFilesToInstructorExamUploadAndDeleteTempFile(CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq Request)
		{
			return base.Channel.CopyTempFilesToInstructorExamUploadAndDeleteTempFile(Request);
		}
	}
}
