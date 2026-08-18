using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000B0 RID: 176
	public class MyButton : Button
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x00034F58 File Offset: 0x00033F58
		public MyButton()
		{
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			this.InitializeComponent();
			this.ForeColor = SystemColors.ControlText;
			this.ForeColorHighlight = SystemColors.HotTrack;
			this.BackColor = SystemColors.Control;
			this.BackColorHighlight = SystemColors.ControlLightLight;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00035070 File Offset: 0x00034070
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
				if (this.foregroundBrush != null)
				{
					this.foregroundBrush.Dispose();
				}
				if (this.backgroundBrush != null)
				{
					this.backgroundBrush.Dispose();
				}
				if (this.borderPen != null)
				{
					this.borderPen.Dispose();
				}
				if (this.backgroundHighlightBrush != null)
				{
					this.backgroundHighlightBrush.Dispose();
				}
				if (this.foregroundHighlightBrush != null)
				{
					this.foregroundHighlightBrush.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003512B File Offset: 0x0003412B
		private void InitializeComponent()
		{
			base.Name = "MyButton";
			base.Size = new Size(320, 48);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00035150 File Offset: 0x00034150
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00035175 File Offset: 0x00034175
		public int TitleFontSize
		{
			get
			{
				return (this.titleFontSize < 1) ? 14 : this.titleFontSize;
			}
			set
			{
				this.titleFontSize = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00035188 File Offset: 0x00034188
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x000351A0 File Offset: 0x000341A0
		public bool BackGradientOn
		{
			get
			{
				return this.backGradientOn;
			}
			set
			{
				this.backGradientOn = value;
				this.SetBrush(this.BackColor, this.backGradientOn, ref this.backgroundBrush);
				base.Invalidate();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000351CC File Offset: 0x000341CC
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x000351E4 File Offset: 0x000341E4
		public bool BackHighlightGradientOn
		{
			get
			{
				return this.backHighlightGradientOn;
			}
			set
			{
				this.backHighlightGradientOn = value;
				this.SetBrush(this.backColorHighlight, this.backHighlightGradientOn, ref this.backgroundHighlightBrush);
				base.Invalidate();
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00035210 File Offset: 0x00034210
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00035228 File Offset: 0x00034228
		public Color BackColorHighlight
		{
			get
			{
				return this.backColorHighlight;
			}
			set
			{
				this.backColorHighlight = value;
				this.SetBrush(this.backColorHighlight, this.backHighlightGradientOn, ref this.backgroundHighlightBrush);
				base.Invalidate();
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00035254 File Offset: 0x00034254
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0003526C File Offset: 0x0003426C
		public Color ForeColorHighlight
		{
			get
			{
				return this.foreColorHighlight;
			}
			set
			{
				this.foreColorHighlight = value;
				if (this.foregroundHighlightBrush != null)
				{
					this.foregroundHighlightBrush.Dispose();
				}
				this.foregroundHighlightBrush = new SolidBrush(this.foreColorHighlight);
				base.Invalidate();
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x000352B4 File Offset: 0x000342B4
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x000352CC File Offset: 0x000342CC
		public MyBorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				this.borderStyle = value;
				switch (this.borderStyle)
				{
				case MyBorderStyle.none:
					this.borderWidth = 0;
					break;
				case MyBorderStyle.roundedBox:
					this.borderWidth = 4;
					break;
				default:
					this.borderWidth = 0;
					break;
				}
				base.Invalidate();
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0003531C File Offset: 0x0003431C
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00035334 File Offset: 0x00034334
		public MyBorderStyle BorderStyleHighlight
		{
			get
			{
				return this.borderStyleHighlight;
			}
			set
			{
				this.borderStyleHighlight = value;
				switch (this.borderStyleHighlight)
				{
				case MyBorderStyle.none:
					this.borderWidthHighlight = 0;
					break;
				case MyBorderStyle.roundedBox:
					this.borderWidthHighlight = 4;
					break;
				default:
					this.borderWidthHighlight = 0;
					break;
				}
				base.Invalidate();
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00035384 File Offset: 0x00034384
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0003539C File Offset: 0x0003439C
		public int PadTop
		{
			get
			{
				return this.padTop;
			}
			set
			{
				this.padTop = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000353B0 File Offset: 0x000343B0
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x000353C8 File Offset: 0x000343C8
		public int PadBottom
		{
			get
			{
				return this.padBottom;
			}
			set
			{
				this.padBottom = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000353DC File Offset: 0x000343DC
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x000353F4 File Offset: 0x000343F4
		public int PadLeft
		{
			get
			{
				return this.padLeft;
			}
			set
			{
				this.padLeft = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00035408 File Offset: 0x00034408
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00035420 File Offset: 0x00034420
		public int PadRight
		{
			get
			{
				return this.padRight;
			}
			set
			{
				this.padRight = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00035434 File Offset: 0x00034434
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0003544C File Offset: 0x0003444C
		public int PadBetweenImageAndText
		{
			get
			{
				return this.padBetweenImageAndText;
			}
			set
			{
				this.padBetweenImageAndText = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00035460 File Offset: 0x00034460
		private Rectangle ClientRectanglePadded
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				clientRectangle.Offset(this.padLeft + this.borderWidth, this.padTop + this.borderWidth);
				clientRectangle.Inflate(-(this.padLeft + this.padRight + this.borderWidth + this.borderWidth), -(this.padTop + this.padBottom + this.borderWidth + this.borderWidth));
				return clientRectangle;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000354DC File Offset: 0x000344DC
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x000354F4 File Offset: 0x000344F4
		public int BackGradientIncrement
		{
			get
			{
				return this.backGradientIncrement;
			}
			set
			{
				this.backGradientIncrement = value;
				this.SetBrush(this.BackColor, this.backGradientOn, ref this.backgroundBrush);
				base.Invalidate();
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00035520 File Offset: 0x00034520
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00035538 File Offset: 0x00034538
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (this.foregroundBrush != null)
				{
					this.foregroundBrush.Dispose();
				}
				this.foregroundBrush = new SolidBrush(value);
				base.ForeColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0003557C File Offset: 0x0003457C
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00035594 File Offset: 0x00034594
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.SetBrush(this.BackColor, this.backGradientOn, ref this.backgroundBrush);
				base.Invalidate();
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000355BF File Offset: 0x000345BF
		protected override void OnPaint(PaintEventArgs e)
		{
			this.DrawBackground(e.Graphics);
			this.DrawForeground(e.Graphics);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000355DC File Offset: 0x000345DC
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000355E0 File Offset: 0x000345E0
		private void DrawBackground(Graphics g)
		{
			bool flag = (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left;
			if (this.mouseIsOver && !flag)
			{
				g.FillRectangle(this.backgroundHighlightBrush, base.ClientRectangle);
			}
			else
			{
				g.FillRectangle(this.backgroundBrush, base.ClientRectangle);
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0003563C File Offset: 0x0003463C
		private void DrawForeground(Graphics g)
		{
			Rectangle clientRectanglePadded = this.ClientRectanglePadded;
			Image image;
			if (base.Image != null)
			{
				image = base.Image;
			}
			else if (base.ImageList != null && base.ImageIndex >= 0)
			{
				image = base.ImageList.Images[base.ImageIndex];
			}
			else
			{
				image = null;
			}
			Rectangle empty;
			Rectangle r;
			if (image == null)
			{
				empty = Rectangle.Empty;
				r = clientRectanglePadded;
			}
			else
			{
				empty = new Rectangle(clientRectanglePadded.Left, clientRectanglePadded.Top, image.Width + this.padBetweenImageAndText, image.Height);
				r = new Rectangle(empty.Right + 1, empty.Top, clientRectanglePadded.Width - empty.Width, clientRectanglePadded.Height);
			}
			if (this.mouseIsOver)
			{
				this.DrawBorder(g, this.borderStyleHighlight, this.borderWidthHighlight, base.ClientRectangle);
			}
			else
			{
				this.DrawBorder(g, this.borderStyle, this.borderWidth, base.ClientRectangle);
			}
			if (image != null && !empty.IsEmpty)
			{
				if (this.mouseIsOver)
				{
					g.DrawImageUnscaled(image, empty);
				}
				else
				{
					g.DrawImageUnscaled(image, empty);
				}
			}
			if (this.Text.Length > 0)
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
				Brush brush;
				if (this.mouseIsOver)
				{
					brush = this.foregroundHighlightBrush;
				}
				else
				{
					brush = this.foregroundBrush;
				}
				this.Text = this.Text.Replace("\\n", "\n");
				int num = this.Text.IndexOf("\\n");
				if (num < 0)
				{
					num = this.Text.IndexOf("\n");
				}
				string s;
				if (num > 0)
				{
					string text = this.Text.Substring(0, num);
					s = this.Text.Substring(num + 1);
					using (Font font = new Font(this.Font.FontFamily, (float)this.TitleFontSize))
					{
						g.DrawString(text, font, brush, r, stringFormat);
						SizeF sizeF = g.MeasureString(text, font);
						float value = sizeF.Height + sizeF.Height / 3f;
						r.Y += Convert.ToInt32(value);
					}
				}
				else
				{
					s = this.Text;
				}
				g.DrawString(s, this.Font, brush, r, stringFormat);
				brush = null;
			}
			if (this.Focused)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				clientRectangle.Inflate(-5, -5);
				clientRectangle.Offset(1, 1);
				using (Pen pen = new Pen(this.ForeColor))
				{
					pen.DashStyle = DashStyle.Dot;
					g.DrawRectangle(pen, clientRectangle);
					pen.DashStyle = DashStyle.Solid;
					clientRectangle = base.ClientRectangle;
					clientRectangle.Inflate(-1, -1);
					g.DrawRectangle(pen, clientRectangle);
				}
			}
			if (!base.Enabled)
			{
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(127, 128, 128, 128)))
				{
					g.FillRectangle(solidBrush, base.ClientRectangle);
				}
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00035A10 File Offset: 0x00034A10
		private void DrawBorder(Graphics g, MyBorderStyle borderStyle, int borderWidth, Rectangle cr)
		{
			if (borderStyle == MyBorderStyle.roundedBox)
			{
				cr.Inflate(-1, -1);
				GraphicsPath roundedRectanglePath = MyGraphicsRoutines.GetRoundedRectanglePath(cr, borderWidth);
				g.DrawPath(this.borderPen, roundedRectanglePath);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00035A49 File Offset: 0x00034A49
		private void SetBackgroundHighlightBrush()
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00035A4C File Offset: 0x00034A4C
		private void SetBrush(Color baseColor, bool gradientOn, ref Brush theBrush)
		{
			Color backColor = this.BackColor;
			if (theBrush != null)
			{
				theBrush.Dispose();
			}
			if (gradientOn)
			{
				int num = (int)baseColor.R + this.backGradientIncrement;
				int num2 = (int)baseColor.G + this.backGradientIncrement;
				int num3 = (int)baseColor.B + this.backGradientIncrement;
				if (num > 255)
				{
					num = 255;
				}
				if (num2 > 255)
				{
					num2 = 255;
				}
				if (num3 > 255)
				{
					num3 = 255;
				}
				if (num < 0)
				{
					num = 0;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num3 < 0)
				{
					num3 = 0;
				}
				Color color = Color.FromArgb(num, num2, num3);
				theBrush = new LinearGradientBrush(base.ClientRectangle, baseColor, color, 90f, false);
			}
			else
			{
				theBrush = new SolidBrush(baseColor);
			}
			base.Invalidate();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00035B58 File Offset: 0x00034B58
		protected override void OnSizeChanged(EventArgs e)
		{
			if (base.Size.Width > 0 && base.Size.Height > 0)
			{
				if (this.backHighlightGradientOn)
				{
					this.SetBrush(this.backColorHighlight, this.backHighlightGradientOn, ref this.backgroundHighlightBrush);
				}
				if (this.backGradientOn)
				{
					this.SetBrush(this.BackColor, this.backGradientOn, ref this.backgroundBrush);
				}
			}
			base.OnSizeChanged(e);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00035BE8 File Offset: 0x00034BE8
		protected override void OnMouseEnter(EventArgs e)
		{
			this.mouseIsOver = true;
			base.OnMouseEnter(e);
			base.Invalidate();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00035C01 File Offset: 0x00034C01
		protected override void OnMouseLeave(EventArgs e)
		{
			this.mouseIsOver = false;
			base.OnMouseLeave(e);
			base.Invalidate();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00035C1A File Offset: 0x00034C1A
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			base.Invalidate();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00035C2C File Offset: 0x00034C2C
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			base.Invalidate();
		}

		// Token: 0x0400052B RID: 1323
		private Container components = null;

		// Token: 0x0400052C RID: 1324
		private Brush foregroundBrush = null;

		// Token: 0x0400052D RID: 1325
		private Brush foregroundHighlightBrush = null;

		// Token: 0x0400052E RID: 1326
		private Brush backgroundBrush = null;

		// Token: 0x0400052F RID: 1327
		private Brush backgroundHighlightBrush = null;

		// Token: 0x04000530 RID: 1328
		private int backGradientIncrement = -25;

		// Token: 0x04000531 RID: 1329
		private int padTop = 4;

		// Token: 0x04000532 RID: 1330
		private int padBottom = 4;

		// Token: 0x04000533 RID: 1331
		private int padLeft = 4;

		// Token: 0x04000534 RID: 1332
		private int padRight = 4;

		// Token: 0x04000535 RID: 1333
		private int padBetweenImageAndText = 20;

		// Token: 0x04000536 RID: 1334
		private Color backColorHighlight = SystemColors.Control;

		// Token: 0x04000537 RID: 1335
		private Color foreColorHighlight = SystemColors.HotTrack;

		// Token: 0x04000538 RID: 1336
		private MyBorderStyle borderStyle = MyBorderStyle.none;

		// Token: 0x04000539 RID: 1337
		private MyBorderStyle borderStyleHighlight = MyBorderStyle.none;

		// Token: 0x0400053A RID: 1338
		private int borderWidth = 0;

		// Token: 0x0400053B RID: 1339
		private int borderWidthHighlight = 0;

		// Token: 0x0400053C RID: 1340
		private Pen borderPen = new Pen(SystemColors.WindowFrame);

		// Token: 0x0400053D RID: 1341
		private bool backGradientOn = false;

		// Token: 0x0400053E RID: 1342
		private bool backHighlightGradientOn = true;

		// Token: 0x0400053F RID: 1343
		private bool mouseIsOver = false;

		// Token: 0x04000540 RID: 1344
		private int titleFontSize;
	}
}
