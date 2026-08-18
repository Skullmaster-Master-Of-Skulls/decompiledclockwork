using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BF2 RID: 3058
	public class OrgChartGroupItemDropEventArguments : EventArgs
	{
		// Token: 0x06007488 RID: 29832 RVA: 0x001B2CAD File Offset: 0x001B0EAD
		public OrgChartGroupItemDropEventArguments(OrgChartGroupItem sourceGroupItem, OrgChartNode destinationNode)
		{
			this._sourceGroupItem = sourceGroupItem;
			this._destinationNode = destinationNode;
		}

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x06007489 RID: 29833 RVA: 0x001B2CC3 File Offset: 0x001B0EC3
		public OrgChartGroupItem SourceGroupItem
		{
			get
			{
				return this._sourceGroupItem;
			}
		}

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x0600748A RID: 29834 RVA: 0x001B2CCB File Offset: 0x001B0ECB
		public OrgChartNode DestinationNode
		{
			get
			{
				return this._destinationNode;
			}
		}

		// Token: 0x04001FB9 RID: 8121
		private OrgChartGroupItem _sourceGroupItem;

		// Token: 0x04001FBA RID: 8122
		private OrgChartNode _destinationNode;
	}
}
