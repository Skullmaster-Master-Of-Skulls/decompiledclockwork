using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000388 RID: 904
	internal class NoResizeSelectionBorderGlyph : SelectionGlyphBase
	{
		// Token: 0x0600250B RID: 9483 RVA: 0x000E662A File Offset: 0x000E482A
		internal NoResizeSelectionBorderGlyph(Rectangle controlBounds, SelectionRules rules, SelectionBorderGlyphType type, Behavior behavior) : base(behavior)
		{
			this.InitializeGlyph(controlBounds, rules, type);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000E6640 File Offset: 0x000E4840
		private void InitializeGlyph(Rectangle controlBounds, SelectionRules selRules, SelectionBorderGlyphType type)
		{
			this.rules = SelectionRules.None;
			this.hitTestCursor = Cursors.Default;
			if ((selRules & SelectionRules.Moveable) != SelectionRules.None)
			{
				this.rules = SelectionRules.Moveable;
				this.hitTestCursor = Cursors.SizeAll;
			}
			this.bounds = DesignerUtils.GetBoundsForNoResizeSelectionType(controlBounds, type);
			this.hitBounds = this.bounds;
			switch (type)
			{
			case SelectionBorderGlyphType.Top:
			case SelectionBorderGlyphType.Bottom:
				this.hitBounds.Y = this.hitBounds.Y - (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE) / 2;
				this.hitBounds.Height = this.hitBounds.Height + (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE);
				return;
			case SelectionBorderGlyphType.Left:
			case SelectionBorderGlyphType.Right:
				this.hitBounds.X = this.hitBounds.X - (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE) / 2;
				this.hitBounds.Width = this.hitBounds.Width + (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000E6470 File Offset: 0x000E4670
		public override void Paint(PaintEventArgs pe)
		{
			DesignerUtils.DrawSelectionBorder(pe.Graphics, this.bounds);
		}
	}
}
