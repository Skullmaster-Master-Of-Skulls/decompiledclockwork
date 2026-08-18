using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ReportFunctions.Properties;

namespace ReportFunctions
{
	// Token: 0x0200004F RID: 79
	public partial class InputList : Form
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x0004E78C File Offset: 0x0004D78C
		public InputList(string title, string caption, DataTable t, string displayMember, bool MultipleSelect)
		{
			this.InitializeComponent();
			this.listBox1.DataSource = t;
			this.listBox1.DisplayMember = displayMember;
			this.label1.Text = title;
			this.Text = caption;
			this.selectedIndices = null;
			if (MultipleSelect)
			{
				this.listBox1.SelectionMode = SelectionMode.MultiExtended;
			}
			else
			{
				this.listBox1.SelectionMode = SelectionMode.One;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0004E808 File Offset: 0x0004D808
		public InputList(string title, string caption, DataTable t, string displayMember, ArrayList SelectedIndices, bool MultipleSelect)
		{
			this.InitializeComponent();
			this.listBox1.DataSource = t;
			this.listBox1.DisplayMember = displayMember;
			this.label1.Text = title;
			this.Text = caption;
			this.selectedIndices = SelectedIndices;
			if (MultipleSelect)
			{
				this.listBox1.SelectionMode = SelectionMode.MultiExtended;
			}
			else
			{
				this.listBox1.SelectionMode = SelectionMode.One;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0004EE84 File Offset: 0x0004DE84
		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedIndex >= 0)
			{
				this.btn_ok_Click(this.btn_ok, null);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0004EEB2 File Offset: 0x0004DEB2
		private void btn_fake_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0004EEBC File Offset: 0x0004DEBC
		private void btn_fakeAccept_Click(object sender, EventArgs e)
		{
			this.btn_ok_Click(this.btn_ok, null);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0004EED0 File Offset: 0x0004DED0
		private void InputList_Load(object sender, EventArgs e)
		{
			this.listBox1.SelectedIndex = -1;
			if (this.selectedIndices != null)
			{
				foreach (object obj in this.selectedIndices)
				{
					int index = (int)obj;
					try
					{
						this.listBox1.SetSelected(index, true);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0004EF70 File Offset: 0x0004DF70
		public DataRow SelectedRow
		{
			get
			{
				DataRow result;
				if (this.listBox1.SelectedIndex < 0)
				{
					result = null;
				}
				else
				{
					result = ((DataTable)this.listBox1.DataSource).Rows[this.listBox1.SelectedIndex];
				}
				return result;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0004EFC0 File Offset: 0x0004DFC0
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0004EFCA File Offset: 0x0004DFCA
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x04000271 RID: 625
		private ArrayList selectedIndices;
	}
}
