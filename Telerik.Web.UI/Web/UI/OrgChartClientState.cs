using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000C00 RID: 3072
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class OrgChartClientState
	{
		// Token: 0x04001FDA RID: 8154
		public int drillDownLevel;

		// Token: 0x04001FDB RID: 8155
		public string[] expandedNodes;

		// Token: 0x04001FDC RID: 8156
		public string[] collapsedNodes;

		// Token: 0x04001FDD RID: 8157
		public string[] expandedGroups;

		// Token: 0x04001FDE RID: 8158
		public string[] collapsedGroups;
	}
}
