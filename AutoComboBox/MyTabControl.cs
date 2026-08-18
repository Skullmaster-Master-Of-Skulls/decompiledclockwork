using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace AutoComboBox
{
	// Token: 0x02000021 RID: 33
	public class MyTabControl : UserControl
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000B430 File Offset: 0x0000A430
		public MyTabControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000B450 File Offset: 0x0000A450
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					foreach (object obj in this.p_tabStrip.Controls)
					{
						Button button = (Button)obj;
						if (button != null)
						{
							MyTabPage myTabPage = (MyTabPage)button.Tag;
							base.Controls.Remove(myTabPage);
							myTabPage.Dispose();
							button.Tag = null;
							button.Click -= this.btn_Click;
							button.Dispose();
						}
					}
					this.p_tabStrip.Controls.Clear();
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000B548 File Offset: 0x0000A548
		private void InitializeComponent()
		{
			this.p_tabStrip = new FlowLayoutPanel();
			base.SuspendLayout();
			this.p_tabStrip.AutoSize = true;
			this.p_tabStrip.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.p_tabStrip.BorderStyle = BorderStyle.FixedSingle;
			this.p_tabStrip.Dock = DockStyle.Top;
			this.p_tabStrip.Location = new Point(0, 0);
			this.p_tabStrip.Name = "p_tabStrip";
			this.p_tabStrip.Size = new Size(720, 2);
			this.p_tabStrip.TabIndex = 1;
			base.Controls.Add(this.p_tabStrip);
			base.Name = "MyTabControl";
			base.Size = new Size(720, 488);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000B627 File Offset: 0x0000A627
		public void AddTabPage(MyTabPage myTabPage, string text)
		{
			myTabPage.Text = text;
			this.AddTabPage(myTabPage);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000B63C File Offset: 0x0000A63C
		public void AddTabPage(MyTabPage myTabPage)
		{
			Button button = new Button();
			button.Text = myTabPage.Text;
			button.Tag = myTabPage;
			button.TabStop = (this.p_tabStrip.Controls.Count == 0);
			this.p_tabStrip.Controls.Add(button);
			button.Click += this.btn_Click;
			this.ResizeButton(button);
			button.Margin = new Padding(1, 3, 1, 1);
			myTabPage.Dock = DockStyle.None;
			myTabPage.Visible = false;
			base.Controls.Add(myTabPage);
			if (this.selectedTabPage == null)
			{
				this.ShowTabPage(myTabPage);
			}
			else
			{
				this.FadoutButton(button);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000B6FC File Offset: 0x0000A6FC
		private void ResizeButton(Button btn)
		{
			Graphics graphics = btn.CreateGraphics();
			btn.Width = (int)graphics.MeasureString(btn.Text + " *", btn.Font).Width + 12;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000B740 File Offset: 0x0000A740
		private bool AnyFieldsFilledIn(Control parentControl)
		{
			foreach (object obj in parentControl.Controls)
			{
				Control control = (Control)obj;
				bool flag;
				if (control is MyDynamicControl)
				{
					flag = ((MyDynamicControl)control).FilledIn;
				}
				else if (control is RadioButton)
				{
					flag = ((RadioButton)control).Checked;
				}
				else
				{
					flag = (!(control is Label) && control.Tag is DataRow && control.Text.Trim().Length > 0);
				}
				if (flag)
				{
					return true;
				}
				if (control.Controls.Count > 0)
				{
					if (this.AnyFieldsFilledIn(control))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000B86C File Offset: 0x0000A86C
		public void DisplayIfFieldsAreFilledIn(MyTabPage mtp, bool showYes)
		{
			Button button = this.FindTabButton(mtp);
			if (button != null)
			{
				button.Text = (showYes ? (mtp.Text + " *") : mtp.Text);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000B8B0 File Offset: 0x0000A8B0
		public void ClearDisplayIfFieldsAreFilledIn()
		{
			foreach (object obj in this.p_tabStrip.Controls)
			{
				Button button = (Button)obj;
				MyTabPage myTabPage = (MyTabPage)button.Tag;
				button.Text = myTabPage.Text;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000B930 File Offset: 0x0000A930
		public void ShowDisplayIfFieldsAreFilledIn()
		{
			foreach (object obj in this.p_tabStrip.Controls)
			{
				Button button = (Button)obj;
				MyTabPage myTabPage = (MyTabPage)button.Tag;
				this.DisplayIfFieldsAreFilledIn(myTabPage, this.AnyFieldsFilledIn(myTabPage));
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000B9B4 File Offset: 0x0000A9B4
		public void ShowTabPage(MyTabPage mtp)
		{
			if (this.selectedTabPage != null)
			{
				this.DisplayIfFieldsAreFilledIn(this.selectedTabPage, this.AnyFieldsFilledIn(this.selectedTabPage));
				this.selectedTabPage.Dock = DockStyle.None;
				this.selectedTabPage.SendToBack();
				this.selectedTabPage.Visible = false;
				this.selectedTabPage = null;
			}
			if (mtp != null)
			{
				this.selectedTabPage = mtp;
				this.selectedTabPage.Dock = DockStyle.Fill;
				this.selectedTabPage.Visible = true;
				this.selectedTabPage.BringToFront();
				foreach (object obj in mtp.Controls)
				{
					Control control = (Control)obj;
					if (control is MyWebBrowser)
					{
						MyWebBrowser myWebBrowser = (MyWebBrowser)control;
						myWebBrowser.RefreshSummary();
					}
				}
			}
			foreach (object obj2 in this.p_tabStrip.Controls)
			{
				Button button = (Button)obj2;
				MyTabPage myTabPage = (MyTabPage)button.Tag;
				if (this.selectedTabPage != null && myTabPage == this.selectedTabPage)
				{
					this.FocusButton(button);
				}
				else
				{
					this.FadoutButton(button);
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000BB70 File Offset: 0x0000AB70
		private Button FindTabButton(MyTabPage mtp)
		{
			foreach (object obj in this.p_tabStrip.Controls)
			{
				Button button = (Button)obj;
				MyTabPage myTabPage = (MyTabPage)button.Tag;
				if (myTabPage == mtp)
				{
					return button;
				}
			}
			return null;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000BC00 File Offset: 0x0000AC00
		public void FocusButton(Button btn)
		{
			btn.BackColor = SystemColors.ActiveCaption;
			btn.ForeColor = SystemColors.ActiveCaptionText;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000BC1B File Offset: 0x0000AC1B
		public void FadoutButton(Button btn)
		{
			btn.BackColor = SystemColors.InactiveCaption;
			btn.ForeColor = SystemColors.InactiveCaptionText;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000BC38 File Offset: 0x0000AC38
		private void btn_Click(object sender, EventArgs e)
		{
			if (sender == null)
			{
				this.ShowTabPage(null);
			}
			else
			{
				this.ShowTabPage((MyTabPage)((Button)sender).Tag);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000BC74 File Offset: 0x0000AC74
		public Panel TabButtonsPanel
		{
			get
			{
				return this.p_tabStrip;
			}
		}

		// Token: 0x04000155 RID: 341
		private FlowLayoutPanel p_tabStrip;

		// Token: 0x04000156 RID: 342
		private Container components = null;

		// Token: 0x04000157 RID: 343
		public MyTabPage selectedTabPage = null;
	}
}
