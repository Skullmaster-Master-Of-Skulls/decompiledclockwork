namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000C9 RID: 201
	public partial class InputDataTableWithFilter : global::System.Windows.Forms.Form
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0003D38C File Offset: 0x0003C38C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this.radGridView1 != null)
				{
					try
					{
						this.radGridView1.DoubleClick -= new global::System.EventHandler(this.radGridView1_DoubleClick);
					}
					catch
					{
					}
				}
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0003D404 File Offset: 0x0003C404
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_selectAll = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_export = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_exportToFormattedText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToTabDelimiteredText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToDelimiteredText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToAccess = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToExcel = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.radGridView1 = new global::TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid.CtrlGrid();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_selectAll,
				this.toolStripSeparator2,
				this.btn_export,
				this.toolStripSeparator1,
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 415);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(606, 39);
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.TabStop = true;
			this.btn_selectAll.Image = global::AutoComboBox.Properties.Resources.document_ok;
			this.btn_selectAll.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_selectAll.Name = "btn_selectAll";
			this.btn_selectAll.Size = new global::System.Drawing.Size(107, 36);
			this.btn_selectAll.Text = "Select &all";
			this.btn_selectAll.Click += new global::System.EventHandler(this.btn_selectAll_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_export.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_exportToFormattedText,
				this.btn_exportToTabDelimiteredText,
				this.btn_exportToDelimiteredText,
				this.btn_exportToAccess,
				this.btn_exportToExcel
			});
			this.btn_export.Image = global::AutoComboBox.Properties.Resources.export2;
			this.btn_export.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_export.Name = "btn_export";
			this.btn_export.Size = new global::System.Drawing.Size(98, 36);
			this.btn_export.Text = "E&xport";
			this.btn_export.Visible = false;
			this.btn_exportToFormattedText.Name = "btn_exportToFormattedText";
			this.btn_exportToFormattedText.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToFormattedText.Text = "Export to &formatted text (.txt)";
			this.btn_exportToTabDelimiteredText.Name = "btn_exportToTabDelimiteredText";
			this.btn_exportToTabDelimiteredText.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToTabDelimiteredText.Text = "Export to &tab delimitered text (.txt)";
			this.btn_exportToTabDelimiteredText.Visible = false;
			this.btn_exportToDelimiteredText.Name = "btn_exportToDelimiteredText";
			this.btn_exportToDelimiteredText.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToDelimiteredText.Text = "Export to &delimitered text (.csv)";
			this.btn_exportToDelimiteredText.Visible = false;
			this.btn_exportToAccess.Name = "btn_exportToAccess";
			this.btn_exportToAccess.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToAccess.Text = "Export to &Access";
			this.btn_exportToAccess.Visible = false;
			this.btn_exportToExcel.Name = "btn_exportToExcel";
			this.btn_exportToExcel.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToExcel.Text = "Export to &Excel";
			this.btn_exportToExcel.Click += new global::System.EventHandler(this.btn_exportToExcel_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.toolStripSeparator1.Visible = false;
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.radGridView1.AutoGenerateColumns = true;
			this.radGridView1.DataSource = null;
			this.radGridView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.radGridView1.DontShowFilteringRow = false;
			this.radGridView1.EnableAlternatingRowColor = true;
			this.radGridView1.EnableFiltering = true;
			this.radGridView1.EnableGrouping = true;
			this.radGridView1.Location = new global::System.Drawing.Point(0, 0);
			this.radGridView1.MultiSelect = false;
			this.radGridView1.Name = "radGridView1";
			this.radGridView1.Size = new global::System.Drawing.Size(606, 415);
			this.radGridView1.TabIndex = 12;
			this.radGridView1.ThemeName = "Office2010Silver";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(606, 454);
			base.Controls.Add(this.radGridView1);
			base.Controls.Add(this.toolStrip1);
			base.KeyPreview = true;
			base.Name = "InputDataTableWithFilter";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Item chooser";
			base.Load += new global::System.EventHandler(this.InputDataTableWithFilter_Load);
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.InputDataTableWithFilter_KeyDown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040005DB RID: 1499
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040005DC RID: 1500
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040005DD RID: 1501
		private global::System.Windows.Forms.ToolStripDropDownButton btn_export;

		// Token: 0x040005DE RID: 1502
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToFormattedText;

		// Token: 0x040005DF RID: 1503
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToTabDelimiteredText;

		// Token: 0x040005E0 RID: 1504
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToDelimiteredText;

		// Token: 0x040005E1 RID: 1505
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToAccess;

		// Token: 0x040005E2 RID: 1506
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToExcel;

		// Token: 0x040005E3 RID: 1507
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040005E4 RID: 1508
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x040005E5 RID: 1509
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040005E6 RID: 1510
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x040005E7 RID: 1511
		private global::System.Windows.Forms.ToolStripButton btn_selectAll;

		// Token: 0x040005E8 RID: 1512
		private global::TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid.CtrlGrid radGridView1;
	}
}
