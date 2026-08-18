using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001113 RID: 4371
	internal class GridGroupingContext
	{
		// Token: 0x04002F24 RID: 12068
		public int groupLevel;

		// Token: 0x04002F25 RID: 12069
		public string parentGroupIndex;

		// Token: 0x04002F26 RID: 12070
		public bool parentGroupExpanded;

		// Token: 0x04002F27 RID: 12071
		public string currentItemGroupIndex = "";

		// Token: 0x04002F28 RID: 12072
		public int itemIndexInGroup;
	}
}
