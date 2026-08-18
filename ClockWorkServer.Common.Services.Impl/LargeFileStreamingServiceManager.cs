using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000048 RID: 72
	public class LargeFileStreamingServiceManager : ILargeFileStreaming, IService
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public StreamingFileDTO DownloadLargeFile(DownloadLargeFileMessageReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return filesStorageManager.DownloadLargeFile(request.FileIdentifier.ToDomaintObject()).ToDTO();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000D810 File Offset: 0x0000BA10
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeFileAsync(DownloadLargeFileMessageReq request)
		{
			LargeFileStreamingServiceManager.<DownloadLargeFileAsync>d__1 <DownloadLargeFileAsync>d__ = new LargeFileStreamingServiceManager.<DownloadLargeFileAsync>d__1();
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.request = request;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<LargeFileStreamingServiceManager.<DownloadLargeFileAsync>d__1>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public UploadLargeFileResp UploadLargeFile(StreamingFileDTO request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return new UploadLargeFileResp
			{
				FileInfo = filesStorageManager.UploadLargeFile(request.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		[DebuggerStepThrough]
		public Task<UploadLargeFileResp> UploadLargeFileAsync(StreamingFileDTO request)
		{
			LargeFileStreamingServiceManager.<UploadLargeFileAsync>d__3 <UploadLargeFileAsync>d__ = new LargeFileStreamingServiceManager.<UploadLargeFileAsync>d__3();
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadLargeFileResp>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.request = request;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<LargeFileStreamingServiceManager.<UploadLargeFileAsync>d__3>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public StreamingFileDTO DownloadLargeTempFile(DownloadLargeFileMessageReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return filesStorageManager.DownloadLargeTempFile(request.FileIdentifier.ToDomaintObject()).ToDTO();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D928 File Offset: 0x0000BB28
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeTempFileAsync(DownloadLargeFileMessageReq request)
		{
			LargeFileStreamingServiceManager.<DownloadLargeTempFileAsync>d__5 <DownloadLargeTempFileAsync>d__ = new LargeFileStreamingServiceManager.<DownloadLargeTempFileAsync>d__5();
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.request = request;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<LargeFileStreamingServiceManager.<DownloadLargeTempFileAsync>d__5>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D974 File Offset: 0x0000BB74
		public UploadLargeFileResp UploadLargeTempFile(StreamingFileDTO request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return new UploadLargeFileResp
			{
				FileInfo = filesStorageManager.UploadLargeTempFile(request.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		[DebuggerStepThrough]
		public Task<UploadLargeFileResp> UploadLargeTempFileAsync(StreamingFileDTO request)
		{
			LargeFileStreamingServiceManager.<UploadLargeTempFileAsync>d__7 <UploadLargeTempFileAsync>d__ = new LargeFileStreamingServiceManager.<UploadLargeTempFileAsync>d__7();
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadLargeFileResp>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.request = request;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<LargeFileStreamingServiceManager.<UploadLargeTempFileAsync>d__7>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}
	}
}
