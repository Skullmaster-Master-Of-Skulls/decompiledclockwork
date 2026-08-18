using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000384 RID: 900
	internal class LockedBorderGlyph : SelectionGlyphBase
	{
		// Token: 0x06002501 RID: 9473 RVA: 0x000E6432 File Offset: 0x000E4632
		internal LockedBorderGlyph(Rectangle controlBounds, SelectionBorderGlyphType type) : base(null)
		{
			this.InitializeGlyph(controlBounds, type);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000E6443 File Offset: 0x000E4643
		private void InitializeGlyph(Rectangle controlBounds, SelectionBorderGlyphType type)
		{
			this.hitTestCursor = Cursors.Default;
			this.rules = SelectionRules.None;
			this.bounds = DesignerUtils.GetBoundsForSelectionType(controlBounds, type);
			this.hitBounds = this.bounds;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000E6470 File Offset: 0x000E4670
		public override void Paint(PaintEventArgs pe)
		{
			DesignerUtils.DrawSelectionBorder(pe.Graphics, this.bounds);
		}
	}
}
