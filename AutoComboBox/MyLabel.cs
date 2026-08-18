using System;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace AutoComboBox
{
	// Token: 0x020000F1 RID: 241
	public class MyLabel : Label
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0004B264 File Offset: 0x0004A264
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0004B27C File Offset: 0x0004A27C
		public bool ActAsLink
		{
			get
			{
				return this.actAsLink;
			}
			set
			{
				this.actAsLink = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0004B288 File Offset: 0x0004A288
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0004B2A0 File Offset: 0x0004A2A0
		public string HelpText
		{
			get
			{
				return this.helpText;
			}
			set
			{
				this.helpText = value;
				if (this.helpText.Length > 0)
				{
					this.btn = new Button();
					this.btn.Text = "Insert Template";
					this.btn.AutoSize = true;
					base.Controls.Add(this.btn);
					this.btn.Dock = DockStyle.Right;
					this.btn.Click += this.btn_Click;
				}
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0004B32C File Offset: 0x0004A32C
		private void btn_Click(object sender, EventArgs e)
		{
			Control parent = base.Parent;
			if (parent != null)
			{
				bool flag = false;
				for (int i = 0; i < parent.Controls.Count; i++)
				{
					Control control = parent.Controls[i];
					if (flag)
					{
						if (control is TextBox && control.Enabled && !((TextBox)control).ReadOnly)
						{
							if (!((TextBox)control).ReadOnly)
							{
								Control control2 = control;
								control2.Text += this.helpText;
							}
							break;
						}
						if (control is MyRichText)
						{
							MyRichText myRichText = (MyRichText)control;
							if (!myRichText.BaseReadOnly && control.Enabled)
							{
								string plainText = myRichText.PlainText;
								if (this.helpText.IndexOf("{\\rtf") == 0)
								{
									myRichText.Text = this.helpText;
								}
								else
								{
									myRichText.PlainText = this.helpText;
								}
								if (plainText.Trim().Length > 0)
								{
									myRichText.RichTextBox.AppendText(plainText);
								}
							}
							break;
						}
					}
					else if (control == this)
					{
						flag = true;
					}
				}
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0004B4B0 File Offset: 0x0004A4B0
		protected override void Dispose(bool disposing)
		{
			if (this.btn != null)
			{
				this.btn.Click -= this.btn_Click;
				this.btn.Dispose();
				this.btn = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0004B500 File Offset: 0x0004A500
		public MyLabel()
		{
			this.actAsLink = false;
			this.helpText = "";
			this.mouseIsOver = false;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0004B52C File Offset: 0x0004A52C
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (this.actAsLink)
			{
				Font font = this.Font;
				this.oldFontStyle = font.Style;
				this.Font = new Font(font, font.Style | FontStyle.Underline);
				font.Dispose();
				base.Invalidate();
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0004B58C File Offset: 0x0004A58C
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.actAsLink)
			{
				Font font = this.Font;
				this.Font = new Font(font, this.oldFontStyle);
				font.Dispose();
				base.Invalidate();
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0004B5DB File Offset: 0x0004A5DB
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
		}

		// Token: 0x040006E4 RID: 1764
		private bool actAsLink;

		// Token: 0x040006E5 RID: 1765
		private string helpText;

		// Token: 0x040006E6 RID: 1766
		private bool mouseIsOver;

		// Token: 0x040006E7 RID: 1767
		private Button btn;

		// Token: 0x040006E8 RID: 1768
		private FontStyle oldFontStyle = FontStyle.Regular;
	}
}
