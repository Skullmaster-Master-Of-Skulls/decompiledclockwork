using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using AutoComboBox.Properties;

namespace AutoComboBox.HelperForms
{
	// Token: 0x0200003C RID: 60
	public partial class HtmlMessageBox : Form
	{
		// Token: 0x06000203 RID: 515 RVA: 0x00012711 File Offset: 0x00011711
		public HtmlMessageBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001272A File Offset: 0x0001172A
		private void HtmlMessageBox_Load(object sender, EventArgs e)
		{
			this.myWebBrowser1.HideEverythingButBrowser();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00012739 File Offset: 0x00011739
		public void ShowMessage(string html)
		{
			this.myWebBrowser1.ShowHtml(html);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0001274C File Offset: 0x0001174C
		public static DialogResult ShowHtmlMessageBox(string title, string html)
		{
			return HtmlMessageBox.ShowHtmlMessageBox(null, title, html);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00012768 File Offset: 0x00011768
		public static DialogResult ShowHtmlMessageBox(IWin32Window owner, string title, string html)
		{
			HtmlMessageBox htmlMessageBox = new HtmlMessageBox();
			htmlMessageBox.Text = title;
			htmlMessageBox.ShowMessage(html);
			return htmlMessageBox.ShowDialog(owner);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00012799 File Offset: 0x00011799
		private void btn_print_Click(object sender, EventArgs e)
		{
			this.myWebBrowser1.Print();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000127A8 File Offset: 0x000117A8
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
