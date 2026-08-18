using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000D0 RID: 208
	public class MySplitter : Splitter
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x0003EB64 File Offset: 0x0003DB64
		public MySplitter()
		{
			this.InitializeComponent();
			this.buttonHeight = 40;
			this.buttonBackColour = Color.Blue;
			this.buttonForeColour = Color.Black;
			this.buttonHatchStyle = HatchStyle.DottedDiamond;
			this.buttonCursor = Cursors.Hand;
			this.lastMouseDownX = -1;
			this.lastMouseDownY = -1;
			this.originalCursor = null;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0003EBD0 File Offset: 0x0003DBD0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0003EC0B File Offset: 0x0003DC0B
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0003EC1C File Offset: 0x0003DC1C
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0003EC34 File Offset: 0x0003DC34
		[Description("Gets or sets the height of the button that is located vertically in the middle.")]
		[Category("Appearance")]
		public int ButtonHeight
		{
			get
			{
				return this.buttonHeight;
			}
			set
			{
				this.buttonHeight = value;
				this.Refresh();
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0003EC48 File Offset: 0x0003DC48
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0003EC60 File Offset: 0x0003DC60
		[Description("Gets or sets the background colour of the button that is located vertically in the middle.")]
		[Category("Appearance")]
		public Color ButtonBackColour
		{
			get
			{
				return this.buttonBackColour;
			}
			set
			{
				this.buttonBackColour = value;
				this.Refresh();
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0003EC74 File Offset: 0x0003DC74
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0003EC8C File Offset: 0x0003DC8C
		[Category("Appearance")]
		[Description("Gets or sets the background hatch style of the button that is located vertically in the middle.")]
		public HatchStyle ButtonHatchStyle
		{
			get
			{
				return this.buttonHatchStyle;
			}
			set
			{
				this.buttonHatchStyle = value;
				this.Refresh();
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0003ECA0 File Offset: 0x0003DCA0
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0003ECB8 File Offset: 0x0003DCB8
		[Category("Appearance")]
		[Description("Gets or sets the fore colour of the button that is located vertically in the middle.")]
		public Color ButtonForeColour
		{
			get
			{
				return this.buttonForeColour;
			}
			set
			{
				this.buttonForeColour = value;
				this.Refresh();
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0003ECCC File Offset: 0x0003DCCC
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0003ECE4 File Offset: 0x0003DCE4
		[Description("Gets or sets the cursor (on mouse over) for button that is located vertically in the middle.")]
		[Category("Appearance")]
		public Cursor ButtonCursor
		{
			get
			{
				return this.buttonCursor;
			}
			set
			{
				this.buttonCursor = value;
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060007F5 RID: 2037 RVA: 0x0003ECF0 File Offset: 0x0003DCF0
		// (remove) Token: 0x060007F6 RID: 2038 RVA: 0x0003ED2C File Offset: 0x0003DD2C
		public event MySplitter.ButtonClickHandler ButtonClick;

		// Token: 0x060007F7 RID: 2039 RVA: 0x0003ED68 File Offset: 0x0003DD68
		protected override void OnMouseDown(MouseEventArgs e)
		{
			Rectangle buttonRectangle = this.GetButtonRectangle();
			if (!buttonRectangle.IsEmpty)
			{
				if (buttonRectangle.Contains(new Point(e.X, e.Y)))
				{
					this.lastMouseDownX = e.X;
					this.lastMouseDownY = e.Y;
				}
				else
				{
					this.lastMouseDownX = -1;
					this.lastMouseDownY = -1;
				}
			}
			base.OnMouseDown(e);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0003EDE0 File Offset: 0x0003DDE0
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.lastMouseDownX >= 0 && this.lastMouseDownY >= 0)
			{
				int num = Math.Abs(e.X - this.lastMouseDownX);
				int num2 = Math.Abs(e.Y - this.lastMouseDownY);
				if (num < 4 && num2 < 4)
				{
					this.lastMouseDownX = -1;
					this.lastMouseDownY = -1;
					this.OnButtonClick(this);
				}
			}
			this.lastMouseDownX = -1;
			this.lastMouseDownY = -1;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0003EE70 File Offset: 0x0003DE70
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			Rectangle buttonRectangle = this.GetButtonRectangle();
			if (!buttonRectangle.IsEmpty)
			{
				Point pt = new Point(e.X, e.Y);
				if (buttonRectangle.Contains(pt))
				{
					if (this.Cursor != this.buttonCursor)
					{
						this.originalCursor = this.Cursor;
						this.Cursor = this.buttonCursor;
					}
					return;
				}
			}
			if (this.Cursor == this.buttonCursor && this.originalCursor != null)
			{
				this.Cursor = this.originalCursor;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0003EF2C File Offset: 0x0003DF2C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle buttonRectangle = this.GetButtonRectangle();
			if (!buttonRectangle.IsEmpty)
			{
				HatchBrush brush = new HatchBrush(this.buttonHatchStyle, this.buttonForeColour, this.buttonBackColour);
				e.Graphics.FillRectangle(brush, buttonRectangle);
				Pen pen = new Pen(this.buttonForeColour);
				e.Graphics.DrawRectangle(pen, buttonRectangle);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0003EF98 File Offset: 0x0003DF98
		private Rectangle GetButtonRectangle()
		{
			int num = base.Height - this.buttonHeight;
			if (num > 0)
			{
				int num2 = Convert.ToInt32(num / 2);
				if (num2 >= 0)
				{
					return new Rectangle(0, num2, base.Width, this.buttonHeight);
				}
			}
			return Rectangle.Empty;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0003EFF4 File Offset: 0x0003DFF4
		protected void OnButtonClick(object sender)
		{
			if (this.ButtonClick != null)
			{
				this.ButtonClick(sender);
			}
		}

		// Token: 0x040005FD RID: 1533
		private IContainer components = null;

		// Token: 0x040005FE RID: 1534
		private int buttonHeight;

		// Token: 0x040005FF RID: 1535
		private Color buttonBackColour;

		// Token: 0x04000600 RID: 1536
		private Color buttonForeColour;

		// Token: 0x04000601 RID: 1537
		private HatchStyle buttonHatchStyle;

		// Token: 0x04000602 RID: 1538
		private Cursor buttonCursor;

		// Token: 0x04000603 RID: 1539
		private Cursor originalCursor;

		// Token: 0x04000605 RID: 1541
		private int lastMouseDownX;

		// Token: 0x04000606 RID: 1542
		private int lastMouseDownY;

		// Token: 0x020000D1 RID: 209
		// (Invoke) Token: 0x060007FE RID: 2046
		public delegate void ButtonClickHandler(object sender);
	}
}
