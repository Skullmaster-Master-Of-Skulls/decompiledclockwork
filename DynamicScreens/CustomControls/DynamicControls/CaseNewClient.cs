using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.UI.WinForms.People.Controls.PeopleChoosers;
using UnivOleDb;

namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x0200002E RID: 46
	public partial class CaseNewClient : Form
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0001EA84 File Offset: 0x0001DA84
		protected void ctrlStudentStaffGroupRoomChooser2_OnResultSelected(object sender, PersonChooserCalendarEventArgs e)
		{
			int pid = this.Pid;
			this.LookupCrossRefCases(pid);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001EAA4 File Offset: 0x0001DAA4
		public CaseNewClient()
		{
			this.InitializeComponent();
			this.ctrlStudentStaffGroupRoomChooser2.Init(false, 1, new eUserGroupObjectType[]
			{
				eUserGroupObjectType.Student
			});
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001EAEC File Offset: 0x0001DAEC
		public CaseNewClient(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ClientType clientType, int newUserFormNumber, ArrayList eventHandlers, int whoAmIPersonID, int underlyingInfoPcPid, int formNumber)
		{
			this.whoAmIPersonID = whoAmIPersonID;
			this.formNumber = formNumber;
			this.da = da;
			this.tripleDES = tripleDES;
			this.newUserFormNumber = newUserFormNumber;
			this.eventHandlers = eventHandlers;
			this.underlyingInfoPcPid = underlyingInfoPcPid;
			this.InitializeComponent();
			this.ctrlStudentStaffGroupRoomChooser2.Init(true);
			switch (clientType)
			{
			case ClientType.PrimaryClient:
				this.rbtn_primaryClient.Checked = true;
				break;
			case ClientType.Client:
				this.rbtn_client.Checked = true;
				break;
			case ClientType.Respondent:
				this.rbtn_respondent.Checked = true;
				break;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001EB9D File Offset: 0x0001DB9D
		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001EBA8 File Offset: 0x0001DBA8
		private void btn_ok_Click(object sender, EventArgs e)
		{
			string text;
			bool flag = this.ValidateThisForm(out text);
			if (flag)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			else
			{
				MessageBox.Show(text);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001EBE4 File Offset: 0x0001DBE4
		private bool ValidateThisForm(out string msg)
		{
			int selectedPid = this.ctrlStudentStaffGroupRoomChooser2.SelectedPid;
			bool result;
			if (selectedPid > 0)
			{
				if (this.rbtn_client.Checked || this.rbtn_primaryClient.Checked || this.rbtn_respondent.Checked)
				{
					msg = "";
					result = true;
				}
				else
				{
					msg = "Please select a user type (client, primary client or respondent) in order to continue.";
					result = false;
				}
			}
			else
			{
				msg = "Please select a user from the list in order to continue.";
				result = false;
			}
			return result;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001EC60 File Offset: 0x0001DC60
		private void btn_addNewUser_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.eventHandlers)
			{
				object[] array = (object[])obj;
				int num = (int)array[0];
				if (num == 6)
				{
					AddNewUserRequest addNewUserRequest = (AddNewUserRequest)array[1];
					if (addNewUserRequest != null)
					{
						int num2 = addNewUserRequest(this, new EventArgs(), this.newUserFormNumber);
						if (num2 > 0)
						{
						}
					}
					break;
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001ED1C File Offset: 0x0001DD1C
		public int Pid
		{
			get
			{
				return this.ctrlStudentStaffGroupRoomChooser2.SelectedPid;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001ED3C File Offset: 0x0001DD3C
		public ClientType ClientType
		{
			get
			{
				ClientType result;
				if (this.rbtn_client.Checked)
				{
					result = ClientType.Client;
				}
				else if (this.rbtn_primaryClient.Checked)
				{
					result = ClientType.PrimaryClient;
				}
				else if (this.rbtn_respondent.Checked)
				{
					result = ClientType.Respondent;
				}
				else
				{
					result = ClientType.Client;
				}
				return result;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001ED91 File Offset: 0x0001DD91
		private void CaseNewClient_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.ctrlStudentStaffGroupRoomChooser2;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001EDA4 File Offset: 0x0001DDA4
		private void LookupCrossRefCases(int pid)
		{
			int num = (this.lv_cases.Tag == null) ? 0 : ((int)this.lv_cases.Tag);
			if (num != pid)
			{
				this.da.SelectCommand.CommandText = "SELECT ipc.personid AS infopcid,ipc.student_no AS title,ipc.dateentered,ipc.whoentered,p.firstname,p.lastname,p.student_no,ll.lookuptext AS status FROM infopc ipc LEFT JOIN people p ON p.personid=ipc.whoentered LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=@screennum AND dsc.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcaption LIKE '%status%' AND controlcode=3) LEFT JOIN pcdata2 pcd ON pcd.infopcid=ipc.personid AND pcd.controlid=dsc.controlid LEFT JOIN lookuplists ll ON ll.lookuplistid=pcd.valint WHERE ipc.isactive=1 AND ipc.personid IN (SELECT infopcid AS personid FROM infopcpeople WHERE personid=@pid) AND NOT ipc.personid=@infopcpid ORDER BY ipc.dateentered DESC";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@pid", pid);
				this.da.SelectCommand.Parameters.Add("@infopcpid", this.underlyingInfoPcPid);
				this.da.SelectCommand.Parameters.Add("@screennum", this.formNumber);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				dataTable = this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no",
					"title"
				});
				this.lv_cases.BeginUpdate();
				this.lv_cases.Items.Clear();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					ListViewItem listViewItem = new ListViewItem(dataRow["title"].ToString());
					listViewItem.SubItems.Add((dataRow["dateentered"] == DBNull.Value) ? "?" : ((DateTime)dataRow["dateentered"]).ToString("MMM d, yyyy"));
					listViewItem.SubItems.Add(dataRow["status"].ToString());
					listViewItem.SubItems.Add(dataRow["firstname"].ToString() + " " + dataRow["lastname"].ToString());
					listViewItem.Tag = dataRow;
					this.lv_cases.Items.Add(listViewItem);
				}
				this.lv_cases.EndUpdate();
				this.lv_cases.Tag = pid;
			}
		}

		// Token: 0x040001D0 RID: 464
		private int whoAmIPersonID;

		// Token: 0x040001D1 RID: 465
		private int underlyingInfoPcPid;

		// Token: 0x040001D2 RID: 466
		private int formNumber;

		// Token: 0x040001D3 RID: 467
		private UnivDataAdapter da;

		// Token: 0x040001D4 RID: 468
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040001D5 RID: 469
		private int newUserFormNumber = 1;

		// Token: 0x040001D6 RID: 470
		private ArrayList eventHandlers;
	}
}
