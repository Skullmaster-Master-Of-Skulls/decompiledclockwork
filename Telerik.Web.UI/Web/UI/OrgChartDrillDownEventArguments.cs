using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BF0 RID: 3056
	public class OrgChartDrillDownEventArguments : EventArgs
	{
		// Token: 0x06007484 RID: 29828 RVA: 0x001B2C7F File Offset: 0x001B0E7F
		public OrgChartDrillDownEventArguments(OrgChartNode sourceNode)
		{
			this._sourceNode = sourceNode;
		}

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x06007485 RID: 29829 RVA: 0x001B2C8E File Offset: 0x001B0E8E
		public OrgChartNode SourceNode
		{
			get
			{
				return this._sourceNode;
			}
		}

		// Token: 0x04001FB7 RID: 8119
		private OrgChartNode _sourceNode;
	}
}
