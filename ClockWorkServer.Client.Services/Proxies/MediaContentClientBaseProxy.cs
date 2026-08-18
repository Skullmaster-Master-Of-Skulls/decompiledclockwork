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
	// Token: 0x02000009 RID: 9
	internal class MediaContentClientBaseProxy : ClientBase<IMediaContent>, IMediaContent, IService
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002E98 File Offset: 0x00001098
		public MediaContentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EA3 File Offset: 0x000010A3
		public MediaContentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EB0 File Offset: 0x000010B0
		[DebuggerStepThrough]
		public Task<GetMediaContentMatchingResp> GetMediaContentMatchingAsync(GetMediaContentMatchingReq request)
		{
			MediaContentClientBaseProxy.<GetMediaContentMatchingAsync>d__2 <GetMediaContentMatchingAsync>d__ = new MediaContentClientBaseProxy.<GetMediaContentMatchingAsync>d__2();
			<GetMediaContentMatchingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetMediaContentMatchingResp>.Create();
			<GetMediaContentMatchingAsync>d__.<>4__this = this;
			<GetMediaContentMatchingAsync>d__.request = request;
			<GetMediaContentMatchingAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingAsync>d__.<>t__builder.Start<MediaContentClientBaseProxy.<GetMediaContentMatchingAsync>d__2>(ref <GetMediaContentMatchingAsync>d__);
			return <GetMediaContentMatchingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EFC File Offset: 0x000010FC
		public GetMediaContentMatchingResp GetMediaContentMatching(GetMediaContentMatchingReq request)
		{
			return base.Channel.GetMediaContentMatching(request);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F1C File Offset: 0x0000111C
		public LoadMediaContentByIdResp LoadMediaContentById(LoadMediaContentByIdReq request)
		{
			return base.Channel.LoadMediaContentById(request);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F3C File Offset: 0x0000113C
		public LoadMediaContentByIdentifierResp LoadMediaContentByIdentifier(LoadMediaContentByIdentifierReq request)
		{
			return base.Channel.LoadMediaContentByIdentifier(request);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F5C File Offset: 0x0000115C
		public LoadMediaContentByISBNResp LoadMediaContentByISBN(LoadMediaContentByISBNReq request)
		{
			return base.Channel.LoadMediaContentByISBN(request);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F7C File Offset: 0x0000117C
		public LoadMediaContentByCourseResp LoadMediaContentByCourse(LoadMediaContentByCourseReq request)
		{
			return base.Channel.LoadMediaContentByCourse(request);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F9C File Offset: 0x0000119C
		public LoadMediaContentByPublisherResp LoadMediaContentByPublisher(LoadMediaContentByPublisherReq request)
		{
			return base.Channel.LoadMediaContentByPublisher(request);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FBC File Offset: 0x000011BC
		public LoadMediaContentByCategoryResp LoadMediaContentByCategory(LoadMediaContentByCategoryReq request)
		{
			return base.Channel.LoadMediaContentByCategory(request);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FDC File Offset: 0x000011DC
		public CreateMediaContentResp CreateMediaContent(CreateMediaContentReq request)
		{
			return base.Channel.CreateMediaContent(request);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FFC File Offset: 0x000011FC
		public UpdateMediaContentResp UpdateMediaContent(UpdateMediaContentReq request)
		{
			return base.Channel.UpdateMediaContent(request);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000301C File Offset: 0x0000121C
		public DeleteMediaContentResp DeleteMediaContent(DeleteMediaContentReq request)
		{
			return base.Channel.DeleteMediaContent(request);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000303C File Offset: 0x0000123C
		public GetAllMediaContentWithFormatsResp GetAllMediaContent(GetAllMediaContentWithFormatsReq request)
		{
			return base.Channel.GetAllMediaContent(request);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000305C File Offset: 0x0000125C
		public GetMediaContentPerFormatInfoByIdResp GetMediaContentPerFormatInfoById(GetMediaContentPerFormatInfoByIdReq request)
		{
			return base.Channel.GetMediaContentPerFormatInfoById(request);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000307C File Offset: 0x0000127C
		public LoadMediaContentPerFormatInfoByMediaContentResp LoadMediaContentPerFormatInfoByMediaContent(LoadMediaContentPerFormatInfoByMediaContentReq request)
		{
			return base.Channel.LoadMediaContentPerFormatInfoByMediaContent(request);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000309C File Offset: 0x0000129C
		public GetMediaContentPerFormatStatusListResp GetMediaContentPerFormatStatusList(GetMediaContentPerFormatStatusListReq request)
		{
			return base.Channel.GetMediaContentPerFormatStatusList(request);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030BC File Offset: 0x000012BC
		public GetMediaContentThumbnailResp GetMediaContentThumbnail(GetMediaContentThumbnailReq request)
		{
			return base.Channel.GetMediaContentThumbnail(request);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030DC File Offset: 0x000012DC
		[DebuggerStepThrough]
		public Task<GetMediaContentThumbnailBytesResp> GetMediaContentThumbnailBytesAsync(GetMediaContentThumbnailBytesReq request)
		{
			MediaContentClientBaseProxy.<GetMediaContentThumbnailBytesAsync>d__18 <GetMediaContentThumbnailBytesAsync>d__ = new MediaContentClientBaseProxy.<GetMediaContentThumbnailBytesAsync>d__18();
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetMediaContentThumbnailBytesResp>.Create();
			<GetMediaContentThumbnailBytesAsync>d__.<>4__this = this;
			<GetMediaContentThumbnailBytesAsync>d__.request = request;
			<GetMediaContentThumbnailBytesAsync>d__.<>1__state = -1;
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Start<MediaContentClientBaseProxy.<GetMediaContentThumbnailBytesAsync>d__18>(ref <GetMediaContentThumbnailBytesAsync>d__);
			return <GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003128 File Offset: 0x00001328
		public GetMediaContentThumbnailBytesResp GetMediaContentThumbnailBytes(GetMediaContentThumbnailBytesReq request)
		{
			return base.Channel.GetMediaContentThumbnailBytes(request);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003148 File Offset: 0x00001348
		public SetMediaContentThumbnailResp SetMediaContentThumbnail(SetMediaContentThumbnailReq request)
		{
			return base.Channel.SetMediaContentThumbnail(request);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003168 File Offset: 0x00001368
		public GetMediaContentCoverImageResp GetMediaContentCoverImage(GetMediaContentCoverImageReq request)
		{
			return base.Channel.GetMediaContentCoverImage(request);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003188 File Offset: 0x00001388
		public GetMediaContentCoverImageBytesResp GetMediaContentCoverImageBytes(GetMediaContentCoverImageBytesReq request)
		{
			return base.Channel.GetMediaContentCoverImageBytes(request);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000031A8 File Offset: 0x000013A8
		public SetMediaContentCoverResp SetMediaContentCover(SetMediaContentCoverReq request)
		{
			return base.Channel.SetMediaContentCover(request);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000031C8 File Offset: 0x000013C8
		public GetMediaContentPerFormatStatusResp GetMediaContentPerFormatStatus(GetMediaContentPerFormatStatusReq request)
		{
			return base.Channel.GetMediaContentPerFormatStatus(request);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031E8 File Offset: 0x000013E8
		public GetMediaContentCoursesResp GetMediaContentCourses(GetMediaContentCoursesReq request)
		{
			return base.Channel.GetMediaContentCourses(request);
		}
	}
}
