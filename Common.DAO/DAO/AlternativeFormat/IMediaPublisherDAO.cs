using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000CD RID: 205
	public interface IMediaPublisherDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005E1 RID: 1505
		int CreatePublisher(MediaPublisher publisher);

		// Token: 0x060005E2 RID: 1506
		bool UpdatePublisher(MediaPublisher publisher);

		// Token: 0x060005E3 RID: 1507
		bool DeletePublisher(int publisherId);

		// Token: 0x060005E4 RID: 1508
		MediaPublisher LoadPublisherById(int publisherId);

		// Token: 0x060005E5 RID: 1509
		MediaPublisher LoadPublisherByName(string publisherName);

		// Token: 0x060005E6 RID: 1510
		IList<MediaPublisher> LoadAllPublishers();
	}
}
