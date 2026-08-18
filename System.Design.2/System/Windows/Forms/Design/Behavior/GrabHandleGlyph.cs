using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000382 RID: 898
	internal class GrabHandleGlyph : SelectionGlyphBase
	{
		// Token: 0x060024FF RID: 9471 RVA: 0x000E60CC File Offset: 0x000E42CC
		internal GrabHandleGlyph(Rectangle controlBounds, GrabHandleGlyphType type, Behavior behavior, bool primarySelection) : base(behavior)
		{
			this.isPrimary = primarySelection;
			this.hitTestCursor = Cursors.Default;
			this.rules = SelectionRules.None;
			switch (type)
			{
			case GrabHandleGlyphType.UpperLeft:
				this.bounds = new Rectangle(controlBounds.X + DesignerUtils.HANDLEOVERLAP - DesignerUtils.HANDLESIZE, controlBounds.Y + DesignerUtils.HANDLEOVERLAP - DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
				this.hitTestCursor = Cursors.SizeNWSE;
				this.rules = (SelectionRules.TopSizeable | SelectionRules.LeftSizeable);
				break;
			case GrabHandleGlyphType.UpperRight:
				this.bounds = new Rectangle(controlBounds.Right - DesignerUtils.HANDLEOVERLAP, controlBounds.Y + DesignerUtils.HANDLEOVERLAP - DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
				this.hitTestCursor = Cursors.SizeNESW;
				this.rules = (SelectionRules.TopSizeable | SelectionRules.RightSizeable);
				break;
			case GrabHandleGlyphType.LowerLeft:
				this.bounds = new Rectangle(controlBounds.X + DesignerUtils.HANDLEOVERLAP - DesignerUtils.HANDLESIZE, controlBounds.Bottom - DesignerUtils.HANDLEOVERLAP, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
				this.hitTestCursor = Cursors.SizeNESW;
				this.rules = (SelectionRules.BottomSizeable | SelectionRules.LeftSizeable);
				break;
			case GrabHandleGlyphType.LowerRight:
				this.bounds = new Rectangle(controlBounds.Right - DesignerUtils.HANDLEOVERLAP, controlBounds.Bottom - DesignerUtils.HANDLEOVERLAP, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
				this.hitTestCursor = Cursors.SizeNWSE;
				this.rules = (SelectionRules.BottomSizeable | SelectionRules.RightSizeable);
				break;
			case GrabHandleGlyphType.MiddleTop:
				if (controlBounds.Width >= 2 * DesignerUtils.HANDLEOVERLAP + 2 * DesignerUtils.HANDLESIZE)
				{
					this.bounds = new Rectangle(controlBounds.X + controlBounds.Width / 2 - DesignerUtils.HANDLESIZE / 2, controlBounds.Y + DesignerUtils.HANDLEOVERLAP - DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
					this.hitTestCursor = Cursors.SizeNS;
					this.rules = SelectionRules.TopSizeable;
				}
				break;
			case GrabHandleGlyphType.MiddleBottom:
				if (controlBounds.Width >= 2 * DesignerUtils.HANDLEOVERLAP + 2 * DesignerUtils.HANDLESIZE)
				{
					this.bounds = new Rectangle(controlBounds.X + controlBounds.Width / 2 - DesignerUtils.HANDLESIZE / 2, controlBounds.Bottom - DesignerUtils.HANDLEOVERLAP, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
					this.hitTestCursor = Cursors.SizeNS;
					this.rules = SelectionRules.BottomSizeable;
				}
				break;
			case GrabHandleGlyphType.MiddleLeft:
				if (controlBounds.Height >= 2 * DesignerUtils.HANDLEOVERLAP + 2 * DesignerUtils.HANDLESIZE)
				{
					this.bounds = new Rectangle(controlBounds.X + DesignerUtils.HANDLEOVERLAP - DesignerUtils.HANDLESIZE, controlBounds.Y + controlBounds.Height / 2 - DesignerUtils.HANDLESIZE / 2, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
					this.hitTestCursor = Cursors.SizeWE;
					this.rules = SelectionRules.LeftSizeable;
				}
				break;
			case GrabHandleGlyphType.MiddleRight:
				if (controlBounds.Height >= 2 * DesignerUtils.HANDLEOVERLAP + 2 * DesignerUtils.HANDLESIZE)
				{
					this.bounds = new Rectangle(controlBounds.Right - DesignerUtils.HANDLEOVERLAP, controlBounds.Y + controlBounds.Height / 2 - DesignerUtils.HANDLESIZE / 2, DesignerUtils.HANDLESIZE, DesignerUtils.HANDLESIZE);
					this.hitTestCursor = Cursors.SizeWE;
					this.rules = SelectionRules.RightSizeable;
				}
				break;
			}
			this.hitBounds = this.bounds;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000E6418 File Offset: 0x000E4618
		public override void Paint(PaintEventArgs pe)
		{
			DesignerUtils.DrawGrabHandle(pe.Graphics, this.bounds, this.isPrimary, this);
		}

		// Token: 0x04001ACE RID: 6862
		private bool isPrimary;
	}
}
