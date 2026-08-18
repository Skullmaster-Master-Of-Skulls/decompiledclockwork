namespace AutoComboBox
{
	// Token: 0x0200002A RID: 42
	public partial class DataTableViewProperties : global::System.Windows.Forms.Form
	{
		// Token: 0x06000128 RID: 296 RVA: 0x0000C670 File Offset: 0x0000B670
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000C6AC File Offset: 0x0000B6AC
		private void InitializeComponent()
		{
			global::System.Resources.ResourceManager resourceManager = new global::System.Resources.ResourceManager(typeof(global::AutoComboBox.DataTableViewProperties));
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.lbl_numRows = new global::System.Windows.Forms.Label();
			this.lbl_numCols = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.lbl_sort = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.dataGrid1 = new global::System.Windows.Forms.DataGrid();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid1).BeginInit();
			base.SuspendLayout();
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(480, 48);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of rows:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.label1.Click += new global::System.EventHandler(this.label1_Click);
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new global::System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(480, 48);
			this.label2.TabIndex = 1;
			this.label2.Text = "Number of columns:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.lbl_numRows.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.lbl_numRows.Location = new global::System.Drawing.Point(480, 0);
			this.lbl_numRows.Name = "lbl_numRows";
			this.lbl_numRows.Size = new global::System.Drawing.Size(120, 48);
			this.lbl_numRows.TabIndex = 2;
			this.lbl_numRows.Text = "0";
			this.lbl_numRows.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_numCols.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.lbl_numCols.Location = new global::System.Drawing.Point(480, 0);
			this.lbl_numCols.Name = "lbl_numCols";
			this.lbl_numCols.Size = new global::System.Drawing.Size(120, 48);
			this.lbl_numCols.TabIndex = 3;
			this.lbl_numCols.Text = "0";
			this.lbl_numCols.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.label3.Location = new global::System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(104, 150);
			this.label3.TabIndex = 4;
			this.label3.Text = "Sort:";
			this.label3.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.lbl_sort.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbl_sort.Location = new global::System.Drawing.Point(104, 0);
			this.lbl_sort.Name = "lbl_sort";
			this.lbl_sort.Size = new global::System.Drawing.Size(496, 150);
			this.lbl_sort.TabIndex = 5;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.lbl_numRows);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new global::System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(600, 48);
			this.panel1.TabIndex = 6;
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.lbl_numCols);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new global::System.Drawing.Point(0, 48);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(600, 48);
			this.panel2.TabIndex = 7;
			this.panel3.Controls.Add(this.dataGrid1);
			this.panel3.Controls.Add(this.lbl_sort);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new global::System.Drawing.Point(0, 96);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(600, 150);
			this.panel3.TabIndex = 8;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = global::System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new global::System.Drawing.Point(104, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new global::System.Drawing.Size(496, 150);
			this.dataGrid1.TabIndex = 6;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(9, 22);
			base.ClientSize = new global::System.Drawing.Size(600, 246);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			this.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "DataTableViewProperties";
			this.Text = "Data table properties";
			base.Load += new global::System.EventHandler(this.DataTableViewProperties_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid1).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000164 RID: 356
		private global::System.ComponentModel.Container components = null;

		// Token: 0x04000165 RID: 357
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000166 RID: 358
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000167 RID: 359
		private global::System.Windows.Forms.Label lbl_numRows;

		// Token: 0x04000168 RID: 360
		private global::System.Windows.Forms.Label lbl_numCols;

		// Token: 0x04000169 RID: 361
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400016A RID: 362
		private global::System.Windows.Forms.Label lbl_sort;

		// Token: 0x0400016B RID: 363
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400016C RID: 364
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400016D RID: 365
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x0400016E RID: 366
		private global::System.Windows.Forms.DataGrid dataGrid1;
	}
}
