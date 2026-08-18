using System;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.AlternativeFormat.Adapters
{
	// Token: 0x02000163 RID: 355
	public static class EBookSearchRequestAdapter
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x000756B8 File Offset: 0x000738B8
		public static bool IsValid(this EBookSearchRequest request)
		{
			return !string.IsNullOrEmpty(request.Id) || !string.IsNullOrEmpty(request.SearchText) || !string.IsNullOrEmpty(request.Title) || !string.IsNullOrEmpty(request.ISBN) || !string.IsNullOrEmpty(request.Author) || !string.IsNullOrEmpty(request.Publisher);
		}
	}
}
