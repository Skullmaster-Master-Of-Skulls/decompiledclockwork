using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D5B RID: 3419
	internal class BackgroundWorkerWithState : BackgroundWorker
	{
		// Token: 0x06007F9C RID: 32668 RVA: 0x001D2830 File Offset: 0x001D0A30
		public BackgroundWorkerWithState(AdomdClientRequestInfo state)
		{
			this.State = state;
		}

		// Token: 0x1700289D RID: 10397
		// (get) Token: 0x06007F9D RID: 32669 RVA: 0x001D283F File Offset: 0x001D0A3F
		// (set) Token: 0x06007F9E RID: 32670 RVA: 0x001D2847 File Offset: 0x001D0A47
		public AdomdClientRequestInfo State { get; private set; }
	}
}
