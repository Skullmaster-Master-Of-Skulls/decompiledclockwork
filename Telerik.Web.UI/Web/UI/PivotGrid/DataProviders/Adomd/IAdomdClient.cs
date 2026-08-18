using System;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D52 RID: 3410
	internal interface IAdomdClient
	{
		// Token: 0x14000135 RID: 309
		// (add) Token: 0x06007F21 RID: 32545
		// (remove) Token: 0x06007F22 RID: 32546
		event EventHandler<AdomdClientRequestCompletedEventArgs> SendRequestCompleted;

		// Token: 0x06007F23 RID: 32547
		void SendRequestAsync(AdomdClientRequestInfo requestInfo);
	}
}
