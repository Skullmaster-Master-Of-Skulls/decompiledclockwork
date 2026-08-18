using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000EE RID: 238
	public interface IMediaContentFileManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000777 RID: 1911
		MediaContentFileWithoutData CreateMediaContentFileInfo(MediaContentFileWithoutData fileInfo);

		// Token: 0x06000778 RID: 1912
		Task<MediaContentFileWithoutData> CreateMediaContentFileInfoAsync(MediaContentFileWithoutData fileInfo);

		// Token: 0x06000779 RID: 1913
		void UpdateMediaContentFileWithoutData(MediaContentFileWithoutData mediaContentFile);

		// Token: 0x0600077A RID: 1914
		Task DeleteMediaContentFileAsync(FileIdentifier fileId);

		// Token: 0x0600077B RID: 1915
		int GetCountAvailableAlternateFormatFiles(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x0600077C RID: 1916
		IList<MediaContentFileWithoutData> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0);

		// Token: 0x0600077D RID: 1917
		IList<StudentMediaContentFileWithProofOfPurchaseInfo> LoadMediaContentFileByStudentId(int studentId);

		// Token: 0x0600077E RID: 1918
		Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate);

		// Token: 0x0600077F RID: 1919
		IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x06000780 RID: 1920
		Task<IList<MediaContentFileWithoutData>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x06000781 RID: 1921
		IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0);

		// Token: 0x06000782 RID: 1922
		IList<MediaContentFileWithoutData> GetMediaContentFileMatching(string searchText, int lucourseid = 0);

		// Token: 0x06000783 RID: 1923
		Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate);
	}
}
