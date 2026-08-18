using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000392 RID: 914
	internal class TableLayoutPanelResizeGlyph : Glyph
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x000E9A18 File Offset: 0x000E7C18
		internal TableLayoutPanelResizeGlyph(Rectangle controlBounds, TableLayoutStyle style, Cursor hitTestCursor, Behavior behavior) : base(behavior)
		{
			this.bounds = controlBounds;
			this.hitTestCursor = hitTestCursor;
			this.style = style;
			if (style is ColumnStyle)
			{
				this.type = TableLayoutPanelResizeGlyph.TableLayoutResizeType.Column;
				return;
			}
			this.type = TableLayoutPanelResizeGlyph.TableLayoutResizeType.Row;
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x000E9A4E File Offset: 0x000E7C4E
		public override Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000E9A56 File Offset: 0x000E7C56
		public TableLayoutStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000E9A5E File Offset: 0x000E7C5E
		public TableLayoutPanelResizeGlyph.TableLayoutResizeType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000E9A66 File Offset: 0x000E7C66
		public override Cursor GetHitTest(Point p)
		{
			if (this.bounds.Contains(p))
			{
				return this.hitTestCursor;
			}
			return null;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00003937 File Offset: 0x00001B37
		public override void Paint(PaintEventArgs pe)
		{
		}

		// Token: 0x04001B35 RID: 6965
		private Rectangle bounds;

		// Token: 0x04001B36 RID: 6966
		private Cursor hitTestCursor;

		// Token: 0x04001B37 RID: 6967
		private TableLayoutStyle style;

		// Token: 0x04001B38 RID: 6968
		private TableLayoutPanelResizeGlyph.TableLayoutResizeType type;

		// Token: 0x020005AB RID: 1451
		public enum TableLayoutResizeType
		{
			// Token: 0x040022AE RID: 8878
			Column,
			// Token: 0x040022AF RID: 8879
			Row
		}
	}
}
