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
	// Token: 0x020000A0 RID: 160
	internal class LargeFileStreamingClientBaseProxy : ClientBase<ILargeFileStreaming>, ILargeFileStreaming, IService
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0001163F File Offset: 0x0000F83F
		public LargeFileStreamingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001164A File Offset: 0x0000F84A
		public LargeFileStreamingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00011658 File Offset: 0x0000F858
		public StreamingFileDTO DownloadLargeFile(DownloadLargeFileMessageReq request)
		{
			return base.Channel.DownloadLargeFile(request);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00011678 File Offset: 0x0000F878
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeFileAsync(DownloadLargeFileMessageReq request)
		{
			LargeFileStreamingClientBaseProxy.<DownloadLargeFileAsync>d__3 <DownloadLargeFileAsync>d__ = new LargeFileStreamingClientBaseProxy.<DownloadLargeFileAsync>d__3();
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.request = request;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<LargeFileStreamingClientBaseProxy.<DownloadLargeFileAsync>d__3>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public UploadLargeFileResp UploadLargeFile(StreamingFileDTO file)
		{
			return base.Channel.UploadLargeFile(file);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000116E4 File Offset: 0x0000F8E4
		[DebuggerStepThrough]
		public Task<UploadLargeFileResp> UploadLargeFileAsync(StreamingFileDTO file)
		{
			LargeFileStreamingClientBaseProxy.<UploadLargeFileAsync>d__5 <UploadLargeFileAsync>d__ = new LargeFileStreamingClientBaseProxy.<UploadLargeFileAsync>d__5();
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadLargeFileResp>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.file = file;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<LargeFileStreamingClientBaseProxy.<UploadLargeFileAsync>d__5>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00011730 File Offset: 0x0000F930
		public StreamingFileDTO DownloadLargeTempFile(DownloadLargeFileMessageReq request)
		{
			return base.Channel.DownloadLargeTempFile(request);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00011750 File Offset: 0x0000F950
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeTempFileAsync(DownloadLargeFileMessageReq request)
		{
			LargeFileStreamingClientBaseProxy.<DownloadLargeTempFileAsync>d__7 <DownloadLargeTempFileAsync>d__ = new LargeFileStreamingClientBaseProxy.<DownloadLargeTempFileAsync>d__7();
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.request = request;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<LargeFileStreamingClientBaseProxy.<DownloadLargeTempFileAsync>d__7>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001179C File Offset: 0x0000F99C
		public UploadLargeFileResp UploadLargeTempFile(StreamingFileDTO file)
		{
			return base.Channel.UploadLargeTempFile(file);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000117BC File Offset: 0x0000F9BC
		[DebuggerStepThrough]
		public Task<UploadLargeFileResp> UploadLargeTempFileAsync(StreamingFileDTO file)
		{
			LargeFileStreamingClientBaseProxy.<UploadLargeTempFileAsync>d__9 <UploadLargeTempFileAsync>d__ = new LargeFileStreamingClientBaseProxy.<UploadLargeTempFileAsync>d__9();
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadLargeFileResp>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.file = file;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<LargeFileStreamingClientBaseProxy.<UploadLargeTempFileAsync>d__9>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}
	}
}
