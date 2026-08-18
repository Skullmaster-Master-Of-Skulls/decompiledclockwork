using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003F0 RID: 1008
	public class ToolStripPanelRenderEventArgs : EventArgs
	{
		// Token: 0x0600454C RID: 17740 RVA: 0x00123338 File Offset: 0x00121538
		public ToolStripPanelRenderEventArgs(Graphics g, ToolStripPanel toolStripPanel)
		{
			this.toolStripPanel = toolStripPanel;
			this.graphics = g;
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x0012334E File Offset: 0x0012154E
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x0600454E RID: 17742 RVA: 0x00123356 File Offset: 0x00121556
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this.toolStripPanel;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x0012335E File Offset: 0x0012155E
		// (set) Token: 0x06004550 RID: 17744 RVA: 0x00123366 File Offset: 0x00121566
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

		// Token: 0x04002650 RID: 9808
		private ToolStripPanel toolStripPanel;

		// Token: 0x04002651 RID: 9809
		private Graphics graphics;

		// Token: 0x04002652 RID: 9810
		private bool handled;
	}
}
