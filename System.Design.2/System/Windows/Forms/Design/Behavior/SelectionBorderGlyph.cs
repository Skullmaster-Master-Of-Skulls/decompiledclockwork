using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200038A RID: 906
	internal class SelectionBorderGlyph : SelectionGlyphBase
	{
		// Token: 0x06002519 RID: 9497 RVA: 0x000E805C File Offset: 0x000E625C
		internal SelectionBorderGlyph(Rectangle controlBounds, SelectionRules rules, SelectionBorderGlyphType type, Behavior behavior) : base(behavior)
		{
			this.InitializeGlyph(controlBounds, rules, type);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000E8070 File Offset: 0x000E6270
		private void InitializeGlyph(Rectangle controlBounds, SelectionRules selRules, SelectionBorderGlyphType type)
		{
			this.rules = SelectionRules.None;
			this.hitTestCursor = Cursors.Default;
			this.bounds = DesignerUtils.GetBoundsForSelectionType(controlBounds, type);
			this.hitBounds = this.bounds;
			switch (type)
			{
			case SelectionBorderGlyphType.Top:
				if ((selRules & SelectionRules.TopSizeable) != SelectionRules.None)
				{
					this.hitTestCursor = Cursors.SizeNS;
					this.rules = SelectionRules.TopSizeable;
				}
				this.hitBounds.Y = this.hitBounds.Y - (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE) / 2;
				this.hitBounds.Height = this.hitBounds.Height + (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE);
				return;
			case SelectionBorderGlyphType.Bottom:
				if ((selRules & SelectionRules.BottomSizeable) != SelectionRules.None)
				{
					this.hitTestCursor = Cursors.SizeNS;
					this.rules = SelectionRules.BottomSizeable;
				}
				this.hitBounds.Y = this.hitBounds.Y - (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE) / 2;
				this.hitBounds.Height = this.hitBounds.Height + (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE);
				return;
			case SelectionBorderGlyphType.Left:
				if ((selRules & SelectionRules.LeftSizeable) != SelectionRules.None)
				{
					this.hitTestCursor = Cursors.SizeWE;
					this.rules = SelectionRules.LeftSizeable;
				}
				this.hitBounds.X = this.hitBounds.X - (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE) / 2;
				this.hitBounds.Width = this.hitBounds.Width + (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE);
				return;
			case SelectionBorderGlyphType.Right:
				if ((selRules & SelectionRules.RightSizeable) != SelectionRules.None)
				{
					this.hitTestCursor = Cursors.SizeWE;
					this.rules = SelectionRules.RightSizeable;
				}
				this.hitBounds.X = this.hitBounds.X - (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE) / 2;
				this.hitBounds.Width = this.hitBounds.Width + (DesignerUtils.SELECTIONBORDERHITAREA - DesignerUtils.SELECTIONBORDERSIZE);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000E6470 File Offset: 0x000E4670
		public override void Paint(PaintEventArgs pe)
		{
			DesignerUtils.DrawSelectionBorder(pe.Graphics, this.bounds);
		}
	}
}
