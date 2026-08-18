using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x02000098 RID: 152
	public interface IBookSearchClientManager : IWebService
	{
		// Token: 0x060004B1 RID: 1201
		IList<EBookSearchResultDTO> SearchForVolumes(EBookSearchRequestDTO request);

		// Token: 0x060004B2 RID: 1202
		Task<IList<EBookSearchResultDTO>> SearchForVolumesAsync(EBookSearchRequestDTO request);

		// Token: 0x060004B3 RID: 1203
		EBookSearchResultDTO GetVolumeByISBN(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All);

		// Token: 0x060004B4 RID: 1204
		Task<EBookSearchResultDTO> GetVolumeByISBNAsync(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All);

		// Token: 0x060004B5 RID: 1205
		EBookSearchResultDTO GetVolumeById(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All);

		// Token: 0x060004B6 RID: 1206
		Task<EBookSearchResultDTO> GetVolumeByIdAsync(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All);
	}
}
