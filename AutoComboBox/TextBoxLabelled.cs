using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000E7 RID: 231
	public class TextBoxLabelled : UserControl
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0004586C File Offset: 0x0004486C
		public TextBoxLabelled(double labelWidthFraction, string labelText)
		{
			this.InitializeComponent();
			this.labelWidthFraction = labelWidthFraction;
			this.lbl.Text = labelText;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000458A1 File Offset: 0x000448A1
		public TextBoxLabelled()
		{
			this.InitializeComponent();
			this.labelWidthFraction = 0.0;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000458D4 File Offset: 0x000448D4
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x000458F1 File Offset: 0x000448F1
		public bool TextBoxMultiline
		{
			get
			{
				return this.txt.Multiline;
			}
			set
			{
				this.txt.Multiline = value;
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00045904 File Offset: 0x00044904
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

		// Token: 0x060008FE RID: 2302 RVA: 0x00045940 File Offset: 0x00044940
		private void InitializeComponent()
		{
			this.lbl = new Label();
			this.txt = new TextBox();
			this.lbl_spacerTop = new Label();
			this.chk_showPassword = new CheckBox();
			base.SuspendLayout();
			this.lbl.Dock = DockStyle.Left;
			this.lbl.Location = new Point(0, 0);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(120, 40);
			this.lbl.TabIndex = 0;
			this.lbl.Text = "The label:";
			this.lbl.TextAlign = ContentAlignment.MiddleLeft;
			this.txt.Dock = DockStyle.Fill;
			this.txt.Location = new Point(120, 8);
			this.txt.Name = "txt";
			this.txt.Size = new Size(128, 20);
			this.txt.TabIndex = 1;
			this.lbl_spacerTop.Dock = DockStyle.Top;
			this.lbl_spacerTop.Location = new Point(120, 0);
			this.lbl_spacerTop.Name = "lbl_spacerTop";
			this.lbl_spacerTop.Size = new Size(128, 8);
			this.lbl_spacerTop.TabIndex = 2;
			this.chk_showPassword.AutoSize = true;
			this.chk_showPassword.Dock = DockStyle.Right;
			this.chk_showPassword.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chk_showPassword.Location = new Point(248, 0);
			this.chk_showPassword.Name = "chk_showPassword";
			this.chk_showPassword.Padding = new Padding(4, 0, 0, 0);
			this.chk_showPassword.Size = new Size(112, 40);
			this.chk_showPassword.TabIndex = 3;
			this.chk_showPassword.Text = "Show password";
			this.chk_showPassword.UseVisualStyleBackColor = true;
			this.chk_showPassword.Visible = false;
			this.chk_showPassword.CheckedChanged += this.chk_showPassword_CheckedChanged;
			base.Controls.Add(this.txt);
			base.Controls.Add(this.lbl_spacerTop);
			base.Controls.Add(this.lbl);
			base.Controls.Add(this.chk_showPassword);
			base.Name = "TextBoxLabelled";
			base.Size = new Size(360, 40);
			base.SizeChanged += this.TextBoxLabelled_SizeChanged;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00045C04 File Offset: 0x00044C04
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00045C1C File Offset: 0x00044C1C
		public double LabelWidthFraction
		{
			get
			{
				return this.labelWidthFraction;
			}
			set
			{
				this.labelWidthFraction = value;
				this.AdjustLabelWidth();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00045C30 File Offset: 0x00044C30
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00045C4D File Offset: 0x00044C4D
		public string LabelText
		{
			get
			{
				return this.lbl.Text;
			}
			set
			{
				this.lbl.Text = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00045C60 File Offset: 0x00044C60
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00045C7D File Offset: 0x00044C7D
		public new string Text
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00045C90 File Offset: 0x00044C90
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00045CAD File Offset: 0x00044CAD
		public string TextBoxText
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00045CC0 File Offset: 0x00044CC0
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00045CDD File Offset: 0x00044CDD
		public HorizontalAlignment TextBoxTextAlign
		{
			get
			{
				return this.txt.TextAlign;
			}
			set
			{
				this.txt.TextAlign = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00045CF0 File Offset: 0x00044CF0
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00045D10 File Offset: 0x00044D10
		public char TextBoxPasswordChar
		{
			get
			{
				return this.txt.PasswordChar;
			}
			set
			{
				this.txt.PasswordChar = value;
				this.passwordChar = this.txt.PasswordChar;
				if (this.passwordChar != ' ' && this.passwordChar != '\0')
				{
					this.chk_showPassword.Visible = true;
				}
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00045D68 File Offset: 0x00044D68
		private void TextBoxLabelled_SizeChanged(object sender, EventArgs e)
		{
			int num = base.Height - this.txt.Height - base.DockPadding.Top - base.DockPadding.Bottom;
			int num2 = Convert.ToInt32(num / 2);
			if (num2 < 0)
			{
				num2 = 0;
			}
			this.lbl_spacerTop.Height = num2;
			this.AdjustLabelWidth();
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00045DCC File Offset: 0x00044DCC
		private void AdjustLabelWidth()
		{
			if (this.labelWidthFraction > 0.0)
			{
				int num = Convert.ToInt32((double)base.Width * this.labelWidthFraction);
				if (num <= 0)
				{
					num = 1;
				}
				this.lbl.Width = num;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00045E20 File Offset: 0x00044E20
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00045E3D File Offset: 0x00044E3D
		public bool TextBoxEnabled
		{
			get
			{
				return this.txt.Enabled;
			}
			set
			{
				this.txt.Enabled = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00045E50 File Offset: 0x00044E50
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00045E6D File Offset: 0x00044E6D
		public bool TextBoxReadOnly
		{
			get
			{
				return this.txt.ReadOnly;
			}
			set
			{
				this.txt.ReadOnly = value;
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00045E80 File Offset: 0x00044E80
		private void chk_showPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chk_showPassword.Checked)
			{
				this.txt.PasswordChar = '\0';
			}
			else
			{
				this.txt.PasswordChar = this.passwordChar;
			}
		}

		// Token: 0x04000686 RID: 1670
		private TextBox txt;

		// Token: 0x04000687 RID: 1671
		private Label lbl_spacerTop;

		// Token: 0x04000688 RID: 1672
		private Label lbl;

		// Token: 0x04000689 RID: 1673
		private CheckBox chk_showPassword;

		// Token: 0x0400068A RID: 1674
		private Container components = null;

		// Token: 0x0400068B RID: 1675
		private double labelWidthFraction;

		// Token: 0x0400068C RID: 1676
		private char passwordChar = ' ';
	}
}
