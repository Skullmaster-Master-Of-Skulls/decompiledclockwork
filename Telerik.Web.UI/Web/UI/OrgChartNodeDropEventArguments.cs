using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BF4 RID: 3060
	public class OrgChartNodeDropEventArguments : EventArgs
	{
		// Token: 0x0600748D RID: 29837 RVA: 0x001B2CEA File Offset: 0x001B0EEA
		public OrgChartNodeDropEventArguments(OrgChartNode sourceNode, OrgChartNode destinationNode)
		{
			this._sourceNode = sourceNode;
			this._destination = destinationNode;
		}

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x0600748E RID: 29838 RVA: 0x001B2D00 File Offset: 0x001B0F00
		public OrgChartNode SourceNode
		{
			get
			{
				return this._sourceNode;
			}
		}

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x0600748F RID: 29839 RVA: 0x001B2D08 File Offset: 0x001B0F08
		public OrgChartNode DestinationNode
		{
			get
			{
				return this._destination;
			}
		}

		// Token: 0x04001FBC RID: 8124
		private OrgChartNode _sourceNode;

		// Token: 0x04001FBD RID: 8125
		private OrgChartNode _destination;
	}
}
