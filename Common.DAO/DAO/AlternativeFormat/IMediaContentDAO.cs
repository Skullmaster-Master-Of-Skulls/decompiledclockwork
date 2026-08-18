using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000CA RID: 202
	public interface IMediaContentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005BA RID: 1466
		IList<MediaContent> GetMediaContentMatchingUsingEquivalentCoursesAlt(string searchText, int lucourseid = 0);

		// Token: 0x060005BB RID: 1467
		IList<MediaContent> GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAlt(string searchText, int lucourseid = 0);

		// Token: 0x060005BC RID: 1468
		Task<IList<MediaContent>> GetMediaContentMatchingUsingEquivalentCoursesAltAsync(string searchText, int lucourseid = 0);

		// Token: 0x060005BD RID: 1469
		Task<IList<MediaContent>> GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync(string searchText, int lucourseid = 0);

		// Token: 0x060005BE RID: 1470
		MediaContent LoadMediaContentById(Guid mediaContentId);

		// Token: 0x060005BF RID: 1471
		MediaContent LoadMediaContentByISBN(string isbn);

		// Token: 0x060005C0 RID: 1472
		IList<MediaContent> LoadMediaContentByTitle(string title);

		// Token: 0x060005C1 RID: 1473
		IList<MediaContent> LoadMediaContentByCourseUsingEquivalentCoursesAlt(int courseId);

		// Token: 0x060005C2 RID: 1474
		IList<MediaContent> LoadMediaContentByCourseUsingUserDefinedEquivalentCoursesAlt(int courseId);

		// Token: 0x060005C3 RID: 1475
		IList<MediaContent> LoadMediaContentByPublisher(int publisherId);

		// Token: 0x060005C4 RID: 1476
		IList<MediaContent> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory);

		// Token: 0x060005C5 RID: 1477
		MediaContentIdentifier CreateMediaContent(MediaContent mediaContent);

		// Token: 0x060005C6 RID: 1478
		void UpdateMediaContent(MediaContent mediaContent);

		// Token: 0x060005C7 RID: 1479
		bool DeleteMediaContent(Guid mediaContentId);

		// Token: 0x060005C8 RID: 1480
		IList<MediaContent> GetAllMediaContent();

		// Token: 0x060005C9 RID: 1481
		MediaContentPerFormatInfo GetMediaContentPerFormatInfoById(int mediaContentPerFormat);

		// Token: 0x060005CA RID: 1482
		IList<MediaContentPerFormatInfo> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId);

		// Token: 0x060005CB RID: 1483
		int GetMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat);

		// Token: 0x060005CC RID: 1484
		Task<int> GetMediaContentPerFormatIdAsync(Guid mediaContentId, MediaContentFormat mediaContentFormat);

		// Token: 0x060005CD RID: 1485
		Image GetMediaContentThumbnail(Guid mediaContentId);

		// Token: 0x060005CE RID: 1486
		byte[] GetMediaContentThumbnailBytes(Guid mediaContentId);

		// Token: 0x060005CF RID: 1487
		Task<byte[]> GetMediaContentThumbnailBytesAsync(Guid mediaContentId);

		// Token: 0x060005D0 RID: 1488
		void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail);

		// Token: 0x060005D1 RID: 1489
		Task SetMediaContentThumbnailAsync(Guid mediaContentId, Image thumbnail);

		// Token: 0x060005D2 RID: 1490
		bool IsThumbnailAvailable(Guid mediaContentId);

		// Token: 0x060005D3 RID: 1491
		Image GetMediaContentCoverImage(Guid mediaContentId);

		// Token: 0x060005D4 RID: 1492
		byte[] GetMediaContentCoverImageBytes(Guid mediaContentId);

		// Token: 0x060005D5 RID: 1493
		void SetMediaContentCoverImage(Guid mediaContentId, Image cover, Image thumbnail);

		// Token: 0x060005D6 RID: 1494
		IList<LookupCourseBase> GetMediaContentCourses(Guid mediaContentId);
	}
}
