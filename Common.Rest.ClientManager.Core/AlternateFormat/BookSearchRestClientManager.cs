using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x02000085 RID: 133
	public class BookSearchRestClientManager : BearerTokenRestProxy<IBookSearchClientManager>, IBookSearchClientManager, IWebService
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x0000EEEB File Offset: 0x0000D0EB
		public BookSearchRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000EEF5 File Offset: 0x0000D0F5
		public BookSearchRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000EF00 File Offset: 0x0000D100
		public IList<EBookSearchResultDTO> SearchForVolumes(EBookSearchRequestDTO request)
		{
			return base.GetMany<EBookSearchResultDTO>(string.Format("booksearch/volumes?id='{0}'&searchtext='{1}'&isbn='{2}'&title='{3}'&author='{4}'&publisher='{5}'&maxnumberofbookstoreturn={6}", new object[]
			{
				request.Id,
				request.SearchText,
				request.ISBN,
				request.Title,
				request.Author,
				request.Publisher,
				request.MaxNumberOfBooksToReturn
			}), true);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000EF68 File Offset: 0x0000D168
		public async Task<IList<EBookSearchResultDTO>> SearchForVolumesAsync(EBookSearchRequestDTO request)
		{
			return await this.GetManyAsync<EBookSearchResultDTO>(string.Format("booksearch/volumes?id='{0}'&searchtext='{1}'&isbn='{2}'&title='{3}'&author='{4}'&publisher='{5}'&maxnumberofbookstoreturn={6}", new object[]
			{
				request.Id,
				request.SearchText,
				request.ISBN,
				request.Title,
				request.Author,
				request.Publisher,
				request.MaxNumberOfBooksToReturn
			}), true).ConfigureAwait(false);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		public EBookSearchResultDTO GetVolumeByISBN(string isbn)
		{
			return base.Get<EBookSearchResultDTO>(string.Format("isbn/{0}/{1}", isbn, eBookSearchProviderName.Google), true);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		public async Task<EBookSearchResultDTO> GetVolumeByISBNAsync(string isbn)
		{
			return await this.GetAsync<EBookSearchResultDTO>(string.Format("isbn/{0}/{1}", isbn, eBookSearchProviderName.Google), true).ConfigureAwait(false);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000F01D File Offset: 0x0000D21D
		public EBookSearchResultDTO GetVolumeById(string id)
		{
			return base.Get<EBookSearchResultDTO>(string.Format("id/{0}/{1}", id, eBookSearchProviderName.Google), true);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000F038 File Offset: 0x0000D238
		public async Task<EBookSearchResultDTO> GetVolumeByIdAsync(string id)
		{
			return await this.GetAsync<EBookSearchResultDTO>(string.Format("id/{0}/{1}", id, eBookSearchProviderName.Google), true).ConfigureAwait(false);
		}
	}
}
