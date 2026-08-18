using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000361 RID: 865
	public class SearchForVirtualItemEventArgs : EventArgs
	{
		// Token: 0x06003877 RID: 14455 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public SearchForVirtualItemEventArgs(bool isTextSearch, bool isPrefixSearch, bool includeSubItemsInSearch, string text, Point startingPoint, SearchDirectionHint direction, int startIndex)
		{
			this.isTextSearch = isTextSearch;
			this.isPrefixSearch = isPrefixSearch;
			this.includeSubItemsInSearch = includeSubItemsInSearch;
			this.text = text;
			this.startingPoint = startingPoint;
			this.direction = direction;
			this.startIndex = startIndex;
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000FAAB7 File Offset: 0x000F8CB7
		public bool IsTextSearch
		{
			get
			{
				return this.isTextSearch;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000FAABF File Offset: 0x000F8CBF
		public bool IncludeSubItemsInSearch
		{
			get
			{
				return this.includeSubItemsInSearch;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000FAAC7 File Offset: 0x000F8CC7
		// (set) Token: 0x0600387B RID: 14459 RVA: 0x000FAACF File Offset: 0x000F8CCF
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

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000FAAD8 File Offset: 0x000F8CD8
		public bool IsPrefixSearch
		{
			get
			{
				return this.isPrefixSearch;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000FAAE0 File Offset: 0x000F8CE0
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x000FAAE8 File Offset: 0x000F8CE8
		public Point StartingPoint
		{
			get
			{
				return this.startingPoint;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000FAAF0 File Offset: 0x000F8CF0
		public SearchDirectionHint Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000FAAF8 File Offset: 0x000F8CF8
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x040021C0 RID: 8640
		private bool isTextSearch;

		// Token: 0x040021C1 RID: 8641
		private bool isPrefixSearch;

		// Token: 0x040021C2 RID: 8642
		private bool includeSubItemsInSearch;

		// Token: 0x040021C3 RID: 8643
		private string text;

		// Token: 0x040021C4 RID: 8644
		private Point startingPoint;

		// Token: 0x040021C5 RID: 8645
		private SearchDirectionHint direction;

		// Token: 0x040021C6 RID: 8646
		private int startIndex;

		// Token: 0x040021C7 RID: 8647
		private int index = -1;
	}
}
