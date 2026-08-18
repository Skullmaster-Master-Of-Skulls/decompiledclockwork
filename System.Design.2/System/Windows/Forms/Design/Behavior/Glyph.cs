using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200037F RID: 895
	public abstract class Glyph
	{
		// Token: 0x060024EC RID: 9452 RVA: 0x000E6024 File Offset: 0x000E4224
		protected Glyph(Behavior behavior)
		{
			this.behavior = behavior;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000E6033 File Offset: 0x000E4233
		public virtual Behavior Behavior
		{
			get
			{
				return this.behavior;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x0009B82F File Offset: 0x00099A2F
		public virtual Rectangle Bounds
		{
			get
			{
				return Rectangle.Empty;
			}
		}

		// Token: 0x060024EF RID: 9455
		public abstract Cursor GetHitTest(Point p);

		// Token: 0x060024F0 RID: 9456
		public abstract void Paint(PaintEventArgs pe);

		// Token: 0x060024F1 RID: 9457 RVA: 0x000E603B File Offset: 0x000E423B
		protected void SetBehavior(Behavior behavior)
		{
			this.behavior = behavior;
		}

		// Token: 0x04001AC9 RID: 6857
		private Behavior behavior;
	}
}
