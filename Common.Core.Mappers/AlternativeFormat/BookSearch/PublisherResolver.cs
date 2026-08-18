using System;
using AutoMapper;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch
{
	// Token: 0x02000228 RID: 552
	internal class PublisherResolver : ValueResolver<EBookSearchResult, MediaPublisher>
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0002B1D8 File Offset: 0x000293D8
		protected override MediaPublisher ResolveCore(EBookSearchResult source)
		{
			MediaPublisher result;
			if (!string.IsNullOrEmpty(source.Publisher))
			{
				(result = new MediaPublisher()).Name = source.Publisher;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
