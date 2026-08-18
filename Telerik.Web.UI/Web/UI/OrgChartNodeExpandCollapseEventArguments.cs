using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BE2 RID: 3042
	public class OrgChartNodeExpandCollapseEventArguments : EventArgs
	{
		// Token: 0x060073FE RID: 29694 RVA: 0x001B1236 File Offset: 0x001AF436
		public OrgChartNodeExpandCollapseEventArguments(OrgChartNode sourceNode, OrgChartNodeExpandCollapseState state)
		{
			this._sourceNode = sourceNode;
			this._state = state;
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x060073FF RID: 29695 RVA: 0x001B124C File Offset: 0x001AF44C
		public OrgChartNode SourceNode
		{
			get
			{
				return this._sourceNode;
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x06007400 RID: 29696 RVA: 0x001B1254 File Offset: 0x001AF454
		public OrgChartNodeExpandCollapseState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x04001F85 RID: 8069
		private OrgChartNode _sourceNode;

		// Token: 0x04001F86 RID: 8070
		private OrgChartNodeExpandCollapseState _state;
	}
}
