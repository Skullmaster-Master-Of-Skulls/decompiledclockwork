using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid;

namespace AutoComboBox.HelperForms
{
	// Token: 0x02000086 RID: 134
	public partial class DataGridView2 : Form
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0002CC52 File Offset: 0x0002BC52
		public DataGridView2()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002CC79 File Offset: 0x0002BC79
		public DataGridView2(object dataSource)
		{
			this.InitializeComponent();
			this.DataSource = dataSource;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0002CCB4 File Offset: 0x0002BCB4
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0002CCA8 File Offset: 0x0002BCA8
		public string Title
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0002CCCC File Offset: 0x0002BCCC
		public static DialogResult ShowDataGridView2(IWin32Window owner, string title, object dataSource, params string[] colsToHide)
		{
			DataGridView2 dataGridView = new DataGridView2();
			dataGridView.Title = title;
			dataGridView.DataSource = dataSource;
			dataGridView.HideColumns(colsToHide);
			return dataGridView.ShowDialog(owner);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002CD04 File Offset: 0x0002BD04
		public static DialogResult ShowDataGridView2_AllowUserToSelect(IWin32Window owner, string title, string caption, object dataSource, out object selectedObject, params string[] colsToHide)
		{
			DataGridView2 dataGridView = new DataGridView2();
			dataGridView.DataSource = dataSource;
			dataGridView.HideColumns(colsToHide);
			dataGridView.UserAllowedToSelect = true;
			dataGridView.Title = title;
			dataGridView.Caption = caption;
			DialogResult dialogResult = dataGridView.ShowDialog(owner);
			if (dialogResult == DialogResult.OK)
			{
				selectedObject = dataGridView.SelectedObject;
			}
			else
			{
				selectedObject = null;
			}
			return dialogResult;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0002CD68 File Offset: 0x0002BD68
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0002CD9C File Offset: 0x0002BD9C
		public string Caption
		{
			get
			{
				return this.lbl_caption.Visible ? this.lbl_caption.Text : "";
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.lbl_caption.Visible = false;
				}
				else
				{
					this.lbl_caption.Visible = true;
					this.lbl_caption.Text = value;
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0002CDE4 File Offset: 0x0002BDE4
		public object SelectedObject
		{
			get
			{
				return this.selectedObject;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002CDFC File Offset: 0x0002BDFC
		private void SetSelectedObject()
		{
			this.selectedObject = this.radGridView1.SelectedRows.FirstSelectedRow<object>();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002CE18 File Offset: 0x0002BE18
		protected override bool ProcessCmdKey(ref Message m, Keys k)
		{
			bool result;
			if (this.userAllowedToSelect && k == Keys.Return && this.radGridView1.SelectedRows.Count > 0)
			{
				base.DialogResult = DialogResult.OK;
				this.SetSelectedObject();
				result = true;
			}
			else if (k == Keys.Escape)
			{
				base.Close();
				result = true;
			}
			else
			{
				result = base.ProcessCmdKey(ref m, k);
			}
			return result;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0002CE88 File Offset: 0x0002BE88
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0002CEA0 File Offset: 0x0002BEA0
		public bool UserAllowedToSelect
		{
			get
			{
				return this.userAllowedToSelect;
			}
			set
			{
				this.userAllowedToSelect = value;
				if (this.userAllowedToSelect)
				{
					this.btn_select.Visible = true;
					this.radGridView1.SelectionMode = 1;
					this.radGridView1.MultiSelect = false;
				}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002CEEC File Offset: 0x0002BEEC
		public void HideColumns(params string[] colNames)
		{
			foreach (string text in colNames)
			{
				this.radGridView1.Columns.SetVisible(text, false);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002CF28 File Offset: 0x0002BF28
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0002CF45 File Offset: 0x0002BF45
		public object DataSource
		{
			get
			{
				return this.radGridView1.DataSource;
			}
			set
			{
				this.radGridView1.SetDataSource(value);
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002CF55 File Offset: 0x0002BF55
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002CF5F File Offset: 0x0002BF5F
		private void btn_exportToXml_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002CF62 File Offset: 0x0002BF62
		private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002CF68 File Offset: 0x0002BF68
		private void btn_select_Click(object sender, EventArgs e)
		{
			if (this.radGridView1.SelectedRows.Count > 0)
			{
				this.SetSelectedObject();
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			else
			{
				MessageBox.Show("Please select an item first.");
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002CFB8 File Offset: 0x0002BFB8
		private void radGridView1_DoubleClick(object sender, EventArgs e)
		{
			if (this.userAllowedToSelect && this.radGridView1.SelectedRows.Count > 0)
			{
				this.SetSelectedObject();
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002D004 File Offset: 0x0002C004
		private void DataGridView2_Load(object sender, EventArgs e)
		{
			if (this.radGridView1.Rows.Count > 0)
			{
				this.radGridView1.SetSelection(0);
			}
			base.ActiveControl = this.radGridView1;
		}

		// Token: 0x0400047C RID: 1148
		private object selectedObject = null;

		// Token: 0x0400047D RID: 1149
		private bool userAllowedToSelect = false;
	}
}
