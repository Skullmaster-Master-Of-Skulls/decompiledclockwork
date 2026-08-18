using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000005 RID: 5
	public class MediaContentServiceManager : IMediaContent, IService
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000025D8 File Offset: 0x000007D8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025EC File Offset: 0x000007EC
		public GetMediaContentMatchingResp GetMediaContentMatching(GetMediaContentMatchingReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentMatchingResp
			{
				MediaContents = mediaContentManager.GetMediaContentMatching(request.SearchText, request.LUCourseId).ToDTO()
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002630 File Offset: 0x00000830
		[DebuggerStepThrough]
		public Task<GetMediaContentMatchingResp> GetMediaContentMatchingAsync(GetMediaContentMatchingReq request)
		{
			MediaContentServiceManager.<GetMediaContentMatchingAsync>d__2 <GetMediaContentMatchingAsync>d__ = new MediaContentServiceManager.<GetMediaContentMatchingAsync>d__2();
			<GetMediaContentMatchingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetMediaContentMatchingResp>.Create();
			<GetMediaContentMatchingAsync>d__.<>4__this = this;
			<GetMediaContentMatchingAsync>d__.request = request;
			<GetMediaContentMatchingAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingAsync>d__.<>t__builder.Start<MediaContentServiceManager.<GetMediaContentMatchingAsync>d__2>(ref <GetMediaContentMatchingAsync>d__);
			return <GetMediaContentMatchingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000267C File Offset: 0x0000087C
		public LoadMediaContentByIdResp LoadMediaContentById(LoadMediaContentByIdReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentByIdResp
			{
				MediaContent = mediaContentManager.LoadMediaContentById(request.MediaContentID).ToDTO()
			};
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026B8 File Offset: 0x000008B8
		public LoadMediaContentByIdentifierResp LoadMediaContentByIdentifier(LoadMediaContentByIdentifierReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentByIdentifierResp
			{
				MediaContent = mediaContentManager.LoadMediaContentByIdentifier(request.Identifier.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026F8 File Offset: 0x000008F8
		public LoadMediaContentByISBNResp LoadMediaContentByISBN(LoadMediaContentByISBNReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentByISBNResp
			{
				MediaContent = mediaContentManager.LoadMediaContentByISBN(request.ISBN).ToDTO()
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002734 File Offset: 0x00000934
		public LoadMediaContentByCourseResp LoadMediaContentByCourse(LoadMediaContentByCourseReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentByCourseResp
			{
				MediaContents = mediaContentManager.LoadMediaContentByCourse(request.CourseID).ToDTO()
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002770 File Offset: 0x00000970
		public LoadMediaContentByPublisherResp LoadMediaContentByPublisher(LoadMediaContentByPublisherReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentByPublisherResp
			{
				MediaContents = mediaContentManager.LoadMediaContentByPublisher(request.PublisherID).ToDTO()
			};
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027AC File Offset: 0x000009AC
		public LoadMediaContentByCategoryResp LoadMediaContentByCategory(LoadMediaContentByCategoryReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentByCategoryResp
			{
				MediaContents = mediaContentManager.LoadMediaContentByCategory(request.MediaContentCategory).ToDTO()
			};
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027E8 File Offset: 0x000009E8
		public CreateMediaContentResp CreateMediaContent(CreateMediaContentReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			MediaContent mediaContent = request.MediaContent.ToDomainObject();
			return new CreateMediaContentResp
			{
				Identifier = mediaContentManager.CreateMediaContent(mediaContent).ToDTO()
			};
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000282C File Offset: 0x00000A2C
		public UpdateMediaContentResp UpdateMediaContent(UpdateMediaContentReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			mediaContentManager.UpdateMediaContent(request.MediaContent.ToDomainObject());
			return new UpdateMediaContentResp();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002864 File Offset: 0x00000A64
		public DeleteMediaContentResp DeleteMediaContent(DeleteMediaContentReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new DeleteMediaContentResp
			{
				WasDeleted = mediaContentManager.DeleteMediaContent(request.MediaContent.MediaContentUniqueId)
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028A0 File Offset: 0x00000AA0
		public GetAllMediaContentWithFormatsResp GetAllMediaContent(GetAllMediaContentWithFormatsReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetAllMediaContentWithFormatsResp
			{
				MediaContentList = mediaContentManager.GetAllMediaContent().ToDTO()
			};
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028D8 File Offset: 0x00000AD8
		public GetMediaContentPerFormatInfoByIdResp GetMediaContentPerFormatInfoById(GetMediaContentPerFormatInfoByIdReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentPerFormatInfoByIdResp
			{
				MediaContentPerFormatInfo = mediaContentManager.GetMediaContentPerFormatInfoById(request.MediaContentPerFormatId).ToDTO()
			};
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002914 File Offset: 0x00000B14
		public LoadMediaContentPerFormatInfoByMediaContentResp LoadMediaContentPerFormatInfoByMediaContent(LoadMediaContentPerFormatInfoByMediaContentReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new LoadMediaContentPerFormatInfoByMediaContentResp
			{
				MediaContentPerFormatInfoList = mediaContentManager.LoadMediaContentPerFormatInfoByMediaContent(request.MediaContentId).ToDTO()
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002950 File Offset: 0x00000B50
		public GetMediaContentPerFormatStatusListResp GetMediaContentPerFormatStatusList(GetMediaContentPerFormatStatusListReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentPerFormatStatusListResp
			{
				MediaContentPerFormatStatusList = mediaContentManager.GetMediaContentPerFormatStatusList(request.MediaContentId, request.StudentId).ToDTO()
			};
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002994 File Offset: 0x00000B94
		public GetMediaContentThumbnailResp GetMediaContentThumbnail(GetMediaContentThumbnailReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentThumbnailResp
			{
				Thumbnail = mediaContentManager.GetMediaContentThumbnail(request.Identifier.ToDomainObject())
			};
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029D0 File Offset: 0x00000BD0
		public GetMediaContentThumbnailBytesResp GetMediaContentThumbnailBytes(GetMediaContentThumbnailBytesReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentThumbnailBytesResp
			{
				Thumbnail = mediaContentManager.GetMediaContentThumbnailBytes(request.Identifier.ToDomainObject())
			};
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A0C File Offset: 0x00000C0C
		[DebuggerStepThrough]
		public Task<GetMediaContentThumbnailBytesResp> GetMediaContentThumbnailBytesAsync(GetMediaContentThumbnailBytesReq request)
		{
			MediaContentServiceManager.<GetMediaContentThumbnailBytesAsync>d__18 <GetMediaContentThumbnailBytesAsync>d__ = new MediaContentServiceManager.<GetMediaContentThumbnailBytesAsync>d__18();
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetMediaContentThumbnailBytesResp>.Create();
			<GetMediaContentThumbnailBytesAsync>d__.<>4__this = this;
			<GetMediaContentThumbnailBytesAsync>d__.request = request;
			<GetMediaContentThumbnailBytesAsync>d__.<>1__state = -1;
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Start<MediaContentServiceManager.<GetMediaContentThumbnailBytesAsync>d__18>(ref <GetMediaContentThumbnailBytesAsync>d__);
			return <GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A58 File Offset: 0x00000C58
		public SetMediaContentThumbnailResp SetMediaContentThumbnail(SetMediaContentThumbnailReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			mediaContentManager.SetMediaContentThumbnail(request.MediaContentId, request.Thumbnail);
			return new SetMediaContentThumbnailResp();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A90 File Offset: 0x00000C90
		public GetMediaContentCoverImageResp GetMediaContentCoverImage(GetMediaContentCoverImageReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentCoverImageResp
			{
				CoverImage = mediaContentManager.GetMediaContentCoverImage(request.Identifier.ToDomainObject())
			};
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002ACC File Offset: 0x00000CCC
		public GetMediaContentCoverImageBytesResp GetMediaContentCoverImageBytes(GetMediaContentCoverImageBytesReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentCoverImageBytesResp
			{
				CoverImage = mediaContentManager.GetMediaContentCoverImageBytes(request.Identifier.ToDomainObject())
			};
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B08 File Offset: 0x00000D08
		public SetMediaContentCoverResp SetMediaContentCover(SetMediaContentCoverReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			mediaContentManager.SetMediaContentCover(request.MediaContentId, request.CoverImage);
			return new SetMediaContentCoverResp();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B40 File Offset: 0x00000D40
		public GetMediaContentPerFormatStatusResp GetMediaContentPerFormatStatus(GetMediaContentPerFormatStatusReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			return new GetMediaContentPerFormatStatusResp
			{
				Status = ((request.MediaContentPerFormatId > 0) ? mediaContentManager.GetMediaContentPerFormatStatus(request.MediaContentPerFormatId, request.StudentId, request.CheckIfAlreadyExits).ToDTO() : mediaContentManager.GetMediaContentPerFormatStatus(request.MediaContentId, request.MediaContentFormat, request.StudentId, request.CheckIfAlreadyExits).ToDTO())
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public GetMediaContentCoursesResp GetMediaContentCourses(GetMediaContentCoursesReq request)
		{
			IMediaContentManager mediaContentManager = new MediaContentManager(request.GetOperationContext());
			GetMediaContentCoursesResp getMediaContentCoursesResp = new GetMediaContentCoursesResp();
			IList<LookupCourseBase> mediaContentCourses = mediaContentManager.GetMediaContentCourses(request.MediaContentId);
			getMediaContentCoursesResp.Courses = ((mediaContentCourses != null) ? mediaContentCourses.ToDTO() : null);
			return getMediaContentCoursesResp;
		}
	}
}
