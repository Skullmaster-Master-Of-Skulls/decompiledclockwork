using System;

namespace System.Windows.Forms
{
	// Token: 0x0200038B RID: 907
	public class TabControlEventArgs : EventArgs
	{
		// Token: 0x06003BAF RID: 15279 RVA: 0x001057A8 File Offset: 0x001039A8
		public TabControlEventArgs(TabPage tabPage, int tabPageIndex, TabControlAction action)
		{
			this.tabPage = tabPage;
			this.tabPageIndex = tabPageIndex;
			this.action = action;
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x001057C5 File Offset: 0x001039C5
		public TabPage TabPage
		{
			get
			{
				return this.tabPage;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x001057CD File Offset: 0x001039CD
		public int TabPageIndex
		{
			get
			{
				return this.tabPageIndex;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x001057D5 File Offset: 0x001039D5
		public TabControlAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400237E RID: 9086
		private TabPage tabPage;

		// Token: 0x0400237F RID: 9087
		private int tabPageIndex;

		// Token: 0x04002380 RID: 9088
		private TabControlAction action;
	}
}
