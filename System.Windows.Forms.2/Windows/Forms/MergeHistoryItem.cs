using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x020003E5 RID: 997
	internal class MergeHistoryItem
	{
		// Token: 0x060043D4 RID: 17364 RVA: 0x0011EFD3 File Offset: 0x0011D1D3
		public MergeHistoryItem(MergeAction mergeAction)
		{
			this.mergeAction = mergeAction;
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x0011EFF0 File Offset: 0x0011D1F0
		public MergeAction MergeAction
		{
			get
			{
				return this.mergeAction;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x0011EFF8 File Offset: 0x0011D1F8
		// (set) Token: 0x060043D7 RID: 17367 RVA: 0x0011F000 File Offset: 0x0011D200
		public ToolStripItem TargetItem
		{
			get
			{
				return this.targetItem;
			}
			set
			{
				this.targetItem = value;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x0011F009 File Offset: 0x0011D209
		// (set) Token: 0x060043D9 RID: 17369 RVA: 0x0011F011 File Offset: 0x0011D211
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x0011F01A File Offset: 0x0011D21A
		// (set) Token: 0x060043DB RID: 17371 RVA: 0x0011F022 File Offset: 0x0011D222
		public int PreviousIndex
		{
			get
			{
				return this.previousIndex;
			}
			set
			{
				this.previousIndex = value;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x0011F02B File Offset: 0x0011D22B
		// (set) Token: 0x060043DD RID: 17373 RVA: 0x0011F033 File Offset: 0x0011D233
		public ToolStripItemCollection PreviousIndexCollection
		{
			get
			{
				return this.previousIndexCollection;
			}
			set
			{
				this.previousIndexCollection = value;
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x0011F03C File Offset: 0x0011D23C
		// (set) Token: 0x060043DF RID: 17375 RVA: 0x0011F044 File Offset: 0x0011D244
		public ToolStripItemCollection IndexCollection
		{
			get
			{
				return this.indexCollection;
			}
			set
			{
				this.indexCollection = value;
			}
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x0011F050 File Offset: 0x0011D250
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MergeAction: ",
				this.mergeAction.ToString(),
				" | TargetItem: ",
				(this.TargetItem == null) ? "null" : this.TargetItem.Text,
				" Index: ",
				this.index.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x040025F8 RID: 9720
		private MergeAction mergeAction;

		// Token: 0x040025F9 RID: 9721
		private ToolStripItem targetItem;

		// Token: 0x040025FA RID: 9722
		private int index = -1;

		// Token: 0x040025FB RID: 9723
		private int previousIndex = -1;

		// Token: 0x040025FC RID: 9724
		private ToolStripItemCollection previousIndexCollection;

		// Token: 0x040025FD RID: 9725
		private ToolStripItemCollection indexCollection;
	}
}
