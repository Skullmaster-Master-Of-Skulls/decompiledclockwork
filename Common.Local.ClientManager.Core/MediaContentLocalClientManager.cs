using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Local.ClientManager.Core.AlternateFormat
{
	// Token: 0x02000002 RID: 2
	public class MediaContentLocalClientManager : IMediaContentClientManager, IWebService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public IList<MediaContentDTO> GetMediaContentMatching(string searchText, int lucourseid = 0)
		{
			IMediaContentManager mediaContentManager = ObjectFactory.Resolve<IMediaContentManager>();
			mediaContentManager.OpContext = null;
			IList<MediaContent> mediaContentMatching = mediaContentManager.GetMediaContentMatching(searchText, lucourseid);
			return ((mediaContentMatching != null) ? (from g in mediaContentMatching
			select this._mapper.Map<MediaContent, MediaContentDTO>(g)).ToList<MediaContentDTO>() : null) ?? new List<MediaContentDTO>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000208B File Offset: 0x0000028B
		public Task<IList<MediaContentDTO>> GetMediaContentMatchingAsync(string searchText, int lucourseid = 0)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentDTO LoadMediaContentById(Guid mediaContentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentDTO LoadMediaContentByIdentifier(MediaContentIdentifierDTO identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentDTO LoadMediaContentByISBN(string isbn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<MediaContentDTO> LoadMediaContentByCourse(int courseId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<MediaContentDTO> LoadMediaContentByPublisher(int publisherId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<MediaContentDTO> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentIdentifierDTO CreateMediaContent(MediaContentDTO mediaContent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000208B File Offset: 0x0000028B
		public void UpdateMediaContent(MediaContentDTO mediaContent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000208B File Offset: 0x0000028B
		public bool DeleteMediaContent(MediaContentDTO mediaContent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<MediaContentDTO> GetAllMediaContent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentDTO GetMediaContentWithFormats(Guid mediaContentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentPerFormatInfoDTO GetMediaContentPerFormatInfoById(int mediaContentPerFormat)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<MediaContentPerFormatInfoDTO> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(int mediaContentPerFormat, int studentId, bool checkIfAlreadyExists = true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000208B File Offset: 0x0000028B
		public MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId, bool checkIfAlreadyExists = true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<MediaContentPerFormatStatusInfoDTO> GetMediaContentPerFormatStatusList(Guid mediaContentId, int studentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000208B File Offset: 0x0000028B
		public Image GetMediaContentThumbnail(MediaContentIdentifierDTO identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000208B File Offset: 0x0000028B
		public byte[] GetMediaContentThumbnailBytes(MediaContentIdentifierDTO identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000208B File Offset: 0x0000028B
		public Task<byte[]> GetMediaContentThumbnailBytesAsync(MediaContentIdentifierDTO identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000208B File Offset: 0x0000028B
		public Image GetMediaContentCoverImage(MediaContentIdentifierDTO identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000208B File Offset: 0x0000028B
		public byte[] GetMediaContentCoverImageBytes(MediaContentIdentifierDTO identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000208B File Offset: 0x0000028B
		public void SetMediaContentCover(Guid mediaContentId, Image cover)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000208B File Offset: 0x0000028B
		public void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000208B File Offset: 0x0000028B
		public IList<LookupCourseBaseDTO> GetMediaContentCourses(Guid mediaContentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000001 RID: 1
		private readonly IMapper _mapper;
	}
}
