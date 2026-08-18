using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200035B RID: 859
	[ComVisible(true)]
	public class ScrollEventArgs : EventArgs
	{
		// Token: 0x06003854 RID: 14420 RVA: 0x000FA52A File Offset: 0x000F872A
		public ScrollEventArgs(ScrollEventType type, int newValue)
		{
			this.type = type;
			this.newValue = newValue;
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000FA547 File Offset: 0x000F8747
		public ScrollEventArgs(ScrollEventType type, int newValue, ScrollOrientation scroll)
		{
			this.type = type;
			this.newValue = newValue;
			this.scrollOrientation = scroll;
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000FA56B File Offset: 0x000F876B
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue)
		{
			this.type = type;
			this.newValue = newValue;
			this.oldValue = oldValue;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000FA58F File Offset: 0x000F878F
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
		{
			this.type = type;
			this.newValue = newValue;
			this.scrollOrientation = scroll;
			this.oldValue = oldValue;
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000FA5BB File Offset: 0x000F87BB
		public ScrollOrientation ScrollOrientation
		{
			get
			{
				return this.scrollOrientation;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000FA5C3 File Offset: 0x000F87C3
		public ScrollEventType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000FA5CB File Offset: 0x000F87CB
		// (set) Token: 0x0600385B RID: 14427 RVA: 0x000FA5D3 File Offset: 0x000F87D3
		public int NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000FA5DC File Offset: 0x000F87DC
		public int OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x0400219E RID: 8606
		private readonly ScrollEventType type;

		// Token: 0x0400219F RID: 8607
		private int newValue;

		// Token: 0x040021A0 RID: 8608
		private ScrollOrientation scrollOrientation;

		// Token: 0x040021A1 RID: 8609
		private int oldValue = -1;
	}
}
