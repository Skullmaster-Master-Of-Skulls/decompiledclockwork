using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BF1 RID: 3057
	public class OrgChartGroupItemDataBoundEventArguments : EventArgs
	{
		// Token: 0x06007486 RID: 29830 RVA: 0x001B2C96 File Offset: 0x001B0E96
		public OrgChartGroupItemDataBoundEventArguments(OrgChartGroupItem item)
		{
			this._item = item;
		}

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x06007487 RID: 29831 RVA: 0x001B2CA5 File Offset: 0x001B0EA5
		public OrgChartGroupItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04001FB8 RID: 8120
		private OrgChartGroupItem _item;
	}
}
