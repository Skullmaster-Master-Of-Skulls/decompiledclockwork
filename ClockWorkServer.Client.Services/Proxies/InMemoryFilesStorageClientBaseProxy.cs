using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A2 RID: 162
	internal class InMemoryFilesStorageClientBaseProxy : ClientBase<IInMemoryFilesStorage>, IInMemoryFilesStorage, IService
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00011B37 File Offset: 0x0000FD37
		public InMemoryFilesStorageClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00011B42 File Offset: 0x0000FD42
		public InMemoryFilesStorageClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00011B50 File Offset: 0x0000FD50
		public DownloadFileResp DownloadFile(DownloadFileReq request)
		{
			return base.Channel.DownloadFile(request);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00011B70 File Offset: 0x0000FD70
		[DebuggerStepThrough]
		public Task<DownloadFileResp> DownloadFileAsync(DownloadFileReq request)
		{
			InMemoryFilesStorageClientBaseProxy.<DownloadFileAsync>d__3 <DownloadFileAsync>d__ = new InMemoryFilesStorageClientBaseProxy.<DownloadFileAsync>d__3();
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadFileResp>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.request = request;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageClientBaseProxy.<DownloadFileAsync>d__3>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public UploadFileResp UploadFile(UploadFileReq request)
		{
			return base.Channel.UploadFile(request);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00011BDC File Offset: 0x0000FDDC
		[DebuggerStepThrough]
		public Task<UploadFileResp> UploadFileAsync(UploadFileReq request)
		{
			InMemoryFilesStorageClientBaseProxy.<UploadFileAsync>d__5 <UploadFileAsync>d__ = new InMemoryFilesStorageClientBaseProxy.<UploadFileAsync>d__5();
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadFileResp>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.request = request;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageClientBaseProxy.<UploadFileAsync>d__5>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00011C28 File Offset: 0x0000FE28
		public DownloadFileResp DownloadTempFile(DownloadFileReq request)
		{
			return base.Channel.DownloadTempFile(request);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00011C48 File Offset: 0x0000FE48
		[DebuggerStepThrough]
		public Task<DownloadFileResp> DownloadTempFileAsync(DownloadFileReq request)
		{
			InMemoryFilesStorageClientBaseProxy.<DownloadTempFileAsync>d__7 <DownloadTempFileAsync>d__ = new InMemoryFilesStorageClientBaseProxy.<DownloadTempFileAsync>d__7();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadFileResp>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.request = request;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageClientBaseProxy.<DownloadTempFileAsync>d__7>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00011C94 File Offset: 0x0000FE94
		public UploadFileResp UploadTempFile(UploadFileReq request)
		{
			return base.Channel.UploadTempFile(request);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		[DebuggerStepThrough]
		public Task<UploadFileResp> UploadTempFileAsync(UploadFileReq request)
		{
			InMemoryFilesStorageClientBaseProxy.<UploadTempFileAsync>d__9 <UploadTempFileAsync>d__ = new InMemoryFilesStorageClientBaseProxy.<UploadTempFileAsync>d__9();
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadFileResp>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.request = request;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageClientBaseProxy.<UploadTempFileAsync>d__9>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00011D00 File Offset: 0x0000FF00
		public DeleteFileResp DeleteFile(DeleteFileReq request)
		{
			return base.Channel.DeleteFile(request);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00011D20 File Offset: 0x0000FF20
		[DebuggerStepThrough]
		public Task<DeleteFileResp> DeleteFileAsync(DeleteFileReq request)
		{
			InMemoryFilesStorageClientBaseProxy.<DeleteFileAsync>d__11 <DeleteFileAsync>d__ = new InMemoryFilesStorageClientBaseProxy.<DeleteFileAsync>d__11();
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteFileResp>.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.request = request;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageClientBaseProxy.<DeleteFileAsync>d__11>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00011D6C File Offset: 0x0000FF6C
		public DeleteFileResp DeleteTempFile(DeleteFileReq request)
		{
			return base.Channel.DeleteFile(request);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00011D8C File Offset: 0x0000FF8C
		[DebuggerStepThrough]
		public Task<DeleteFileResp> DeleteTempFileAsync(DeleteFileReq request)
		{
			InMemoryFilesStorageClientBaseProxy.<DeleteTempFileAsync>d__13 <DeleteTempFileAsync>d__ = new InMemoryFilesStorageClientBaseProxy.<DeleteTempFileAsync>d__13();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteFileResp>.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.request = request;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageClientBaseProxy.<DeleteTempFileAsync>d__13>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}
	}
}
