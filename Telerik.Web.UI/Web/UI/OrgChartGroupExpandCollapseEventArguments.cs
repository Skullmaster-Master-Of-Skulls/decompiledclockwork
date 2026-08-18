using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BE1 RID: 3041
	public class OrgChartGroupExpandCollapseEventArguments : EventArgs
	{
		// Token: 0x060073FB RID: 29691 RVA: 0x001B1210 File Offset: 0x001AF410
		public OrgChartGroupExpandCollapseEventArguments(OrgChartNode sourceNode, OrgChartGroupExpandCollapseState state)
		{
			this._sourceNode = sourceNode;
			this._state = state;
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x060073FC RID: 29692 RVA: 0x001B1226 File Offset: 0x001AF426
		public OrgChartNode SourceNode
		{
			get
			{
				return this._sourceNode;
			}
		}

		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x060073FD RID: 29693 RVA: 0x001B122E File Offset: 0x001AF42E
		public OrgChartGroupExpandCollapseState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x04001F83 RID: 8067
		private OrgChartNode _sourceNode;

		// Token: 0x04001F84 RID: 8068
		private OrgChartGroupExpandCollapseState _state;
	}
}
