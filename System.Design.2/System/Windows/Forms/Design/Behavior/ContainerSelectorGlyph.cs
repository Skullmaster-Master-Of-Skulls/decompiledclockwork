using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000379 RID: 889
	internal sealed class ContainerSelectorGlyph : Glyph
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x000E23D0 File Offset: 0x000E05D0
		internal ContainerSelectorGlyph(Rectangle containerBounds, int glyphSize, int glyphOffset, ContainerSelectorBehavior behavior) : base(behavior)
		{
			this.relatedBehavior = behavior;
			this.glyphBounds = new Rectangle(containerBounds.X + glyphOffset, containerBounds.Y - (int)((double)glyphSize * 0.5), glyphSize, glyphSize);
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x000E240D File Offset: 0x000E060D
		public override Rectangle Bounds
		{
			get
			{
				return this.glyphBounds;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x000E2415 File Offset: 0x000E0615
		public Behavior RelatedBehavior
		{
			get
			{
				return this.relatedBehavior;
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000E241D File Offset: 0x000E061D
		public override Cursor GetHitTest(Point p)
		{
			if (this.glyphBounds.Contains(p) || this.relatedBehavior.OkToMove)
			{
				return Cursors.SizeAll;
			}
			return null;
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x000E2441 File Offset: 0x000E0641
		private Bitmap MoveGlyph
		{
			get
			{
				if (this.glyph == null)
				{
					this.glyph = new Bitmap(typeof(ContainerSelectorGlyph), "MoverGlyph.bmp");
					this.glyph.MakeTransparent();
				}
				return this.glyph;
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000E2476 File Offset: 0x000E0676
		public override void Paint(PaintEventArgs pe)
		{
			pe.Graphics.DrawImage(this.MoveGlyph, this.glyphBounds);
		}

		// Token: 0x04001A76 RID: 6774
		private Rectangle glyphBounds;

		// Token: 0x04001A77 RID: 6775
		private ContainerSelectorBehavior relatedBehavior;

		// Token: 0x04001A78 RID: 6776
		private Bitmap glyph;
	}
}
