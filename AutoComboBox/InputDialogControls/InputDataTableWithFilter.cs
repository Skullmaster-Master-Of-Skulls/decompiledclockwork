using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AutoComboBox.Properties;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000C9 RID: 201
	public partial class InputDataTableWithFilter : Form
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0003DAF6 File Offset: 0x0003CAF6
		public InputDataTableWithFilter()
		{
			this.InitializeComponent();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003DB0F File Offset: 0x0003CB0F
		public InputDataTableWithFilter(BindingSource bindingSource)
		{
			this.InitializeComponent();
			this.radGridView1.DataSource = bindingSource;
			this.radGridView1.BestFitColumns();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0003DB44 File Offset: 0x0003CB44
		public InputDataTableWithFilter(BindingSource bindingSource, string[] colRenames)
		{
			this.InitializeComponent();
			this.radGridView1.DataSource = bindingSource;
			if (colRenames != null)
			{
				foreach (string text in colRenames)
				{
					int num = text.IndexOf('=');
					if (num < 0)
					{
						text += "=";
						num = text.IndexOf('=');
					}
					if (num > 0)
					{
						string text2 = text.Substring(0, num);
						string value = (text.Length > num + 1) ? text.Substring(num + 1) : "";
						this.radGridView1.Columns.SetVisible(text2, !string.IsNullOrEmpty(value));
					}
				}
			}
			this.radGridView1.BestFitColumns();
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0003DC30 File Offset: 0x0003CC30
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0003DC47 File Offset: 0x0003CC47
		public bool DefaultSelectAll { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0003DC68 File Offset: 0x0003CC68
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0003DD10 File Offset: 0x0003CD10
		public List<DataRow> SelectedDataRows
		{
			get
			{
				return this.radGridView1.SelectedRows.DataBoundItems<DataRowView>().ToList<DataRowView>().ConvertAll<DataRow>((DataRowView g) => g.Row);
			}
			set
			{
				List<int> idsToSelect = value.ConvertAll<int>((DataRow h) => (int)h[0]);
				this.radGridView1.Rows.SelectAndFocusAllRowsFirstOrDefaultDataBoundItem<DataRowView>((DataRowView g) => g != null && idsToSelect.Contains((int)g.Row[0]));
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0003DD70 File Offset: 0x0003CD70
		public void HideColumns(params string[] colNames)
		{
			foreach (string text in colNames)
			{
				this.radGridView1.Columns.SetVisible(text, false);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0003DDAC File Offset: 0x0003CDAC
		public DataRow SelectedDataRow
		{
			get
			{
				DataRowView dataRowView = this.radGridView1.SelectedRows.FirstSelectedRow<DataRowView>();
				return (dataRowView == null) ? null : dataRowView.Row;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0003DDDC File Offset: 0x0003CDDC
		public static DialogResult ShowDialog(IWin32Window owner, BindingSource bindingSource, out DataRow selectedDataRow)
		{
			return InputDataTableWithFilter.ShowDialog(owner, bindingSource, out selectedDataRow, new string[0], 0);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003DE00 File Offset: 0x0003CE00
		public static DialogResult ShowDialog(IWin32Window owner, BindingSource bindingSource, out DataRow selectedDataRow, string[] columnRenames, int overrideWidth)
		{
			InputDataTableWithFilter inputDataTableWithFilter = new InputDataTableWithFilter(bindingSource, columnRenames);
			if (overrideWidth > 0)
			{
				inputDataTableWithFilter.Width = overrideWidth;
			}
			DialogResult dialogResult = inputDataTableWithFilter.ShowDialog(owner);
			selectedDataRow = ((dialogResult == DialogResult.OK) ? inputDataTableWithFilter.SelectedDataRow : null);
			return dialogResult;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0003DE47 File Offset: 0x0003CE47
		private void radGridView1_DoubleClick(object sender, EventArgs e)
		{
			this.btn_ok_Click(this.btn_ok, new EventArgs());
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0003DE5C File Offset: 0x0003CE5C
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003DE68 File Offset: 0x0003CE68
		private void btn_ok_Click(object sender, EventArgs e)
		{
			if (this.SelectedDataRow != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			else
			{
				MessageBox.Show("Please select an item first, or click 'Cancel'.");
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003DEA1 File Offset: 0x0003CEA1
		public void SetColumnFormatting(string colName, string formatString)
		{
			this.radGridView1.SetExistingColumnFormatString(colName, formatString);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0003DEB2 File Offset: 0x0003CEB2
		public void AddFormatting(string colName, eRowFormattingConditionType condition, string val, Color backColour, Color foreColour)
		{
			this.radGridView1.AddFormatting(colName, condition, val, backColour, foreColour, true);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0003DEC9 File Offset: 0x0003CEC9
		private void btn_exportToExcel_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003DECC File Offset: 0x0003CECC
		private void btn_selectAll_Click(object sender, EventArgs e)
		{
			this.SelectAll();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0003DED6 File Offset: 0x0003CED6
		private void SelectAll()
		{
			this.radGridView1.SelectAll();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0003DEE8 File Offset: 0x0003CEE8
		private void InputDataTableWithFilter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyCode == Keys.A)
				{
					this.SelectAll();
				}
			}
			else if (e.KeyCode == Keys.Escape)
			{
				base.Close();
			}
			else if (e.KeyCode == Keys.Return)
			{
				this.btn_ok_Click(this.btn_ok, new EventArgs());
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0003DF5C File Offset: 0x0003CF5C
		private void InputDataTableWithFilter_Load(object sender, EventArgs e)
		{
			this.radGridView1.MultiSelect = true;
			this.radGridView1.DoubleClick += this.radGridView1_DoubleClick;
			if (this.DefaultSelectAll)
			{
				this.radGridView1.SelectAll();
			}
			base.ActiveControl = this.radGridView1;
		}
	}
}
