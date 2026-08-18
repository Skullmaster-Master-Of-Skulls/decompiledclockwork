using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003F2 RID: 1010
	public class ToolStripContentPanelRenderEventArgs : EventArgs
	{
		// Token: 0x06004555 RID: 17749 RVA: 0x0012336F File Offset: 0x0012156F
		public ToolStripContentPanelRenderEventArgs(Graphics g, ToolStripContentPanel contentPanel)
		{
			this.contentPanel = contentPanel;
			this.graphics = g;
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x00123385 File Offset: 0x00121585
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x0012338D File Offset: 0x0012158D
		// (set) Token: 0x06004558 RID: 17752 RVA: 0x00123395 File Offset: 0x00121595
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x0012339E File Offset: 0x0012159E
		public ToolStripContentPanel ToolStripContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x04002653 RID: 9811
		private ToolStripContentPanel contentPanel;

		// Token: 0x04002654 RID: 9812
		private Graphics graphics;

		// Token: 0x04002655 RID: 9813
		private bool handled;
	}
}
