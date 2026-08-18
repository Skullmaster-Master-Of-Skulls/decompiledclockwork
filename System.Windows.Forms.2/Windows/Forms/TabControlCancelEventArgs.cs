using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000389 RID: 905
	public class TabControlCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06003BA7 RID: 15271 RVA: 0x00105771 File Offset: 0x00103971
		public TabControlCancelEventArgs(TabPage tabPage, int tabPageIndex, bool cancel, TabControlAction action) : base(cancel)
		{
			this.tabPage = tabPage;
			this.tabPageIndex = tabPageIndex;
			this.action = action;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x00105790 File Offset: 0x00103990
		public TabPage TabPage
		{
			get
			{
				return this.tabPage;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x00105798 File Offset: 0x00103998
		public int TabPageIndex
		{
			get
			{
				return this.tabPageIndex;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x001057A0 File Offset: 0x001039A0
		public TabControlAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400237B RID: 9083
		private TabPage tabPage;

		// Token: 0x0400237C RID: 9084
		private int tabPageIndex;

		// Token: 0x0400237D RID: 9085
		private TabControlAction action;
	}
}
