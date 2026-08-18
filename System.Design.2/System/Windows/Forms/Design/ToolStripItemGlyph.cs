using System;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200035D RID: 861
	internal class ToolStripItemGlyph : ControlBodyGlyph
	{
		// Token: 0x060022CB RID: 8907 RVA: 0x000D6B97 File Offset: 0x000D4D97
		public ToolStripItemGlyph(ToolStripItem item, ToolStripItemDesigner itemDesigner, Rectangle bounds, Behavior b) : base(bounds, Cursors.Default, item, b)
		{
			this._item = item;
			this._bounds = bounds;
			this._itemDesigner = itemDesigner;
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000D6BBD File Offset: 0x000D4DBD
		public ToolStripItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000D6BC5 File Offset: 0x000D4DC5
		public override Rectangle Bounds
		{
			get
			{
				return this._bounds;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000D6BCD File Offset: 0x000D4DCD
		public ToolStripItemDesigner ItemDesigner
		{
			get
			{
				return this._itemDesigner;
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000D6BD5 File Offset: 0x000D4DD5
		public override Cursor GetHitTest(Point p)
		{
			if (this._item.Visible && this._bounds.Contains(p))
			{
				return Cursors.Default;
			}
			return null;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000D6BF9 File Offset: 0x000D4DF9
		public override void Paint(PaintEventArgs pe)
		{
			if (this._item is ToolStripControlHost && this._item.IsOnDropDown)
			{
				if (this._item is ToolStripComboBox && VisualStyleRenderer.IsSupported)
				{
					return;
				}
				this._item.Invalidate();
			}
		}

		// Token: 0x040019CC RID: 6604
		private ToolStripItem _item;

		// Token: 0x040019CD RID: 6605
		private Rectangle _bounds;

		// Token: 0x040019CE RID: 6606
		private ToolStripItemDesigner _itemDesigner;
	}
}
