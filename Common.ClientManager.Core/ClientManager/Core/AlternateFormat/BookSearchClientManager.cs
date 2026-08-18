using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Core.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200009C RID: 156
	public class BookSearchClientManager : IBookSearchClientManager, IWebService
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00019934 File Offset: 0x00017B34
		public IList<EBookSearchResultDTO> SearchForVolumes(EBookSearchRequestDTO bRequest)
		{
			List<EBookSearchResultDTO> list = new List<EBookSearchResultDTO>();
			SearchForVolumesReq searchForVolumesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SearchForVolumesReq>();
			bool flag = bRequest.SearchProviderType.HasFlag(eBookSearchProviderType.ExternalOnly);
			if (flag)
			{
				IBookSearchManager bookSearchManager = new BookSearchManager(new OperationContext
				{
					TenantId = searchForVolumesReq.TenantId,
					AppContext = searchForVolumesReq.ApplicationContext,
					WhoAmI = searchForVolumesReq.WhoAmI
				});
				EBookSearchRequest ebookSearchRequest = bRequest.ToDomainObject();
				ebookSearchRequest.SearchProviderType = eBookSearchProviderType.ExternalOnly;
				list.AddRange(bookSearchManager.SearchForVolumes(ebookSearchRequest).ToDTO());
			}
			bool flag2 = bRequest.SearchProviderType.HasFlag(eBookSearchProviderType.LocalOnly);
			if (flag2)
			{
				bRequest.SearchProviderType = eBookSearchProviderType.LocalOnly;
				searchForVolumesReq.BookSearchRequest = bRequest;
				list.AddRange(ClientServiceFactory.GetClientInstance<IBookSearch>().SearchForVolumes(searchForVolumesReq).BookSearchResult);
			}
			return list;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00019A14 File Offset: 0x00017C14
		[DebuggerStepThrough]
		public Task<IList<EBookSearchResultDTO>> SearchForVolumesAsync(EBookSearchRequestDTO bRequest)
		{
			BookSearchClientManager.<SearchForVolumesAsync>d__1 <SearchForVolumesAsync>d__ = new BookSearchClientManager.<SearchForVolumesAsync>d__1();
			<SearchForVolumesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<EBookSearchResultDTO>>.Create();
			<SearchForVolumesAsync>d__.<>4__this = this;
			<SearchForVolumesAsync>d__.bRequest = bRequest;
			<SearchForVolumesAsync>d__.<>1__state = -1;
			<SearchForVolumesAsync>d__.<>t__builder.Start<BookSearchClientManager.<SearchForVolumesAsync>d__1>(ref <SearchForVolumesAsync>d__);
			return <SearchForVolumesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00019A60 File Offset: 0x00017C60
		public EBookSearchResultDTO GetVolumeByISBN(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			GetVolumeByISBNReq getVolumeByISBNReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetVolumeByISBNReq>();
			bool flag = searchType.HasFlag(eBookSearchProviderType.ExternalOnly);
			if (flag)
			{
				IBookSearchManager bookSearchManager = new BookSearchManager(new OperationContext
				{
					TenantId = getVolumeByISBNReq.TenantId,
					AppContext = getVolumeByISBNReq.ApplicationContext,
					WhoAmI = getVolumeByISBNReq.WhoAmI
				});
				EBookSearchResult volumeByISBN = bookSearchManager.GetVolumeByISBN(isbn, eBookSearchProviderType.ExternalOnly);
				bool flag2 = volumeByISBN != null;
				if (flag2)
				{
					return volumeByISBN.ToDTO();
				}
			}
			bool flag3 = searchType.HasFlag(eBookSearchProviderType.LocalOnly);
			EBookSearchResultDTO result;
			if (flag3)
			{
				getVolumeByISBNReq.ISBN = isbn;
				getVolumeByISBNReq.SearchType = eBookSearchProviderType.LocalOnly;
				result = ClientServiceFactory.GetClientInstance<IBookSearch>().GetVolumeByISBN(getVolumeByISBNReq).BookSearchResult;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019B24 File Offset: 0x00017D24
		[DebuggerStepThrough]
		public Task<EBookSearchResultDTO> GetVolumeByISBNAsync(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			BookSearchClientManager.<GetVolumeByISBNAsync>d__3 <GetVolumeByISBNAsync>d__ = new BookSearchClientManager.<GetVolumeByISBNAsync>d__3();
			<GetVolumeByISBNAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResultDTO>.Create();
			<GetVolumeByISBNAsync>d__.<>4__this = this;
			<GetVolumeByISBNAsync>d__.isbn = isbn;
			<GetVolumeByISBNAsync>d__.searchType = searchType;
			<GetVolumeByISBNAsync>d__.<>1__state = -1;
			<GetVolumeByISBNAsync>d__.<>t__builder.Start<BookSearchClientManager.<GetVolumeByISBNAsync>d__3>(ref <GetVolumeByISBNAsync>d__);
			return <GetVolumeByISBNAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00019B78 File Offset: 0x00017D78
		public EBookSearchResultDTO GetVolumeById(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			GetVolumeByIdReq getVolumeByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetVolumeByIdReq>();
			bool flag = searchType.HasFlag(eBookSearchProviderType.ExternalOnly);
			if (flag)
			{
				IBookSearchManager bookSearchManager = new BookSearchManager(new OperationContext
				{
					TenantId = getVolumeByIdReq.TenantId,
					AppContext = getVolumeByIdReq.ApplicationContext,
					WhoAmI = getVolumeByIdReq.WhoAmI
				});
				EBookSearchResult volumeById = bookSearchManager.GetVolumeById(id, eBookSearchProviderType.ExternalOnly);
				bool flag2 = volumeById != null;
				if (flag2)
				{
					return volumeById.ToDTO();
				}
			}
			bool flag3 = searchType.HasFlag(eBookSearchProviderType.LocalOnly);
			EBookSearchResultDTO result;
			if (flag3)
			{
				getVolumeByIdReq.Id = id;
				getVolumeByIdReq.SearchType = searchType;
				result = ClientServiceFactory.GetClientInstance<IBookSearch>().GetVolumeById(getVolumeByIdReq).BookSearchResult;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019C3C File Offset: 0x00017E3C
		[DebuggerStepThrough]
		public Task<EBookSearchResultDTO> GetVolumeByIdAsync(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			BookSearchClientManager.<GetVolumeByIdAsync>d__5 <GetVolumeByIdAsync>d__ = new BookSearchClientManager.<GetVolumeByIdAsync>d__5();
			<GetVolumeByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResultDTO>.Create();
			<GetVolumeByIdAsync>d__.<>4__this = this;
			<GetVolumeByIdAsync>d__.id = id;
			<GetVolumeByIdAsync>d__.searchType = searchType;
			<GetVolumeByIdAsync>d__.<>1__state = -1;
			<GetVolumeByIdAsync>d__.<>t__builder.Start<BookSearchClientManager.<GetVolumeByIdAsync>d__5>(ref <GetVolumeByIdAsync>d__);
			return <GetVolumeByIdAsync>d__.<>t__builder.Task;
		}
	}
}
