using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AlternativeFormat.Adapters;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.AlternativeFormat.BookSearch
{
	// Token: 0x0200015F RID: 351
	public class BookSearchManager : IBookSearchManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00074CAB File Offset: 0x00072EAB
		public BookSearchManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00074CC0 File Offset: 0x00072EC0
		public IList<EBookSearchResult> SearchForVolumes(EBookSearchRequest request)
		{
			List<EBookSearchResult> list = new List<EBookSearchResult>();
			bool flag = !request.IsValid();
			IList<EBookSearchResult> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IList<IBookSearchProvider> bookSearchProviderList = BookSearchProviderFactory.GetBookSearchProviderList(this.OpContext, request.SearchProviderType);
				foreach (IBookSearchProvider bookSearchProvider in bookSearchProviderList)
				{
					bool flag2 = !string.IsNullOrEmpty(request.Id);
					if (flag2)
					{
						EBookSearchResult volumeById = bookSearchProvider.GetVolumeById(request.Id);
						bool flag3 = volumeById != null;
						if (flag3)
						{
							return new List<EBookSearchResult>
							{
								volumeById
							};
						}
					}
					bool flag4 = !string.IsNullOrEmpty(request.ISBN);
					if (flag4)
					{
						EBookSearchResult volumeByISBN = bookSearchProvider.GetVolumeByISBN(request.ISBN);
						bool flag5 = volumeByISBN != null;
						if (flag5)
						{
							return new List<EBookSearchResult>
							{
								volumeByISBN
							};
						}
					}
					IList<EBookSearchResult> list2 = bookSearchProvider.SearchForVolumes(request);
					bool flag6 = list2 != null;
					if (flag6)
					{
						list.AddRange(list2);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00074DE8 File Offset: 0x00072FE8
		public EBookSearchResult GetVolumeByISBN(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			IList<IBookSearchProvider> bookSearchProviderList = BookSearchProviderFactory.GetBookSearchProviderList(this.OpContext, searchType);
			return (from provider in bookSearchProviderList
			select (provider != null) ? provider.GetVolumeByISBN(isbn) : null).FirstOrDefault((EBookSearchResult result) => result != null);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00074E4C File Offset: 0x0007304C
		public EBookSearchResult GetVolumeById(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			IList<IBookSearchProvider> bookSearchProviderList = BookSearchProviderFactory.GetBookSearchProviderList(this.OpContext, searchType);
			return (from provider in bookSearchProviderList
			select (provider != null) ? provider.GetVolumeById(id) : null).FirstOrDefault((EBookSearchResult result) => result != null);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00074EB0 File Offset: 0x000730B0
		[DebuggerStepThrough]
		public Task<IList<EBookSearchResult>> SearchForVolumesAsync(EBookSearchRequest request)
		{
			BookSearchManager.<SearchForVolumesAsync>d__4 <SearchForVolumesAsync>d__ = new BookSearchManager.<SearchForVolumesAsync>d__4();
			<SearchForVolumesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<EBookSearchResult>>.Create();
			<SearchForVolumesAsync>d__.<>4__this = this;
			<SearchForVolumesAsync>d__.request = request;
			<SearchForVolumesAsync>d__.<>1__state = -1;
			<SearchForVolumesAsync>d__.<>t__builder.Start<BookSearchManager.<SearchForVolumesAsync>d__4>(ref <SearchForVolumesAsync>d__);
			return <SearchForVolumesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00074EFC File Offset: 0x000730FC
		[DebuggerStepThrough]
		public Task<EBookSearchResult> GetVolumeByISBNAsync(string isbn, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			BookSearchManager.<GetVolumeByISBNAsync>d__5 <GetVolumeByISBNAsync>d__ = new BookSearchManager.<GetVolumeByISBNAsync>d__5();
			<GetVolumeByISBNAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResult>.Create();
			<GetVolumeByISBNAsync>d__.<>4__this = this;
			<GetVolumeByISBNAsync>d__.isbn = isbn;
			<GetVolumeByISBNAsync>d__.searchType = searchType;
			<GetVolumeByISBNAsync>d__.<>1__state = -1;
			<GetVolumeByISBNAsync>d__.<>t__builder.Start<BookSearchManager.<GetVolumeByISBNAsync>d__5>(ref <GetVolumeByISBNAsync>d__);
			return <GetVolumeByISBNAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00074F50 File Offset: 0x00073150
		[DebuggerStepThrough]
		public Task<EBookSearchResult> GetVolumeByIdAsync(string id, eBookSearchProviderType searchType = eBookSearchProviderType.All)
		{
			BookSearchManager.<GetVolumeByIdAsync>d__6 <GetVolumeByIdAsync>d__ = new BookSearchManager.<GetVolumeByIdAsync>d__6();
			<GetVolumeByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResult>.Create();
			<GetVolumeByIdAsync>d__.<>4__this = this;
			<GetVolumeByIdAsync>d__.id = id;
			<GetVolumeByIdAsync>d__.searchType = searchType;
			<GetVolumeByIdAsync>d__.<>1__state = -1;
			<GetVolumeByIdAsync>d__.<>t__builder.Start<BookSearchManager.<GetVolumeByIdAsync>d__6>(ref <GetVolumeByIdAsync>d__);
			return <GetVolumeByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00074FA2 File Offset: 0x000731A2
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x00074FAA File Offset: 0x000731AA
		public OperationContext OpContext { get; set; }
	}
}
