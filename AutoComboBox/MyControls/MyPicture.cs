using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000061 RID: 97
	public class MyPicture : UserControl
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0001B7FC File Offset: 0x0001A7FC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001B834 File Offset: 0x0001A834
		private void InitializeComponent()
		{
			this.components = new Container();
			this.toolStrip1 = new ToolStrip();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.lbl_formSummary = new Label();
			this.cm_image = new ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new ToolStripMenuItem();
			this.pasteToolStripMenuItem = new ToolStripMenuItem();
			this.pictureBox1 = new MyPictureBox();
			this.btn_load = new ToolStripButton();
			this.btn_clearImage = new ToolStripButton();
			this.toolStripDropDownButton1 = new ToolStripDropDownButton();
			this.printToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.printPreviewToolStripMenuItem = new ToolStripMenuItem();
			this.pageSetupToolStripMenuItem = new ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
			this.cm_image.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_load,
				this.btn_clearImage,
				this.toolStripSeparator1,
				this.toolStripDropDownButton1
			});
			this.toolStrip1.Location = new Point(0, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(318, 25);
			this.toolStrip1.TabIndex = 5;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.toolStripSeparator1.Visible = false;
			this.lbl_formSummary.AutoSize = true;
			this.lbl_formSummary.Dock = DockStyle.Top;
			this.lbl_formSummary.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl_formSummary.Location = new Point(0, 0);
			this.lbl_formSummary.Name = "lbl_formSummary";
			this.lbl_formSummary.Size = new Size(53, 16);
			this.lbl_formSummary.TabIndex = 4;
			this.lbl_formSummary.Text = "Picture";
			this.cm_image.Items.AddRange(new ToolStripItem[]
			{
				this.copyToolStripMenuItem,
				this.pasteToolStripMenuItem
			});
			this.cm_image.Name = "cm_image";
			this.cm_image.Size = new Size(103, 48);
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new Size(102, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += this.copyToolStripMenuItem_Click;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new Size(102, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += this.pasteToolStripMenuItem_Click;
			this.pictureBox1.ContextMenuStrip = this.cm_image;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Location = new Point(0, 41);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(318, 239);
			this.pictureBox1.SizeMode = MyPictureBox.PhotoBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			this.btn_load.AccessibleDescription = "load image";
			this.btn_load.AccessibleName = "load image";
			this.btn_load.Image = Resources.disk_blue;
			this.btn_load.ImageTransparentColor = Color.Magenta;
			this.btn_load.Name = "btn_load";
			this.btn_load.Size = new Size(89, 22);
			this.btn_load.Text = "Load image";
			this.btn_load.Click += this.btn_load_Click;
			this.btn_clearImage.Image = Resources.document_plain;
			this.btn_clearImage.ImageTransparentColor = Color.Magenta;
			this.btn_clearImage.Name = "btn_clearImage";
			this.btn_clearImage.Size = new Size(90, 22);
			this.btn_clearImage.Text = "Clear image";
			this.btn_clearImage.Click += this.btn_clearImage_Click;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.printToolStripMenuItem,
				this.toolStripMenuItem1,
				this.printPreviewToolStripMenuItem,
				this.pageSetupToolStripMenuItem
			});
			this.toolStripDropDownButton1.Image = Resources.printer;
			this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new Size(61, 22);
			this.toolStripDropDownButton1.Text = "&Print";
			this.toolStripDropDownButton1.Visible = false;
			this.printToolStripMenuItem.Image = Resources.printer;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new Size(143, 22);
			this.printToolStripMenuItem.Text = "&Print (ctrl+p)";
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(140, 6);
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new Size(143, 22);
			this.printPreviewToolStripMenuItem.Text = "Print pre&view";
			this.pageSetupToolStripMenuItem.Image = Resources.printer_view;
			this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
			this.pageSetupToolStripMenuItem.Size = new Size(143, 22);
			this.pageSetupToolStripMenuItem.Text = "Page setu&p";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl_formSummary);
			base.Name = "MyPicture";
			base.Size = new Size(318, 280);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.cm_image.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001BF38 File Offset: 0x0001AF38
		public MyPicture()
		{
			this.ImageSaveLoadSize = Size.Empty;
			this.InitializeComponent();
			this.pictureBox1.SizeMode = MyPictureBox.PhotoBoxSizeMode.ScaleImage;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0001BF6C File Offset: 0x0001AF6C
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0001BF83 File Offset: 0x0001AF83
		public Size ImageSaveLoadSize { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0001BF8C File Offset: 0x0001AF8C
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0001BFA9 File Offset: 0x0001AFA9
		public new BorderStyle BorderStyle
		{
			get
			{
				return this.pictureBox1.BorderStyle;
			}
			set
			{
				this.pictureBox1.BorderStyle = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001BFBC File Offset: 0x0001AFBC
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0001BFD9 File Offset: 0x0001AFD9
		public Image Image
		{
			get
			{
				return this.pictureBox1.Image;
			}
			set
			{
				this.pictureBox1.Image = value;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001BFEC File Offset: 0x0001AFEC
		public string GetBase64String()
		{
			Image image = this.Image;
			string result;
			if (image == null)
			{
				result = "";
			}
			else
			{
				if (this.ImageSaveLoadSize != Size.Empty)
				{
					Image image2 = image;
					Size size = this.GenerateImageDimensions(image2.Width, image2.Height, this.pictureBox1.Width, this.pictureBox1.Height);
					Bitmap bitmap = new Bitmap(image2, size.Width, size.Height);
					image = bitmap;
				}
				ImageConverter imageConverter = new ImageConverter();
				byte[] inArray = new byte[1];
				Bitmap value = (Bitmap)image;
				inArray = (byte[])imageConverter.ConvertTo(value, typeof(byte[]));
				string text = Convert.ToBase64String(inArray);
				result = text;
			}
			return result;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001C0BC File Offset: 0x0001B0BC
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001C0DC File Offset: 0x0001B0DC
		public string Title
		{
			get
			{
				return this.lbl_formSummary.Text;
			}
			set
			{
				if (string.IsNullOrEmpty(this.lbl_formSummary.Text))
				{
					this.lbl_formSummary.Visible = false;
				}
				else
				{
					this.lbl_formSummary.Text = value;
					if (!this.lbl_formSummary.Visible)
					{
						this.lbl_formSummary.Visible = true;
					}
					base.AccessibleDescription = value;
					base.AccessibleName = value;
				}
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001C150 File Offset: 0x0001B150
		private void btn_load_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Please select an image";
			openFileDialog.Filter = "Image Files|*.jpg;*.gif;*.bmp;*.png;*.jpeg|All Files|*.*";
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.Image = Image.FromFile(openFileDialog.FileName);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001C1A1 File Offset: 0x0001B1A1
		private void btn_clearImage_Click(object sender, EventArgs e)
		{
			this.Image = null;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001C1AC File Offset: 0x0001B1AC
		public Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
		{
			double num = 0.0;
			string text;
			if (currH > currW)
			{
				text = "portrait";
			}
			else
			{
				text = "landscape";
			}
			string text2 = text.ToLower();
			if (text2 != null)
			{
				if (!(text2 == "portrait"))
				{
					if (text2 == "landscape")
					{
						if (destH > destW)
						{
							num = (double)destW / (double)currW;
						}
						else
						{
							num = (double)destH / (double)currH;
						}
					}
				}
				else if (destH > destW)
				{
					num = (double)destW / (double)currW;
				}
				else
				{
					num = (double)destH / (double)currH;
				}
			}
			return new Size((int)((double)currW * num), (int)((double)currH * num));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001C25C File Offset: 0x0001B25C
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Image image = this.Image;
			if (image != null)
			{
				Clipboard.SetImage(image);
			}
			else
			{
				MessageBox.Show("No image to copy!");
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001C290 File Offset: 0x0001B290
		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Clipboard.GetDataObject().GetDataPresent("Bitmap"))
			{
				object data = Clipboard.GetDataObject().GetData("Bitmap");
				if (data != null)
				{
					this.pictureBox1.Image = (Image)data;
				}
			}
		}

		// Token: 0x04000353 RID: 851
		private IContainer components = null;

		// Token: 0x04000354 RID: 852
		private ToolStrip toolStrip1;

		// Token: 0x04000355 RID: 853
		private ToolStripButton btn_load;

		// Token: 0x04000356 RID: 854
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000357 RID: 855
		private ToolStripDropDownButton toolStripDropDownButton1;

		// Token: 0x04000358 RID: 856
		private ToolStripMenuItem printToolStripMenuItem;

		// Token: 0x04000359 RID: 857
		private ToolStripSeparator toolStripMenuItem1;

		// Token: 0x0400035A RID: 858
		private ToolStripMenuItem printPreviewToolStripMenuItem;

		// Token: 0x0400035B RID: 859
		private ToolStripMenuItem pageSetupToolStripMenuItem;

		// Token: 0x0400035C RID: 860
		private Label lbl_formSummary;

		// Token: 0x0400035D RID: 861
		private MyPictureBox pictureBox1;

		// Token: 0x0400035E RID: 862
		private ToolStripButton btn_clearImage;

		// Token: 0x0400035F RID: 863
		private ContextMenuStrip cm_image;

		// Token: 0x04000360 RID: 864
		private ToolStripMenuItem copyToolStripMenuItem;

		// Token: 0x04000361 RID: 865
		private ToolStripMenuItem pasteToolStripMenuItem;
	}
}
