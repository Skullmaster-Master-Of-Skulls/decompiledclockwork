using System;

namespace System.Windows.Forms
{
	// Token: 0x0200037B RID: 891
	public class StatusBarPanelClickEventArgs : MouseEventArgs
	{
		// Token: 0x06003A56 RID: 14934 RVA: 0x001016F3 File Offset: 0x000FF8F3
		public StatusBarPanelClickEventArgs(StatusBarPanel statusBarPanel, MouseButtons button, int clicks, int x, int y) : base(button, clicks, x, y, 0)
		{
			this.statusBarPanel = statusBarPanel;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x00101709 File Offset: 0x000FF909
		public StatusBarPanel StatusBarPanel
		{
			get
			{
				return this.statusBarPanel;
			}
		}

		// Token: 0x04002308 RID: 8968
		private readonly StatusBarPanel statusBarPanel;
	}
}
