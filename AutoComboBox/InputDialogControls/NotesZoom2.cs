using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using AutoComboBox.Properties;
using SpellCheckerEx;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x02000080 RID: 128
	public partial class NotesZoom2 : Form
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x00029F4B File Offset: 0x00028F4B
		public NotesZoom2()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00029F74 File Offset: 0x00028F74
		public NotesZoom2(bool ReadOnly)
		{
			this.InitializeComponent();
			if (ReadOnly)
			{
				this.textBox1.ReadOnly = ReadOnly;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00029FBC File Offset: 0x00028FBC
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x00029FED File Offset: 0x00028FED
		public string TextEntered
		{
			get
			{
				return this.textBox1.Text.Replace('\n'.ToString(), Environment.NewLine);
			}
			set
			{
				this.textBox1.Text = value;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002A000 File Offset: 0x00029000
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Add && e.Control)
			{
				this.IncreaseDecreaseFontSize(0.3f);
			}
			else if (e.KeyCode == Keys.Subtract && e.Control)
			{
				this.IncreaseDecreaseFontSize(-0.3f);
			}
			else if (e.KeyCode == Keys.F7)
			{
				this.SpellCheck();
			}
			else
			{
				base.OnKeyUp(e);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002A088 File Offset: 0x00029088
		private void IncreaseDecreaseFontSize(float increment)
		{
			float num = this.textBox1.RichTextBox.ZoomFactor + increment;
			if (num <= 0f)
			{
				num = 0.1f;
			}
			if (num > 1000f)
			{
				num = 1000f;
			}
			this.textBox1.RichTextBox.ZoomFactor = num;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002A0E2 File Offset: 0x000290E2
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002A0EC File Offset: 0x000290EC
		private void btn_save_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002A0FE File Offset: 0x000290FE
		private void btn_spellCheck_Click(object sender, EventArgs e)
		{
			this.SpellCheck();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002A108 File Offset: 0x00029108
		private void SpellCheck()
		{
			this.sharpSpell.SpellCheck();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002A117 File Offset: 0x00029117
		private void btn_increaseFontSize_Click(object sender, EventArgs e)
		{
			this.IncreaseDecreaseFontSize(0.3f);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002A126 File Offset: 0x00029126
		private void btn_decreaseFontSize_Click(object sender, EventArgs e)
		{
			this.IncreaseDecreaseFontSize(-0.3f);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002A138 File Offset: 0x00029138
		private void NotesZoom_Load(object sender, EventArgs e)
		{
			try
			{
				this.sharpSpell = new SpellCheckEx(this.textBox1.RichTextBox, this.GetDictionaryPath(), ClientCache.CurrentInstance.DefaultDictionaryFile);
				this.sharpSpell.UnderlineMisSpelledEnabled = true;
			}
			catch
			{
			}
			base.ActiveControl = this.textBox1;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0002A1A0 File Offset: 0x000291A0
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0002A1C2 File Offset: 0x000291C2
		public float ZoomFactor
		{
			get
			{
				return this.textBox1.RichTextBox.ZoomFactor;
			}
			set
			{
				this.textBox1.RichTextBox.ZoomFactor = value;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002A1D8 File Offset: 0x000291D8
		private string GetDictionaryPath()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TechnoPro\\ClockWork\\Dictionaries");
			string result;
			if (Directory.Exists(text))
			{
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002A212 File Offset: 0x00029212
		private void btn_suspend_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002A224 File Offset: 0x00029224
		private void Print(bool printPreview, string rtf)
		{
			if (this.printDialog == null)
			{
				this.printDialog = new PrintDialog
				{
					UseEXDialog = true
				};
			}
			DialogResult dialogResult = this.printDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				RichTextBoxPrintCtrl richTextBoxPrintCtrl = new RichTextBoxPrintCtrl();
				richTextBoxPrintCtrl.Rtf = rtf;
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

		// Token: 0x0600051F RID: 1311 RVA: 0x0002A318 File Offset: 0x00029318
		private void ppd_Load(object sender, EventArgs e)
		{
			((Form)sender).WindowState = FormWindowState.Maximized;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002A328 File Offset: 0x00029328
		private void btn_print_Click(object sender, EventArgs e)
		{
			this.Print(true, this.textBox1.RichTextBox.Rtf);
		}

		// Token: 0x04000439 RID: 1081
		private SpellCheckEx sharpSpell = null;

		// Token: 0x0400043A RID: 1082
		private PrintDialog printDialog = null;
	}
}
