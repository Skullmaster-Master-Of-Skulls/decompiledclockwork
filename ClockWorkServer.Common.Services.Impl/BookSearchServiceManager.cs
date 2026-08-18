using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000003 RID: 3
	public class BookSearchServiceManager : IBookSearch, IService
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public SearchForVolumesResp SearchForVolumes(SearchForVolumesReq request)
		{
			IBookSearchManager bookSearchManager = new BookSearchManager(request.GetOperationContext());
			return new SearchForVolumesResp
			{
				BookSearchResult = bookSearchManager.SearchForVolumes(request.BookSearchRequest.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public GetVolumeByISBNResp GetVolumeByISBN(GetVolumeByISBNReq request)
		{
			IBookSearchManager bookSearchManager = new BookSearchManager(request.GetOperationContext());
			return new GetVolumeByISBNResp
			{
				BookSearchResult = bookSearchManager.GetVolumeByISBN(request.ISBN, request.SearchType).ToDTO()
			};
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		public GetVolumeByIdResp GetVolumeById(GetVolumeByIdReq request)
		{
			IBookSearchManager bookSearchManager = new BookSearchManager(request.GetOperationContext());
			return new GetVolumeByIdResp
			{
				BookSearchResult = bookSearchManager.GetVolumeById(request.Id, request.SearchType).ToDTO()
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000219C File Offset: 0x0000039C
		[DebuggerStepThrough]
		public Task<SearchForVolumesResp> SearchForVolumesAsync(SearchForVolumesReq request)
		{
			BookSearchServiceManager.<SearchForVolumesAsync>d__3 <SearchForVolumesAsync>d__ = new BookSearchServiceManager.<SearchForVolumesAsync>d__3();
			<SearchForVolumesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SearchForVolumesResp>.Create();
			<SearchForVolumesAsync>d__.<>4__this = this;
			<SearchForVolumesAsync>d__.request = request;
			<SearchForVolumesAsync>d__.<>1__state = -1;
			<SearchForVolumesAsync>d__.<>t__builder.Start<BookSearchServiceManager.<SearchForVolumesAsync>d__3>(ref <SearchForVolumesAsync>d__);
			return <SearchForVolumesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
		[DebuggerStepThrough]
		public Task<GetVolumeByISBNResp> GetVolumeByISBNAsync(GetVolumeByISBNReq request)
		{
			BookSearchServiceManager.<GetVolumeByISBNAsync>d__4 <GetVolumeByISBNAsync>d__ = new BookSearchServiceManager.<GetVolumeByISBNAsync>d__4();
			<GetVolumeByISBNAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetVolumeByISBNResp>.Create();
			<GetVolumeByISBNAsync>d__.<>4__this = this;
			<GetVolumeByISBNAsync>d__.request = request;
			<GetVolumeByISBNAsync>d__.<>1__state = -1;
			<GetVolumeByISBNAsync>d__.<>t__builder.Start<BookSearchServiceManager.<GetVolumeByISBNAsync>d__4>(ref <GetVolumeByISBNAsync>d__);
			return <GetVolumeByISBNAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002234 File Offset: 0x00000434
		[DebuggerStepThrough]
		public Task<GetVolumeByIdResp> GetVolumeByIdAsync(GetVolumeByIdReq request)
		{
			BookSearchServiceManager.<GetVolumeByIdAsync>d__5 <GetVolumeByIdAsync>d__ = new BookSearchServiceManager.<GetVolumeByIdAsync>d__5();
			<GetVolumeByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetVolumeByIdResp>.Create();
			<GetVolumeByIdAsync>d__.<>4__this = this;
			<GetVolumeByIdAsync>d__.request = request;
			<GetVolumeByIdAsync>d__.<>1__state = -1;
			<GetVolumeByIdAsync>d__.<>t__builder.Start<BookSearchServiceManager.<GetVolumeByIdAsync>d__5>(ref <GetVolumeByIdAsync>d__);
			return <GetVolumeByIdAsync>d__.<>t__builder.Task;
		}
	}
}
