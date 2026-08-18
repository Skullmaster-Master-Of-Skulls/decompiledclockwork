using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch
{
	// Token: 0x02000226 RID: 550
	internal class PublisherDTOResolver : ValueResolver<EBookSearchResultDTO, MediaPublisherDTO>
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		protected override MediaPublisherDTO ResolveCore(EBookSearchResultDTO source)
		{
			MediaPublisherDTO result;
			if (!string.IsNullOrEmpty(source.Publisher))
			{
				(result = new MediaPublisherDTO()).Name = source.Publisher;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
