using System;
using Microsoft.AnalysisServices.AdomdClient;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D54 RID: 3412
	internal class AdomdClientRequestCompletedEventArgs : EventArgs
	{
		// Token: 0x06007F33 RID: 32563 RVA: 0x001D137E File Offset: 0x001CF57E
		public AdomdClientRequestCompletedEventArgs(CellSet result, AdomdClientRequestInfo requestInfo, Exception error)
		{
			this.Result = result;
			this.RequestInfo = requestInfo;
			this.Error = error;
		}

		// Token: 0x17002888 RID: 10376
		// (get) Token: 0x06007F34 RID: 32564 RVA: 0x001D139B File Offset: 0x001CF59B
		// (set) Token: 0x06007F35 RID: 32565 RVA: 0x001D13A3 File Offset: 0x001CF5A3
		public Exception Error { get; private set; }

		// Token: 0x17002889 RID: 10377
		// (get) Token: 0x06007F36 RID: 32566 RVA: 0x001D13AC File Offset: 0x001CF5AC
		// (set) Token: 0x06007F37 RID: 32567 RVA: 0x001D13B4 File Offset: 0x001CF5B4
		public CellSet Result { get; private set; }

		// Token: 0x1700288A RID: 10378
		// (get) Token: 0x06007F38 RID: 32568 RVA: 0x001D13BD File Offset: 0x001CF5BD
		// (set) Token: 0x06007F39 RID: 32569 RVA: 0x001D13C5 File Offset: 0x001CF5C5
		internal AdomdClientRequestInfo RequestInfo { get; private set; }
	}
}
