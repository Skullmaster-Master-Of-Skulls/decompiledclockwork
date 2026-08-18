using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200000B RID: 11
	internal class MediaContentFileClientBaseProxy : ClientBase<IMediaContentFile>, IMediaContentFile, IService
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000350F File Offset: 0x0000170F
		public MediaContentFileClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000351A File Offset: 0x0000171A
		public MediaContentFileClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003528 File Offset: 0x00001728
		public CreateMediaContentFileInfoResp CreateMediaContentFileInfo(CreateMediaContentFileInfoReq request)
		{
			return base.Channel.CreateMediaContentFileInfo(request);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003548 File Offset: 0x00001748
		[DebuggerStepThrough]
		public Task<CreateMediaContentFileInfoResp> CreateMediaContentFileInfoAsync(CreateMediaContentFileInfoReq request)
		{
			MediaContentFileClientBaseProxy.<CreateMediaContentFileInfoAsync>d__3 <CreateMediaContentFileInfoAsync>d__ = new MediaContentFileClientBaseProxy.<CreateMediaContentFileInfoAsync>d__3();
			<CreateMediaContentFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateMediaContentFileInfoResp>.Create();
			<CreateMediaContentFileInfoAsync>d__.<>4__this = this;
			<CreateMediaContentFileInfoAsync>d__.request = request;
			<CreateMediaContentFileInfoAsync>d__.<>1__state = -1;
			<CreateMediaContentFileInfoAsync>d__.<>t__builder.Start<MediaContentFileClientBaseProxy.<CreateMediaContentFileInfoAsync>d__3>(ref <CreateMediaContentFileInfoAsync>d__);
			return <CreateMediaContentFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003594 File Offset: 0x00001794
		public LoadMediaContentFileByContentResp LoadMediaContentFileByContent(LoadMediaContentFileByContentReq request)
		{
			return base.Channel.LoadMediaContentFileByContent(request);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035B4 File Offset: 0x000017B4
		public LoadMediaContentFileByStudentIdResp LoadMediaContentFileByStudentId(LoadMediaContentFileByStudentIdReq request)
		{
			return base.Channel.LoadMediaContentFileByStudentId(request);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035D4 File Offset: 0x000017D4
		public Task<LoadAvailableMediaContentFileByStudentIdResp> LoadAvailableMediaContentFileByStudentIdAsync(LoadAvailableMediaContentFileByStudentIdReq request)
		{
			return base.Channel.LoadAvailableMediaContentFileByStudentIdAsync(request);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035F4 File Offset: 0x000017F4
		public UpdateMediaContentFileWithoutDataResp UpdateMediaContentFileWithoutData(UpdateMediaContentFileWithoutDataReq request)
		{
			return base.Channel.UpdateMediaContentFileWithoutData(request);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003614 File Offset: 0x00001814
		[DebuggerStepThrough]
		public Task<DeleteMediaContentFileResp> DeleteMediaContentFileAsync(DeleteMediaContentFileReq request)
		{
			MediaContentFileClientBaseProxy.<DeleteMediaContentFileAsync>d__8 <DeleteMediaContentFileAsync>d__ = new MediaContentFileClientBaseProxy.<DeleteMediaContentFileAsync>d__8();
			<DeleteMediaContentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteMediaContentFileResp>.Create();
			<DeleteMediaContentFileAsync>d__.<>4__this = this;
			<DeleteMediaContentFileAsync>d__.request = request;
			<DeleteMediaContentFileAsync>d__.<>1__state = -1;
			<DeleteMediaContentFileAsync>d__.<>t__builder.Start<MediaContentFileClientBaseProxy.<DeleteMediaContentFileAsync>d__8>(ref <DeleteMediaContentFileAsync>d__);
			return <DeleteMediaContentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003660 File Offset: 0x00001860
		public GetMediaContentFileMatchingResp GetMediaContentFileMatching(GetMediaContentFileMatchingReq request)
		{
			return base.Channel.GetMediaContentFileMatching(request);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003680 File Offset: 0x00001880
		public LoadMediaContentFileByMediaContentPerFormatIdResp LoadMediaContentFileByMediaContentPerFormatId(LoadMediaContentFileByMediaContentPerFormatIdReq request)
		{
			return base.Channel.LoadMediaContentFileByMediaContentPerFormatId(request);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000036A0 File Offset: 0x000018A0
		public Task<LoadMediaContentFileByMediaContentPerFormatIdResp> LoadMediaContentFileByMediaContentPerFormatIdAsync(LoadMediaContentFileByMediaContentPerFormatIdReq request)
		{
			return base.Channel.LoadMediaContentFileByMediaContentPerFormatIdAsync(request);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036C0 File Offset: 0x000018C0
		public LoadMediaContentFileByMediaContentAndFormatResp LoadMediaContentFileByMediaContentAndFormat(LoadMediaContentFileByMediaContentAndFormatReq request)
		{
			return base.Channel.LoadMediaContentFileByMediaContentAndFormat(request);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036E0 File Offset: 0x000018E0
		[DebuggerStepThrough]
		public Task<LoadAvailableMediaContentFileByStudentAndMediaContentResp> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(LoadAvailableMediaContentFileByStudentAndMediaContentReq request)
		{
			MediaContentFileClientBaseProxy.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__13 <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__ = new MediaContentFileClientBaseProxy.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__13();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAvailableMediaContentFileByStudentAndMediaContentResp>.Create();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.request = request;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Start<MediaContentFileClientBaseProxy.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__13>(ref <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__);
			return <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Task;
		}
	}
}
