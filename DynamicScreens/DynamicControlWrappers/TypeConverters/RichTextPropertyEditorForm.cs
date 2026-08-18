using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using DynamicScreens.Properties;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200005E RID: 94
	public partial class RichTextPropertyEditorForm : Form
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x000412C2 File Offset: 0x000402C2
		public RichTextPropertyEditorForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000412DC File Offset: 0x000402DC
		private void RichTextPropertyEditor_Load(object sender, EventArgs e)
		{
			if (this.richText.Trim().Length > 0 && this.richText.IndexOf("{\\rtf") == 0)
			{
				this.richTextBox.Text = this.richText;
				this.textBox.Visible = false;
				this.richTextBox.Visible = true;
				this.btn_switchRichPlain.Text = "Switch to plain text";
			}
			else
			{
				this.textBox.Text = this.richText;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00041370 File Offset: 0x00040370
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00041388 File Offset: 0x00040388
		public string RichText
		{
			get
			{
				return this.richText;
			}
			set
			{
				this.richText = value;
				this.richTextBox.Text = this.richText;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000413A4 File Offset: 0x000403A4
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			this.richText = (this.richTextBox.Visible ? this.richTextBox.Text : this.textBox.Text);
			base.Close();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000413E1 File Offset: 0x000403E1
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000413EC File Offset: 0x000403EC
		private void btn_switchRichPlain_Click(object sender, EventArgs e)
		{
			if (this.richTextBox.Visible)
			{
				this.textBox.Text = this.richTextBox.PlainText;
				this.richTextBox.Visible = false;
				this.textBox.Visible = true;
				this.btn_switchRichPlain.Text = "Switch to rich text";
			}
			else
			{
				this.richTextBox.PlainText = this.textBox.Text;
				this.textBox.Visible = false;
				this.richTextBox.Visible = true;
				this.btn_switchRichPlain.Text = "Switch to plain text";
			}
		}

		// Token: 0x0400037C RID: 892
		private string richText;
	}
}
