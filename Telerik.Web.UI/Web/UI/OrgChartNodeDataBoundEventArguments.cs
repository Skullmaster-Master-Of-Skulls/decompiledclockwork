using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BF3 RID: 3059
	public class OrgChartNodeDataBoundEventArguments : EventArgs
	{
		// Token: 0x0600748B RID: 29835 RVA: 0x001B2CD3 File Offset: 0x001B0ED3
		public OrgChartNodeDataBoundEventArguments(OrgChartNode node)
		{
			this._node = node;
		}

		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x0600748C RID: 29836 RVA: 0x001B2CE2 File Offset: 0x001B0EE2
		public OrgChartNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x04001FBB RID: 8123
		private OrgChartNode _node;
	}
}
