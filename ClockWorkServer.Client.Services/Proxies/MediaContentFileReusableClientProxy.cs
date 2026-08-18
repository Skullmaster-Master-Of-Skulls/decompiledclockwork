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
	// Token: 0x0200000A RID: 10
	public class MediaContentFileReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaContentFile>, IMediaContentFile, IService
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003206 File Offset: 0x00001406
		public MediaContentFileReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003211 File Offset: 0x00001411
		public MediaContentFileReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003220 File Offset: 0x00001420
		public CreateMediaContentFileInfoResp CreateMediaContentFileInfo(CreateMediaContentFileInfoReq request)
		{
			return this.WrapServiceMethod<CreateMediaContentFileInfoResp>(() => this.Proxy.CreateMediaContentFileInfo(request));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003258 File Offset: 0x00001458
		[DebuggerStepThrough]
		public Task<CreateMediaContentFileInfoResp> CreateMediaContentFileInfoAsync(CreateMediaContentFileInfoReq request)
		{
			MediaContentFileReusableClientProxy.<CreateMediaContentFileInfoAsync>d__3 <CreateMediaContentFileInfoAsync>d__ = new MediaContentFileReusableClientProxy.<CreateMediaContentFileInfoAsync>d__3();
			<CreateMediaContentFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateMediaContentFileInfoResp>.Create();
			<CreateMediaContentFileInfoAsync>d__.<>4__this = this;
			<CreateMediaContentFileInfoAsync>d__.request = request;
			<CreateMediaContentFileInfoAsync>d__.<>1__state = -1;
			<CreateMediaContentFileInfoAsync>d__.<>t__builder.Start<MediaContentFileReusableClientProxy.<CreateMediaContentFileInfoAsync>d__3>(ref <CreateMediaContentFileInfoAsync>d__);
			return <CreateMediaContentFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032A4 File Offset: 0x000014A4
		public LoadMediaContentFileByContentResp LoadMediaContentFileByContent(LoadMediaContentFileByContentReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentFileByContentResp>(() => this.Proxy.LoadMediaContentFileByContent(request));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032DC File Offset: 0x000014DC
		public LoadMediaContentFileByStudentIdResp LoadMediaContentFileByStudentId(LoadMediaContentFileByStudentIdReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentFileByStudentIdResp>(() => this.Proxy.LoadMediaContentFileByStudentId(request));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003314 File Offset: 0x00001514
		public Task<LoadAvailableMediaContentFileByStudentIdResp> LoadAvailableMediaContentFileByStudentIdAsync(LoadAvailableMediaContentFileByStudentIdReq request)
		{
			return this.WrapServiceMethod<Task<LoadAvailableMediaContentFileByStudentIdResp>>(() => this.Proxy.LoadAvailableMediaContentFileByStudentIdAsync(request));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000334C File Offset: 0x0000154C
		public UpdateMediaContentFileWithoutDataResp UpdateMediaContentFileWithoutData(UpdateMediaContentFileWithoutDataReq request)
		{
			return this.WrapServiceMethod<UpdateMediaContentFileWithoutDataResp>(() => this.Proxy.UpdateMediaContentFileWithoutData(request));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003384 File Offset: 0x00001584
		[DebuggerStepThrough]
		public Task<DeleteMediaContentFileResp> DeleteMediaContentFileAsync(DeleteMediaContentFileReq request)
		{
			MediaContentFileReusableClientProxy.<DeleteMediaContentFileAsync>d__8 <DeleteMediaContentFileAsync>d__ = new MediaContentFileReusableClientProxy.<DeleteMediaContentFileAsync>d__8();
			<DeleteMediaContentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteMediaContentFileResp>.Create();
			<DeleteMediaContentFileAsync>d__.<>4__this = this;
			<DeleteMediaContentFileAsync>d__.request = request;
			<DeleteMediaContentFileAsync>d__.<>1__state = -1;
			<DeleteMediaContentFileAsync>d__.<>t__builder.Start<MediaContentFileReusableClientProxy.<DeleteMediaContentFileAsync>d__8>(ref <DeleteMediaContentFileAsync>d__);
			return <DeleteMediaContentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000033D0 File Offset: 0x000015D0
		public GetMediaContentFileMatchingResp GetMediaContentFileMatching(GetMediaContentFileMatchingReq request)
		{
			return this.WrapServiceMethod<GetMediaContentFileMatchingResp>(() => this.Proxy.GetMediaContentFileMatching(request));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003408 File Offset: 0x00001608
		public LoadMediaContentFileByMediaContentPerFormatIdResp LoadMediaContentFileByMediaContentPerFormatId(LoadMediaContentFileByMediaContentPerFormatIdReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentFileByMediaContentPerFormatIdResp>(() => this.Proxy.LoadMediaContentFileByMediaContentPerFormatId(request));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003440 File Offset: 0x00001640
		[DebuggerStepThrough]
		public Task<LoadMediaContentFileByMediaContentPerFormatIdResp> LoadMediaContentFileByMediaContentPerFormatIdAsync(LoadMediaContentFileByMediaContentPerFormatIdReq request)
		{
			MediaContentFileReusableClientProxy.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__11 <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__ = new MediaContentFileReusableClientProxy.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__11();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadMediaContentFileByMediaContentPerFormatIdResp>.Create();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>4__this = this;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.request = request;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>1__state = -1;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Start<MediaContentFileReusableClientProxy.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__11>(ref <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__);
			return <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000348C File Offset: 0x0000168C
		public LoadMediaContentFileByMediaContentAndFormatResp LoadMediaContentFileByMediaContentAndFormat(LoadMediaContentFileByMediaContentAndFormatReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentFileByMediaContentAndFormatResp>(() => this.Proxy.LoadMediaContentFileByMediaContentAndFormat(request));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000034C4 File Offset: 0x000016C4
		[DebuggerStepThrough]
		public Task<LoadAvailableMediaContentFileByStudentAndMediaContentResp> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(LoadAvailableMediaContentFileByStudentAndMediaContentReq request)
		{
			MediaContentFileReusableClientProxy.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__13 <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__ = new MediaContentFileReusableClientProxy.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__13();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAvailableMediaContentFileByStudentAndMediaContentResp>.Create();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.request = request;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Start<MediaContentFileReusableClientProxy.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__13>(ref <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__);
			return <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Task;
		}
	}
}
