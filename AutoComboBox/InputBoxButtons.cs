using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000AB RID: 171
	public partial class InputBoxButtons : Form
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x000330F4 File Offset: 0x000320F4
		public InputBoxButtons(string title, string question, string button1Text, string button2Text, InputBoxButtons.DialogIcon dialogIcon)
		{
			this.InitializeComponent();
			if (dialogIcon < InputBoxButtons.DialogIcon.Refresh)
			{
				this.lbl_image.Visible = false;
			}
			else
			{
				this.lbl_image.ImageIndex = (int)dialogIcon;
			}
			if (button1Text != null)
			{
				this.button1.Text = button1Text;
			}
			else
			{
				this.button1.Visible = false;
			}
			if (button2Text != null)
			{
				this.button2.Text = button2Text;
			}
			else
			{
				this.button2.Visible = false;
			}
			this.ResizeButtons();
			this.Text = title;
			this.label1.Text = question;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000337F0 File Offset: 0x000327F0
		public Button Button1
		{
			get
			{
				return this.button1;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00033808 File Offset: 0x00032808
		public Button Button2
		{
			get
			{
				return this.button2;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00033820 File Offset: 0x00032820
		public Button Btn_Cancel
		{
			get
			{
				return this.btn_cancel;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00033838 File Offset: 0x00032838
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00033842 File Offset: 0x00032842
		private void button1_Click(object sender, EventArgs e)
		{
			this.buttonClickedNum = 1;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0003385B File Offset: 0x0003285B
		private void button2_Click(object sender, EventArgs e)
		{
			this.buttonClickedNum = 2;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00033874 File Offset: 0x00032874
		private void label1_TextChanged(object sender, EventArgs e)
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				Graphics graphics = this.label1.CreateGraphics();
				string text = this.label1.Text;
				StringFormat stringFormat = new StringFormat(StringFormatFlags.FitBlackBox);
				SizeF layoutArea = new SizeF((float)this.label1.Width, (float)Screen.PrimaryScreen.WorkingArea.Height);
				int num;
				int num2;
				graphics.MeasureString(text, this.label1.Font, layoutArea, stringFormat, out num, out num2);
				SizeF sizeF = graphics.MeasureString("qW", this.label1.Font);
				int num3 = base.Height - base.ClientSize.Height;
				float num4 = (float)num2 * sizeF.Height;
				if (num4 > 0f)
				{
					if (num4 < (float)this.imageList1.ImageSize.Height)
					{
						num4 = (float)this.imageList1.ImageSize.Height;
					}
					this.label1.Height = (int)num4;
				}
				int num5 = num3 + base.DockPadding.Top + this.label1.Height + this.panel1.Height + base.DockPadding.Bottom + SystemInformation.Border3DSize.Height * 2;
				if (num5 > 0)
				{
					base.Height = num5;
				}
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000339F0 File Offset: 0x000329F0
		private void ResizeButtons()
		{
			int num = this.panel1.Height - (this.button1.Top + this.button1.Height);
			this.ResizeButton(this.btn_cancel, -1);
			this.ResizeButton(this.button1, this.btn_cancel.Height);
			if (this.button1.Height > this.btn_cancel.Height)
			{
				this.btn_cancel.Height = this.button1.Height;
			}
			this.ResizeButton(this.button2, this.button1.Height);
			if (this.button2.Height > this.button1.Height)
			{
				this.button1.Height = this.button2.Height;
				this.btn_cancel.Height = this.button1.Height;
			}
			this.panel1.Height = this.button1.Top + this.button1.Height + num;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00033B0C File Offset: 0x00032B0C
		private void ResizeButton(Button btn, int minButtonHeight)
		{
			Graphics graphics = btn.CreateGraphics();
			string text = btn.Text;
			StringFormat stringFormat = new StringFormat(StringFormatFlags.FitBlackBox);
			SizeF layoutArea = new SizeF((float)(btn.Width - SystemInformation.Border3DSize.Width * 2), (float)Screen.PrimaryScreen.WorkingArea.Height);
			int num;
			int num2;
			graphics.MeasureString(text, btn.Font, layoutArea, stringFormat, out num, out num2);
			SizeF sizeF = graphics.MeasureString("qW", btn.Font);
			float num3 = (float)num2 * sizeF.Height + ((float)this.button2.Height - sizeF.Height);
			if (num3 > 0f)
			{
				if (minButtonHeight > 0)
				{
					if (num3 > (float)minButtonHeight)
					{
						btn.Height = (int)num3;
					}
					else
					{
						btn.Height = minButtonHeight;
					}
				}
				else
				{
					btn.Height = (int)num3;
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00033BFE File Offset: 0x00032BFE
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00033C08 File Offset: 0x00032C08
		private void button2_Click_1(object sender, EventArgs e)
		{
			this.buttonClickedNum = 2;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00033C21 File Offset: 0x00032C21
		private void MyDialog_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00033C24 File Offset: 0x00032C24
		public void HideButton2()
		{
			this.button2.Visible = false;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00033C34 File Offset: 0x00032C34
		public void SetButton2Text(string text)
		{
			this.button2.Text = text;
		}

		// Token: 0x0400050A RID: 1290
		public int buttonClickedNum = -1;

		// Token: 0x020000AC RID: 172
		public enum DialogIcon
		{
			// Token: 0x0400050C RID: 1292
			Refresh,
			// Token: 0x0400050D RID: 1293
			QuestionMark
		}
	}
}
