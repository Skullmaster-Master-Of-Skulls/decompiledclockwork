using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200038C RID: 908
	internal abstract class SelectionGlyphBase : Glyph
	{
		// Token: 0x0600251C RID: 9500 RVA: 0x000E820E File Offset: 0x000E640E
		internal SelectionGlyphBase(Behavior behavior) : base(behavior)
		{
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000E8217 File Offset: 0x000E6417
		public SelectionRules SelectionRules
		{
			get
			{
				return this.rules;
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000E821F File Offset: 0x000E641F
		public override Cursor GetHitTest(Point p)
		{
			if (this.hitBounds.Contains(p))
			{
				return this.hitTestCursor;
			}
			return null;
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000E8237 File Offset: 0x000E6437
		public Cursor HitTestCursor
		{
			get
			{
				return this.hitTestCursor;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000E823F File Offset: 0x000E643F
		public override Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00003937 File Offset: 0x00001B37
		public override void Paint(PaintEventArgs pe)
		{
		}

		// Token: 0x04001AF8 RID: 6904
		protected Rectangle bounds;

		// Token: 0x04001AF9 RID: 6905
		protected Rectangle hitBounds;

		// Token: 0x04001AFA RID: 6906
		protected Cursor hitTestCursor;

		// Token: 0x04001AFB RID: 6907
		protected SelectionRules rules;
	}
}
