using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using UnivOleDb;

namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x0200000A RID: 10
	public partial class CaseDetail : Form
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004761 File Offset: 0x00003761
		public CaseDetail()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000477C File Offset: 0x0000377C
		public int InfoPcPid
		{
			get
			{
				return this.infoPcPid;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004794 File Offset: 0x00003794
		public CaseSingle CaseSingle
		{
			get
			{
				CaseSingle result;
				if (this.caseSingle == null)
				{
					result = null;
				}
				else
				{
					this.caseSingle.CasePeople = this.clientsRespondentsTable;
					result = this.caseSingle;
				}
				return result;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000047D4 File Offset: 0x000037D4
		public CaseDetail(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int caseScreenNum, int pid, ArrayList eventHandlers, int infoPcPid, int whoAmIPersonID, CaseSingle caseSingle)
		{
			this.whoAmIPersonID = whoAmIPersonID;
			this.eventHandlers = eventHandlers;
			this.caseScreenNum = caseScreenNum;
			this.pid = pid;
			this.da = da;
			this.tripleDES = tripleDES;
			this.caseSingle = caseSingle;
			this.infoPcPid = infoPcPid;
			this.InitializeComponent();
			ScreenInfo screenInfo = DynamicScreen.GetScreenInfo(da, caseScreenNum, this.p_data, true);
			if (screenInfo != null)
			{
				Panel panel = this.p_data;
				DataTable controlListTable = DynamicScreen.LoadControls(da, caseScreenNum);
				DataSet lookupTablesForControls = new DataSet();
				DataSet dataSet = new DataSet();
				DynamicScreen.TranslateControls(da, tripleDES, ref panel, screenInfo, controlListTable, ref dataSet, null, lookupTablesForControls, eventHandlers, 0, "", new int[0], new int[0]);
			}
			this.LoadClientsRespondents();
			this.LoadCaseDetail();
			this.LoadCaseDynamicData();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000048A8 File Offset: 0x000038A8
		private void LoadCaseDynamicData()
		{
			this.data = DynamicScreen.LoadData(this.da, this.p_data, this.caseScreenNum, this.infoPcPid, "maininfopc", "otherinfopc", "datetimeinfopc", "imageinfopc", this.tripleDES, false, false, -1, UseDefaults.dontUseDefaults, true);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000048F8 File Offset: 0x000038F8
		private void LoadCaseDetail()
		{
			if (this.infoPcPid <= 0)
			{
				string text = DateTime.Now.Year.ToString();
				text = text.Substring(2);
				byte[] parameterValue = this.tripleDES.Encrypt(text);
				this.da.SelectCommand.CommandText = "INSERT INTO infopc (student_no,dateentered,whoentered,description,title) VALUES (@student_no,getdate(),@whoami,'','')";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@student_no", parameterValue);
				this.da.SelectCommand.Parameters.Add("@whoami", this.whoAmIPersonID);
				this.infoPcPid = this.da.FillReturnIdentity(new DataTable(), "personid", "infopc");
				text = text + "_" + this.infoPcPid.ToString();
				parameterValue = this.tripleDES.Encrypt(text);
				this.da.SelectCommand.CommandText = "UPDATE infopc SET student_no=@student_no WHERE personid=@id";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
				this.da.SelectCommand.Parameters.Add("@student_no", parameterValue);
				this.da.Fill(new DataTable());
			}
			this.da.SelectCommand.CommandText = "SELECT ipc.personid AS infopcid,ipc.student_no,ipc.dateentered,ipc.whoentered,p.firstname,p.lastname,p.student_no,ipc.title FROM infopc ipc LEFT JOIN people p ON p.personid=ipc.whoentered WHERE ipc.personid=@id";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				if (dataRow["dateentered"] != DBNull.Value)
				{
					this.dtp_dateAdded.Value = (DateTime)dataRow["dateentered"];
				}
				if (dataRow["student_no"] != DBNull.Value)
				{
					this.txt_student_no.Text = this.tripleDES.Decrypt((byte[])dataRow["student_no"]);
				}
				if (dataRow["title"] != DBNull.Value)
				{
					this.txt_title.Text = (string)dataRow["title"];
				}
				this.p_data.Caption = string.Format("Case #: {0}", this.txt_student_no.Text);
				ListViewItem primaryClient = this.GetPrimaryClient();
				if (primaryClient != null)
				{
					DataRow dataRow2 = (DataRow)primaryClient.Tag;
					this.p_data.PrimaryClientDescription = string.Format("{0} {1} ({2})", dataRow2["firstname"].ToString(), dataRow2["lastname"].ToString(), dataRow2["student_no"].ToString());
					this.p_data.PrimaryClientPid = ((dataRow2["personid"] == DBNull.Value) ? 0 : ((int)dataRow2["personid"]));
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004C78 File Offset: 0x00003C78
		private ListViewItem CreateClientRespondentLvi(DataRow dr)
		{
			string value = (dr["firstname"] == DBNull.Value) ? "" : ((string)dr["firstname"]);
			string value2 = (dr["middlename"] == DBNull.Value) ? "" : ((string)dr["middlename"]);
			string value3 = (dr["lastname"] == DBNull.Value) ? "" : ((string)dr["lastname"]);
			string value4 = (dr["student_no"] == DBNull.Value) ? "" : ((string)dr["student_no"]);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value3);
			stringBuilder.Append(", ");
			stringBuilder.Append(value);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(value2);
			}
			if (!string.IsNullOrEmpty(value4))
			{
				stringBuilder.Append(" (");
				stringBuilder.Append(value4);
				stringBuilder.Append(")");
			}
			string text;
			switch ((int)dr["usertype"])
			{
			case 0:
				text = "Primary Client";
				break;
			case 1:
				text = "Client";
				break;
			case 2:
				text = "Respondent";
				break;
			default:
				text = "Unknown";
				break;
			}
			return new ListViewItem(stringBuilder.ToString())
			{
				SubItems = 
				{
					text
				},
				Tag = dr
			};
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004E28 File Offset: 0x00003E28
		private ListViewItem GetPrimaryClient()
		{
			ListViewItem result = null;
			foreach (object obj in this.lv_clientsRespondents.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				DataRow dataRow = (DataRow)listViewItem.Tag;
				switch ((int)dataRow["usertype"])
				{
				case 0:
					return listViewItem;
				case 1:
					result = listViewItem;
					break;
				default:
					result = listViewItem;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004EE0 File Offset: 0x00003EE0
		private void LoadClientsRespondents()
		{
			if (this.caseSingle != null)
			{
				this.clientsRespondentsTable = this.caseSingle.CasePeople;
			}
			else
			{
				this.clientsRespondentsTable = new DataTable();
				this.da.SelectCommand.CommandText = "SELECT ipc.personid AS infopcid,ipcp.personid,ipcp.usertype,p.firstname,p.lastname,p.middlename,p.student_no FROM infopc ipc LEFT JOIN infopcpeople ipcp ON ipcp.infopcid=ipc.personid LEFT JOIN people p ON p.personid=ipcp.personid WHERE ipc.personid=@id ORDER BY ipcp.usertype";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
				this.da.Fill(this.clientsRespondentsTable);
				this.clientsRespondentsTable = this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, this.clientsRespondentsTable, new string[]
				{
					"firstname",
					"lastname",
					"middlename",
					"student_no"
				});
				this.clientsRespondentsTable.AcceptChanges();
			}
			foreach (object obj in this.clientsRespondentsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					ListViewItem listViewItem = this.CreateClientRespondentLvi(dataRow);
					this.lv_clientsRespondents.Items.Add(listViewItem);
				}
			}
			if (this.infoPcPid <= 0)
			{
				if (this.pid > 0)
				{
					this.AddNewClientRespondent(this.pid, ClientType.PrimaryClient);
				}
				else
				{
					Control topLevelControl = this.p_data.TopLevelControl;
					string text;
					if (topLevelControl != null)
					{
						text = this.GetStudentName(topLevelControl);
						if (string.IsNullOrEmpty(text))
						{
							text = "?";
						}
					}
					else
					{
						text = "?";
					}
					ListViewItem listViewItem = new ListViewItem(text);
					listViewItem.SubItems.Add("Primary Client");
					this.lv_clientsRespondents.Items.Add(listViewItem);
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005108 File Offset: 0x00004108
		private string GetStudentName(Control parent)
		{
			string result;
			if (parent.Name.Equals("txt_firstName") || parent.Name.Equals("txt_lastName"))
			{
				result = parent.Text;
			}
			else
			{
				foreach (object obj in parent.Controls)
				{
					Control parent2 = (Control)obj;
					string studentName = this.GetStudentName(parent2);
					if (studentName != null)
					{
						return studentName;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000051C0 File Offset: 0x000041C0
		public DataTable CasePeople
		{
			get
			{
				return this.clientsRespondentsTable;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000051D8 File Offset: 0x000041D8
		private void AddNewClientRespondent(int pid, ClientType clientType)
		{
			this.da.SelectCommand.CommandText = "SELECT @id AS infopcid,@pid AS personid,@clienttype AS usertype,p.firstname,p.lastname,p.middlename,p.student_no FROM people p WHERE p.personid=@pid";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
			this.da.SelectCommand.Parameters.Add("@pid", pid);
			this.da.SelectCommand.Parameters.Add("@clienttype", (int)clientType);
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			dataTable = this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"middlename",
				"student_no"
			});
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				this.clientsRespondentsTable.ImportRow(dataRow);
				ListViewItem value = this.CreateClientRespondentLvi(dataRow);
				this.lv_clientsRespondents.Items.Add(value);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005348 File Offset: 0x00004348
		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005352 File Offset: 0x00004352
		private void CaseDetail_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005358 File Offset: 0x00004358
		private void btn_newClient_Click(object sender, EventArgs e)
		{
			CaseNewClient caseNewClient = new CaseNewClient(this.da, this.tripleDES, (this.lv_clientsRespondents.Items.Count > 0) ? ClientType.Client : ClientType.PrimaryClient, 1, this.eventHandlers, this.whoAmIPersonID, this.infoPcPid, this.caseScreenNum);
			DialogResult dialogResult = caseNewClient.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				int num = caseNewClient.Pid;
				if (num > 0)
				{
					this.AddNewClientRespondent(num, caseNewClient.ClientType);
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000053E0 File Offset: 0x000043E0
		private void btn_newRespondent_Click(object sender, EventArgs e)
		{
			CaseNewClient caseNewClient = new CaseNewClient(this.da, this.tripleDES, ClientType.Respondent, 1, this.eventHandlers, this.whoAmIPersonID, this.infoPcPid, this.caseScreenNum);
			DialogResult dialogResult = caseNewClient.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				int num = caseNewClient.Pid;
				if (num > 0)
				{
					this.AddNewClientRespondent(num, caseNewClient.ClientType);
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005451 File Offset: 0x00004451
		private void btn_ok_Click(object sender, EventArgs e)
		{
			this.Save(true);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000545C File Offset: 0x0000445C
		private void Save(bool closeWhenSuccessful)
		{
			DynamicScreen.SaveData(ref this.data, this.p_data, this.caseScreenNum, this.infoPcPid, "mainInfopc", "otherInfopc", "datetimeinfopc", this.tripleDES, -1);
			int num = 0;
			DataTable t = this.data.Tables["mainInfoTable"];
			Exception ex;
			num += CaseDetail.SaveDataPS(this.da, t, "maininfopc", this.caseScreenNum, this.infoPcPid, this.whoAmIPersonID, out ex);
			t = this.data.Tables["otherInfoTable"];
			num += CaseDetail.SaveDataPS(this.da, t, "otherInfoPC", this.caseScreenNum, this.infoPcPid, this.whoAmIPersonID, out ex);
			t = this.data.Tables["dateTimeInfoTable"];
			num += CaseDetail.SaveDataPS(this.da, t, "dateTimeInfoPC", this.caseScreenNum, this.infoPcPid, this.whoAmIPersonID, out ex);
			t = this.data.Tables["imageInfoTable"];
			num += CaseDetail.SaveDataPS(this.da, t, "imageInfoPC", this.caseScreenNum, this.infoPcPid, this.whoAmIPersonID, out ex);
			string parameterValue = this.txt_title.Text.Trim();
			this.da.SelectCommand.CommandText = "UPDATE infopc SET title=@title WHERE personid=@id";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@title", parameterValue);
			this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
			this.da.Fill(new DataTable());
			this.SavePMTablePeople();
			if (closeWhenSuccessful)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000564C File Offset: 0x0000464C
		private void SavePMTablePeople()
		{
			List<DataRow> list = new List<DataRow>(this.clientsRespondentsTable.Rows.Count);
			for (int i = 0; i < this.clientsRespondentsTable.Rows.Count; i++)
			{
				list.Add(this.clientsRespondentsTable.Rows[i]);
			}
			for (int i = 0; i < list.Count; i++)
			{
				DataRow dataRow = list[i];
				if (dataRow.RowState == DataRowState.Deleted)
				{
					dataRow.RejectChanges();
					int num = (int)dataRow["personid"];
					int num2 = (int)dataRow["usertype"];
					this.da.SelectCommand.CommandText = "DELETE FROM infopcpeople WHERE infopcid=@id AND personid=@personid AND usertype=@clienttype";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
					this.da.SelectCommand.Parameters.Add("@personid", num);
					this.da.SelectCommand.Parameters.Add("@clienttype", num2);
					this.da.Fill(new DataTable());
					dataRow.Delete();
				}
				else if (dataRow.RowState == DataRowState.Added)
				{
					this.da.SelectCommand.CommandText = "INSERT INTO infopcpeople (infopcid,personid,usertype) SELECT @id,@personid,@clienttype WHERE NOT EXISTS(SELECT personid FROM infopcpeople WHERE infopcid=@id AND personid=@personid AND usertype=@clienttype)";
					int num = (int)dataRow["personid"];
					int num2 = (int)dataRow["usertype"];
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@id", this.infoPcPid);
					this.da.SelectCommand.Parameters.Add("@personid", num);
					this.da.SelectCommand.Parameters.Add("@clienttype", num2);
					this.da.Fill(new DataTable());
				}
				dataRow.AcceptChanges();
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000058AC File Offset: 0x000048AC
		public static int SaveDataPS(UnivDataAdapter da, DataTable t, string tableName, int screenNum, int studentPid, int whoModifiedPid, out Exception exception)
		{
			return CaseDetail.SaveDataPS(da, t, tableName, screenNum, studentPid, whoModifiedPid, out exception, true);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000058D0 File Offset: 0x000048D0
		public static int SaveDataPS(UnivDataAdapter da, DataTable t, string tableName, int studentPid, int whoModifiedPid, out Exception exception)
		{
			return CaseDetail.SaveDataPS(da, t, tableName, -1, studentPid, whoModifiedPid, out exception, false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000058F4 File Offset: 0x000048F4
		public static int SaveDataPS(UnivDataAdapter da, DataTable t, string tableName, int screenNum, int studentPid, int whoModifiedPid, out Exception exception, bool tablesStoreScreenNum)
		{
			int num = 0;
			try
			{
				da.Connection.Open();
			}
			catch
			{
				try
				{
					da.Connection.Close();
					da.Connection.Open();
				}
				catch (Exception ex)
				{
					exception = ex;
					return 0;
				}
			}
			if (t != null)
			{
				foreach (object obj in t.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState == DataRowState.Added)
					{
						da.SelectCommand.CommandText = "DELETE FROM " + tableName + " WHERE personid=@personid AND controlid=@controlid;";
						if (tablesStoreScreenNum)
						{
							UnivCommand selectCommand = da.SelectCommand;
							selectCommand.CommandText = selectCommand.CommandText + "INSERT INTO " + tableName + " (screennum,personid,controlid,controlvalue) VALUES (@screennum,@personid,@controlid,@controlvalue)";
						}
						else
						{
							UnivCommand selectCommand2 = da.SelectCommand;
							selectCommand2.CommandText = selectCommand2.CommandText + "INSERT INTO " + tableName + " (personid,controlid,controlvalue) VALUES (@personid,@controlid,@controlvalue)";
						}
						da.SelectCommand.Parameters.Clear();
						if (tablesStoreScreenNum)
						{
							da.SelectCommand.Parameters.Add("@screennum", screenNum);
						}
						da.SelectCommand.Parameters.Add("@personid", studentPid);
						da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
						da.SelectCommand.Parameters.Add("@controlvalue", dataRow["controlvalue"]);
						da.SelectCommand.ExecuteNonQuery2();
						num++;
					}
					else if (dataRow.RowState == DataRowState.Deleted)
					{
						dataRow.RejectChanges();
						da.SelectCommand.CommandText = "DELETE FROM " + tableName + " WHERE personid=@personid AND controlid=@controlid";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@screennum", screenNum);
						da.SelectCommand.Parameters.Add("@personid", studentPid);
						da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
						da.SelectCommand.ExecuteNonQuery2();
						dataRow.Delete();
						num++;
					}
					else if (dataRow.RowState == DataRowState.Modified)
					{
						da.SelectCommand.CommandText = "DELETE FROM " + tableName + " WHERE personid=@personid AND controlid=@controlid AND NOT dataid=@dataid;";
						UnivCommand selectCommand3 = da.SelectCommand;
						selectCommand3.CommandText = selectCommand3.CommandText + "UPDATE " + tableName + " SET controlvalue=@controlvalue WHERE dataid=@dataid";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@controlvalue", dataRow["controlvalue"]);
						da.SelectCommand.Parameters.Add("@dataid", dataRow["dataid"]);
						da.SelectCommand.Parameters.Add("@personid", studentPid);
						da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
						da.SelectCommand.ExecuteNonQuery2();
						num++;
					}
				}
				t.AcceptChanges();
			}
			da.Connection.Close();
			exception = null;
			return num;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005CF8 File Offset: 0x00004CF8
		private void btn_removeSelectedClientRespondent_Click(object sender, EventArgs e)
		{
			if (this.lv_clientsRespondents.SelectedItems.Count == 1)
			{
				if (this.lv_clientsRespondents.Items.Count > 1)
				{
					ListViewItem listViewItem = this.lv_clientsRespondents.SelectedItems[0];
					if (listViewItem.Tag != null)
					{
						DataRow dataRow = (DataRow)listViewItem.Tag;
						DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Delete item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dialogResult == DialogResult.Yes)
						{
							listViewItem.Tag = null;
							dataRow.Delete();
							this.lv_clientsRespondents.Items.Remove(listViewItem);
						}
					}
				}
				else
				{
					MessageBox.Show("Removing all clients from this case is not allowed.  Please add another client before attempting to delete this one.  There must always be at least one client in the list.");
				}
			}
			else
			{
				MessageBox.Show("Please select one row first.");
			}
		}

		// Token: 0x0400002B RID: 43
		private ArrayList eventHandlers;

		// Token: 0x0400002C RID: 44
		private int infoPcPid;

		// Token: 0x0400002D RID: 45
		private int whoAmIPersonID;

		// Token: 0x0400002E RID: 46
		private CaseSingle caseSingle;

		// Token: 0x0400002F RID: 47
		private DataSet data;

		// Token: 0x04000030 RID: 48
		private DataTable clientsRespondentsTable;

		// Token: 0x04000031 RID: 49
		private UnivDataAdapter da;

		// Token: 0x04000032 RID: 50
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000033 RID: 51
		private int caseScreenNum;

		// Token: 0x04000034 RID: 52
		private int pid;
	}
}
