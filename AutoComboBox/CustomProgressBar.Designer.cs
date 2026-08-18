namespace AutoComboBox
{
	// Token: 0x0200007D RID: 125
	public partial class CustomProgressBar : global::System.Windows.Forms.Form
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00027C40 File Offset: 0x00026C40
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

		// Token: 0x060004EA RID: 1258 RVA: 0x00027C7C File Offset: 0x00026C7C
		private void InitializeComponent()
		{
			this.progressBar1 = new global::System.Windows.Forms.ProgressBar();
			this.progressBar2 = new global::System.Windows.Forms.ProgressBar();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.statusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.progressBar1.Location = new global::System.Drawing.Point(6, 24);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new global::System.Drawing.Size(576, 18);
			this.progressBar1.TabIndex = 0;
			this.progressBar2.Location = new global::System.Drawing.Point(6, 54);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new global::System.Drawing.Size(576, 18);
			this.progressBar2.TabIndex = 1;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(592, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Please wait ...";
			this.btn_cancel.Location = new global::System.Drawing.Point(450, 78);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(126, 36);
			this.btn_cancel.TabIndex = 4;
			this.btn_cancel.Text = "Cancel";
			this.statusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripStatusLabel1
			});
			this.statusStrip1.Location = new global::System.Drawing.Point(0, 118);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new global::System.Drawing.Size(592, 22);
			this.statusStrip1.TabIndex = 5;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new global::System.Drawing.Size(577, 17);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(592, 140);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.btn_cancel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar2);
			base.Controls.Add(this.progressBar1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Name = "CustomProgressBar";
			this.Text = "CustomProgressBar";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000414 RID: 1044
		public global::System.Windows.Forms.ProgressBar progressBar1;

		// Token: 0x04000415 RID: 1045
		public global::System.Windows.Forms.ProgressBar progressBar2;

		// Token: 0x04000416 RID: 1046
		public global::System.Windows.Forms.Label label1;

		// Token: 0x04000417 RID: 1047
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x04000418 RID: 1048
		private global::System.Windows.Forms.StatusStrip statusStrip1;

		// Token: 0x04000419 RID: 1049
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

		// Token: 0x0400041A RID: 1050
		private global::System.ComponentModel.IContainer components;
	}
}
