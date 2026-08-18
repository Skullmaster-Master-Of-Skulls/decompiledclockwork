using System;
using AutoMapper;

namespace TechnoPro.Common.Core.GoogleBooks.Mappers
{
	// Token: 0x02000005 RID: 5
	internal class PublishedDateResolver : ValueResolver<GoogleBookSearchProvider.Item, DateTime?>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002ABC File Offset: 0x00000CBC
		protected override DateTime? ResolveCore(GoogleBookSearchProvider.Item source)
		{
			DateTime value;
			return (source.volumeInfo != null && !string.IsNullOrEmpty(source.volumeInfo.publishedDate) && DateTime.TryParse(source.volumeInfo.publishedDate, out value)) ? new DateTime?(value) : null;
		}
	}
}
