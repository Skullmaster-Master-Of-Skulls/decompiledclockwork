namespace ImportExportClassLibrary
{
	// Token: 0x02000045 RID: 69
	public partial class DataTableViewProperties : global::System.Windows.Forms.Form
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0001C746 File Offset: 0x0001B746
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001C768 File Offset: 0x0001B768
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.lbl_numRows = new global::System.Windows.Forms.Label();
			this.lbl_numCols = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.lbl_sort = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.Location = new global::System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(112, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "# rows:";
			this.label2.Location = new global::System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(112, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "# columns:";
			this.lbl_numRows.Location = new global::System.Drawing.Point(128, 8);
			this.lbl_numRows.Name = "lbl_numRows";
			this.lbl_numRows.Size = new global::System.Drawing.Size(88, 16);
			this.lbl_numRows.TabIndex = 2;
			this.lbl_numRows.Text = "0";
			this.lbl_numRows.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.lbl_numCols.Location = new global::System.Drawing.Point(128, 40);
			this.lbl_numCols.Name = "lbl_numCols";
			this.lbl_numCols.Size = new global::System.Drawing.Size(88, 16);
			this.lbl_numCols.TabIndex = 3;
			this.lbl_numCols.Text = "0";
			this.lbl_numCols.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.label3.Location = new global::System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(112, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Sort:";
			this.lbl_sort.Location = new global::System.Drawing.Point(128, 72);
			this.lbl_sort.Name = "lbl_sort";
			this.lbl_sort.Size = new global::System.Drawing.Size(304, 56);
			this.lbl_sort.TabIndex = 5;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.ClientSize = new global::System.Drawing.Size(440, 422);
			base.Controls.Add(this.lbl_sort);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.lbl_numCols);
			base.Controls.Add(this.lbl_numRows);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Name = "DataTableViewProperties";
			this.Text = "DataTableViewProperties";
			base.Load += new global::System.EventHandler(this.DataTableViewProperties_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x0400017F RID: 383
		private global::System.ComponentModel.Container components;

		// Token: 0x04000180 RID: 384
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000181 RID: 385
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000182 RID: 386
		private global::System.Windows.Forms.Label lbl_numRows;

		// Token: 0x04000183 RID: 387
		private global::System.Windows.Forms.Label lbl_numCols;

		// Token: 0x04000184 RID: 388
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000185 RID: 389
		private global::System.Windows.Forms.Label lbl_sort;
	}
}
