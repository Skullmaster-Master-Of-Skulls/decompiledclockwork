using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.Properties;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000C6 RID: 198
	public class MyFile : UserControl, MyDynamicControl
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0003BFE8 File Offset: 0x0003AFE8
		public new string ToString()
		{
			return "Un-supported";
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0003BFFF File Offset: 0x0003AFFF
		public void FromString(string s)
		{
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0003C004 File Offset: 0x0003B004
		public object ReportObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0003C018 File Offset: 0x0003B018
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0003C030 File Offset: 0x0003B030
		public string Filename
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
				this.txt.Text = this.filename;
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003C04C File Offset: 0x0003B04C
		public MyFile()
		{
			this.filename = "";
			this.InitializeComponent();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003C070 File Offset: 0x0003B070
		private void txt_ButtonCustomClick(object sender, EventArgs e)
		{
			if (this.filename.Length < 1)
			{
				MessageBox.Show("No file!");
			}
			else if (!File.Exists(this.filename))
			{
				MessageBox.Show("Can't find file!");
			}
			else
			{
				try
				{
					Process.Start(this.filename);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003C0F0 File Offset: 0x0003B0F0
		private void txt_ButtonCustom2Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult dialogResult = openFileDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.Filename = openFileDialog.FileName;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0003C128 File Offset: 0x0003B128
		public bool FilledIn
		{
			get
			{
				return this.txt.Text.Trim().Length > 0;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003C154 File Offset: 0x0003B154
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0003C18C File Offset: 0x0003B18C
		private void InitializeComponent()
		{
			this.txt = new TextBoxX();
			base.SuspendLayout();
			this.txt.Border.Class = "TextBoxBorder";
			this.txt.ButtonCustom.Image = Resources.folder_out;
			this.txt.ButtonCustom.Text = "view";
			this.txt.ButtonCustom.Visible = true;
			this.txt.ButtonCustom2.Image = Resources.paperclip;
			this.txt.ButtonCustom2.Text = "browse";
			this.txt.ButtonCustom2.Visible = true;
			this.txt.Dock = DockStyle.Fill;
			this.txt.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txt.Location = new Point(0, 0);
			this.txt.Margin = new Padding(4);
			this.txt.Name = "txt";
			this.txt.Size = new Size(388, 22);
			this.txt.TabIndex = 0;
			this.txt.ButtonCustomClick += this.txt_ButtonCustomClick;
			this.txt.ButtonCustom2Click += this.txt_ButtonCustom2Click;
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.txt);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(4);
			base.Name = "MyFile";
			base.Size = new Size(388, 28);
			base.ResumeLayout(false);
		}

		// Token: 0x040005C2 RID: 1474
		private string filename;

		// Token: 0x040005C3 RID: 1475
		private IContainer components = null;

		// Token: 0x040005C4 RID: 1476
		private TextBoxX txt;
	}
}
