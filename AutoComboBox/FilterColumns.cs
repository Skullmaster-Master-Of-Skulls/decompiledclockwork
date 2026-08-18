using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox
{
	// Token: 0x02000104 RID: 260
	public partial class FilterColumns : Form
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0004E98F File Offset: 0x0004D98F
		public FilterColumns(DataView dataView, bool showTableName)
		{
			this.InitializeComponent();
			this.dv = dataView;
			this.p_tableName.Visible = showTableName;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0004F2A0 File Offset: 0x0004E2A0
		private void listView1_SizeChanged(object sender, EventArgs e)
		{
			int num = this.listView1.Width - 25 - this.listView1.Columns[0].Width;
			if (num > 5)
			{
				this.listView1.Columns[1].Width = num;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0004F2F8 File Offset: 0x0004E2F8
		private void FilterColumns_Load(object sender, EventArgs e)
		{
			DataTable table = this.dv.Table;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				ListViewItem listViewItem = new ListViewItem("");
				listViewItem.SubItems.Add(dataColumn.ColumnName);
				listViewItem.Checked = true;
				listViewItem.Tag = dataColumn;
				this.listView1.Items.Add(listViewItem);
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0004F3AC File Offset: 0x0004E3AC
		private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0004F3AF File Offset: 0x0004E3AF
		private void MENU_lv_clearChecks_Click(object sender, EventArgs e)
		{
			this.SetChecks(false);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0004F3BC File Offset: 0x0004E3BC
		private void SetChecks(bool _checked)
		{
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = _checked;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0004F428 File Offset: 0x0004E428
		private void MENU_lv_checkAll_Click(object sender, EventArgs e)
		{
			this.SetChecks(true);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0004F433 File Offset: 0x0004E433
		private void btn_OK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0004F448 File Offset: 0x0004E448
		private void btn_uncheckAll_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = false;
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0004F4B4 File Offset: 0x0004E4B4
		private void btn_checkAll_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Checked = true;
			}
		}

		// Token: 0x04000789 RID: 1929
		public DataView dv;
	}
}
