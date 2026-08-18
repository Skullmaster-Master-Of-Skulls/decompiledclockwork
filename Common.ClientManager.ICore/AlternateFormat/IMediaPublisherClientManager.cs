using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x0200009D RID: 157
	public interface IMediaPublisherClientManager : IWebService
	{
		// Token: 0x060004FD RID: 1277
		int CreatePublisher(MediaPublisherDTO publisher);

		// Token: 0x060004FE RID: 1278
		bool UpdatePublisher(MediaPublisherDTO publisher);

		// Token: 0x060004FF RID: 1279
		bool DeletePublisher(int publisherId);

		// Token: 0x06000500 RID: 1280
		MediaPublisherDTO LoadPublisherById(int publisherId);

		// Token: 0x06000501 RID: 1281
		MediaPublisherDTO LoadPublisherByName(string publisherName);

		// Token: 0x06000502 RID: 1282
		IList<MediaPublisherDTO> LoadAllPublishers();
	}
}
