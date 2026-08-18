using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003C8 RID: 968
	public class ToolStripGripRenderEventArgs : ToolStripRenderEventArgs
	{
		// Token: 0x06004194 RID: 16788 RVA: 0x00118C43 File Offset: 0x00116E43
		public ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip) : base(g, toolStrip)
		{
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x00118C4D File Offset: 0x00116E4D
		public Rectangle GripBounds
		{
			get
			{
				return base.ToolStrip.GripRectangle;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x00118C5A File Offset: 0x00116E5A
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return base.ToolStrip.GripDisplayStyle;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x00118C67 File Offset: 0x00116E67
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return base.ToolStrip.GripStyle;
			}
		}
	}
}
