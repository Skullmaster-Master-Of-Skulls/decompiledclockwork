namespace AutoComboBox
{
	// Token: 0x02000106 RID: 262
	public partial class MyProgressScreen : global::System.Windows.Forms.Form
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x000501FC File Offset: 0x0004F1FC
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

		// Token: 0x06000A4F RID: 2639 RVA: 0x00050238 File Offset: 0x0004F238
		private void InitializeComponent()
		{
			this.progressBar1 = new global::System.Windows.Forms.ProgressBar();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.progressBar1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.progressBar1.Location = new global::System.Drawing.Point(4, 20);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new global::System.Drawing.Size(592, 24);
			this.progressBar1.Step = 1;
			this.progressBar1.TabIndex = 0;
			this.label1.Cursor = global::System.Windows.Forms.Cursors.AppStarting;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(592, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Please wait ...";
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.ClientSize = new global::System.Drawing.Size(600, 48);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.label1);
			this.Cursor = global::System.Windows.Forms.Cursors.AppStarting;
			base.DockPadding.All = 4;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Name = "MyProgressScreen";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MyProgressScreen";
			base.Load += new global::System.EventHandler(this.MyProgressScreen_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x04000799 RID: 1945
		private global::System.Windows.Forms.ProgressBar progressBar1;

		// Token: 0x0400079A RID: 1946
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400079B RID: 1947
		private global::System.ComponentModel.Container components = null;
	}
}
