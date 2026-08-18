using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200002B RID: 43
	public partial class InputPersonChooser : Form
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000D9EA File Offset: 0x0000C9EA
		public InputPersonChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000DA1C File Offset: 0x0000CA1C
		public InputPersonChooser(DataView dv_studentName, DataView dv_student_no, DataView dv_staff)
		{
			this.InitializeComponent();
			this.cmb_studentName.DataSource = dv_studentName;
			this.cmb_studentName.DisplayMember = "lastfirstname";
			this.cmb_studentName.ValueMember = "personid";
			this.cmb_student_no.DataSource = dv_student_no;
			this.cmb_student_no.DisplayMember = "student_no";
			this.cmb_student_no.ValueMember = "personid";
			this.cmb_staff.DataSource = dv_staff;
			this.cmb_staff.DisplayMember = "lastfirstname";
			this.cmb_staff.ValueMember = "personid";
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000DAE8 File Offset: 0x0000CAE8
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000DB00 File Offset: 0x0000CB00
		public int DefaultTabIndex
		{
			get
			{
				return this.defaultTabIndex;
			}
			set
			{
				this.defaultTabIndex = value;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000DB0C File Offset: 0x0000CB0C
		private void InputPersonChooser_Load(object sender, EventArgs e)
		{
			if (this.defaultTabIndex <= 0 || this.defaultTabIndex >= this.tabControl1.TabPages.Count)
			{
				base.ActiveControl = this.cmb_studentName;
			}
			else
			{
				this.tabControl1.SelectedIndex = this.defaultTabIndex;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000DB65 File Offset: 0x0000CB65
		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000DB6F File Offset: 0x0000CB6F
		private void btn_select_Click(object sender, EventArgs e)
		{
			this.SelectCurrent();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000DB7C File Offset: 0x0000CB7C
		private void SelectCurrent()
		{
			TabPage selectedTab = this.tabControl1.SelectedTab;
			int num = 0;
			string text = "";
			if (selectedTab != null)
			{
				if (selectedTab == this.tp_studentNames)
				{
					DataRow dataRow = this.cmb_studentName.SelectedDataRow();
					if (dataRow != null && dataRow["personid"] != DBNull.Value)
					{
						num = (int)dataRow["personid"];
						text = dataRow[this.cmb_studentName.DisplayMember].ToString();
					}
				}
				else if (selectedTab == this.tp_studentNumbers)
				{
					DataRow dataRow = this.cmb_student_no.SelectedDataRow();
					if (dataRow != null && dataRow["personid"] != DBNull.Value)
					{
						num = (int)dataRow["personid"];
						DataView dataView = (DataView)this.cmb_studentName.DataSource;
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow row = dataRowView.Row;
							if (row["personid"] != DBNull.Value)
							{
								int num2 = (int)row["personid"];
								if (num2 == num)
								{
									text = row[this.cmb_studentName.DisplayMember].ToString();
									break;
								}
							}
						}
					}
				}
				else if (selectedTab == this.tp_staff)
				{
					DataRow dataRow = this.cmb_staff.SelectedDataRow();
					if (dataRow != null && dataRow["personid"] != DBNull.Value)
					{
						num = (int)dataRow["personid"];
						text = dataRow[this.cmb_staff.DisplayMember].ToString();
					}
				}
			}
			if (num <= 0)
			{
				MessageBox.Show("Please select a student or staff before clicking this button.  Nothing was done.");
				this.selectedPid = 0;
				this.selectedDescription = "";
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				this.selectedPid = num;
				this.selectedDescription = text;
				base.Close();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000DDF0 File Offset: 0x0000CDF0
		public int SelectedPid
		{
			get
			{
				return this.selectedPid;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000DE08 File Offset: 0x0000CE08
		public string SelectedDescription
		{
			get
			{
				return this.selectedDescription;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000DE20 File Offset: 0x0000CE20
		private void cmb_studentName_EnterPressed(object sender, KeyPressEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
			{
				this.SelectCurrent();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000DE50 File Offset: 0x0000CE50
		private void cmb_student_no_EnterPressed(object sender, KeyPressEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
			{
				this.SelectCurrent();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000DE80 File Offset: 0x0000CE80
		private void cmb_staff_EnterPressed(object sender, KeyPressEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
			{
				this.SelectCurrent();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000DEB0 File Offset: 0x0000CEB0
		private void InputPersonChooser_KeyUp(object sender, KeyEventArgs e)
		{
			int num = this.tabControl1.SelectedIndex;
			if (e.KeyCode == Keys.Prior)
			{
				num--;
				if (num < 0)
				{
					num = this.tabControl1.TabPages.Count - 1;
				}
			}
			else if (e.KeyCode == Keys.Next)
			{
				num++;
				if (num >= this.tabControl1.TabPages.Count)
				{
					num = 0;
				}
			}
			this.tabControl1.SelectedIndex = num;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000DF40 File Offset: 0x0000CF40
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				base.ActiveControl = this.cmb_studentName;
				break;
			case 1:
				base.ActiveControl = this.cmb_student_no;
				break;
			case 2:
				base.ActiveControl = this.cmb_staff;
				break;
			}
		}

		// Token: 0x0400017E RID: 382
		private int defaultTabIndex = 0;

		// Token: 0x0400017F RID: 383
		private int selectedPid = 0;

		// Token: 0x04000180 RID: 384
		private string selectedDescription = "";
	}
}
