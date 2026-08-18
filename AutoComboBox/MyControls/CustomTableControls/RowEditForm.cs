using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000059 RID: 89
	public partial class RowEditForm : Form
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00019990 File Offset: 0x00018990
		public RowEditForm(TableProperty tp, DataGridViewRow row, bool showApply)
		{
			this.InitializeComponent();
			this.rowEdit = new RowEdit(tp, row, this, showApply);
			base.SuspendLayout();
			this.rowEdit.Location = new Point(0, 0);
			this.rowEdit.Name = "rowEdit1";
			this.rowEdit.TabIndex = 0;
			base.Controls.Add(this.rowEdit);
			base.ClientSize = this.rowEdit.Size;
			this.rowEdit.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			base.Name = "RowEditForm";
			base.ResumeLayout(false);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00019AC3 File Offset: 0x00018AC3
		private void RowEditForm_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0400031B RID: 795
		private RowEdit rowEdit;
	}
}
