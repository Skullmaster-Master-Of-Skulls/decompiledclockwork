using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000F3 RID: 243
	public interface IMediaPublisherManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007C9 RID: 1993
		int CreatePublisher(MediaPublisher publisher);

		// Token: 0x060007CA RID: 1994
		bool UpdatePublisher(MediaPublisher publisher);

		// Token: 0x060007CB RID: 1995
		bool DeletePublisher(int publisherId);

		// Token: 0x060007CC RID: 1996
		MediaPublisher LoadPublisherById(int publisherId);

		// Token: 0x060007CD RID: 1997
		MediaPublisher LoadPublisherByName(string publisherName);

		// Token: 0x060007CE RID: 1998
		IList<MediaPublisher> LoadAllPublishers();
	}
}
