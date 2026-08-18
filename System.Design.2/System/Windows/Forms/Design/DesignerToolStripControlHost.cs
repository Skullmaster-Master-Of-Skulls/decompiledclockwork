using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D8 RID: 728
	internal class DesignerToolStripControlHost : ToolStripControlHost, IComponent, IDisposable
	{
		// Token: 0x06001CF1 RID: 7409 RVA: 0x000AE2EF File Offset: 0x000AC4EF
		public DesignerToolStripControlHost(Control c) : base(c)
		{
			base.Margin = Padding.Empty;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x000AE303 File Offset: 0x000AC503
		protected override Size DefaultSize
		{
			get
			{
				return new Size(92, 22);
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000AE310 File Offset: 0x000AC510
		internal GlyphCollection GetGlyphs(ToolStrip parent, GlyphCollection glyphs, Behavior standardBehavior)
		{
			if (this.b == null)
			{
				this.b = (BehaviorService)parent.Site.GetService(typeof(BehaviorService));
			}
			Point pos = this.b.ControlToAdornerWindow(base.Parent);
			Rectangle bounds = this.Bounds;
			bounds.Offset(pos);
			bounds.Inflate(-2, -2);
			glyphs.Add(new MiniLockedBorderGlyph(bounds, SelectionBorderGlyphType.Top, standardBehavior, true));
			glyphs.Add(new MiniLockedBorderGlyph(bounds, SelectionBorderGlyphType.Bottom, standardBehavior, true));
			glyphs.Add(new MiniLockedBorderGlyph(bounds, SelectionBorderGlyphType.Left, standardBehavior, true));
			glyphs.Add(new MiniLockedBorderGlyph(bounds, SelectionBorderGlyphType.Right, standardBehavior, true));
			return glyphs;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000AE3B4 File Offset: 0x000AC5B4
		internal void RefreshSelectionGlyph()
		{
			ToolStrip toolStrip = base.Control as ToolStrip;
			if (toolStrip != null)
			{
				ToolStripTemplateNode.MiniToolStripRenderer miniToolStripRenderer = toolStrip.Renderer as ToolStripTemplateNode.MiniToolStripRenderer;
				if (miniToolStripRenderer != null)
				{
					miniToolStripRenderer.State = 0;
					toolStrip.Invalidate();
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000AE3EC File Offset: 0x000AC5EC
		internal void SelectControl()
		{
			ToolStrip toolStrip = base.Control as ToolStrip;
			if (toolStrip != null)
			{
				ToolStripTemplateNode.MiniToolStripRenderer miniToolStripRenderer = toolStrip.Renderer as ToolStripTemplateNode.MiniToolStripRenderer;
				if (miniToolStripRenderer != null)
				{
					miniToolStripRenderer.State = 1;
					toolStrip.Invalidate();
				}
			}
		}

		// Token: 0x04001726 RID: 5926
		private BehaviorService b;

		// Token: 0x04001727 RID: 5927
		internal ToolStrip parent;
	}
}
