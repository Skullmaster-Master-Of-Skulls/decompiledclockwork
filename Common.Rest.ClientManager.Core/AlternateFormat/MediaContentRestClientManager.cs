using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x02000087 RID: 135
	public class MediaContentRestClientManager : BearerTokenRestProxy<IMediaContentClientManager>, IMediaContentClientManager, IWebService
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x0000F39E File Offset: 0x0000D59E
		public MediaContentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		public MediaContentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public IList<MediaContentDTO> GetMediaContentMatching(string searchText, int lucourseid = 0)
		{
			List<MediaContentDTO> mediaContentList = new List<MediaContentDTO>();
			mediaContentList.AddRange(base.GetMany<MediaContentDTO>(string.Format("mediacontent/matching/{0}?lucourseid={1}", searchText, lucourseid), true));
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
			IList<EBookSearchResultDTO> source = ObjectFactory.Resolve<IBookSearchClientManager>().SearchForVolumes(request);
			mediaContentList.AddRange(from r in source
			select r.ToMediaContentDTO() into c1
			where mediaContentList.All((MediaContentDTO c2) => !c2.Identifier.Equals(c1.Identifier))
			select c1);
			return mediaContentList;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000F46C File Offset: 0x0000D66C
		public async Task<IList<MediaContentDTO>> GetMediaContentMatchingAsync(string searchText, int lucourseid = 0)
		{
			List<MediaContentDTO> mediaContentList = new List<MediaContentDTO>();
			List<MediaContentDTO> list = mediaContentList;
			IEnumerable<MediaContentDTO> collection = await this.GetManyAsync<MediaContentDTO>(string.Format("mediacontent/matching/{0}?lucourseid={1}", searchText, lucourseid), true).ConfigureAwait(false);
			list.AddRange(collection);
			list = null;
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
			IList<EBookSearchResultDTO> source = await ObjectFactory.Resolve<IBookSearchClientManager>().SearchForVolumesAsync(request).ConfigureAwait(false);
			mediaContentList.AddRange(from r in source
			select r.ToMediaContentDTO() into c1
			where mediaContentList.All((MediaContentDTO c2) => !c2.Identifier.Equals(c1.Identifier))
			select c1);
			return mediaContentList;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
		public MediaContentDTO LoadMediaContentById(Guid mediaContentId)
		{
			return base.Get<MediaContentDTO>(string.Format("mediacontent/uid/{0}", mediaContentId), true);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000F4DA File Offset: 0x0000D6DA
		public MediaContentDTO LoadMediaContentByIdentifier(MediaContentIdentifierDTO identifier)
		{
			return base.Get<MediaContentDTO>(string.Format("mediacontent/id/{0}", identifier), true);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000F4EE File Offset: 0x0000D6EE
		public MediaContentDTO LoadMediaContentByISBN(string isbn)
		{
			return base.Get<MediaContentDTO>(string.Format("mediacontent/isbn/{0}", isbn), true);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000F502 File Offset: 0x0000D702
		public IList<MediaContentDTO> LoadMediaContentByCourse(int courseId)
		{
			return base.GetMany<MediaContentDTO>(string.Format("mediacontent/course/{0}", courseId), true);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000F51B File Offset: 0x0000D71B
		public IList<MediaContentDTO> LoadMediaContentByPublisher(int publisherId)
		{
			return base.GetMany<MediaContentDTO>(string.Format("mediacontent/publisher/{0}", publisherId), true);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000F534 File Offset: 0x0000D734
		public IList<MediaContentDTO> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory)
		{
			return base.GetMany<MediaContentDTO>(string.Format("mediacontent/category/{0}", mediaContentCategory), true);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000F54D File Offset: 0x0000D74D
		public MediaContentIdentifierDTO CreateMediaContent(MediaContentDTO mediaContent)
		{
			return base.Post<MediaContentDTO, MediaContentIdentifierDTO>(mediaContent, "mediacontent");
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000F55B File Offset: 0x0000D75B
		public void UpdateMediaContent(MediaContentDTO mediaContent)
		{
			base.Put<MediaContentDTO>(mediaContent, "mediacontent");
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000F569 File Offset: 0x0000D769
		public bool DeleteMediaContent(MediaContentDTO mediaContent)
		{
			base.Delete(string.Format("mediacontent/{0}", mediaContent.MediaContentUniqueId));
			return true;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000F587 File Offset: 0x0000D787
		public IList<MediaContentWithFormatsDTO> GetAllMediaContentWithFormats()
		{
			return base.GetMany<MediaContentWithFormatsDTO>("mediacontent/withformats", true);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000F595 File Offset: 0x0000D795
		public MediaContentWithFormatsDTO GetMediaContentWithFormats(Guid mediaContentId)
		{
			return base.Get<MediaContentWithFormatsDTO>(string.Format("mediacontent/withformats/uid/{0}", mediaContentId), true);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000F5AE File Offset: 0x0000D7AE
		public MediaContentPerFormatInfoDTO GetMediaContentPerFormatInfoById(int mediaContentPerFormat)
		{
			return base.Get<MediaContentPerFormatInfoDTO>(string.Format("mediacontent/performatinfo/{0}", mediaContentPerFormat), true);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000F5C7 File Offset: 0x0000D7C7
		public IList<MediaContentPerFormatInfoDTO> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId)
		{
			return base.GetMany<MediaContentPerFormatInfoDTO>(string.Format("mediacontent/performatinfo/{0}", mediaContentId), true);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(int mediaContentPerFormat, int studentId, bool checkIfAlreadyExists = true)
		{
			return base.Get<MediaContentPerFormatStatusInfoDTO>(string.Format("mediacontent/performatstatusbyid/studentid/{0}/contentperformatid/{1}?checkifalreadyexists={2}", studentId, mediaContentPerFormat, checkIfAlreadyExists), true);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000F605 File Offset: 0x0000D805
		public MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId, bool checkIfAlreadyExists = true)
		{
			return base.Get<MediaContentPerFormatStatusInfoDTO>(string.Format("mediacontent/performatstatus/studentid/{0}/contentid/{1}/format/{2}?checkifalreadyexists={3}", new object[]
			{
				studentId,
				mediaContentId,
				mediaContentFormat,
				checkIfAlreadyExists
			}), true);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000F643 File Offset: 0x0000D843
		public IList<MediaContentPerFormatStatusInfoDTO> GetMediaContentPerFormatStatusList(Guid mediaContentId, int studentId)
		{
			return base.GetMany<MediaContentPerFormatStatusInfoDTO>(string.Format("mediacontent/performatstatus/studentid/{0}/contentid/{1}", studentId, mediaContentId), true);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000F662 File Offset: 0x0000D862
		public Image GetMediaContentThumbnail(MediaContentIdentifierDTO identifier)
		{
			return base.Get<Image>(string.Format("mediacontent/thumbnail/contentid/{0}", identifier), true);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000F676 File Offset: 0x0000D876
		public byte[] GetMediaContentThumbnailBytes(MediaContentIdentifierDTO identifier)
		{
			return base.Get<byte[]>(string.Format("mediacontent/thumbnailbytes/contentid/{0}", identifier), true);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000F68C File Offset: 0x0000D88C
		public async Task<byte[]> GetMediaContentThumbnailBytesAsync(MediaContentIdentifierDTO identifier)
		{
			return await this.GetAsync<byte[]>(string.Format("mediacontent/thumbnailbytes/contentid/{0}", identifier), true).ConfigureAwait(false);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000F6D9 File Offset: 0x0000D8D9
		public Image GetMediaContentCoverImage(MediaContentIdentifierDTO identifier)
		{
			return base.Get<Image>(string.Format("mediacontent/coverimage/contentid/{0}", identifier), true);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000F6ED File Offset: 0x0000D8ED
		public byte[] GetMediaContentCoverImageBytes(MediaContentIdentifierDTO identifier)
		{
			return base.Get<byte[]>(string.Format("mediacontent/coverimagebytes/contentid/{0}", identifier), true);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000F701 File Offset: 0x0000D901
		public void SetMediaContentCover(Guid mediaContentId, Image cover)
		{
			base.Put<Image>(cover, string.Format("mediacontent/coverimage/contentid/{0}", mediaContentId));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000F71A File Offset: 0x0000D91A
		public void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail)
		{
			base.Put<Image>(thumbnail, string.Format("thumbnail/contentid/{0}", mediaContentId));
		}
	}
}
