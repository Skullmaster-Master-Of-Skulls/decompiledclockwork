using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000004 RID: 4
	public class MediaContentFileServiceManager : IMediaContentFile, IService
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002280 File Offset: 0x00000480
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002294 File Offset: 0x00000494
		public CreateMediaContentFileInfoResp CreateMediaContentFileInfo(CreateMediaContentFileInfoReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			return new CreateMediaContentFileInfoResp
			{
				MediaContentFile = mediaContentFileManager.CreateMediaContentFileInfo(request.MediaContentFile.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022D4 File Offset: 0x000004D4
		[DebuggerStepThrough]
		public Task<CreateMediaContentFileInfoResp> CreateMediaContentFileInfoAsync(CreateMediaContentFileInfoReq request)
		{
			MediaContentFileServiceManager.<CreateMediaContentFileInfoAsync>d__2 <CreateMediaContentFileInfoAsync>d__ = new MediaContentFileServiceManager.<CreateMediaContentFileInfoAsync>d__2();
			<CreateMediaContentFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateMediaContentFileInfoResp>.Create();
			<CreateMediaContentFileInfoAsync>d__.<>4__this = this;
			<CreateMediaContentFileInfoAsync>d__.request = request;
			<CreateMediaContentFileInfoAsync>d__.<>1__state = -1;
			<CreateMediaContentFileInfoAsync>d__.<>t__builder.Start<MediaContentFileServiceManager.<CreateMediaContentFileInfoAsync>d__2>(ref <CreateMediaContentFileInfoAsync>d__);
			return <CreateMediaContentFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002320 File Offset: 0x00000520
		public LoadMediaContentFileByContentResp LoadMediaContentFileByContent(LoadMediaContentFileByContentReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			return new LoadMediaContentFileByContentResp
			{
				MediaContentFiles = mediaContentFileManager.LoadMediaContentFileByContent(request.MediaContentID, request.StudentId).ToDTO()
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002364 File Offset: 0x00000564
		[DebuggerStepThrough]
		public Task<LoadAvailableMediaContentFileByStudentIdResp> LoadAvailableMediaContentFileByStudentIdAsync(LoadAvailableMediaContentFileByStudentIdReq request)
		{
			MediaContentFileServiceManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__4 <LoadAvailableMediaContentFileByStudentIdAsync>d__ = new MediaContentFileServiceManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__4();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAvailableMediaContentFileByStudentIdResp>.Create();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.request = request;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Start<MediaContentFileServiceManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__4>(ref <LoadAvailableMediaContentFileByStudentIdAsync>d__);
			return <LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023B0 File Offset: 0x000005B0
		public UpdateMediaContentFileWithoutDataResp UpdateMediaContentFileWithoutData(UpdateMediaContentFileWithoutDataReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			mediaContentFileManager.UpdateMediaContentFileWithoutData(request.MediaContentFile.ToDomainObject());
			return new UpdateMediaContentFileWithoutDataResp();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023E8 File Offset: 0x000005E8
		public LoadMediaContentFileByStudentIdResp LoadMediaContentFileByStudentId(LoadMediaContentFileByStudentIdReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			return new LoadMediaContentFileByStudentIdResp
			{
				MediaContentFiles = mediaContentFileManager.LoadMediaContentFileByStudentId(request.StudentId).ToDTO()
			};
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002424 File Offset: 0x00000624
		[DebuggerStepThrough]
		public Task<DeleteMediaContentFileResp> DeleteMediaContentFileAsync(DeleteMediaContentFileReq request)
		{
			MediaContentFileServiceManager.<DeleteMediaContentFileAsync>d__7 <DeleteMediaContentFileAsync>d__ = new MediaContentFileServiceManager.<DeleteMediaContentFileAsync>d__7();
			<DeleteMediaContentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteMediaContentFileResp>.Create();
			<DeleteMediaContentFileAsync>d__.<>4__this = this;
			<DeleteMediaContentFileAsync>d__.request = request;
			<DeleteMediaContentFileAsync>d__.<>1__state = -1;
			<DeleteMediaContentFileAsync>d__.<>t__builder.Start<MediaContentFileServiceManager.<DeleteMediaContentFileAsync>d__7>(ref <DeleteMediaContentFileAsync>d__);
			return <DeleteMediaContentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002470 File Offset: 0x00000670
		public GetMediaContentFileMatchingResp GetMediaContentFileMatching(GetMediaContentFileMatchingReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			return new GetMediaContentFileMatchingResp
			{
				MediaContentFileList = mediaContentFileManager.GetMediaContentFileMatching(request.SearchText, request.LuCourseid).ToDTO()
			};
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024B4 File Offset: 0x000006B4
		public LoadMediaContentFileByMediaContentPerFormatIdResp LoadMediaContentFileByMediaContentPerFormatId(LoadMediaContentFileByMediaContentPerFormatIdReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			return new LoadMediaContentFileByMediaContentPerFormatIdResp
			{
				MediaContentFileList = mediaContentFileManager.LoadMediaContentFileByMediaContentPerFormatId(request.MediaContentPerFormatId, request.StudentId).ToDTO()
			};
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024F8 File Offset: 0x000006F8
		[DebuggerStepThrough]
		public Task<LoadMediaContentFileByMediaContentPerFormatIdResp> LoadMediaContentFileByMediaContentPerFormatIdAsync(LoadMediaContentFileByMediaContentPerFormatIdReq request)
		{
			MediaContentFileServiceManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__10 <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__ = new MediaContentFileServiceManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__10();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadMediaContentFileByMediaContentPerFormatIdResp>.Create();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>4__this = this;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.request = request;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>1__state = -1;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Start<MediaContentFileServiceManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__10>(ref <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__);
			return <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002544 File Offset: 0x00000744
		public LoadMediaContentFileByMediaContentAndFormatResp LoadMediaContentFileByMediaContentAndFormat(LoadMediaContentFileByMediaContentAndFormatReq request)
		{
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(request.GetOperationContext());
			return new LoadMediaContentFileByMediaContentAndFormatResp
			{
				MediaContentFileList = mediaContentFileManager.LoadMediaContentFileByMediaContentPerFormatId(request.MediaContentId, request.MediaContentFormat, request.StudentId).ToDTO()
			};
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000258C File Offset: 0x0000078C
		[DebuggerStepThrough]
		public Task<LoadAvailableMediaContentFileByStudentAndMediaContentResp> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(LoadAvailableMediaContentFileByStudentAndMediaContentReq request)
		{
			MediaContentFileServiceManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__12 <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__ = new MediaContentFileServiceManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__12();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAvailableMediaContentFileByStudentAndMediaContentResp>.Create();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.request = request;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Start<MediaContentFileServiceManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__12>(ref <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__);
			return <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Task;
		}
	}
}
