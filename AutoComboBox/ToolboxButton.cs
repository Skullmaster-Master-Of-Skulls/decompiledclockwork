using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000BD RID: 189
	public class ToolboxButton : Button
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0003A044 File Offset: 0x00039044
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0003A05C File Offset: 0x0003905C
		public int OverImageIndex
		{
			get
			{
				return this.overImageIndex;
			}
			set
			{
				this.overImageIndex = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0003A068 File Offset: 0x00039068
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0003A080 File Offset: 0x00039080
		public bool ShowText
		{
			get
			{
				return this.showText;
			}
			set
			{
				this.showText = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0003A094 File Offset: 0x00039094
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0003A0AC File Offset: 0x000390AC
		public ShowMode ShowImageText
		{
			get
			{
				return this.showImageText;
			}
			set
			{
				this.showImageText = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0003A0C0 File Offset: 0x000390C0
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0003A0DD File Offset: 0x000390DD
		public int Padding_Top
		{
			get
			{
				return this.padding.Top;
			}
			set
			{
				this.padding.Top = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0003A0F4 File Offset: 0x000390F4
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0003A111 File Offset: 0x00039111
		public int Padding_Bottom
		{
			get
			{
				return this.padding.Bottom;
			}
			set
			{
				this.padding.Bottom = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0003A128 File Offset: 0x00039128
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0003A145 File Offset: 0x00039145
		public int Padding_Left
		{
			get
			{
				return this.padding.Left;
			}
			set
			{
				this.padding.Left = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0003A15C File Offset: 0x0003915C
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0003A179 File Offset: 0x00039179
		public int Padding_Right
		{
			get
			{
				return this.padding.Right;
			}
			set
			{
				this.padding.Right = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0003A190 File Offset: 0x00039190
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0003A1A8 File Offset: 0x000391A8
		public bool ShowBorder
		{
			get
			{
				return this.showBorder;
			}
			set
			{
				this.showBorder = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0003A1BC File Offset: 0x000391BC
		public ToolboxButton()
		{
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			this.overImageIndex = -1;
			this.showText = true;
			this.displayMode = ToolboxButton.DisplayMode.normal;
			this.showImageText = ShowMode.nothing;
			this.padding = new EdgePadding(2, 2, 2, 2);
			base.ImageAlign = ContentAlignment.TopCenter;
			this.showBorder = false;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0003A238 File Offset: 0x00039238
		private void DrawGradientOverlayBox(Graphics g, Color c1, Color c2, ref Rectangle r, bool drawBorder, int borderWidth)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(r, c1, c2, 45f, false))
			{
				g.FillRectangle(linearGradientBrush, r);
			}
			if (drawBorder)
			{
				r.Width -= borderWidth;
				r.Height -= borderWidth;
				this.DrawBorder(g, r, borderWidth);
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0003A2D0 File Offset: 0x000392D0
		private void DrawBorder(Graphics g, Rectangle r, int borderWidth)
		{
			GraphicsPath roundedRectanglePath = MyGraphicsRoutines.GetRoundedRectanglePath(r, 1);
			using (Pen pen = new Pen(Color.FromArgb(50, 0, 0, 0)))
			{
				g.DrawPath(pen, roundedRectanglePath);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0003A324 File Offset: 0x00039324
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			using (Brush brush = new SolidBrush(this.BackColor))
			{
				e.Graphics.FillRectangle(brush, clientRectangle);
			}
			int num = 1;
			int num2;
			if (base.Enabled && (this.displayMode & ToolboxButton.DisplayMode.pushed_down) == ToolboxButton.DisplayMode.pushed_down)
			{
				Color c = Color.FromArgb(100, 200, 200, 200);
				Color c2 = Color.FromArgb(100, 150, 150, 150);
				num2 = 2;
				this.DrawGradientOverlayBox(e.Graphics, c, c2, ref clientRectangle, true, num);
			}
			else if (base.Enabled && (this.displayMode & ToolboxButton.DisplayMode.mouse_over) == ToolboxButton.DisplayMode.mouse_over)
			{
				Color c = Color.FromArgb(50, 255, 255, 255);
				Color c2 = Color.FromArgb(50, 200, 200, 200);
				num2 = 0;
				this.DrawGradientOverlayBox(e.Graphics, c, c2, ref clientRectangle, true, num);
			}
			else
			{
				num2 = 0;
				if (this.showBorder)
				{
					clientRectangle.Width -= num;
					clientRectangle.Height -= num;
					this.DrawBorder(e.Graphics, clientRectangle, num);
				}
			}
			Image image = this.GetImage();
			Rectangle rectangle = new Rectangle(clientRectangle.Location, clientRectangle.Size);
			rectangle.Inflate(-this.padding.TotalWidth, -this.padding.TotalHeight);
			int num3 = rectangle.Y;
			int x = rectangle.X;
			if ((this.showImageText & ShowMode.image) == ShowMode.image && image != null)
			{
				int num5;
				int x2;
				int num6;
				int num7;
				switch (base.ImageAlign)
				{
				case ContentAlignment.TopLeft:
				case ContentAlignment.TopCenter:
				case ContentAlignment.TopRight:
				{
					int num4 = rectangle.Width - image.Width;
					if (num4 > 0)
					{
						num4 = Convert.ToInt32(Convert.ToDouble(num4) / 2.0);
					}
					else
					{
						num4 = 0;
					}
					num5 = x + num4;
					x2 = x;
					num6 = num3;
					num7 = num3 + image.Height + 2;
					goto IL_269;
				}
				}
				num5 = x;
				x2 = x + image.Width + 2;
				num6 = num3;
				num7 = num3;
				rectangle.X = x2;
				IL_269:
				if ((this.displayMode & ToolboxButton.DisplayMode.mouse_over) != ToolboxButton.DisplayMode.mouse_over)
				{
					ColorMatrix colorMatrix = new ColorMatrix();
					colorMatrix.Matrix00 = (colorMatrix.Matrix11 = (colorMatrix.Matrix22 = (colorMatrix.Matrix44 = 1f)));
					colorMatrix.Matrix33 = 0.5f;
					ImageAttributes imageAttributes = new ImageAttributes();
					imageAttributes.SetColorMatrix(colorMatrix);
					Rectangle destRect = new Rectangle(num5 + num2, num6 + num2, image.Width, image.Height);
					e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				}
				else
				{
					e.Graphics.DrawImageUnscaled(image, num5 + num2, num6 + num2);
				}
				num3 = num7;
			}
			if (this.showText)
			{
				StringFormat stringFormat = new StringFormat();
				if (base.ImageAlign == ContentAlignment.TopCenter || base.ImageAlign == ContentAlignment.BottomCenter || base.ImageAlign == ContentAlignment.MiddleCenter)
				{
					stringFormat.Alignment = StringAlignment.Center;
				}
				else if (image != null)
				{
					int num8 = image.Height - Convert.ToInt32(this.Font.SizeInPoints);
					if (num8 > 0)
					{
						num3 += Convert.ToInt32(Convert.ToDouble(num8) / 2.0);
					}
				}
				using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
				{
					RectangleF layoutRectangle = new RectangleF((float)(rectangle.X + num2), (float)(num3 + num2), (float)rectangle.Width, (float)rectangle.Height);
					e.Graphics.DrawString(this.Text, this.Font, solidBrush, layoutRectangle, stringFormat);
				}
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0003A798 File Offset: 0x00039798
		private Image GetImage()
		{
			Image result;
			if (base.Image != null)
			{
				result = base.Image;
			}
			else if (base.ImageIndex >= 0 && base.ImageList != null && base.ImageIndex < base.ImageList.Images.Count)
			{
				result = base.ImageList.Images[base.ImageIndex];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0003A80D File Offset: 0x0003980D
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0003A810 File Offset: 0x00039810
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.displayMode |= ToolboxButton.DisplayMode.mouse_over;
			base.Invalidate();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0003A830 File Offset: 0x00039830
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.displayMode &= ~ToolboxButton.DisplayMode.mouse_over;
			base.Invalidate();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0003A851 File Offset: 0x00039851
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.displayMode |= ToolboxButton.DisplayMode.pushed_down;
			base.Invalidate();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0003A871 File Offset: 0x00039871
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.displayMode &= ~ToolboxButton.DisplayMode.pushed_down;
			base.Invalidate();
		}

		// Token: 0x0400058D RID: 1421
		private int overImageIndex;

		// Token: 0x0400058E RID: 1422
		private bool showText;

		// Token: 0x0400058F RID: 1423
		private ToolboxButton.DisplayMode displayMode;

		// Token: 0x04000590 RID: 1424
		private ShowMode showImageText;

		// Token: 0x04000591 RID: 1425
		private EdgePadding padding;

		// Token: 0x04000592 RID: 1426
		private bool showBorder = false;

		// Token: 0x020000BE RID: 190
		[Flags]
		private enum DisplayMode
		{
			// Token: 0x04000594 RID: 1428
			normal = 0,
			// Token: 0x04000595 RID: 1429
			mouse_over = 1,
			// Token: 0x04000596 RID: 1430
			pushed_down = 2
		}
	}
}
