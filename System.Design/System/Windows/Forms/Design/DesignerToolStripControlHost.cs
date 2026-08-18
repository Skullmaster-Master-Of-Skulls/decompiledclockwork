using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200021F RID: 543
	internal class DesignerToolStripControlHost : ToolStripControlHost, IComponent, IDisposable
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x00067537 File Offset: 0x00066537
		public DesignerToolStripControlHost(Control c) : base(c)
		{
			base.Margin = Padding.Empty;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0006754B File Offset: 0x0006654B
		protected override Size DefaultSize
		{
			get
			{
				return new Size(92, 22);
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00067558 File Offset: 0x00066558
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

		// Token: 0x06001468 RID: 5224 RVA: 0x000675FC File Offset: 0x000665FC
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

		// Token: 0x06001469 RID: 5225 RVA: 0x00067634 File Offset: 0x00066634
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

		// Token: 0x04001206 RID: 4614
		private BehaviorService b;

		// Token: 0x04001207 RID: 4615
		internal ToolStrip parent;
	}
}
