using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200037B RID: 891
	internal sealed class DesignerActionGlyph : Glyph
	{
		// Token: 0x060024A3 RID: 9379 RVA: 0x000E2638 File Offset: 0x000E0838
		public DesignerActionGlyph(DesignerActionBehavior behavior, Adorner adorner) : this(behavior, adorner, Rectangle.Empty, null)
		{
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000E2648 File Offset: 0x000E0848
		public DesignerActionGlyph(DesignerActionBehavior behavior, Rectangle alternativeBounds, Control alternativeParent) : this(behavior, null, alternativeBounds, alternativeParent)
		{
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000E2654 File Offset: 0x000E0854
		private DesignerActionGlyph(DesignerActionBehavior behavior, Adorner adorner, Rectangle alternativeBounds, Control alternativeParent) : base(behavior)
		{
			this.adorner = adorner;
			this.alternativeBounds = alternativeBounds;
			this.alternativeParent = alternativeParent;
			this.Invalidate();
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000E2684 File Offset: 0x000E0884
		public override Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000E268C File Offset: 0x000E088C
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x000E2694 File Offset: 0x000E0894
		public DockStyle DockEdge
		{
			get
			{
				return this.dockStyle;
			}
			set
			{
				if (this.dockStyle != value)
				{
					this.dockStyle = value;
				}
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000E26A6 File Offset: 0x000E08A6
		public bool IsInComponentTray
		{
			get
			{
				return this.adorner == null;
			}
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000E26B1 File Offset: 0x000E08B1
		public override Cursor GetHitTest(Point p)
		{
			if (this.bounds.Contains(p))
			{
				this.MouseOver = true;
				return Cursors.Default;
			}
			this.MouseOver = false;
			return null;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x000E26D8 File Offset: 0x000E08D8
		private Image GlyphImageClosed
		{
			get
			{
				if (this.glyphImageClosed == null)
				{
					this.glyphImageClosed = new Bitmap(typeof(DesignerActionGlyph), "Close_left.bmp");
					this.glyphImageClosed.MakeTransparent(Color.Magenta);
					if (DpiHelper.IsScalingRequired)
					{
						DpiHelper.ScaleBitmapLogicalToDevice(ref this.glyphImageClosed, 0);
					}
				}
				return this.glyphImageClosed;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000E2730 File Offset: 0x000E0930
		private Image GlyphImageOpened
		{
			get
			{
				if (this.glyphImageOpened == null)
				{
					this.glyphImageOpened = new Bitmap(typeof(DesignerActionGlyph), "Open_left.bmp");
					this.glyphImageOpened.MakeTransparent(Color.Magenta);
					if (DpiHelper.IsScalingRequired)
					{
						DpiHelper.ScaleBitmapLogicalToDevice(ref this.glyphImageOpened, 0);
					}
				}
				return this.glyphImageOpened;
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000E2788 File Offset: 0x000E0988
		internal void InvalidateOwnerLocation()
		{
			if (this.alternativeParent != null)
			{
				this.alternativeParent.Invalidate(this.bounds);
				return;
			}
			this.adorner.Invalidate(this.bounds);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000E27B8 File Offset: 0x000E09B8
		internal void Invalidate()
		{
			IComponent relatedComponent = ((DesignerActionBehavior)this.Behavior).RelatedComponent;
			Point point = Point.Empty;
			Control control = relatedComponent as Control;
			if (control != null && !(relatedComponent is ToolStripDropDown) && this.adorner != null)
			{
				point = this.adorner.BehaviorService.ControlToAdornerWindow(control);
				point.X += control.Width;
			}
			else
			{
				ComponentTray componentTray = this.alternativeParent as ComponentTray;
				if (componentTray != null)
				{
					ComponentTray.TrayControl trayControlFromComponent = componentTray.GetTrayControlFromComponent(relatedComponent);
					if (trayControlFromComponent != null)
					{
						this.alternativeBounds = trayControlFromComponent.Bounds;
					}
				}
				Rectangle boundsForNoResizeSelectionType = DesignerUtils.GetBoundsForNoResizeSelectionType(this.alternativeBounds, SelectionBorderGlyphType.Top);
				point.X = boundsForNoResizeSelectionType.Right;
				point.Y = boundsForNoResizeSelectionType.Top;
			}
			point.X -= this.GlyphImageOpened.Width + 5;
			point.Y -= this.GlyphImageOpened.Height - 2;
			this.bounds = new Rectangle(point.X, point.Y, this.GlyphImageOpened.Width, this.GlyphImageOpened.Height);
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000E28D6 File Offset: 0x000E0AD6
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x000E28DE File Offset: 0x000E0ADE
		private bool MouseOver
		{
			get
			{
				return this.mouseOver;
			}
			set
			{
				if (this.mouseOver != value)
				{
					this.mouseOver = value;
					this.InvalidateOwnerLocation();
				}
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000E28F8 File Offset: 0x000E0AF8
		public override void Paint(PaintEventArgs pe)
		{
			if (this.Behavior is DesignerActionBehavior)
			{
				if (this.insidePaint)
				{
					return;
				}
				IComponent lastPanelComponent = ((DesignerActionBehavior)this.Behavior).ParentUI.LastPanelComponent;
				IComponent relatedComponent = ((DesignerActionBehavior)this.Behavior).RelatedComponent;
				Image image;
				if (lastPanelComponent != null && lastPanelComponent == relatedComponent)
				{
					image = this.GlyphImageOpened;
				}
				else
				{
					image = this.GlyphImageClosed;
				}
				try
				{
					this.insidePaint = true;
					pe.Graphics.DrawImage(image, this.bounds.Left, this.bounds.Top);
					if (this.MouseOver || (lastPanelComponent != null && lastPanelComponent == relatedComponent))
					{
						pe.Graphics.FillRectangle(DesignerUtils.HoverBrush, Rectangle.Inflate(this.bounds, -1, -1));
					}
				}
				finally
				{
					this.insidePaint = false;
				}
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000E29CC File Offset: 0x000E0BCC
		internal void UpdateAlternativeBounds(Rectangle newBounds)
		{
			this.alternativeBounds = newBounds;
			this.Invalidate();
		}

		// Token: 0x04001A7E RID: 6782
		internal const int CONTROLOVERLAP_X = 5;

		// Token: 0x04001A7F RID: 6783
		internal const int CONTROLOVERLAP_Y = 2;

		// Token: 0x04001A80 RID: 6784
		private Rectangle bounds;

		// Token: 0x04001A81 RID: 6785
		private Adorner adorner;

		// Token: 0x04001A82 RID: 6786
		private bool mouseOver;

		// Token: 0x04001A83 RID: 6787
		private Rectangle alternativeBounds = Rectangle.Empty;

		// Token: 0x04001A84 RID: 6788
		private Control alternativeParent;

		// Token: 0x04001A85 RID: 6789
		private bool insidePaint;

		// Token: 0x04001A86 RID: 6790
		private DockStyle dockStyle;

		// Token: 0x04001A87 RID: 6791
		private Bitmap glyphImageClosed;

		// Token: 0x04001A88 RID: 6792
		private Bitmap glyphImageOpened;
	}
}
