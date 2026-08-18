using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClockWorkAPI.EntityExtensions;
using ClockWorkAPI.Properties;
using EncryptionClassLibrary;
using SettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataForms.FormWrappers
{
	// Token: 0x0200001E RID: 30
	public partial class ftmDataPerStudent : Form
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007D00 File Offset: 0x00006D00
		public ftmDataPerStudent()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007D19 File Offset: 0x00006D19
		private void ftmDataPerStudent_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007D1C File Offset: 0x00006D1C
		public void Init(UnivDataAdapter da, int pid, int screenNum)
		{
			this.pid = pid;
			this.screenNum = screenNum;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			this.lbl_student.Text = this.GetDisplayName();
			this.dps = new DataPerStudent();
			base.Controls.Add(this.dps);
			this.dps.Dock = DockStyle.Fill;
			this.dps.BringToFront();
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			PersonBaseDTO whoAmI = new PersonBaseDTO
			{
				PersonId = 0,
				FirstName = "",
				MiddleName = "",
				LastName = "",
				Student_no = "",
				CoreGroup = eCoreGroupDTO.Unknown,
				Tag = new PersonExt()
			};
			this.dps.Init(da, tripleDES, pid, screenNum, false, ref dataSet, ref dataSet2, new ArrayList(), "ps", whoAmI, new Settings(1, da), new Permissions(new int[]
			{
				10
			}, new DataTable(), new DataTable()));
			this.dps.RenderForm();
			this.dps.LoadDataAndDisplay();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007E54 File Offset: 0x00006E54
		private string GetDisplayName()
		{
			DataTable dataTable = new DataTable();
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = "SELECT * FROM people WHERE personid=@pid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", this.pid);
			da.Fill(dataTable);
			string result;
			if (dataTable.Rows.Count > 0)
			{
				TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
				dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"middlename",
					"lastname",
					"student_no"
				});
				DataRow dataRow = dataTable.Rows[0];
				result = string.Format("{0}, {1} [{2}]", dataRow["lastname"].ToString(), dataRow["firstname"].ToString(), dataRow["student_no"].ToString());
			}
			else
			{
				result = "?";
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007F71 File Offset: 0x00006F71
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007F7B File Offset: 0x00006F7B
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.dps.SaveChanges(true);
			base.Close();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007F94 File Offset: 0x00006F94
		private void ftmDataPerStudent_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dps.AnyChanges())
			{
				DialogResult dialogResult = MessageBox.Show("Would you like to save your changes?", "Changes will be lost", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					this.dps.SaveChanges(true);
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					e.Cancel = true;
					return;
				}
			}
			if (this.dps != null)
			{
				base.Controls.Remove(this.dps);
				this.dps.Dispose();
				this.dps = null;
			}
		}

		// Token: 0x040000A6 RID: 166
		private int pid;

		// Token: 0x040000A7 RID: 167
		private int screenNum;

		// Token: 0x040000A8 RID: 168
		private DataPerStudent dps;
	}
}
