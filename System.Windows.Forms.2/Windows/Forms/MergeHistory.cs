using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	// Token: 0x020003E4 RID: 996
	internal class MergeHistory
	{
		// Token: 0x060043D1 RID: 17361 RVA: 0x0011EFA1 File Offset: 0x0011D1A1
		public MergeHistory(ToolStrip mergedToolStrip)
		{
			this.mergedToolStrip = mergedToolStrip;
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060043D2 RID: 17362 RVA: 0x0011EFB0 File Offset: 0x0011D1B0
		public Stack<MergeHistoryItem> MergeHistoryItemsStack
		{
			get
			{
				if (this.mergeHistoryItemsStack == null)
				{
					this.mergeHistoryItemsStack = new Stack<MergeHistoryItem>();
				}
				return this.mergeHistoryItemsStack;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x0011EFCB File Offset: 0x0011D1CB
		public ToolStrip MergedToolStrip
		{
			get
			{
				return this.mergedToolStrip;
			}
		}

		// Token: 0x040025F6 RID: 9718
		private Stack<MergeHistoryItem> mergeHistoryItemsStack;

		// Token: 0x040025F7 RID: 9719
		private ToolStrip mergedToolStrip;
	}
}
