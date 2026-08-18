using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000385 RID: 901
	internal class LockedHandleGlyph : SelectionGlyphBase
	{
		// Token: 0x06002504 RID: 9476 RVA: 0x000E6484 File Offset: 0x000E4684
		internal LockedHandleGlyph(Rectangle controlBounds, bool primarySelection) : base(null)
		{
			this.isPrimary = primarySelection;
			this.hitTestCursor = Cursors.Default;
			this.rules = SelectionRules.None;
			this.bounds = new Rectangle(controlBounds.X + DesignerUtils.LOCKHANDLEOVERLAP - DesignerUtils.LOCKHANDLEWIDTH, controlBounds.Y + DesignerUtils.LOCKHANDLEOVERLAP - DesignerUtils.LOCKHANDLEHEIGHT, DesignerUtils.LOCKHANDLEWIDTH, DesignerUtils.LOCKHANDLEHEIGHT);
			this.hitBounds = this.bounds;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000E64F8 File Offset: 0x000E46F8
		public override void Paint(PaintEventArgs pe)
		{
			DesignerUtils.DrawLockedHandle(pe.Graphics, this.bounds, this.isPrimary, this);
		}

		// Token: 0x04001AD8 RID: 6872
		private bool isPrimary;
	}
}
