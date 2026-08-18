using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C8F RID: 3215
	public class DataProviderStatusChangedEventArgs : EventArgs
	{
		// Token: 0x060078B3 RID: 30899 RVA: 0x001BCD29 File Offset: 0x001BAF29
		public DataProviderStatusChangedEventArgs(DataProviderStatus oldStatus, DataProviderStatus newStatus, bool resultsChanges, Exception error)
		{
			this.OldStatus = oldStatus;
			this.NewStatus = newStatus;
			this.ResultsChanged = resultsChanges;
			this.Error = error;
		}

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x060078B4 RID: 30900 RVA: 0x001BCD4E File Offset: 0x001BAF4E
		// (set) Token: 0x060078B5 RID: 30901 RVA: 0x001BCD56 File Offset: 0x001BAF56
		public Exception Error { get; private set; }

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x060078B6 RID: 30902 RVA: 0x001BCD5F File Offset: 0x001BAF5F
		// (set) Token: 0x060078B7 RID: 30903 RVA: 0x001BCD67 File Offset: 0x001BAF67
		public DataProviderStatus OldStatus { get; private set; }

		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x060078B8 RID: 30904 RVA: 0x001BCD70 File Offset: 0x001BAF70
		// (set) Token: 0x060078B9 RID: 30905 RVA: 0x001BCD78 File Offset: 0x001BAF78
		public DataProviderStatus NewStatus { get; private set; }

		// Token: 0x170026F7 RID: 9975
		// (get) Token: 0x060078BA RID: 30906 RVA: 0x001BCD81 File Offset: 0x001BAF81
		// (set) Token: 0x060078BB RID: 30907 RVA: 0x001BCD89 File Offset: 0x001BAF89
		public bool ResultsChanged { get; private set; }
	}
}
