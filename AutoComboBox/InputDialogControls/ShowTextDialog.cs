using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using AutoComboBox.Properties;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200001F RID: 31
	public partial class ShowTextDialog : Form
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000A651 File Offset: 0x00009651
		public ShowTextDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000A671 File Offset: 0x00009671
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000A67C File Offset: 0x0000967C
		public HtmlRichTextBox Rtf
		{
			get
			{
				return this.rtf;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000A694 File Offset: 0x00009694
		private void btn_increaseFontSize_Click(object sender, EventArgs e)
		{
			this.IncreaseDecreaseFontSize(0.3f);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000A6A3 File Offset: 0x000096A3
		private void btn_decreaseFontSize_Click(object sender, EventArgs e)
		{
			this.IncreaseDecreaseFontSize(-0.3f);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000A6B4 File Offset: 0x000096B4
		private void IncreaseDecreaseFontSize(float increment)
		{
			float num = this.rtf.ZoomFactor + increment;
			if (num <= 0f)
			{
				num = 0.1f;
			}
			if (num > 1000f)
			{
				num = 1000f;
			}
			this.rtf.ZoomFactor = num;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000A704 File Offset: 0x00009704
		private void btn_print_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000A707 File Offset: 0x00009707
		private void ppd_Load(object sender, EventArgs e)
		{
			((Form)sender).WindowState = FormWindowState.Maximized;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000A717 File Offset: 0x00009717
		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Print(true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000A722 File Offset: 0x00009722
		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Print(false);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A730 File Offset: 0x00009730
		private void Print(bool printPreview)
		{
			if (this.printDialog == null)
			{
				this.printDialog = new PrintDialog();
				this.printDialog.UseEXDialog = true;
			}
			DialogResult dialogResult = this.printDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				RichTextBoxPrintCtrl richTextBoxPrintCtrl = new RichTextBoxPrintCtrl();
				richTextBoxPrintCtrl.Rtf = this.rtf.Rtf;
				PrintDocument printDocument = new PrintDocument();
				printDocument.BeginPrint += richTextBoxPrintCtrl.printDocument1_BeginPrint;
				printDocument.PrintPage += richTextBoxPrintCtrl.printDocument1_PrintPage;
				this.printDialog.Document = printDocument;
				if (printPreview)
				{
					PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
					printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
					printPreviewDialog.Document = printDocument;
					printPreviewDialog.Load += this.ppd_Load;
					printPreviewDialog.ShowDialog();
				}
				else
				{
					printDocument.Print();
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A82C File Offset: 0x0000982C
		private void ShowTextDialog_Load(object sender, EventArgs e)
		{
			this.rtf.SelectionStart = 0;
			this.rtf.SelectionLength = 0;
		}

		// Token: 0x04000146 RID: 326
		private PrintDialog printDialog = null;
	}
}
