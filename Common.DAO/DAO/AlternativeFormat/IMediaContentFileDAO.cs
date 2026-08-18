using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000C8 RID: 200
	public interface IMediaContentFileDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600057D RID: 1405
		MediaContentFileWithoutData CreateMediaContentFileInfo(MediaContentFileWithoutData fileInfo);

		// Token: 0x0600057E RID: 1406
		Task<MediaContentFileWithoutData> CreateMediaContentFileInfoAsync(MediaContentFileWithoutData fileInfo);

		// Token: 0x0600057F RID: 1407
		void UpdateMediaContentFileWithoutData(MediaContentFileWithoutData mediaContentFile);

		// Token: 0x06000580 RID: 1408
		void DeleteMediaContentFile(FileIdentifier fileId);

		// Token: 0x06000581 RID: 1409
		Task DeleteMediaContentFileAsync(FileIdentifier fileId);

		// Token: 0x06000582 RID: 1410
		IList<MediaContentFileWithoutData> GetMediaContentFileMatchingUsingEquivalentCoursesAlt(string searchText, int lucourseid = 0);

		// Token: 0x06000583 RID: 1411
		IList<MediaContentFileWithoutData> GetMediaContentFileMatchingUsingUserDefinedEquivalentCoursesAlt(string searchText, int lucourseid = 0);

		// Token: 0x06000584 RID: 1412
		IList<MediaContentFileWithoutData> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0);

		// Token: 0x06000585 RID: 1413
		IList<StudentMediaContentFileWithProofOfPurchaseInfo> LoadMediaContentFileByStudentId(int studentId);

		// Token: 0x06000586 RID: 1414
		Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate);

		// Token: 0x06000587 RID: 1415
		Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate);

		// Token: 0x06000588 RID: 1416
		IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x06000589 RID: 1417
		Task<IList<MediaContentFileWithoutData>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x0600058A RID: 1418
		IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0);

		// Token: 0x0600058B RID: 1419
		int GetCountAvailableAlternateFormatFiles(int mediaContentPerFormatId, int studentId = 0);
	}
}
