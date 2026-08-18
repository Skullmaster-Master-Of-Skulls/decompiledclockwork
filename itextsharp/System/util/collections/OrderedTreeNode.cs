using System;

namespace System.util.collections
{
	// Token: 0x02000424 RID: 1060
	public class OrderedTreeNode
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x000DBE22 File Offset: 0x000DAE22
		// (set) Token: 0x06002408 RID: 9224 RVA: 0x000DBE2A File Offset: 0x000DAE2A
		public IComparable Key
		{
			get
			{
				return this.ordKey;
			}
			set
			{
				this.ordKey = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000DBE33 File Offset: 0x000DAE33
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000DBE3B File Offset: 0x000DAE3B
		public object Data
		{
			get
			{
				return this.objData;
			}
			set
			{
				this.objData = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000DBE44 File Offset: 0x000DAE44
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x000DBE4C File Offset: 0x000DAE4C
		public bool Color
		{
			get
			{
				return this.intColor;
			}
			set
			{
				this.intColor = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000DBE55 File Offset: 0x000DAE55
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x000DBE5D File Offset: 0x000DAE5D
		public OrderedTreeNode Left
		{
			get
			{
				return this.rbnLeft;
			}
			set
			{
				this.rbnLeft = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000DBE66 File Offset: 0x000DAE66
		// (set) Token: 0x06002410 RID: 9232 RVA: 0x000DBE6E File Offset: 0x000DAE6E
		public OrderedTreeNode Right
		{
			get
			{
				return this.rbnRight;
			}
			set
			{
				this.rbnRight = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000DBE77 File Offset: 0x000DAE77
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x000DBE7F File Offset: 0x000DAE7F
		public OrderedTreeNode Parent
		{
			get
			{
				return this.rbnParent;
			}
			set
			{
				this.rbnParent = value;
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000DBE88 File Offset: 0x000DAE88
		public OrderedTreeNode()
		{
			this.Color = false;
		}

		// Token: 0x0400190D RID: 6413
		public const bool RED = false;

		// Token: 0x0400190E RID: 6414
		public const bool BLACK = true;

		// Token: 0x0400190F RID: 6415
		private IComparable ordKey;

		// Token: 0x04001910 RID: 6416
		private object objData;

		// Token: 0x04001911 RID: 6417
		private bool intColor;

		// Token: 0x04001912 RID: 6418
		private OrderedTreeNode rbnLeft;

		// Token: 0x04001913 RID: 6419
		private OrderedTreeNode rbnRight;

		// Token: 0x04001914 RID: 6420
		private OrderedTreeNode rbnParent;
	}
}
