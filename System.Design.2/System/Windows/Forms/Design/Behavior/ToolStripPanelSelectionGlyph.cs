using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000396 RID: 918
	internal sealed class ToolStripPanelSelectionGlyph : ControlBodyGlyph
	{
		// Token: 0x06002566 RID: 9574 RVA: 0x000EAA74 File Offset: 0x000E8C74
		internal ToolStripPanelSelectionGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, IServiceProvider provider, ToolStripPanelSelectionBehavior behavior) : base(bounds, cursor, relatedComponent, behavior)
		{
			this.relatedBehavior = behavior;
			this.provider = provider;
			this.relatedPanel = (relatedComponent as ToolStripPanel);
			this.behaviorService = (BehaviorService)provider.GetService(typeof(BehaviorService));
			if (this.behaviorService == null)
			{
				return;
			}
			if ((IDesignerHost)provider.GetService(typeof(IDesignerHost)) == null)
			{
				return;
			}
			this.UpdateGlyph();
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000EAAFD File Offset: 0x000E8CFD
		// (set) Token: 0x06002568 RID: 9576 RVA: 0x000EAB05 File Offset: 0x000E8D05
		public bool IsExpanded
		{
			get
			{
				return this.isExpanded;
			}
			set
			{
				if (value != this.isExpanded)
				{
					this.isExpanded = value;
					this.UpdateGlyph();
				}
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000EAB20 File Offset: 0x000E8D20
		public void UpdateGlyph()
		{
			if (this.behaviorService != null)
			{
				Rectangle bounds = this.behaviorService.ControlRectInAdornerWindow(this.relatedPanel);
				this.glyphBounds = Rectangle.Empty;
				ToolStripContainer toolStripContainer = this.relatedPanel.Parent as ToolStripContainer;
				if (toolStripContainer != null)
				{
					this.baseParent = toolStripContainer.Parent;
				}
				if (this.image != null)
				{
					this.image.Dispose();
					this.image = null;
				}
				if (!this.isExpanded)
				{
					this.CollapseGlyph(bounds);
					return;
				}
				this.ExpandGlyph(bounds);
			}
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000EABA4 File Offset: 0x000E8DA4
		private void SetBitmap(string fileName)
		{
			this.image = new Bitmap(typeof(ToolStripPanelSelectionGlyph), fileName);
			this.image.MakeTransparent(Color.Magenta);
			if (DpiHelper.IsScalingRequired)
			{
				Bitmap bitmap;
				if (this.image.Width > this.image.Height)
				{
					this.imageWidth = DpiHelper.LogicalToDeviceUnitsX(50);
					this.imageHeight = DpiHelper.LogicalToDeviceUnitsY(6);
					bitmap = DpiHelper.CreateResizedBitmap(this.image, new Size(this.imageWidth, this.imageHeight));
				}
				else
				{
					this.imageHeight = DpiHelper.LogicalToDeviceUnitsX(6);
					this.imageWidth = DpiHelper.LogicalToDeviceUnitsY(50);
					bitmap = DpiHelper.CreateResizedBitmap(this.image, new Size(this.imageHeight, this.imageWidth));
				}
				if (bitmap != null)
				{
					this.image.Dispose();
					this.image = bitmap;
				}
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000EAC80 File Offset: 0x000E8E80
		private void CollapseGlyph(Rectangle bounds)
		{
			switch (this.relatedPanel.Dock)
			{
			case DockStyle.Top:
			{
				this.SetBitmap("topopen.bmp");
				int num = (bounds.Width - this.imageWidth) / 2;
				if (num > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X + num, bounds.Y + bounds.Height, this.imageWidth, this.imageHeight);
					return;
				}
				break;
			}
			case DockStyle.Bottom:
			{
				this.SetBitmap("bottomopen.bmp");
				int num = (bounds.Width - this.imageWidth) / 2;
				if (num > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X + num, bounds.Y - this.imageHeight, this.imageWidth, this.imageHeight);
					return;
				}
				break;
			}
			case DockStyle.Left:
			{
				this.SetBitmap("leftopen.bmp");
				int num2 = (bounds.Height - this.imageWidth) / 2;
				if (num2 > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X + bounds.Width, bounds.Y + num2, this.imageHeight, this.imageWidth);
					return;
				}
				break;
			}
			case DockStyle.Right:
			{
				this.SetBitmap("rightopen.bmp");
				int num2 = (bounds.Height - this.imageWidth) / 2;
				if (num2 > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X - this.imageHeight, bounds.Y + num2, this.imageHeight, this.imageWidth);
					return;
				}
				break;
			}
			default:
				throw new Exception(SR.GetString("ToolStripPanelGlyphUnsupportedDock"));
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000EAE14 File Offset: 0x000E9014
		private void ExpandGlyph(Rectangle bounds)
		{
			switch (this.relatedPanel.Dock)
			{
			case DockStyle.Top:
			{
				this.SetBitmap("topclose.bmp");
				int num = (bounds.Width - this.imageWidth) / 2;
				if (num > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X + num, bounds.Y + bounds.Height, this.imageWidth, this.imageHeight);
					return;
				}
				break;
			}
			case DockStyle.Bottom:
			{
				this.SetBitmap("bottomclose.bmp");
				int num = (bounds.Width - this.imageWidth) / 2;
				if (num > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X + num, bounds.Y - this.imageHeight, this.imageWidth, this.imageHeight);
					return;
				}
				break;
			}
			case DockStyle.Left:
			{
				this.SetBitmap("leftclose.bmp");
				int num2 = (bounds.Height - this.imageWidth) / 2;
				if (num2 > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X + bounds.Width, bounds.Y + num2, this.imageHeight, this.imageWidth);
					return;
				}
				break;
			}
			case DockStyle.Right:
			{
				this.SetBitmap("rightclose.bmp");
				int num2 = (bounds.Height - this.imageWidth) / 2;
				if (num2 > 0)
				{
					this.glyphBounds = new Rectangle(bounds.X - this.imageHeight, bounds.Y + num2, this.imageHeight, this.imageWidth);
					return;
				}
				break;
			}
			default:
				throw new Exception(SR.GetString("ToolStripPanelGlyphUnsupportedDock"));
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000EAFA5 File Offset: 0x000E91A5
		public override Rectangle Bounds
		{
			get
			{
				return this.glyphBounds;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000EAFB0 File Offset: 0x000E91B0
		public override Cursor GetHitTest(Point p)
		{
			if (this.behaviorService != null && this.baseParent != null)
			{
				Rectangle rectangle = this.behaviorService.ControlRectInAdornerWindow(this.baseParent);
				if (this.glyphBounds != Rectangle.Empty && rectangle.Contains(this.glyphBounds) && this.glyphBounds.Contains(p))
				{
					return Cursors.Hand;
				}
			}
			return null;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000EB018 File Offset: 0x000E9218
		public override void Paint(PaintEventArgs pe)
		{
			if (this.behaviorService != null && this.baseParent != null)
			{
				Rectangle rectangle = this.behaviorService.ControlRectInAdornerWindow(this.baseParent);
				if (this.relatedPanel.Visible && this.image != null && this.glyphBounds != Rectangle.Empty && rectangle.Contains(this.glyphBounds))
				{
					pe.Graphics.DrawImage(this.image, this.glyphBounds.Left, this.glyphBounds.Top);
				}
			}
		}

		// Token: 0x04001B49 RID: 6985
		private ToolStripPanel relatedPanel;

		// Token: 0x04001B4A RID: 6986
		private Rectangle glyphBounds;

		// Token: 0x04001B4B RID: 6987
		private IServiceProvider provider;

		// Token: 0x04001B4C RID: 6988
		private ToolStripPanelSelectionBehavior relatedBehavior;

		// Token: 0x04001B4D RID: 6989
		private Bitmap image;

		// Token: 0x04001B4E RID: 6990
		private Control baseParent;

		// Token: 0x04001B4F RID: 6991
		private BehaviorService behaviorService;

		// Token: 0x04001B50 RID: 6992
		private bool isExpanded;

		// Token: 0x04001B51 RID: 6993
		private const int imageWidthOriginal = 50;

		// Token: 0x04001B52 RID: 6994
		private const int imageHeightOriginal = 6;

		// Token: 0x04001B53 RID: 6995
		private int imageWidth = 50;

		// Token: 0x04001B54 RID: 6996
		private int imageHeight = 6;
	}
}
