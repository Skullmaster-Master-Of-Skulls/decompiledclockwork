using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000376 RID: 886
	public class ControlBodyGlyph : ComponentGlyph
	{
		// Token: 0x0600247B RID: 9339 RVA: 0x000E1E2D File Offset: 0x000E002D
		public ControlBodyGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, ControlDesigner designer) : base(relatedComponent, new ControlDesigner.TransparentBehavior(designer))
		{
			this.bounds = bounds;
			this.hitTestCursor = cursor;
			this.component = relatedComponent;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000E1E52 File Offset: 0x000E0052
		public ControlBodyGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, Behavior behavior) : base(relatedComponent, behavior)
		{
			this.bounds = bounds;
			this.hitTestCursor = cursor;
			this.component = relatedComponent;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000E1E74 File Offset: 0x000E0074
		public override Cursor GetHitTest(Point p)
		{
			bool flag = !(this.component is Control) || ((Control)this.component).Visible;
			if (flag && this.bounds.Contains(p))
			{
				return this.hitTestCursor;
			}
			return null;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000E1EBB File Offset: 0x000E00BB
		public override Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x04001A6C RID: 6764
		private Rectangle bounds;

		// Token: 0x04001A6D RID: 6765
		private Cursor hitTestCursor;

		// Token: 0x04001A6E RID: 6766
		private IComponent component;
	}
}
