using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000386 RID: 902
	internal class MiniLockedBorderGlyph : SelectionGlyphBase
	{
		// Token: 0x06002506 RID: 9478 RVA: 0x000E6512 File Offset: 0x000E4712
		internal MiniLockedBorderGlyph(Rectangle controlBounds, SelectionBorderGlyphType type, Behavior behavior, bool primarySelection) : base(behavior)
		{
			this.InitializeGlyph(controlBounds, type, primarySelection);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000E6528 File Offset: 0x000E4728
		private void InitializeGlyph(Rectangle controlBounds, SelectionBorderGlyphType type, bool primarySelection)
		{
			this.hitTestCursor = Cursors.Default;
			this.rules = SelectionRules.None;
			int borderSize = 1;
			this.type = type;
			this.bounds = DesignerUtils.GetBoundsForSelectionType(controlBounds, type, borderSize);
			this.hitBounds = this.bounds;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000E656A File Offset: 0x000E476A
		public override void Paint(PaintEventArgs pe)
		{
			pe.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlText), this.bounds);
		}

		// Token: 0x04001AD9 RID: 6873
		private SelectionBorderGlyphType type;
	}
}
