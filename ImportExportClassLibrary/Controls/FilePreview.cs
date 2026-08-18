using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace ImportExportClassLibrary.Controls
{
	// Token: 0x02000020 RID: 32
	public class FilePreview : UserControl
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004FB2 File Offset: 0x00003FB2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004FD4 File Offset: 0x00003FD4
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FilePreview));
			this.richTextBox1 = new RichTextBox();
			this.myWebBrowser1 = new MyWebBrowser();
			base.SuspendLayout();
			this.richTextBox1.Dock = DockStyle.Fill;
			this.richTextBox1.Location = new Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new Size(540, 375);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.richTextBox1.Visible = false;
			this.myWebBrowser1.Css = componentResourceManager.GetString("myWebBrowser1.Css");
			this.myWebBrowser1.Dock = DockStyle.Fill;
			this.myWebBrowser1.Location = new Point(0, 0);
			this.myWebBrowser1.MyPanel = null;
			this.myWebBrowser1.Name = "myWebBrowser1";
			this.myWebBrowser1.Size = new Size(540, 375);
			this.myWebBrowser1.TabIndex = 1;
			this.myWebBrowser1.Title = "";
			this.myWebBrowser1.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.myWebBrowser1);
			base.Controls.Add(this.richTextBox1);
			base.Name = "FilePreview";
			base.Size = new Size(540, 375);
			base.ResumeLayout(false);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005174 File Offset: 0x00004174
		public FilePreview()
		{
			this.InitializeComponent();
			this.myWebBrowser1.HideEverythingButBrowser();
			this.myWebBrowser1.RemoveNavigatingHandler();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005198 File Offset: 0x00004198
		public string PreviewFile(string fileName)
		{
			this.HideAll();
			string text = Path.GetExtension(fileName).ToLower();
			string key;
			switch (key = text)
			{
			case ".html":
			case ".htm":
				return this.ShowBrowser(fileName);
			case ".pdf":
				return this.ShowBrowser(fileName);
			case ".doc":
			case ".docx":
				return this.ShowRichTextBox(fileName);
			case ".xls":
			case ".xlsx":
				return "Not supported";
			case ".txt":
				return this.ShowRichTextBox(fileName);
			case ".rtf":
				return this.ShowRichTextBox(fileName);
			case ".jpg":
			case ".jpeg":
				return this.ShowBrowser(fileName);
			case ".png":
				return this.ShowBrowser(fileName);
			case ".gif":
				return this.ShowBrowser(fileName);
			case ".bmp":
				return this.ShowBrowser(fileName);
			case ".tiff":
				return this.ShowBrowser(fileName);
			}
			return "Not supported";
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005348 File Offset: 0x00004348
		private string ShowRichTextBox(string fileName)
		{
			this.richTextBox1.Visible = true;
			string result;
			try
			{
				string text = Path.GetExtension(fileName).ToLower();
				string a;
				if ((a = text) != null)
				{
					if (a == ".doc" || a == ".docx")
					{
						TemplatesClass.PreviewWord(fileName, this.richTextBox1);
						return null;
					}
					if (a == ".rtf")
					{
						this.richTextBox1.Rtf = File.ReadAllText(fileName);
						return null;
					}
				}
				this.richTextBox1.Text = File.ReadAllText(fileName);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000053F0 File Offset: 0x000043F0
		private string ShowBrowser(string fileName)
		{
			string result;
			try
			{
				this.myWebBrowser1.Visible = true;
				result = this.myWebBrowser1.NavigateTo(fileName);
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005434 File Offset: 0x00004434
		public void HideAll()
		{
			this.myWebBrowser1.Visible = false;
			this.richTextBox1.Visible = false;
		}

		// Token: 0x04000039 RID: 57
		private IContainer components;

		// Token: 0x0400003A RID: 58
		private RichTextBox richTextBox1;

		// Token: 0x0400003B RID: 59
		private MyWebBrowser myWebBrowser1;
	}
}
