using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClockWorkLogger;
using Common.Web;
using TechnoPro.Common.Core.GoogleBooks.Mappers;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.GoogleBooks
{
	// Token: 0x02000002 RID: 2
	public class GoogleBookSearchProvider : IBookSearchProvider, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public OperationContext OpContext { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		private string GoogleBooksAPIUrl { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public GoogleBookSearchProvider(OperationContext opContext)
		{
			this.OpContext = this.OpContext;
			this.GoogleBooksAPIUrl = "https://www.googleapis.com/books/v1/volumes";
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002095 File Offset: 0x00000295
		public eBookSearchProviderType SearchProviderType
		{
			get
			{
				return eBookSearchProviderType.ExternalOnly;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public IList<EBookSearchResult> SearchForVolumes(EBookSearchRequest request)
		{
			List<EBookSearchResult> list = new List<EBookSearchResult>();
			IList<EBookSearchResult> result;
			try
			{
				string queryStringFormRequest = this.GetQueryStringFormRequest(request);
				bool flag = string.IsNullOrEmpty(queryStringFormRequest);
				if (flag)
				{
					result = list;
				}
				else
				{
					string uri = (request.MaxNumberOfBooksToReturn > 0) ? string.Format("{0}?{1}&printType=books&maxResults={2}", this.GoogleBooksAPIUrl, queryStringFormRequest, Math.Min(request.MaxNumberOfBooksToReturn, 40)) : (this.GoogleBooksAPIUrl + "?" + queryStringFormRequest + "&printType=books");
					GoogleBookSearchProvider.RootObject objectFromWeb = uri.GetObjectFromWeb<GoogleBookSearchProvider.RootObject>();
					bool flag2 = ((objectFromWeb != null) ? objectFromWeb.items : null) != null;
					if (flag2)
					{
						list.AddRange(from item in objectFromWeb.items
						select item.ToBookSearchResult() into book
						where book != null
						select book);
					}
					result = list;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("GoogleBookSearchProvider::SearchForVolumes: {0}", ex.ToString()), ex);
				result = list;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		public EBookSearchResult GetVolumeByISBN(string isbn)
		{
			EBookSearchResult result;
			try
			{
				string uri = this.GoogleBooksAPIUrl + "?q=isbn:" + isbn + "&printType=books";
				GoogleBookSearchProvider.RootObject objectFromWeb = uri.GetObjectFromWeb<GoogleBookSearchProvider.RootObject>();
				EBookSearchResult ebookSearchResult;
				if (objectFromWeb == null)
				{
					ebookSearchResult = null;
				}
				else
				{
					List<GoogleBookSearchProvider.Item> items = objectFromWeb.items;
					if (items == null)
					{
						ebookSearchResult = null;
					}
					else
					{
						ebookSearchResult = (from item in items
						select item.ToBookSearchResult()).FirstOrDefault((EBookSearchResult b) => b != null);
					}
				}
				EBookSearchResult ebookSearchResult2 = ebookSearchResult;
				bool flag = ebookSearchResult2 != null && string.IsNullOrEmpty(ebookSearchResult2.ISBN);
				if (flag)
				{
					ebookSearchResult2.ISBN = isbn;
				}
				result = ebookSearchResult2;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("GoogleBookSearchProvider::GetVolumenByISBN: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022A0 File Offset: 0x000004A0
		public EBookSearchResult GetVolumeById(string id)
		{
			EBookSearchResult result;
			try
			{
				string uri = this.GoogleBooksAPIUrl + "/" + id;
				GoogleBookSearchProvider.Item objectFromWeb = uri.GetObjectFromWeb<GoogleBookSearchProvider.Item>();
				result = ((objectFromWeb != null) ? objectFromWeb.ToBookSearchResult() : null);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("GoogleBookSearchProvider::GetVolumenById: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000230C File Offset: 0x0000050C
		[DebuggerStepThrough]
		public Task<IList<EBookSearchResult>> SearchForVolumesAsync(EBookSearchRequest request)
		{
			GoogleBookSearchProvider.<SearchForVolumesAsync>d__14 <SearchForVolumesAsync>d__ = new GoogleBookSearchProvider.<SearchForVolumesAsync>d__14();
			<SearchForVolumesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<EBookSearchResult>>.Create();
			<SearchForVolumesAsync>d__.<>4__this = this;
			<SearchForVolumesAsync>d__.request = request;
			<SearchForVolumesAsync>d__.<>1__state = -1;
			<SearchForVolumesAsync>d__.<>t__builder.Start<GoogleBookSearchProvider.<SearchForVolumesAsync>d__14>(ref <SearchForVolumesAsync>d__);
			return <SearchForVolumesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002358 File Offset: 0x00000558
		[DebuggerStepThrough]
		public Task<EBookSearchResult> GetVolumeByISBNAsync(string isbn)
		{
			GoogleBookSearchProvider.<GetVolumeByISBNAsync>d__15 <GetVolumeByISBNAsync>d__ = new GoogleBookSearchProvider.<GetVolumeByISBNAsync>d__15();
			<GetVolumeByISBNAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResult>.Create();
			<GetVolumeByISBNAsync>d__.<>4__this = this;
			<GetVolumeByISBNAsync>d__.isbn = isbn;
			<GetVolumeByISBNAsync>d__.<>1__state = -1;
			<GetVolumeByISBNAsync>d__.<>t__builder.Start<GoogleBookSearchProvider.<GetVolumeByISBNAsync>d__15>(ref <GetVolumeByISBNAsync>d__);
			return <GetVolumeByISBNAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023A4 File Offset: 0x000005A4
		[DebuggerStepThrough]
		public Task<EBookSearchResult> GetVolumeByIdAsync(string id)
		{
			GoogleBookSearchProvider.<GetVolumeByIdAsync>d__16 <GetVolumeByIdAsync>d__ = new GoogleBookSearchProvider.<GetVolumeByIdAsync>d__16();
			<GetVolumeByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResult>.Create();
			<GetVolumeByIdAsync>d__.<>4__this = this;
			<GetVolumeByIdAsync>d__.id = id;
			<GetVolumeByIdAsync>d__.<>1__state = -1;
			<GetVolumeByIdAsync>d__.<>t__builder.Start<GoogleBookSearchProvider.<GetVolumeByIdAsync>d__16>(ref <GetVolumeByIdAsync>d__);
			return <GetVolumeByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023F0 File Offset: 0x000005F0
		private string GetQueryStringFormRequest(EBookSearchRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("q=");
			bool flag = false;
			bool flag2 = !string.IsNullOrEmpty(request.SearchText);
			if (flag2)
			{
				stringBuilder.Append(request.SearchText);
				flag = true;
			}
			bool flag3 = !string.IsNullOrEmpty(request.ISBN);
			if (flag3)
			{
				stringBuilder.AppendFormat(flag ? "+isbn:{0}" : "isbn:{0}", request.ISBN);
				flag = true;
			}
			bool flag4 = !string.IsNullOrEmpty(request.Title);
			if (flag4)
			{
				stringBuilder.AppendFormat(flag ? "+intitle:{0}" : "intitle:{0}", request.Title);
				flag = true;
			}
			bool flag5 = !string.IsNullOrEmpty(request.Author);
			if (flag5)
			{
				stringBuilder.AppendFormat(flag ? "+inauthor:{0}" : "inauthor:{0}", request.Author);
				flag = true;
			}
			bool flag6 = !string.IsNullOrEmpty(request.Publisher);
			if (flag6)
			{
				stringBuilder.AppendFormat(flag ? "+inpublisher:{0}" : "inpublisher:{0}", request.Publisher);
				flag = true;
			}
			return flag ? stringBuilder.ToString() : string.Empty;
		}

		// Token: 0x02000006 RID: 6
		internal class IndustryIdentifier
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000015 RID: 21 RVA: 0x00002B16 File Offset: 0x00000D16
			// (set) Token: 0x06000016 RID: 22 RVA: 0x00002B1E File Offset: 0x00000D1E
			public string type { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000017 RID: 23 RVA: 0x00002B27 File Offset: 0x00000D27
			// (set) Token: 0x06000018 RID: 24 RVA: 0x00002B2F File Offset: 0x00000D2F
			public string identifier { get; set; }
		}

		// Token: 0x02000007 RID: 7
		internal class ImageLinks
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001A RID: 26 RVA: 0x00002B41 File Offset: 0x00000D41
			// (set) Token: 0x0600001B RID: 27 RVA: 0x00002B49 File Offset: 0x00000D49
			public string smallThumbnail { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B52 File Offset: 0x00000D52
			// (set) Token: 0x0600001D RID: 29 RVA: 0x00002B5A File Offset: 0x00000D5A
			public string thumbnail { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002B63 File Offset: 0x00000D63
			// (set) Token: 0x0600001F RID: 31 RVA: 0x00002B6B File Offset: 0x00000D6B
			public string small { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002B74 File Offset: 0x00000D74
			// (set) Token: 0x06000021 RID: 33 RVA: 0x00002B7C File Offset: 0x00000D7C
			public string medium { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00002B85 File Offset: 0x00000D85
			// (set) Token: 0x06000023 RID: 35 RVA: 0x00002B8D File Offset: 0x00000D8D
			public string large { get; set; }
		}

		// Token: 0x02000008 RID: 8
		internal class VolumeInfo
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000025 RID: 37 RVA: 0x00002B96 File Offset: 0x00000D96
			// (set) Token: 0x06000026 RID: 38 RVA: 0x00002B9E File Offset: 0x00000D9E
			public string title { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000027 RID: 39 RVA: 0x00002BA7 File Offset: 0x00000DA7
			// (set) Token: 0x06000028 RID: 40 RVA: 0x00002BAF File Offset: 0x00000DAF
			public List<string> authors { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BB8 File Offset: 0x00000DB8
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002BC0 File Offset: 0x00000DC0
			public string publisher { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002BC9 File Offset: 0x00000DC9
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002BD1 File Offset: 0x00000DD1
			public string publishedDate { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002BDA File Offset: 0x00000DDA
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002BE2 File Offset: 0x00000DE2
			public string description { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00002BEB File Offset: 0x00000DEB
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00002BF3 File Offset: 0x00000DF3
			public List<GoogleBookSearchProvider.IndustryIdentifier> industryIdentifiers { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002BFC File Offset: 0x00000DFC
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00002C04 File Offset: 0x00000E04
			public string printType { get; set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00002C0D File Offset: 0x00000E0D
			// (set) Token: 0x06000034 RID: 52 RVA: 0x00002C15 File Offset: 0x00000E15
			public List<string> categories { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00002C1E File Offset: 0x00000E1E
			// (set) Token: 0x06000036 RID: 54 RVA: 0x00002C26 File Offset: 0x00000E26
			public double averageRating { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000037 RID: 55 RVA: 0x00002C2F File Offset: 0x00000E2F
			// (set) Token: 0x06000038 RID: 56 RVA: 0x00002C37 File Offset: 0x00000E37
			public int ratingsCount { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C40 File Offset: 0x00000E40
			// (set) Token: 0x0600003A RID: 58 RVA: 0x00002C48 File Offset: 0x00000E48
			public string contentVersion { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C51 File Offset: 0x00000E51
			// (set) Token: 0x0600003C RID: 60 RVA: 0x00002C59 File Offset: 0x00000E59
			public GoogleBookSearchProvider.ImageLinks imageLinks { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600003D RID: 61 RVA: 0x00002C62 File Offset: 0x00000E62
			// (set) Token: 0x0600003E RID: 62 RVA: 0x00002C6A File Offset: 0x00000E6A
			public string language { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C73 File Offset: 0x00000E73
			// (set) Token: 0x06000040 RID: 64 RVA: 0x00002C7B File Offset: 0x00000E7B
			public string previewLink { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C84 File Offset: 0x00000E84
			// (set) Token: 0x06000042 RID: 66 RVA: 0x00002C8C File Offset: 0x00000E8C
			public string infoLink { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C95 File Offset: 0x00000E95
			// (set) Token: 0x06000044 RID: 68 RVA: 0x00002C9D File Offset: 0x00000E9D
			public string canonicalVolumeLink { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CA6 File Offset: 0x00000EA6
			// (set) Token: 0x06000046 RID: 70 RVA: 0x00002CAE File Offset: 0x00000EAE
			public int pageCount { get; set; }
		}

		// Token: 0x02000009 RID: 9
		internal class SaleInfo
		{
			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000048 RID: 72 RVA: 0x00002CB7 File Offset: 0x00000EB7
			// (set) Token: 0x06000049 RID: 73 RVA: 0x00002CBF File Offset: 0x00000EBF
			public string country { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600004A RID: 74 RVA: 0x00002CC8 File Offset: 0x00000EC8
			// (set) Token: 0x0600004B RID: 75 RVA: 0x00002CD0 File Offset: 0x00000ED0
			public string saleability { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CD9 File Offset: 0x00000ED9
			// (set) Token: 0x0600004D RID: 77 RVA: 0x00002CE1 File Offset: 0x00000EE1
			public bool isEbook { get; set; }
		}

		// Token: 0x0200000A RID: 10
		internal class Epub
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600004F RID: 79 RVA: 0x00002CEA File Offset: 0x00000EEA
			// (set) Token: 0x06000050 RID: 80 RVA: 0x00002CF2 File Offset: 0x00000EF2
			public bool isAvailable { get; set; }
		}

		// Token: 0x0200000B RID: 11
		internal class Pdf
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00002CFB File Offset: 0x00000EFB
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D03 File Offset: 0x00000F03
			public bool isAvailable { get; set; }
		}

		// Token: 0x0200000C RID: 12
		internal class AccessInfo
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D0C File Offset: 0x00000F0C
			// (set) Token: 0x06000056 RID: 86 RVA: 0x00002D14 File Offset: 0x00000F14
			public string country { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00002D1D File Offset: 0x00000F1D
			// (set) Token: 0x06000058 RID: 88 RVA: 0x00002D25 File Offset: 0x00000F25
			public string viewability { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D2E File Offset: 0x00000F2E
			// (set) Token: 0x0600005A RID: 90 RVA: 0x00002D36 File Offset: 0x00000F36
			public bool embeddable { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D3F File Offset: 0x00000F3F
			// (set) Token: 0x0600005C RID: 92 RVA: 0x00002D47 File Offset: 0x00000F47
			public bool publicDomain { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D50 File Offset: 0x00000F50
			// (set) Token: 0x0600005E RID: 94 RVA: 0x00002D58 File Offset: 0x00000F58
			public string textToSpeechPermission { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D61 File Offset: 0x00000F61
			// (set) Token: 0x06000060 RID: 96 RVA: 0x00002D69 File Offset: 0x00000F69
			public GoogleBookSearchProvider.Epub epub { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D72 File Offset: 0x00000F72
			// (set) Token: 0x06000062 RID: 98 RVA: 0x00002D7A File Offset: 0x00000F7A
			public GoogleBookSearchProvider.Pdf pdf { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D83 File Offset: 0x00000F83
			// (set) Token: 0x06000064 RID: 100 RVA: 0x00002D8B File Offset: 0x00000F8B
			public string webReaderLink { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000065 RID: 101 RVA: 0x00002D94 File Offset: 0x00000F94
			// (set) Token: 0x06000066 RID: 102 RVA: 0x00002D9C File Offset: 0x00000F9C
			public string accessViewStatus { get; set; }
		}

		// Token: 0x0200000D RID: 13
		internal class SearchInfo
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x06000068 RID: 104 RVA: 0x00002DA5 File Offset: 0x00000FA5
			// (set) Token: 0x06000069 RID: 105 RVA: 0x00002DAD File Offset: 0x00000FAD
			public string textSnippet { get; set; }
		}

		// Token: 0x0200000E RID: 14
		internal class Item
		{
			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600006B RID: 107 RVA: 0x00002DB6 File Offset: 0x00000FB6
			// (set) Token: 0x0600006C RID: 108 RVA: 0x00002DBE File Offset: 0x00000FBE
			public string kind { get; set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600006D RID: 109 RVA: 0x00002DC7 File Offset: 0x00000FC7
			// (set) Token: 0x0600006E RID: 110 RVA: 0x00002DCF File Offset: 0x00000FCF
			public string id { get; set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600006F RID: 111 RVA: 0x00002DD8 File Offset: 0x00000FD8
			// (set) Token: 0x06000070 RID: 112 RVA: 0x00002DE0 File Offset: 0x00000FE0
			public string etag { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000071 RID: 113 RVA: 0x00002DE9 File Offset: 0x00000FE9
			// (set) Token: 0x06000072 RID: 114 RVA: 0x00002DF1 File Offset: 0x00000FF1
			public string selfLink { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000073 RID: 115 RVA: 0x00002DFA File Offset: 0x00000FFA
			// (set) Token: 0x06000074 RID: 116 RVA: 0x00002E02 File Offset: 0x00001002
			public GoogleBookSearchProvider.VolumeInfo volumeInfo { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000075 RID: 117 RVA: 0x00002E0B File Offset: 0x0000100B
			// (set) Token: 0x06000076 RID: 118 RVA: 0x00002E13 File Offset: 0x00001013
			public GoogleBookSearchProvider.SaleInfo saleInfo { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000077 RID: 119 RVA: 0x00002E1C File Offset: 0x0000101C
			// (set) Token: 0x06000078 RID: 120 RVA: 0x00002E24 File Offset: 0x00001024
			public GoogleBookSearchProvider.AccessInfo accessInfo { get; set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00002E2D File Offset: 0x0000102D
			// (set) Token: 0x0600007A RID: 122 RVA: 0x00002E35 File Offset: 0x00001035
			public GoogleBookSearchProvider.SearchInfo searchInfo { get; set; }
		}

		// Token: 0x0200000F RID: 15
		internal class RootObject
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600007C RID: 124 RVA: 0x00002E3E File Offset: 0x0000103E
			// (set) Token: 0x0600007D RID: 125 RVA: 0x00002E46 File Offset: 0x00001046
			public string kind { get; set; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600007E RID: 126 RVA: 0x00002E4F File Offset: 0x0000104F
			// (set) Token: 0x0600007F RID: 127 RVA: 0x00002E57 File Offset: 0x00001057
			public int totalItems { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000080 RID: 128 RVA: 0x00002E60 File Offset: 0x00001060
			// (set) Token: 0x06000081 RID: 129 RVA: 0x00002E68 File Offset: 0x00001068
			public List<GoogleBookSearchProvider.Item> items { get; set; }
		}
	}
}
