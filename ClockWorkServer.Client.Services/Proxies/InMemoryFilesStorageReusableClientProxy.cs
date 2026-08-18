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
	// Token: 0x020000A1 RID: 161
	public class InMemoryFilesStorageReusableClientProxy : WCFTokenBasedReusableClientProxy<IInMemoryFilesStorage>, IInMemoryFilesStorage, IService
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00011807 File Offset: 0x0000FA07
		public InMemoryFilesStorageReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00011812 File Offset: 0x0000FA12
		public InMemoryFilesStorageReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00011820 File Offset: 0x0000FA20
		public DownloadFileResp DownloadFile(DownloadFileReq request)
		{
			return this.WrapServiceMethod<DownloadFileResp>(() => this.Proxy.DownloadFile(request));
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00011858 File Offset: 0x0000FA58
		[DebuggerStepThrough]
		public Task<DownloadFileResp> DownloadFileAsync(DownloadFileReq request)
		{
			InMemoryFilesStorageReusableClientProxy.<DownloadFileAsync>d__3 <DownloadFileAsync>d__ = new InMemoryFilesStorageReusableClientProxy.<DownloadFileAsync>d__3();
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadFileResp>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.request = request;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageReusableClientProxy.<DownloadFileAsync>d__3>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public UploadFileResp UploadFile(UploadFileReq request)
		{
			return this.WrapServiceMethod<UploadFileResp>(() => this.Proxy.UploadFile(request));
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000118DC File Offset: 0x0000FADC
		[DebuggerStepThrough]
		public Task<UploadFileResp> UploadFileAsync(UploadFileReq request)
		{
			InMemoryFilesStorageReusableClientProxy.<UploadFileAsync>d__5 <UploadFileAsync>d__ = new InMemoryFilesStorageReusableClientProxy.<UploadFileAsync>d__5();
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadFileResp>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.request = request;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageReusableClientProxy.<UploadFileAsync>d__5>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00011928 File Offset: 0x0000FB28
		public DownloadFileResp DownloadTempFile(DownloadFileReq request)
		{
			return this.WrapServiceMethod<DownloadFileResp>(() => this.Proxy.DownloadTempFile(request));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00011960 File Offset: 0x0000FB60
		[DebuggerStepThrough]
		public Task<DownloadFileResp> DownloadTempFileAsync(DownloadFileReq request)
		{
			InMemoryFilesStorageReusableClientProxy.<DownloadTempFileAsync>d__7 <DownloadTempFileAsync>d__ = new InMemoryFilesStorageReusableClientProxy.<DownloadTempFileAsync>d__7();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadFileResp>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.request = request;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageReusableClientProxy.<DownloadTempFileAsync>d__7>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000119AC File Offset: 0x0000FBAC
		public UploadFileResp UploadTempFile(UploadFileReq request)
		{
			return this.WrapServiceMethod<UploadFileResp>(() => this.Proxy.UploadTempFile(request));
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000119E4 File Offset: 0x0000FBE4
		[DebuggerStepThrough]
		public Task<UploadFileResp> UploadTempFileAsync(UploadFileReq request)
		{
			InMemoryFilesStorageReusableClientProxy.<UploadTempFileAsync>d__9 <UploadTempFileAsync>d__ = new InMemoryFilesStorageReusableClientProxy.<UploadTempFileAsync>d__9();
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadFileResp>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.request = request;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageReusableClientProxy.<UploadTempFileAsync>d__9>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00011A30 File Offset: 0x0000FC30
		public DeleteFileResp DeleteFile(DeleteFileReq request)
		{
			return this.WrapServiceMethod<DeleteFileResp>(() => this.Proxy.DeleteFile(request));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00011A68 File Offset: 0x0000FC68
		[DebuggerStepThrough]
		public Task<DeleteFileResp> DeleteFileAsync(DeleteFileReq request)
		{
			InMemoryFilesStorageReusableClientProxy.<DeleteFileAsync>d__11 <DeleteFileAsync>d__ = new InMemoryFilesStorageReusableClientProxy.<DeleteFileAsync>d__11();
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteFileResp>.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.request = request;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageReusableClientProxy.<DeleteFileAsync>d__11>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public DeleteFileResp DeleteTempFile(DeleteFileReq request)
		{
			return this.WrapServiceMethod<DeleteFileResp>(() => this.Proxy.DeleteTempFile(request));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00011AEC File Offset: 0x0000FCEC
		[DebuggerStepThrough]
		public Task<DeleteFileResp> DeleteTempFileAsync(DeleteFileReq request)
		{
			InMemoryFilesStorageReusableClientProxy.<DeleteTempFileAsync>d__13 <DeleteTempFileAsync>d__ = new InMemoryFilesStorageReusableClientProxy.<DeleteTempFileAsync>d__13();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteFileResp>.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.request = request;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageReusableClientProxy.<DeleteTempFileAsync>d__13>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}
	}
}
