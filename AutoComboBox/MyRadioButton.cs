using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace AutoComboBox
{
	// Token: 0x020000D8 RID: 216
	public class MyRadioButton : RadioButton, MyDynamicControl
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x00041D54 File Offset: 0x00040D54
		public MyRadioButton()
		{
			base.AutoCheck = false;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00041D90 File Offset: 0x00040D90
		public new string ToString()
		{
			return base.Checked ? "1" : "0";
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00041DB6 File Offset: 0x00040DB6
		public void FromString(string s)
		{
			base.Checked = (s.CompareTo("1") == 0);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00041DD0 File Offset: 0x00040DD0
		public object ReportObject
		{
			get
			{
				return base.Checked;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00041DF0 File Offset: 0x00040DF0
		public bool FilledIn
		{
			get
			{
				return base.Checked;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00041E08 File Offset: 0x00040E08
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00041E20 File Offset: 0x00040E20
		public bool AutoSizeHeight
		{
			get
			{
				return this.autoSizeHeight;
			}
			set
			{
				this.autoSizeHeight = value;
				this.ResizeHeight();
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00041E34 File Offset: 0x00040E34
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00041E4C File Offset: 0x00040E4C
		public new ControlPadding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00041E58 File Offset: 0x00040E58
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle textRectangle = this.GetTextRectangle();
			StringFormat stringFormat = this.GetStringFormat();
			using (Brush brush = new SolidBrush(this.BackColor))
			{
				e.Graphics.FillRectangle(brush, base.ClientRectangle);
			}
			Color foreColor = this.ForeColor;
			using (Brush brush2 = new SolidBrush(foreColor))
			{
				e.Graphics.DrawString(this.Text, this.Font, brush2, textRectangle, stringFormat);
			}
			ButtonState state;
			if (base.Enabled)
			{
				if (base.Checked)
				{
					state = ButtonState.Checked;
				}
				else
				{
					state = ButtonState.Normal;
				}
			}
			else if (base.Checked)
			{
				state = (ButtonState.Checked | ButtonState.Inactive);
			}
			else
			{
				state = ButtonState.Inactive;
			}
			int num = Convert.ToInt32((base.ClientSize.Height - this.padding.TopAndBottom - this.radioCheckSize.Height) / 2);
			Rectangle rectangle = new Rectangle(this.padding.Left, this.padding.Top + num, this.radioCheckSize.Width, this.radioCheckSize.Height);
			ControlPaint.DrawRadioButton(e.Graphics, rectangle, state);
			if (this.Focused)
			{
				this.DrawFocusRectangle(e.Graphics);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00041FF4 File Offset: 0x00040FF4
		private void DrawFocusRectangle(Graphics g)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			clientRectangle.Inflate(-2, -2);
			ControlPaint.DrawFocusRectangle(g, clientRectangle, this.ForeColor, this.BackColor);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00042029 File Offset: 0x00041029
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.DrawFocusRectangle(base.CreateGraphics());
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00042041 File Offset: 0x00041041
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00042054 File Offset: 0x00041054
		private StringFormat GetStringFormat()
		{
			return new StringFormat
			{
				FormatFlags = (StringFormatFlags.FitBlackBox | StringFormatFlags.DisplayFormatControl),
				Trimming = StringTrimming.Character,
				LineAlignment = StringAlignment.Center,
				HotkeyPrefix = HotkeyPrefix.Show
			};
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00042090 File Offset: 0x00041090
		private Rectangle GetTextRectangle()
		{
			if (this.radioCheckSize == Size.Empty)
			{
				this.radioCheckSize = new Size(Convert.ToInt32((double)SystemInformation.MenuCheckSize.Width * 1.2), Convert.ToInt32((double)SystemInformation.MenuCheckSize.Height * 1.2));
			}
			int width = base.Width - this.radioCheckSize.Width - this.padding.LeftAndRightAndMiddle;
			int height = base.Height - this.padding.TopAndBottom;
			return new Rectangle(this.radioCheckSize.Width + this.padding.Left + this.padding.Middle, this.padding.Top, width, height);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00042167 File Offset: 0x00041167
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.ResizeHeight();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0004217C File Offset: 0x0004117C
		private void ResizeHeight()
		{
			if (this.autoSizeHeight)
			{
				this.ResizeHeight(base.CreateGraphics());
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000421A4 File Offset: 0x000411A4
		private void ResizeHeight(Graphics g)
		{
			if (this.autoSizeHeight)
			{
				Rectangle textRectangle = this.GetTextRectangle();
				StringFormat stringFormat = this.GetStringFormat();
				int num = (int)g.MeasureString(this.Text, this.Font, textRectangle.Width, stringFormat).Height + this.padding.TopAndBottom;
				if (num > 0)
				{
					this.ignoreSizeChange = true;
					base.Height = num;
					this.ignoreSizeChange = false;
				}
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00042224 File Offset: 0x00041224
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (!this.ignoreSizeChange)
			{
				this.ResizeHeight();
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00042250 File Offset: 0x00041250
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00042262 File Offset: 0x00041262
		protected override void OnClick(EventArgs e)
		{
			this.CheckRadioButton();
			base.OnClick(e);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00042274 File Offset: 0x00041274
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Space)
			{
				this.CheckRadioButton();
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000422A4 File Offset: 0x000412A4
		private void CheckRadioButton()
		{
			if (base.Parent != null)
			{
				if (base.Parent.Parent != null && base.Parent.Parent is MyRadioGroup)
				{
					MyRadioGroup myRadioGroup = (MyRadioGroup)base.Parent.Parent;
					myRadioGroup.CheckRadioButton(this);
				}
				else if (base.Parent.Parent != null && base.Parent.Parent is Panel)
				{
					Panel panel = (Panel)base.Parent.Parent;
					Control parent = base.Parent;
					foreach (object obj in panel.Controls)
					{
						Control control = (Control)obj;
						if (control is MyRadioGroupPrimaryCheckboxMultiple)
						{
							MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
							if (myRadioGroupPrimaryCheckboxMultiple.PrimaryEquals(this))
							{
								if (!myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked)
								{
									myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked = true;
								}
							}
							else if (myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked)
							{
								myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x04000635 RID: 1589
		private bool autoSizeHeight = false;

		// Token: 0x04000636 RID: 1590
		private ControlPadding padding = new ControlPadding(2, 2, 2, 2, 2);

		// Token: 0x04000637 RID: 1591
		private Size radioCheckSize = Size.Empty;

		// Token: 0x04000638 RID: 1592
		private bool ignoreSizeChange = false;
	}
}
