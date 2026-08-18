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
	// Token: 0x02000049 RID: 73
	public class InMemoryFilesStorageServiceManager : IInMemoryFilesStorage, IService
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public DownloadFileResp DownloadFile(DownloadFileReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return new DownloadFileResp
			{
				File = filesStorageManager.DownloadFile(request.FileIdentifier.ToDomaintObject()).ToDTO()
			};
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		[DebuggerStepThrough]
		public Task<DownloadFileResp> DownloadFileAsync(DownloadFileReq request)
		{
			InMemoryFilesStorageServiceManager.<DownloadFileAsync>d__1 <DownloadFileAsync>d__ = new InMemoryFilesStorageServiceManager.<DownloadFileAsync>d__1();
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadFileResp>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.request = request;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageServiceManager.<DownloadFileAsync>d__1>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public UploadFileResp UploadFile(UploadFileReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return new UploadFileResp
			{
				FileInfo = filesStorageManager.UploadFile(request.File.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		[DebuggerStepThrough]
		public Task<UploadFileResp> UploadFileAsync(UploadFileReq request)
		{
			InMemoryFilesStorageServiceManager.<UploadFileAsync>d__3 <UploadFileAsync>d__ = new InMemoryFilesStorageServiceManager.<UploadFileAsync>d__3();
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadFileResp>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.request = request;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageServiceManager.<UploadFileAsync>d__3>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public DownloadFileResp DownloadTempFile(DownloadFileReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return new DownloadFileResp
			{
				File = filesStorageManager.DownloadTempFile(request.FileIdentifier.ToDomaintObject()).ToDTO()
			};
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000DB74 File Offset: 0x0000BD74
		[DebuggerStepThrough]
		public Task<DownloadFileResp> DownloadTempFileAsync(DownloadFileReq request)
		{
			InMemoryFilesStorageServiceManager.<DownloadTempFileAsync>d__5 <DownloadTempFileAsync>d__ = new InMemoryFilesStorageServiceManager.<DownloadTempFileAsync>d__5();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadFileResp>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.request = request;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageServiceManager.<DownloadTempFileAsync>d__5>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		public UploadFileResp UploadTempFile(UploadFileReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			return new UploadFileResp
			{
				FileInfo = filesStorageManager.UploadTempFile(request.File.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000DC08 File Offset: 0x0000BE08
		[DebuggerStepThrough]
		public Task<UploadFileResp> UploadTempFileAsync(UploadFileReq request)
		{
			InMemoryFilesStorageServiceManager.<UploadTempFileAsync>d__7 <UploadTempFileAsync>d__ = new InMemoryFilesStorageServiceManager.<UploadTempFileAsync>d__7();
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadFileResp>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.request = request;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageServiceManager.<UploadTempFileAsync>d__7>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public DeleteFileResp DeleteFile(DeleteFileReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			filesStorageManager.DeleteFile(request.FileIdentifier.ToDomaintObject());
			return new DeleteFileResp();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000DC90 File Offset: 0x0000BE90
		[DebuggerStepThrough]
		public Task<DeleteFileResp> DeleteFileAsync(DeleteFileReq request)
		{
			InMemoryFilesStorageServiceManager.<DeleteFileAsync>d__9 <DeleteFileAsync>d__ = new InMemoryFilesStorageServiceManager.<DeleteFileAsync>d__9();
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteFileResp>.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.request = request;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageServiceManager.<DeleteFileAsync>d__9>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		public DeleteFileResp DeleteTempFile(DeleteFileReq request)
		{
			IFilesStorageManager filesStorageManager = ObjectFactory.Resolve<IFilesStorageManager>();
			filesStorageManager.OpContext = request.GetOperationContext();
			filesStorageManager.DeleteTempFile(request.FileIdentifier.ToDomaintObject());
			return new DeleteFileResp();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000DD18 File Offset: 0x0000BF18
		[DebuggerStepThrough]
		public Task<DeleteFileResp> DeleteTempFileAsync(DeleteFileReq request)
		{
			InMemoryFilesStorageServiceManager.<DeleteTempFileAsync>d__11 <DeleteTempFileAsync>d__ = new InMemoryFilesStorageServiceManager.<DeleteTempFileAsync>d__11();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteFileResp>.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.request = request;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<InMemoryFilesStorageServiceManager.<DeleteTempFileAsync>d__11>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}
	}
}
