using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.ICore.AlternativeFormat.BookSearch
{
	// Token: 0x020000F7 RID: 247
	public interface IBookSearchProvider : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600080C RID: 2060
		IList<EBookSearchResult> SearchForVolumes(EBookSearchRequest request);

		// Token: 0x0600080D RID: 2061
		EBookSearchResult GetVolumeByISBN(string isbn);

		// Token: 0x0600080E RID: 2062
		EBookSearchResult GetVolumeById(string id);

		// Token: 0x0600080F RID: 2063
		Task<IList<EBookSearchResult>> SearchForVolumesAsync(EBookSearchRequest request);

		// Token: 0x06000810 RID: 2064
		Task<EBookSearchResult> GetVolumeByISBNAsync(string isbn);

		// Token: 0x06000811 RID: 2065
		Task<EBookSearchResult> GetVolumeByIdAsync(string id);

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000812 RID: 2066
		eBookSearchProviderType SearchProviderType { get; }
	}
}
