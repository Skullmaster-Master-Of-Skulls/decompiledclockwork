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
	// Token: 0x0200009F RID: 159
	public class LargeFileStreamingReusableClientProxy : WCFReusableClientProxy<ILargeFileStreaming>, ILargeFileStreaming, IService
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x00011406 File Offset: 0x0000F606
		public LargeFileStreamingReusableClientProxy(string endpoint) : base(endpoint)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00011419 File Offset: 0x0000F619
		public LargeFileStreamingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00011430 File Offset: 0x0000F630
		public StreamingFileDTO DownloadLargeFile(DownloadLargeFileMessageReq request)
		{
			return this.WrapServiceMethod<StreamingFileDTO>(() => this.Proxy.DownloadLargeFile(request));
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00011468 File Offset: 0x0000F668
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeFileAsync(DownloadLargeFileMessageReq request)
		{
			LargeFileStreamingReusableClientProxy.<DownloadLargeFileAsync>d__3 <DownloadLargeFileAsync>d__ = new LargeFileStreamingReusableClientProxy.<DownloadLargeFileAsync>d__3();
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.request = request;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<LargeFileStreamingReusableClientProxy.<DownloadLargeFileAsync>d__3>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000114B4 File Offset: 0x0000F6B4
		public UploadLargeFileResp UploadLargeFile(StreamingFileDTO file)
		{
			return this.WrapServiceMethod<UploadLargeFileResp>(() => this.Proxy.UploadLargeFile(file));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000114EC File Offset: 0x0000F6EC
		[DebuggerStepThrough]
		public Task<UploadLargeFileResp> UploadLargeFileAsync(StreamingFileDTO file)
		{
			LargeFileStreamingReusableClientProxy.<UploadLargeFileAsync>d__5 <UploadLargeFileAsync>d__ = new LargeFileStreamingReusableClientProxy.<UploadLargeFileAsync>d__5();
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadLargeFileResp>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.file = file;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<LargeFileStreamingReusableClientProxy.<UploadLargeFileAsync>d__5>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00011538 File Offset: 0x0000F738
		public StreamingFileDTO DownloadLargeTempFile(DownloadLargeFileMessageReq request)
		{
			return this.WrapServiceMethod<StreamingFileDTO>(() => this.Proxy.DownloadLargeTempFile(request));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00011570 File Offset: 0x0000F770
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeTempFileAsync(DownloadLargeFileMessageReq request)
		{
			LargeFileStreamingReusableClientProxy.<DownloadLargeTempFileAsync>d__7 <DownloadLargeTempFileAsync>d__ = new LargeFileStreamingReusableClientProxy.<DownloadLargeTempFileAsync>d__7();
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.request = request;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<LargeFileStreamingReusableClientProxy.<DownloadLargeTempFileAsync>d__7>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000115BC File Offset: 0x0000F7BC
		public UploadLargeFileResp UploadLargeTempFile(StreamingFileDTO file)
		{
			return this.WrapServiceMethod<UploadLargeFileResp>(() => this.Proxy.UploadLargeTempFile(file));
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000115F4 File Offset: 0x0000F7F4
		[DebuggerStepThrough]
		public Task<UploadLargeFileResp> UploadLargeTempFileAsync(StreamingFileDTO file)
		{
			LargeFileStreamingReusableClientProxy.<UploadLargeTempFileAsync>d__9 <UploadLargeTempFileAsync>d__ = new LargeFileStreamingReusableClientProxy.<UploadLargeTempFileAsync>d__9();
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadLargeFileResp>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.file = file;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<LargeFileStreamingReusableClientProxy.<UploadLargeTempFileAsync>d__9>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}
	}
}
