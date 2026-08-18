using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.ICore.AlternativeFormat.BookSearch
{
	// Token: 0x020000F6 RID: 246
	public interface IBookSearchManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000806 RID: 2054
		IList<EBookSearchResult> SearchForVolumes(EBookSearchRequest request);

		// Token: 0x06000807 RID: 2055
		EBookSearchResult GetVolumeByISBN(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All);

		// Token: 0x06000808 RID: 2056
		EBookSearchResult GetVolumeById(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All);

		// Token: 0x06000809 RID: 2057
		Task<IList<EBookSearchResult>> SearchForVolumesAsync(EBookSearchRequest request);

		// Token: 0x0600080A RID: 2058
		Task<EBookSearchResult> GetVolumeByISBNAsync(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All);

		// Token: 0x0600080B RID: 2059
		Task<EBookSearchResult> GetVolumeByIdAsync(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All);
	}
}
