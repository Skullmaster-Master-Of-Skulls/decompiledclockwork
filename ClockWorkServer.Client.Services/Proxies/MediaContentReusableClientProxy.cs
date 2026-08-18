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
	// Token: 0x02000008 RID: 8
	public class MediaContentReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaContent>, IMediaContent, IService
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002916 File Offset: 0x00000B16
		public MediaContentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002921 File Offset: 0x00000B21
		public MediaContentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002930 File Offset: 0x00000B30
		[DebuggerStepThrough]
		public Task<GetMediaContentMatchingResp> GetMediaContentMatchingAsync(GetMediaContentMatchingReq request)
		{
			MediaContentReusableClientProxy.<GetMediaContentMatchingAsync>d__2 <GetMediaContentMatchingAsync>d__ = new MediaContentReusableClientProxy.<GetMediaContentMatchingAsync>d__2();
			<GetMediaContentMatchingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetMediaContentMatchingResp>.Create();
			<GetMediaContentMatchingAsync>d__.<>4__this = this;
			<GetMediaContentMatchingAsync>d__.request = request;
			<GetMediaContentMatchingAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingAsync>d__.<>t__builder.Start<MediaContentReusableClientProxy.<GetMediaContentMatchingAsync>d__2>(ref <GetMediaContentMatchingAsync>d__);
			return <GetMediaContentMatchingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000297C File Offset: 0x00000B7C
		public GetMediaContentMatchingResp GetMediaContentMatching(GetMediaContentMatchingReq request)
		{
			return this.WrapServiceMethod<GetMediaContentMatchingResp>(() => this.Proxy.GetMediaContentMatching(request));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029B4 File Offset: 0x00000BB4
		public LoadMediaContentByIdResp LoadMediaContentById(LoadMediaContentByIdReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentByIdResp>(() => this.Proxy.LoadMediaContentById(request));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029EC File Offset: 0x00000BEC
		public LoadMediaContentByIdentifierResp LoadMediaContentByIdentifier(LoadMediaContentByIdentifierReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentByIdentifierResp>(() => this.Proxy.LoadMediaContentByIdentifier(request));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A24 File Offset: 0x00000C24
		public LoadMediaContentByISBNResp LoadMediaContentByISBN(LoadMediaContentByISBNReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentByISBNResp>(() => this.Proxy.LoadMediaContentByISBN(request));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A5C File Offset: 0x00000C5C
		public LoadMediaContentByCourseResp LoadMediaContentByCourse(LoadMediaContentByCourseReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentByCourseResp>(() => this.Proxy.LoadMediaContentByCourse(request));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A94 File Offset: 0x00000C94
		public LoadMediaContentByPublisherResp LoadMediaContentByPublisher(LoadMediaContentByPublisherReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentByPublisherResp>(() => this.Proxy.LoadMediaContentByPublisher(request));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ACC File Offset: 0x00000CCC
		public LoadMediaContentByCategoryResp LoadMediaContentByCategory(LoadMediaContentByCategoryReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentByCategoryResp>(() => this.Proxy.LoadMediaContentByCategory(request));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B04 File Offset: 0x00000D04
		public CreateMediaContentResp CreateMediaContent(CreateMediaContentReq request)
		{
			return this.WrapServiceMethod<CreateMediaContentResp>(() => this.Proxy.CreateMediaContent(request));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B3C File Offset: 0x00000D3C
		public UpdateMediaContentResp UpdateMediaContent(UpdateMediaContentReq request)
		{
			return this.WrapServiceMethod<UpdateMediaContentResp>(() => this.Proxy.UpdateMediaContent(request));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B74 File Offset: 0x00000D74
		public DeleteMediaContentResp DeleteMediaContent(DeleteMediaContentReq request)
		{
			return this.WrapServiceMethod<DeleteMediaContentResp>(() => this.Proxy.DeleteMediaContent(request));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BAC File Offset: 0x00000DAC
		public GetAllMediaContentWithFormatsResp GetAllMediaContent(GetAllMediaContentWithFormatsReq request)
		{
			return this.WrapServiceMethod<GetAllMediaContentWithFormatsResp>(() => this.Proxy.GetAllMediaContent(request));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public GetMediaContentPerFormatInfoByIdResp GetMediaContentPerFormatInfoById(GetMediaContentPerFormatInfoByIdReq request)
		{
			return this.WrapServiceMethod<GetMediaContentPerFormatInfoByIdResp>(() => this.Proxy.GetMediaContentPerFormatInfoById(request));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C1C File Offset: 0x00000E1C
		public LoadMediaContentPerFormatInfoByMediaContentResp LoadMediaContentPerFormatInfoByMediaContent(LoadMediaContentPerFormatInfoByMediaContentReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentPerFormatInfoByMediaContentResp>(() => this.Proxy.LoadMediaContentPerFormatInfoByMediaContent(request));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C54 File Offset: 0x00000E54
		public GetMediaContentPerFormatStatusListResp GetMediaContentPerFormatStatusList(GetMediaContentPerFormatStatusListReq request)
		{
			return this.WrapServiceMethod<GetMediaContentPerFormatStatusListResp>(() => this.Proxy.GetMediaContentPerFormatStatusList(request));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C8C File Offset: 0x00000E8C
		public GetMediaContentThumbnailResp GetMediaContentThumbnail(GetMediaContentThumbnailReq request)
		{
			return this.WrapServiceMethod<GetMediaContentThumbnailResp>(() => this.Proxy.GetMediaContentThumbnail(request));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CC4 File Offset: 0x00000EC4
		[DebuggerStepThrough]
		public Task<GetMediaContentThumbnailBytesResp> GetMediaContentThumbnailBytesAsync(GetMediaContentThumbnailBytesReq request)
		{
			MediaContentReusableClientProxy.<GetMediaContentThumbnailBytesAsync>d__18 <GetMediaContentThumbnailBytesAsync>d__ = new MediaContentReusableClientProxy.<GetMediaContentThumbnailBytesAsync>d__18();
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetMediaContentThumbnailBytesResp>.Create();
			<GetMediaContentThumbnailBytesAsync>d__.<>4__this = this;
			<GetMediaContentThumbnailBytesAsync>d__.request = request;
			<GetMediaContentThumbnailBytesAsync>d__.<>1__state = -1;
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Start<MediaContentReusableClientProxy.<GetMediaContentThumbnailBytesAsync>d__18>(ref <GetMediaContentThumbnailBytesAsync>d__);
			return <GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D10 File Offset: 0x00000F10
		public GetMediaContentThumbnailBytesResp GetMediaContentThumbnailBytes(GetMediaContentThumbnailBytesReq request)
		{
			return this.WrapServiceMethod<GetMediaContentThumbnailBytesResp>(() => this.Proxy.GetMediaContentThumbnailBytes(request));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D48 File Offset: 0x00000F48
		public SetMediaContentThumbnailResp SetMediaContentThumbnail(SetMediaContentThumbnailReq request)
		{
			return this.WrapServiceMethod<SetMediaContentThumbnailResp>(() => this.Proxy.SetMediaContentThumbnail(request));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D80 File Offset: 0x00000F80
		public GetMediaContentCoverImageResp GetMediaContentCoverImage(GetMediaContentCoverImageReq request)
		{
			return this.WrapServiceMethod<GetMediaContentCoverImageResp>(() => this.Proxy.GetMediaContentCoverImage(request));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public GetMediaContentCoverImageBytesResp GetMediaContentCoverImageBytes(GetMediaContentCoverImageBytesReq request)
		{
			return this.WrapServiceMethod<GetMediaContentCoverImageBytesResp>(() => this.Proxy.GetMediaContentCoverImageBytes(request));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public SetMediaContentCoverResp SetMediaContentCover(SetMediaContentCoverReq request)
		{
			return this.WrapServiceMethod<SetMediaContentCoverResp>(() => this.Proxy.SetMediaContentCover(request));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E28 File Offset: 0x00001028
		public GetMediaContentPerFormatStatusResp GetMediaContentPerFormatStatus(GetMediaContentPerFormatStatusReq request)
		{
			return this.WrapServiceMethod<GetMediaContentPerFormatStatusResp>(() => this.Proxy.GetMediaContentPerFormatStatus(request));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E60 File Offset: 0x00001060
		public GetMediaContentCoursesResp GetMediaContentCourses(GetMediaContentCoursesReq request)
		{
			return this.WrapServiceMethod<GetMediaContentCoursesResp>(() => this.Proxy.GetMediaContentCourses(request));
		}
	}
}
