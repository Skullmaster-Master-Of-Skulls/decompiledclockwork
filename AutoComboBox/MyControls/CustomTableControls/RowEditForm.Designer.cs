namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000059 RID: 89
	public partial class RowEditForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00019A44 File Offset: 0x00018A44
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.ClientSize = new global::System.Drawing.Size(292, 273);
			this.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Name = "RowEditForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit row";
			base.Load += new global::System.EventHandler(this.RowEditForm_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00019AC8 File Offset: 0x00018AC8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400031C RID: 796
		private global::System.ComponentModel.IContainer components = null;
	}
}
