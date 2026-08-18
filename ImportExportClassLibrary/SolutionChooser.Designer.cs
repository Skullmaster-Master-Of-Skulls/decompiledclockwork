namespace ImportExportClassLibrary
{
	// Token: 0x02000046 RID: 70
	public partial class SolutionChooser : global::System.Windows.Forms.Form
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0001CB7C File Offset: 0x0001BB7C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001CB9C File Offset: 0x0001BB9C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ImportExportClassLibrary.SolutionChooser));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.lbl_problem = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btn_fakeOk = new global::System.Windows.Forms.Button();
			this.lbl_data = new global::System.Windows.Forms.Label();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.p_data = new global::System.Windows.Forms.Panel();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_skip = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.panel2.SuspendLayout();
			this.p_data.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.imageList1.Images.SetKeyName(4, "");
			this.imageList1.Images.SetKeyName(5, "");
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(216, 66);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(526, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Select the best solution for this problem:";
			this.panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.lbl_problem);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.btn_fakeOk);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new global::System.Drawing.Point(216, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(526, 66);
			this.panel2.TabIndex = 5;
			this.lbl_problem.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbl_problem.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_problem.Location = new global::System.Drawing.Point(0, 12);
			this.lbl_problem.Name = "lbl_problem";
			this.lbl_problem.Size = new global::System.Drawing.Size(524, 52);
			this.lbl_problem.TabIndex = 1;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(524, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "This is the problem:";
			this.btn_fakeOk.Location = new global::System.Drawing.Point(276, 0);
			this.btn_fakeOk.Name = "btn_fakeOk";
			this.btn_fakeOk.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeOk.TabIndex = 2;
			this.btn_fakeOk.Text = "button1";
			this.btn_fakeOk.Click += new global::System.EventHandler(this.btn_fakeOk_Click);
			this.lbl_data.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_data.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_data.Location = new global::System.Drawing.Point(2, 2);
			this.lbl_data.Name = "lbl_data";
			this.lbl_data.Size = new global::System.Drawing.Size(212, 16);
			this.lbl_data.TabIndex = 3;
			this.lbl_data.Text = "This is the data with the problem:";
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(336, 0);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 6;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView1.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.Location = new global::System.Drawing.Point(216, 84);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(526, 370);
			this.listView1.TabIndex = 7;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new global::System.EventHandler(this.listView1_DoubleClick);
			this.columnHeader1.Width = 492;
			this.p_data.AutoScroll = true;
			this.p_data.Controls.Add(this.lbl_data);
			this.p_data.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.p_data.Location = new global::System.Drawing.Point(0, 0);
			this.p_data.Name = "p_data";
			this.p_data.Padding = new global::System.Windows.Forms.Padding(2);
			this.p_data.Size = new global::System.Drawing.Size(216, 493);
			this.p_data.TabIndex = 8;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_skip,
				this.toolStripSeparator1,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(216, 454);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(526, 39);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_save.Image = global::ImportExportClassLibrary.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(154, 36);
			this.btn_save.Text = "&Fix this problem";
			this.btn_save.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_skip.Image = global::ImportExportClassLibrary.Properties.Resources.redo;
			this.btn_skip.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_skip.Name = "btn_skip";
			this.btn_skip.Size = new global::System.Drawing.Size(219, 36);
			this.btn_skip.Text = "S&kip this problem for now";
			this.btn_skip.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_skip.Click += new global::System.EventHandler(this.btn_skip_Click);
			this.btn_cancel.Image = global::ImportExportClassLibrary.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(742, 493);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.p_data);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "SolutionChooser";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose the best solution to this problem:";
			base.Load += new global::System.EventHandler(this.SolutionChooser_Load);
			this.panel2.ResumeLayout(false);
			this.p_data.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000187 RID: 391
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000188 RID: 392
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000189 RID: 393
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400018A RID: 394
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400018B RID: 395
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400018C RID: 396
		private global::System.Windows.Forms.Label lbl_problem;

		// Token: 0x0400018D RID: 397
		private global::System.Windows.Forms.Button btn_fakeOk;

		// Token: 0x0400018E RID: 398
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x04000190 RID: 400
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x04000191 RID: 401
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04000194 RID: 404
		private global::System.Windows.Forms.Panel p_data;

		// Token: 0x04000195 RID: 405
		private global::System.Windows.Forms.Label lbl_data;

		// Token: 0x04000196 RID: 406
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x04000197 RID: 407
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000198 RID: 408
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x04000199 RID: 409
		private global::System.Windows.Forms.ToolStripButton btn_skip;

		// Token: 0x0400019A RID: 410
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400019B RID: 411
		private global::System.Windows.Forms.ToolStripButton btn_cancel;
	}
}
