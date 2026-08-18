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
	// Token: 0x0200014F RID: 335
	public class TempFileReusableClientProxy : WCFTokenBasedReusableClientProxy<ITempFile>, ITempFile, IService
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x0001FD3A File Offset: 0x0001DF3A
		public TempFileReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0001FD45 File Offset: 0x0001DF45
		public TempFileReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0001FD54 File Offset: 0x0001DF54
		public void DeleteOldTempFiles(DeleteOldTempFilesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteOldTempFiles(Request);
			});
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		public AddNewTempFileResp AddNewTempFile(AddNewTempFileReq Request)
		{
			return this.WrapServiceMethod<AddNewTempFileResp>(() => this.Proxy.AddNewTempFile(Request));
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
		[DebuggerStepThrough]
		public Task<AddNewTempFileResp> AddNewTempFileAsync(AddNewTempFileReq Request)
		{
			TempFileReusableClientProxy.<AddNewTempFileAsync>d__4 <AddNewTempFileAsync>d__ = new TempFileReusableClientProxy.<AddNewTempFileAsync>d__4();
			<AddNewTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AddNewTempFileResp>.Create();
			<AddNewTempFileAsync>d__.<>4__this = this;
			<AddNewTempFileAsync>d__.Request = Request;
			<AddNewTempFileAsync>d__.<>1__state = -1;
			<AddNewTempFileAsync>d__.<>t__builder.Start<TempFileReusableClientProxy.<AddNewTempFileAsync>d__4>(ref <AddNewTempFileAsync>d__);
			return <AddNewTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0001FE10 File Offset: 0x0001E010
		public DownloadTempFileResp DownloadTempFile(DownloadTempFileReq Request)
		{
			return this.WrapServiceMethod<DownloadTempFileResp>(() => this.Proxy.DownloadTempFile(Request));
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0001FE48 File Offset: 0x0001E048
		[DebuggerStepThrough]
		public Task<DownloadTempFileResp> DownloadTempFileAsync(DownloadTempFileReq Request)
		{
			TempFileReusableClientProxy.<DownloadTempFileAsync>d__6 <DownloadTempFileAsync>d__ = new TempFileReusableClientProxy.<DownloadTempFileAsync>d__6();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadTempFileResp>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.Request = Request;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<TempFileReusableClientProxy.<DownloadTempFileAsync>d__6>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0001FE94 File Offset: 0x0001E094
		public void DeleteTempFiles(DeleteTempFilesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTempFiles(Request);
			});
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0001FECC File Offset: 0x0001E0CC
		public void DeleteTempFile(DeleteTempFileReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTempFile(Request);
			});
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001FF04 File Offset: 0x0001E104
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(DeleteTempFileReq Request)
		{
			TempFileReusableClientProxy.<DeleteTempFileAsync>d__9 <DeleteTempFileAsync>d__ = new TempFileReusableClientProxy.<DeleteTempFileAsync>d__9();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.Request = Request;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<TempFileReusableClientProxy.<DeleteTempFileAsync>d__9>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0001FF50 File Offset: 0x0001E150
		public CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp CopyTempFilesToInstructorExamUploadAndDeleteTempFile(CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq Request)
		{
			return this.WrapServiceMethod<CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp>(() => this.Proxy.CopyTempFilesToInstructorExamUploadAndDeleteTempFile(Request));
		}
	}
}
