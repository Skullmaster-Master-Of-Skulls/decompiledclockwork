using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.ICore.Messaging
{
	// Token: 0x0200005D RID: 93
	public interface IMessagingDropBoxManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600028F RID: 655
		void Save(DropBox_IM im);

		// Token: 0x06000290 RID: 656
		IList<DropBox_IM> GetAllIMs(string username);

		// Token: 0x06000291 RID: 657
		int CountIMs(string username);

		// Token: 0x06000292 RID: 658
		DropBox_IM GetIM(int imID);

		// Token: 0x06000293 RID: 659
		void DeleteIM(int imID);
	}
}
