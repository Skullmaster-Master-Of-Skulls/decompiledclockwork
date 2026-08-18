using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000EF RID: 239
	public interface IMediaContentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000784 RID: 1924
		IList<MediaContent> GetMediaContentMatching(string searchText, int lucourseid = 0);

		// Token: 0x06000785 RID: 1925
		Task<IList<MediaContent>> GetMediaContentMatchingAsync(string searchText, int lucourseid = 0);

		// Token: 0x06000786 RID: 1926
		MediaContent LoadMediaContentById(Guid mediaContentId);

		// Token: 0x06000787 RID: 1927
		MediaContent LoadMediaContentByIdentifier(MediaContentIdentifier identifier);

		// Token: 0x06000788 RID: 1928
		MediaContent LoadMediaContentByISBN(string isbn);

		// Token: 0x06000789 RID: 1929
		IList<MediaContent> LoadMediaContentByTitle(string title);

		// Token: 0x0600078A RID: 1930
		IList<MediaContent> LoadMediaContentByCourse(int courseId);

		// Token: 0x0600078B RID: 1931
		IList<MediaContent> LoadMediaContentByPublisher(int publisherId);

		// Token: 0x0600078C RID: 1932
		IList<MediaContent> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory);

		// Token: 0x0600078D RID: 1933
		MediaContentIdentifier CreateMediaContent(MediaContent mediaContent);

		// Token: 0x0600078E RID: 1934
		void UpdateMediaContent(MediaContent mediaContent);

		// Token: 0x0600078F RID: 1935
		bool DeleteMediaContent(Guid mediaContentId);

		// Token: 0x06000790 RID: 1936
		IList<MediaContent> GetAllMediaContent();

		// Token: 0x06000791 RID: 1937
		MediaContentPerFormatInfo GetMediaContentPerFormatInfoById(int mediaContentPerFormat);

		// Token: 0x06000792 RID: 1938
		IList<MediaContentPerFormatInfo> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId);

		// Token: 0x06000793 RID: 1939
		MediaContentPerFormatStatusInfo GetMediaContentPerFormatStatus(int mediaContentPerFormat, int studentId, bool checkIfAlreadyExits = true);

		// Token: 0x06000794 RID: 1940
		MediaContentPerFormatStatusInfo GetMediaContentPerFormatStatus(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId, bool checkIfAlreadyExits = true);

		// Token: 0x06000795 RID: 1941
		IList<MediaContentPerFormatStatusInfo> GetMediaContentPerFormatStatusList(Guid mediaContentId, int studentId);

		// Token: 0x06000796 RID: 1942
		Image GetMediaContentThumbnail(MediaContentIdentifier identifier);

		// Token: 0x06000797 RID: 1943
		byte[] GetMediaContentThumbnailBytes(MediaContentIdentifier identifier);

		// Token: 0x06000798 RID: 1944
		Task<byte[]> GetMediaContentThumbnailBytesAsync(MediaContentIdentifier identifier);

		// Token: 0x06000799 RID: 1945
		void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail);

		// Token: 0x0600079A RID: 1946
		Task SetMediaContentThumbnailAsync(Guid mediaContentId, Image thumbnail);

		// Token: 0x0600079B RID: 1947
		Image GetMediaContentCoverImage(MediaContentIdentifier identifier);

		// Token: 0x0600079C RID: 1948
		byte[] GetMediaContentCoverImageBytes(MediaContentIdentifier identifier);

		// Token: 0x0600079D RID: 1949
		void SetMediaContentCover(Guid mediaContentId, Image cover);

		// Token: 0x0600079E RID: 1950
		IList<LookupCourseBase> GetMediaContentCourses(Guid mediaContentId);
	}
}
