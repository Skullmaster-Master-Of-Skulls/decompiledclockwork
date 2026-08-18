using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000C0B RID: 3083
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class OrgChartPostBackArguments
	{
		// Token: 0x04002040 RID: 8256
		public OrgChartPostBackCommand command;

		// Token: 0x04002041 RID: 8257
		public string sourceNodeHierarchicalIndex;

		// Token: 0x04002042 RID: 8258
		public int sourceGroupItemIndex;

		// Token: 0x04002043 RID: 8259
		public string destinationNodeHierarchicalIndex;
	}
}
