using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox
{
	// Token: 0x02000046 RID: 70
	public class AttachmentsControl : UserControl
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00015738 File Offset: 0x00014738
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00015770 File Offset: 0x00014770
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AttachmentsControl));
			this.lv = new ListViewEx();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.openToolStripMenuItem = new ToolStripMenuItem();
			this.removeToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.copyFilenameToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.attachNewFileToolStripMenuItem = new ToolStripMenuItem();
			this.imageList1 = new ImageList(this.components);
			this.openFileDialog1 = new OpenFileDialog();
			this.labelX1 = new LabelX();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.lv.AccessibleDescription = "Attachments listing";
			this.lv.AccessibleName = "Attachments listing";
			this.lv.Border.Class = "ListViewBorder";
			this.lv.ContextMenuStrip = this.contextMenuStrip1;
			this.lv.Dock = DockStyle.Fill;
			this.lv.Location = new Point(2, 19);
			this.lv.Name = "lv";
			this.lv.Size = new Size(133, 116);
			this.lv.SmallImageList = this.imageList1;
			this.lv.TabIndex = 0;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = View.List;
			this.lv.DoubleClick += this.lv_DoubleClick;
			this.lv.KeyDown += this.lv_KeyDown;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.openToolStripMenuItem,
				this.removeToolStripMenuItem,
				this.toolStripSeparator1,
				this.copyFilenameToolStripMenuItem,
				this.toolStripMenuItem1,
				this.attachNewFileToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(154, 126);
			this.contextMenuStrip1.Opening += this.contextMenuStrip1_Opening;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new Size(153, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += this.openToolStripMenuItem_Click;
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new Size(153, 22);
			this.removeToolStripMenuItem.Text = "&Remove";
			this.removeToolStripMenuItem.Click += this.removeToolStripMenuItem_Click;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(150, 6);
			this.copyFilenameToolStripMenuItem.Name = "copyFilenameToolStripMenuItem";
			this.copyFilenameToolStripMenuItem.Size = new Size(153, 22);
			this.copyFilenameToolStripMenuItem.Text = "Co&py filename";
			this.copyFilenameToolStripMenuItem.Click += this.copyFilenameToolStripMenuItem_Click;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(150, 6);
			this.attachNewFileToolStripMenuItem.Name = "attachNewFileToolStripMenuItem";
			this.attachNewFileToolStripMenuItem.Size = new Size(153, 22);
			this.attachNewFileToolStripMenuItem.Text = "&Attach new file";
			this.attachNewFileToolStripMenuItem.Click += this.attachNewFileToolStripMenuItem_Click;
			this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "page_excel.png");
			this.imageList1.Images.SetKeyName(1, "page_white_word.png");
			this.imageList1.Images.SetKeyName(2, "page_white_link.png");
			this.imageList1.Images.SetKeyName(3, "application_firefox.gif");
			this.imageList1.Images.SetKeyName(4, "file_acrobat.gif");
			this.openFileDialog1.FileName = "openFileDialog1";
			this.labelX1.Dock = DockStyle.Top;
			this.labelX1.Location = new Point(2, 2);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new Size(133, 17);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "Attachments:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.lv);
			base.Controls.Add(this.labelX1);
			base.Name = "AttachmentsControl";
			base.Padding = new Padding(2);
			base.Size = new Size(137, 137);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00015CF4 File Offset: 0x00014CF4
		public AttachmentsControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00015D10 File Offset: 0x00014D10
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00015D28 File Offset: 0x00014D28
		public override string Text
		{
			get
			{
				return this.GetListViewItems();
			}
			set
			{
				this.SetListViewItems(value);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00015D34 File Offset: 0x00014D34
		private string GetListViewItems()
		{
			string text = "";
			foreach (object obj in this.lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string str = (string)listViewItem.Tag;
				if (text.Length > 0)
				{
					text += "; ";
				}
				text += str;
			}
			return text;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00015DE0 File Offset: 0x00014DE0
		public void SetListViewItems(string fileList)
		{
			if (fileList.Trim().Length < 1)
			{
				this.lv.Items.Clear();
			}
			else
			{
				char c = (fileList.IndexOf(';') > 0) ? ';' : ',';
				string[] array = fileList.Split(new char[]
				{
					c
				});
				this.lv.BeginUpdate();
				this.lv.Items.Clear();
				foreach (string text in array)
				{
					string text2 = text.Replace("\"", "");
					ListViewItem listViewItem = new ListViewItem(Path.GetFileName(text2));
					listViewItem.ImageIndex = this.GetExtensionImageIndex(Path.GetExtension(text2).ToLower());
					listViewItem.Tag = text2;
					this.lv.Items.Add(listViewItem);
				}
				this.lv.EndUpdate();
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00015EE8 File Offset: 0x00014EE8
		private int GetExtensionImageIndex(string ext)
		{
			switch (ext)
			{
			case ".doc":
			case ".rtf":
			case ".txt":
				return 1;
			case ".pdf":
				return 4;
			case ".xls":
			case ".xlsx":
				return 0;
			case ".htm":
			case ".html":
				return 3;
			}
			return 2;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00015FC2 File Offset: 0x00014FC2
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.OpenSelectedAttachment();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00015FCC File Offset: 0x00014FCC
		private void OpenSelectedAttachment()
		{
			ListViewItem listViewItem = (this.lv.SelectedItems.Count == 1) ? this.lv.SelectedItems[0] : null;
			if (listViewItem != null)
			{
				string text = (string)listViewItem.Tag;
				try
				{
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(text + ": " + ex.ToString());
				}
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00016050 File Offset: 0x00015050
		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ListViewItem listViewItem = (this.lv.SelectedItems.Count == 1) ? this.lv.SelectedItems[0] : null;
			if (listViewItem != null)
			{
				this.RemoveItem(listViewItem);
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00016098 File Offset: 0x00015098
		private void RemoveItem(ListViewItem lvi)
		{
			this.lv.Items.Remove(lvi);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000160AD File Offset: 0x000150AD
		private void attachNewFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Attach();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000160B8 File Offset: 0x000150B8
		public void Attach()
		{
			DialogResult dialogResult = this.openFileDialog1.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				ListViewItem listViewItem = new ListViewItem(Path.GetFileName(this.openFileDialog1.FileName));
				listViewItem.Tag = this.openFileDialog1.FileName;
				listViewItem.ImageIndex = this.GetExtensionImageIndex(Path.GetExtension(this.openFileDialog1.FileName).ToLower());
				this.lv.Items.Add(listViewItem);
				this.UnselectAllItems(this.lv);
				listViewItem.Selected = true;
				this.lv.EnsureVisible(listViewItem.Index);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00016168 File Offset: 0x00015168
		private void UnselectAllItems(ListView lv)
		{
			foreach (object obj in lv.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Selected = false;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000161D0 File Offset: 0x000151D0
		private void contextMenuStrip1_Layout(object sender, LayoutEventArgs e)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000161D4 File Offset: 0x000151D4
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			bool enabled = this.lv.SelectedItems.Count == 1;
			this.openToolStripMenuItem.Enabled = enabled;
			this.removeToolStripMenuItem.Enabled = enabled;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00016210 File Offset: 0x00015210
		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.OpenSelectedAttachment();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001621C File Offset: 0x0001521C
		private void copyFilenameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ListViewItem listViewItem = (this.lv.SelectedItems.Count == 1) ? this.lv.SelectedItems[0] : null;
			if (listViewItem != null)
			{
				string text = (string)listViewItem.Tag;
				Clipboard.SetText(text);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00016270 File Offset: 0x00015270
		private void lv_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.OpenSelectedAttachment();
			}
		}

		// Token: 0x04000212 RID: 530
		private IContainer components = null;

		// Token: 0x04000213 RID: 531
		private ListViewEx lv;

		// Token: 0x04000214 RID: 532
		private ImageList imageList1;

		// Token: 0x04000215 RID: 533
		private ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000216 RID: 534
		private ToolStripMenuItem openToolStripMenuItem;

		// Token: 0x04000217 RID: 535
		private ToolStripMenuItem removeToolStripMenuItem;

		// Token: 0x04000218 RID: 536
		private ToolStripSeparator toolStripMenuItem1;

		// Token: 0x04000219 RID: 537
		private ToolStripMenuItem attachNewFileToolStripMenuItem;

		// Token: 0x0400021A RID: 538
		private OpenFileDialog openFileDialog1;

		// Token: 0x0400021B RID: 539
		private LabelX labelX1;

		// Token: 0x0400021C RID: 540
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400021D RID: 541
		private ToolStripMenuItem copyFilenameToolStripMenuItem;
	}
}
