using System;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D74 RID: 3444
	internal interface IXmlaClient
	{
		// Token: 0x14000137 RID: 311
		// (add) Token: 0x0600809C RID: 32924
		// (remove) Token: 0x0600809D RID: 32925
		event EventHandler<XmlaClientRequestCompletedEventArgs> SendRequestCompleted;

		// Token: 0x0600809E RID: 32926
		void SendRequestAsync(XmlaClientRequestInfo requestInfo);
	}
}
