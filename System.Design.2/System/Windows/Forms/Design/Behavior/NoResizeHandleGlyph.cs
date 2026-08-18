using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000387 RID: 903
	internal class NoResizeHandleGlyph : SelectionGlyphBase
	{
		// Token: 0x06002509 RID: 9481 RVA: 0x000E6588 File Offset: 0x000E4788
		internal NoResizeHandleGlyph(Rectangle controlBounds, SelectionRules selRules, bool primarySelection, Behavior behavior) : base(behavior)
		{
			this.isPrimary = primarySelection;
			this.hitTestCursor = Cursors.Default;
			this.rules = SelectionRules.None;
			if ((selRules & SelectionRules.Moveable) != SelectionRules.None)
			{
				this.rules = SelectionRules.Moveable;
				this.hitTestCursor = Cursors.SizeAll;
			}
			this.bounds = new Rectangle(controlBounds.X - DesignerUtils.NORESIZEHANDLESIZE, controlBounds.Y - DesignerUtils.NORESIZEHANDLESIZE, DesignerUtils.NORESIZEHANDLESIZE, DesignerUtils.NORESIZEHANDLESIZE);
			this.hitBounds = this.bounds;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000E6610 File Offset: 0x000E4810
		public override void Paint(PaintEventArgs pe)
		{
			DesignerUtils.DrawNoResizeHandle(pe.Graphics, this.bounds, this.isPrimary, this);
		}

		// Token: 0x04001ADA RID: 6874
		private bool isPrimary;
	}
}
