using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ImportExportClassLibrary.Properties;

namespace ImportExportClassLibrary
{
	// Token: 0x02000046 RID: 70
	public partial class SolutionChooser : Form
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0001CB52 File Offset: 0x0001BB52
		public SolutionChooser(ImportItem ii, ImportProblem ip, ProblemSolution[] _Solutions)
		{
			this.InitializeComponent();
			this._solutions = _Solutions;
			this._ii = ii;
			this._ip = ip;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001D5E8 File Offset: 0x0001C5E8
		private void SolutionChooser_Load(object sender, EventArgs e)
		{
			this.DataToScreen();
			this.lbl_problem.Text = this._ip._problemDescription;
			this.listView1.BeginUpdate();
			foreach (ProblemSolution problemSolution in this._solutions)
			{
				ListViewItem listViewItem = new ListViewItem(ImportProblem.ProblemSolutionDescriptions[(int)problemSolution]);
				listViewItem.Tag = problemSolution;
				this.listView1.Items.Add(listViewItem);
			}
			this.listView1.EndUpdate();
			if (this.listView1.Items.Count > 0)
			{
				this.listView1.Items[0].Selected = true;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001D698 File Offset: 0x0001C698
		private void DataToScreen()
		{
			DataRow dataRow = this._ii._dataRow;
			int num = this.lbl_data.Top + this.lbl_data.Height;
			for (int i = 0; i < dataRow.Table.Columns.Count; i++)
			{
				Label label = new Label();
				label.Top = num;
				label.Left = 2;
				label.Height = this.lbl_data.Height;
				string text = dataRow[i].ToString().Trim();
				label.Text = dataRow.Table.Columns[i].ColumnName + ": " + text;
				label.Width = this.p_data.Width;
				this.p_data.Controls.Add(label);
				this.toolTip1.SetToolTip(label, text);
				num += label.Height + 2;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001D785 File Offset: 0x0001C785
		private void btn_fakeOk_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001D790 File Offset: 0x0001C790
		public void Save()
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listView1.SelectedItems[0];
				this.selectedSolution = (ProblemSolution)listViewItem.Tag;
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001D7E0 File Offset: 0x0001C7E0
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001D7E8 File Offset: 0x0001C7E8
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001D7F0 File Offset: 0x0001C7F0
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001D7F8 File Offset: 0x0001C7F8
		private void btn_skip_Click(object sender, EventArgs e)
		{
			this.selectedSolution = ProblemSolution.Unkown;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001D80E File Offset: 0x0001C80E
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0400018F RID: 399
		private ProblemSolution[] _solutions;

		// Token: 0x04000192 RID: 402
		public ProblemSolution selectedSolution = ProblemSolution.Unkown;

		// Token: 0x04000193 RID: 403
		private ImportItem _ii;

		// Token: 0x0400019C RID: 412
		private ImportProblem _ip;
	}
}
