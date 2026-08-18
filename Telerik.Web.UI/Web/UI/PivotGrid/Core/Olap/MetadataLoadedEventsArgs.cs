using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CFE RID: 3326
	internal class MetadataLoadedEventsArgs : EventArgs
	{
		// Token: 0x06007C19 RID: 31769 RVA: 0x001C87D7 File Offset: 0x001C69D7
		public MetadataLoadedEventsArgs(OlapCatalogInfo catalogInfo, OlapCommunicationException error)
		{
			this.CatalogInfo = catalogInfo;
			this.Error = error;
		}

		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x001C87ED File Offset: 0x001C69ED
		// (set) Token: 0x06007C1B RID: 31771 RVA: 0x001C87F5 File Offset: 0x001C69F5
		public OlapCatalogInfo CatalogInfo { get; private set; }

		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x06007C1C RID: 31772 RVA: 0x001C87FE File Offset: 0x001C69FE
		// (set) Token: 0x06007C1D RID: 31773 RVA: 0x001C8806 File Offset: 0x001C6A06
		public OlapCommunicationException Error { get; private set; }
	}
}
