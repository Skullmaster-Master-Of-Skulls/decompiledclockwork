namespace AutoComboBox.HelperForms
{
	// Token: 0x02000086 RID: 134
	public partial class DataGridView2 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0002C634 File Offset: 0x0002B634
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0002C66C File Offset: 0x0002B66C
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_export = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_exportToXml = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportToExcelToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_select = new global::System.Windows.Forms.ToolStripButton();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.lbl_caption = new global::System.Windows.Forms.Label();
			this.radGridView1 = new global::TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid.CtrlGrid();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_export,
				this.toolStripSeparator2,
				this.btn_select,
				this.btn_close
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 473);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(784, 39);
			this.toolStrip1.TabIndex = 13;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_export.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_exportToXml,
				this.exportToExcelToolStripMenuItem
			});
			this.btn_export.Image = global::AutoComboBox.Properties.Resources.export2;
			this.btn_export.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_export.Name = "btn_export";
			this.btn_export.Size = new global::System.Drawing.Size(98, 36);
			this.btn_export.Text = "&Export";
			this.btn_export.Visible = false;
			this.btn_exportToXml.Name = "btn_exportToXml";
			this.btn_exportToXml.Size = new global::System.Drawing.Size(180, 22);
			this.btn_exportToXml.Text = "Export to &xml";
			this.btn_exportToXml.Click += new global::System.EventHandler(this.btn_exportToXml_Click);
			this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
			this.exportToExcelToolStripMenuItem.Size = new global::System.Drawing.Size(180, 22);
			this.exportToExcelToolStripMenuItem.Text = "Export to Excel";
			this.exportToExcelToolStripMenuItem.Visible = false;
			this.exportToExcelToolStripMenuItem.Click += new global::System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_select.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_select.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_select.Name = "btn_select";
			this.btn_select.Size = new global::System.Drawing.Size(88, 36);
			this.btn_select.Text = "&Select";
			this.btn_select.Visible = false;
			this.btn_select.Click += new global::System.EventHandler(this.btn_select_Click);
			this.btn_close.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.lbl_caption.AutoSize = true;
			this.lbl_caption.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_caption.Location = new global::System.Drawing.Point(0, 0);
			this.lbl_caption.Name = "lbl_caption";
			this.lbl_caption.Size = new global::System.Drawing.Size(42, 13);
			this.lbl_caption.TabIndex = 50;
			this.lbl_caption.Text = "caption";
			this.lbl_caption.Visible = false;
			this.radGridView1.AutoGenerateColumns = true;
			this.radGridView1.DataSource = null;
			this.radGridView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.radGridView1.DontShowFilteringRow = false;
			this.radGridView1.EnableAlternatingRowColor = true;
			this.radGridView1.EnableFiltering = true;
			this.radGridView1.EnableGrouping = true;
			this.radGridView1.Location = new global::System.Drawing.Point(0, 13);
			this.radGridView1.MultiSelect = false;
			this.radGridView1.Name = "radGridView1";
			this.radGridView1.Size = new global::System.Drawing.Size(784, 460);
			this.radGridView1.TabIndex = 51;
			this.radGridView1.ThemeName = "Office2010Black";
			this.radGridView1.DoubleClick += new global::System.EventHandler(this.radGridView1_DoubleClick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(784, 512);
			base.Controls.Add(this.radGridView1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl_caption);
			base.Name = "DataGridView2";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Listing";
			base.Load += new global::System.EventHandler(this.DataGridView2_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000472 RID: 1138
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000473 RID: 1139
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000474 RID: 1140
		private global::System.Windows.Forms.ToolStripDropDownButton btn_export;

		// Token: 0x04000475 RID: 1141
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToXml;

		// Token: 0x04000476 RID: 1142
		private global::System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;

		// Token: 0x04000477 RID: 1143
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000478 RID: 1144
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x04000479 RID: 1145
		private global::System.Windows.Forms.ToolStripButton btn_select;

		// Token: 0x0400047A RID: 1146
		private global::System.Windows.Forms.Label lbl_caption;

		// Token: 0x0400047B RID: 1147
		private global::TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid.CtrlGrid radGridView1;
	}
}
