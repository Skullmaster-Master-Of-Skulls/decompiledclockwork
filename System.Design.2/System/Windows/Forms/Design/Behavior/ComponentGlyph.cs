using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000377 RID: 887
	public class ComponentGlyph : Glyph
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x000E1EC3 File Offset: 0x000E00C3
		public ComponentGlyph(IComponent relatedComponent, Behavior behavior) : base(behavior)
		{
			this.relatedComponent = relatedComponent;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000E1ED3 File Offset: 0x000E00D3
		public ComponentGlyph(IComponent relatedComponent) : base(null)
		{
			this.relatedComponent = relatedComponent;
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000E1EE3 File Offset: 0x000E00E3
		public IComponent RelatedComponent
		{
			get
			{
				return this.relatedComponent;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00003598 File Offset: 0x00001798
		public override Cursor GetHitTest(Point p)
		{
			return null;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x00003937 File Offset: 0x00001B37
		public override void Paint(PaintEventArgs pe)
		{
		}

		// Token: 0x04001A6F RID: 6767
		private IComponent relatedComponent;
	}
}
