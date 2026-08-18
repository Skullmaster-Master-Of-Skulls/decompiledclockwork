using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x0200009A RID: 154
	public interface IMediaContentFileClientManager : IWebService
	{
		// Token: 0x060004D0 RID: 1232
		MediaContentFileWithoutDataDTO CreateMediaContentFileInfo(MediaContentFileWithoutDataDTO fileInfo);

		// Token: 0x060004D1 RID: 1233
		Task<MediaContentFileWithoutDataDTO> CreateMediaContentFileInfoAsync(MediaContentFileWithoutDataDTO fileInfo);

		// Token: 0x060004D2 RID: 1234
		IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0);

		// Token: 0x060004D3 RID: 1235
		IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> LoadMediaContentFileByStudentId(int studentId);

		// Token: 0x060004D4 RID: 1236
		void UpdateMediaContentFileWithoutData(MediaContentFileWithoutDataDTO mediaContentFile);

		// Token: 0x060004D5 RID: 1237
		Task DeleteMediaContentFileAsync(FileIdentifierDTO mediaContentFileId);

		// Token: 0x060004D6 RID: 1238
		IList<MediaContentFileWithoutDataDTO> GetMediaContentFileMatching(string searchText, int lucourseid = 0);

		// Token: 0x060004D7 RID: 1239
		IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x060004D8 RID: 1240
		Task<IList<MediaContentFileWithoutDataDTO>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x060004D9 RID: 1241
		IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0);

		// Token: 0x060004DA RID: 1242
		Task<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate);

		// Token: 0x060004DB RID: 1243
		Task<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate);
	}
}
