using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200009D RID: 157
	public class MediaContentClientManager : IMediaContentClientManager, IWebService
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x00019C90 File Offset: 0x00017E90
		public IList<MediaContentDTO> GetMediaContentMatching(string searchText, int lucourseid = 0)
		{
			List<MediaContentDTO> mediaContentList = new List<MediaContentDTO>();
			GetMediaContentMatchingReq getMediaContentMatchingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentMatchingReq>();
			getMediaContentMatchingReq.SearchText = searchText;
			getMediaContentMatchingReq.LUCourseId = lucourseid;
			mediaContentList.AddRange(ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentMatching(getMediaContentMatchingReq).MediaContents);
			EBookSearchRequestDTO ebookSearchRequestDTO;
			if (!searchText.IsValidISBN())
			{
				(ebookSearchRequestDTO = new EBookSearchRequestDTO()).SearchText = searchText;
			}
			else
			{
				(ebookSearchRequestDTO = new EBookSearchRequestDTO()).ISBN = searchText;
			}
			EBookSearchRequestDTO request = ebookSearchRequestDTO;
			IBookSearchClientManager bookSearchClientManager = new BookSearchClientManager();
			IList<EBookSearchResultDTO> source = bookSearchClientManager.SearchForVolumes(request);
			mediaContentList.AddRange(from r in source
			select r.ToMediaContentDTO() into c1
			where mediaContentList.All((MediaContentDTO c2) => !c2.Identifier.Equals(c1.Identifier))
			select c1);
			return mediaContentList;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019D6C File Offset: 0x00017F6C
		[DebuggerStepThrough]
		public Task<IList<MediaContentDTO>> GetMediaContentMatchingAsync(string searchText, int lucourseid = 0)
		{
			MediaContentClientManager.<GetMediaContentMatchingAsync>d__1 <GetMediaContentMatchingAsync>d__ = new MediaContentClientManager.<GetMediaContentMatchingAsync>d__1();
			<GetMediaContentMatchingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentDTO>>.Create();
			<GetMediaContentMatchingAsync>d__.<>4__this = this;
			<GetMediaContentMatchingAsync>d__.searchText = searchText;
			<GetMediaContentMatchingAsync>d__.lucourseid = lucourseid;
			<GetMediaContentMatchingAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingAsync>d__.<>t__builder.Start<MediaContentClientManager.<GetMediaContentMatchingAsync>d__1>(ref <GetMediaContentMatchingAsync>d__);
			return <GetMediaContentMatchingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019DC0 File Offset: 0x00017FC0
		public MediaContentDTO LoadMediaContentById(Guid mediaContentId)
		{
			LoadMediaContentByIdReq loadMediaContentByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentByIdReq>();
			loadMediaContentByIdReq.MediaContentID = mediaContentId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentById(loadMediaContentByIdReq).MediaContent;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public MediaContentDTO LoadMediaContentByIdentifier(MediaContentIdentifierDTO identifier)
		{
			LoadMediaContentByIdentifierReq loadMediaContentByIdentifierReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentByIdentifierReq>();
			loadMediaContentByIdentifierReq.Identifier = identifier;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentByIdentifier(loadMediaContentByIdentifierReq).MediaContent;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019E30 File Offset: 0x00018030
		public MediaContentDTO LoadMediaContentByISBN(string isbn)
		{
			LoadMediaContentByISBNReq loadMediaContentByISBNReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentByISBNReq>();
			loadMediaContentByISBNReq.ISBN = isbn;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentByISBN(loadMediaContentByISBNReq).MediaContent;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019E68 File Offset: 0x00018068
		public IList<MediaContentDTO> LoadMediaContentByCourse(int courseId)
		{
			LoadMediaContentByCourseReq loadMediaContentByCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentByCourseReq>();
			loadMediaContentByCourseReq.CourseID = courseId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentByCourse(loadMediaContentByCourseReq).MediaContents;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019EA0 File Offset: 0x000180A0
		public IList<MediaContentDTO> LoadMediaContentByPublisher(int publisherId)
		{
			LoadMediaContentByPublisherReq loadMediaContentByPublisherReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentByPublisherReq>();
			loadMediaContentByPublisherReq.PublisherID = publisherId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentByPublisher(loadMediaContentByPublisherReq).MediaContents;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00019ED8 File Offset: 0x000180D8
		public IList<MediaContentDTO> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory)
		{
			LoadMediaContentByCategoryReq loadMediaContentByCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentByCategoryReq>();
			loadMediaContentByCategoryReq.MediaContentCategory = mediaContentCategory;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentByCategory(loadMediaContentByCategoryReq).MediaContents;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00019F10 File Offset: 0x00018110
		public MediaContentIdentifierDTO CreateMediaContent(MediaContentDTO mediaContent)
		{
			CreateMediaContentReq createMediaContentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateMediaContentReq>();
			createMediaContentReq.MediaContent = mediaContent;
			CreateMediaContentResp createMediaContentResp = ClientServiceFactory.GetClientInstance<IMediaContent>().CreateMediaContent(createMediaContentReq);
			return mediaContent.Identifier = createMediaContentResp.Identifier;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00019F54 File Offset: 0x00018154
		public void UpdateMediaContent(MediaContentDTO mediaContent)
		{
			UpdateMediaContentReq updateMediaContentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaContentReq>();
			updateMediaContentReq.MediaContent = mediaContent;
			ClientServiceFactory.GetClientInstance<IMediaContent>().UpdateMediaContent(updateMediaContentReq);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00019F84 File Offset: 0x00018184
		public bool DeleteMediaContent(MediaContentDTO mediaContent)
		{
			DeleteMediaContentReq deleteMediaContentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteMediaContentReq>();
			deleteMediaContentReq.MediaContent = mediaContent;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().DeleteMediaContent(deleteMediaContentReq).WasDeleted;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00019FBC File Offset: 0x000181BC
		public IList<MediaContentDTO> GetAllMediaContent()
		{
			GetAllMediaContentWithFormatsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllMediaContentWithFormatsReq>();
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetAllMediaContent(request).MediaContentList;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00019FEC File Offset: 0x000181EC
		public MediaContentPerFormatInfoDTO GetMediaContentPerFormatInfoById(int mediaContentPerFormatId)
		{
			GetMediaContentPerFormatInfoByIdReq getMediaContentPerFormatInfoByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentPerFormatInfoByIdReq>();
			getMediaContentPerFormatInfoByIdReq.MediaContentPerFormatId = mediaContentPerFormatId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentPerFormatInfoById(getMediaContentPerFormatInfoByIdReq).MediaContentPerFormatInfo;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001A024 File Offset: 0x00018224
		public IList<MediaContentPerFormatInfoDTO> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId)
		{
			LoadMediaContentPerFormatInfoByMediaContentReq loadMediaContentPerFormatInfoByMediaContentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentPerFormatInfoByMediaContentReq>();
			loadMediaContentPerFormatInfoByMediaContentReq.MediaContentId = mediaContentId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().LoadMediaContentPerFormatInfoByMediaContent(loadMediaContentPerFormatInfoByMediaContentReq).MediaContentPerFormatInfoList;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001A05C File Offset: 0x0001825C
		public MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(int mediaContentPerFormat, int studentId, bool checkIfAlreadyExits = true)
		{
			GetMediaContentPerFormatStatusReq getMediaContentPerFormatStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentPerFormatStatusReq>();
			getMediaContentPerFormatStatusReq.MediaContentPerFormatId = mediaContentPerFormat;
			getMediaContentPerFormatStatusReq.StudentId = studentId;
			getMediaContentPerFormatStatusReq.CheckIfAlreadyExits = checkIfAlreadyExits;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentPerFormatStatus(getMediaContentPerFormatStatusReq).Status;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001A0A4 File Offset: 0x000182A4
		public MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId, bool checkIfAlreadyExits = true)
		{
			GetMediaContentPerFormatStatusReq getMediaContentPerFormatStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentPerFormatStatusReq>();
			getMediaContentPerFormatStatusReq.MediaContentId = mediaContentId;
			getMediaContentPerFormatStatusReq.MediaContentFormat = mediaContentFormat;
			getMediaContentPerFormatStatusReq.StudentId = studentId;
			getMediaContentPerFormatStatusReq.CheckIfAlreadyExits = checkIfAlreadyExits;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentPerFormatStatus(getMediaContentPerFormatStatusReq).Status;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001A0F4 File Offset: 0x000182F4
		public IList<MediaContentPerFormatStatusInfoDTO> GetMediaContentPerFormatStatusList(Guid mediaContentId, int studentId)
		{
			GetMediaContentPerFormatStatusListReq getMediaContentPerFormatStatusListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentPerFormatStatusListReq>();
			getMediaContentPerFormatStatusListReq.MediaContentId = mediaContentId;
			getMediaContentPerFormatStatusListReq.StudentId = studentId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentPerFormatStatusList(getMediaContentPerFormatStatusListReq).MediaContentPerFormatStatusList;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001A134 File Offset: 0x00018334
		public Image GetMediaContentThumbnail(MediaContentIdentifierDTO identifier)
		{
			GetMediaContentThumbnailReq getMediaContentThumbnailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentThumbnailReq>();
			getMediaContentThumbnailReq.Identifier = identifier;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentThumbnail(getMediaContentThumbnailReq).Thumbnail;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A16C File Offset: 0x0001836C
		public byte[] GetMediaContentThumbnailBytes(MediaContentIdentifierDTO identifier)
		{
			GetMediaContentThumbnailBytesReq getMediaContentThumbnailBytesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentThumbnailBytesReq>();
			getMediaContentThumbnailBytesReq.Identifier = identifier;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentThumbnailBytes(getMediaContentThumbnailBytesReq).Thumbnail;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A1A4 File Offset: 0x000183A4
		[DebuggerStepThrough]
		public Task<byte[]> GetMediaContentThumbnailBytesAsync(MediaContentIdentifierDTO identifier)
		{
			MediaContentClientManager.<GetMediaContentThumbnailBytesAsync>d__19 <GetMediaContentThumbnailBytesAsync>d__ = new MediaContentClientManager.<GetMediaContentThumbnailBytesAsync>d__19();
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<GetMediaContentThumbnailBytesAsync>d__.<>4__this = this;
			<GetMediaContentThumbnailBytesAsync>d__.identifier = identifier;
			<GetMediaContentThumbnailBytesAsync>d__.<>1__state = -1;
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Start<MediaContentClientManager.<GetMediaContentThumbnailBytesAsync>d__19>(ref <GetMediaContentThumbnailBytesAsync>d__);
			return <GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A1F0 File Offset: 0x000183F0
		public void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail)
		{
			SetMediaContentThumbnailReq setMediaContentThumbnailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetMediaContentThumbnailReq>();
			setMediaContentThumbnailReq.MediaContentId = mediaContentId;
			setMediaContentThumbnailReq.Thumbnail = thumbnail;
			ClientServiceFactory.GetClientInstance<IMediaContent>().SetMediaContentThumbnail(setMediaContentThumbnailReq);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001A228 File Offset: 0x00018428
		public Image GetMediaContentCoverImage(MediaContentIdentifierDTO identifier)
		{
			GetMediaContentCoverImageReq getMediaContentCoverImageReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentCoverImageReq>();
			getMediaContentCoverImageReq.Identifier = identifier;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentCoverImage(getMediaContentCoverImageReq).CoverImage;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001A260 File Offset: 0x00018460
		public byte[] GetMediaContentCoverImageBytes(MediaContentIdentifierDTO identifier)
		{
			GetMediaContentCoverImageBytesReq getMediaContentCoverImageBytesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentCoverImageBytesReq>();
			getMediaContentCoverImageBytesReq.Identifier = identifier;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentCoverImageBytes(getMediaContentCoverImageBytesReq).CoverImage;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A298 File Offset: 0x00018498
		public void SetMediaContentCover(Guid mediaContentId, Image cover)
		{
			SetMediaContentCoverReq setMediaContentCoverReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetMediaContentCoverReq>();
			setMediaContentCoverReq.MediaContentId = mediaContentId;
			setMediaContentCoverReq.CoverImage = cover;
			ClientServiceFactory.GetClientInstance<IMediaContent>().SetMediaContentCover(setMediaContentCoverReq);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001A2D0 File Offset: 0x000184D0
		public IList<LookupCourseBaseDTO> GetMediaContentCourses(Guid mediaContentId)
		{
			GetMediaContentCoursesReq getMediaContentCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentCoursesReq>();
			getMediaContentCoursesReq.MediaContentId = mediaContentId;
			return ClientServiceFactory.GetClientInstance<IMediaContent>().GetMediaContentCourses(getMediaContentCoursesReq).Courses;
		}
	}
}
