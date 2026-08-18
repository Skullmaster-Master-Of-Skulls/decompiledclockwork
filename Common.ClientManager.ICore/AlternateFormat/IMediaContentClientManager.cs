using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x02000099 RID: 153
	public interface IMediaContentClientManager : IWebService
	{
		// Token: 0x060004B7 RID: 1207
		IList<MediaContentDTO> GetMediaContentMatching(string searchText, int lucourseid = 0);

		// Token: 0x060004B8 RID: 1208
		Task<IList<MediaContentDTO>> GetMediaContentMatchingAsync(string searchText, int lucourseid = 0);

		// Token: 0x060004B9 RID: 1209
		MediaContentDTO LoadMediaContentById(Guid mediaContentId);

		// Token: 0x060004BA RID: 1210
		MediaContentDTO LoadMediaContentByIdentifier(MediaContentIdentifierDTO identifier);

		// Token: 0x060004BB RID: 1211
		MediaContentDTO LoadMediaContentByISBN(string isbn);

		// Token: 0x060004BC RID: 1212
		IList<MediaContentDTO> LoadMediaContentByCourse(int courseId);

		// Token: 0x060004BD RID: 1213
		IList<MediaContentDTO> LoadMediaContentByPublisher(int publisherId);

		// Token: 0x060004BE RID: 1214
		IList<MediaContentDTO> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory);

		// Token: 0x060004BF RID: 1215
		MediaContentIdentifierDTO CreateMediaContent(MediaContentDTO mediaContent);

		// Token: 0x060004C0 RID: 1216
		void UpdateMediaContent(MediaContentDTO mediaContent);

		// Token: 0x060004C1 RID: 1217
		bool DeleteMediaContent(MediaContentDTO mediaContent);

		// Token: 0x060004C2 RID: 1218
		IList<MediaContentDTO> GetAllMediaContent();

		// Token: 0x060004C3 RID: 1219
		MediaContentPerFormatInfoDTO GetMediaContentPerFormatInfoById(int mediaContentPerFormat);

		// Token: 0x060004C4 RID: 1220
		IList<MediaContentPerFormatInfoDTO> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId);

		// Token: 0x060004C5 RID: 1221
		MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(int mediaContentPerFormat, int studentId, bool checkIfAlreadyExists = true);

		// Token: 0x060004C6 RID: 1222
		MediaContentPerFormatStatusInfoDTO GetMediaContentPerFormatStatus(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId, bool checkIfAlreadyExists = true);

		// Token: 0x060004C7 RID: 1223
		IList<MediaContentPerFormatStatusInfoDTO> GetMediaContentPerFormatStatusList(Guid mediaContentId, int studentId);

		// Token: 0x060004C8 RID: 1224
		IList<LookupCourseBaseDTO> GetMediaContentCourses(Guid mediaContentId);

		// Token: 0x060004C9 RID: 1225
		Image GetMediaContentThumbnail(MediaContentIdentifierDTO identifier);

		// Token: 0x060004CA RID: 1226
		byte[] GetMediaContentThumbnailBytes(MediaContentIdentifierDTO identifier);

		// Token: 0x060004CB RID: 1227
		Task<byte[]> GetMediaContentThumbnailBytesAsync(MediaContentIdentifierDTO identifier);

		// Token: 0x060004CC RID: 1228
		void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail);

		// Token: 0x060004CD RID: 1229
		Image GetMediaContentCoverImage(MediaContentIdentifierDTO identifier);

		// Token: 0x060004CE RID: 1230
		byte[] GetMediaContentCoverImageBytes(MediaContentIdentifierDTO identifier);

		// Token: 0x060004CF RID: 1231
		void SetMediaContentCover(Guid mediaContentId, Image cover);
	}
}
