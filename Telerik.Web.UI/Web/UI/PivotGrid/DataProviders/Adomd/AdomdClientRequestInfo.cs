using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D55 RID: 3413
	internal class AdomdClientRequestInfo : OperationRequestInfo
	{
		// Token: 0x06007F3A RID: 32570 RVA: 0x001D13CE File Offset: 0x001CF5CE
		public AdomdClientRequestInfo(string mdxQuery, AdomdConnectionSettings connectionSettings, IOlapPivotConfiguration pivotConfiguration)
		{
			this.MdxQuery = mdxQuery;
			this.ConnectionSettings = connectionSettings;
			this.PivotConfiguration = pivotConfiguration;
		}

		// Token: 0x1700288B RID: 10379
		// (get) Token: 0x06007F3B RID: 32571 RVA: 0x001D13EB File Offset: 0x001CF5EB
		// (set) Token: 0x06007F3C RID: 32572 RVA: 0x001D13F3 File Offset: 0x001CF5F3
		public string MdxQuery { get; private set; }

		// Token: 0x1700288C RID: 10380
		// (get) Token: 0x06007F3D RID: 32573 RVA: 0x001D13FC File Offset: 0x001CF5FC
		// (set) Token: 0x06007F3E RID: 32574 RVA: 0x001D1404 File Offset: 0x001CF604
		public AdomdConnectionSettings ConnectionSettings { get; private set; }

		// Token: 0x1700288D RID: 10381
		// (get) Token: 0x06007F3F RID: 32575 RVA: 0x001D140D File Offset: 0x001CF60D
		// (set) Token: 0x06007F40 RID: 32576 RVA: 0x001D1415 File Offset: 0x001CF615
		public IOlapPivotConfiguration PivotConfiguration { get; private set; }

		// Token: 0x06007F41 RID: 32577 RVA: 0x001D1420 File Offset: 0x001CF620
		public override bool Equals(object obj)
		{
			AdomdClientRequestInfo adomdClientRequestInfo = obj as AdomdClientRequestInfo;
			return adomdClientRequestInfo != null && this.ConnectionSettings.Equals(adomdClientRequestInfo.ConnectionSettings) && this.MdxQuery.Equals(adomdClientRequestInfo.MdxQuery);
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x001D1470 File Offset: 0x001CF670
		public override int GetHashCode()
		{
			return this.ConnectionSettings.GetHashCode() ^ this.MdxQuery.GetHashCode();
		}
	}
}
