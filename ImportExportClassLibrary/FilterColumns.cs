using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ImportExportClassLibrary.Properties;

namespace ImportExportClassLibrary
{
	// Token: 0x0200004F RID: 79
	public partial class FilterColumns : Form
	{
		// Token: 0x06000316 RID: 790 RVA: 0x00020068 File Offset: 0x0001F068
		public FilterColumns(DataView dataView, bool showTableName)
		{
			this.InitializeComponent();
			this.dv = dataView;
			this.p_tableName.Visible = showTableName;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00020994 File Offset: 0x0001F994
		private void listView1_SizeChanged(object sender, EventArgs e)
		{
			int num = this.listView1.Width - 25 - this.listView1.Columns[0].Width;
			if (num > 5)
			{
				this.listView1.Columns[1].Width = num;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000209E4 File Offset: 0x0001F9E4
		private void FilterColumns_Load(object sender, EventArgs e)
		{
			DataTable table = this.dv.Table;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				ListViewItem listViewItem = new ListViewItem("");
				listViewItem.SubItems.Add(dataColumn.ColumnName);
				if (this.filterColumnsChecked == null)
				{
					listViewItem.Checked = true;
				}
				else
				{
					listViewItem.Checked = (Array.IndexOf<string>(this.filterColumnsChecked, dataColumn.ColumnName.ToLower().Trim()) < 0);
				}
				listViewItem.Tag = dataColumn;
				this.listView1.Items.Add(listViewItem);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00020AB0 File Offset: 0x0001FAB0
		private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00020AB2 File Offset: 0x0001FAB2
		public void SetTitle(string title)
		{
			this.label1.Text = title;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00020AC0 File Offset: 0x0001FAC0
		private void MENU_lv_clearChecks_Click(object sender, EventArgs e)
		{
			this.SetChecks(false);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00020ACC File Offset: 0x0001FACC
		private void SetChecks(bool _checked)
		{
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = _checked;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00020B2C File Offset: 0x0001FB2C
		private void MENU_lv_checkAll_Click(object sender, EventArgs e)
		{
			this.SetChecks(true);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00020B38 File Offset: 0x0001FB38
		private void btn_checkAll_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = true;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00020B98 File Offset: 0x0001FB98
		private void btn_checkNone_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = false;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00020BF8 File Offset: 0x0001FBF8
		private void btn_save_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00020C07 File Offset: 0x0001FC07
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x040001C0 RID: 448
		public DataView dv;

		// Token: 0x040001C1 RID: 449
		public string[] filterColumnsChecked;
	}
}
