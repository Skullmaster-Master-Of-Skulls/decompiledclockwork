using System;

namespace System.Windows.Forms
{
	// Token: 0x020003AC RID: 940
	public class ToolBarButtonClickEventArgs : EventArgs
	{
		// Token: 0x06003D99 RID: 15769 RVA: 0x0010B86B File Offset: 0x00109A6B
		public ToolBarButtonClickEventArgs(ToolBarButton button)
		{
			this.button = button;
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x0010B87A File Offset: 0x00109A7A
		// (set) Token: 0x06003D9B RID: 15771 RVA: 0x0010B882 File Offset: 0x00109A82
		public ToolBarButton Button
		{
			get
			{
				return this.button;
			}
			set
			{
				this.button = value;
			}
		}

		// Token: 0x0400242E RID: 9262
		private ToolBarButton button;
	}
}
