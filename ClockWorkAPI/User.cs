using System;
using System.Collections.Generic;
using System.Data;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.People;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.People;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000088 RID: 136
	public class User
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00024F0C File Offset: 0x00023F0C
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00024F24 File Offset: 0x00023F24
		public GroupList Groups
		{
			get
			{
				return this.groups;
			}
			set
			{
				this.groups = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00024F30 File Offset: 0x00023F30
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00024F48 File Offset: 0x00023F48
		public string Student_no
		{
			get
			{
				return this.student_no;
			}
			set
			{
				this.student_no = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00024F54 File Offset: 0x00023F54
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00024F6C File Offset: 0x00023F6C
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00024F78 File Offset: 0x00023F78
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00024F90 File Offset: 0x00023F90
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00024F9C File Offset: 0x00023F9C
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00024FB4 File Offset: 0x00023FB4
		public string MiddleName
		{
			get
			{
				return this.middleName;
			}
			set
			{
				this.middleName = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00024FC0 File Offset: 0x00023FC0
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00024FD8 File Offset: 0x00023FD8
		public int PersonId
		{
			get
			{
				return this.personId;
			}
			set
			{
				this.personId = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00024FE4 File Offset: 0x00023FE4
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00024FFC File Offset: 0x00023FFC
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00025008 File Offset: 0x00024008
		public User()
		{
			this.student_no = "";
			this.firstName = "";
			this.lastName = "";
			this.middleName = "";
			this.personId = 0;
			this.isActive = true;
			this.groups = new GroupList();
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00025064 File Offset: 0x00024064
		public User(List<DataRow> rowsWithGroupInfo, ref GroupList groupsPool)
		{
			this.groups = new GroupList();
			if (rowsWithGroupInfo != null && rowsWithGroupInfo.Count > 0)
			{
				DataRow dataRow = rowsWithGroupInfo[0];
				this.student_no = dataRow["student_no"].ToString();
				this.firstName = dataRow["firstname"].ToString();
				this.lastName = dataRow["lastname"].ToString();
				this.middleName = dataRow["middlename"].ToString();
				this.personId = ((dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]));
				this.isActive = (dataRow["isactive"] == DBNull.Value || Convert.ToBoolean(dataRow["isactive"]));
				foreach (DataRow dataRow2 in rowsWithGroupInfo)
				{
					int num = (dataRow2["groupid"] == DBNull.Value) ? 0 : ((int)dataRow2["groupid"]);
					if (num > 0)
					{
						Group group = groupsPool.GetGroup(num);
						if (group == null)
						{
							this.groups.Add(groupsPool.AddGroup(dataRow2));
						}
						else
						{
							this.groups.Add(group);
						}
					}
				}
			}
			else
			{
				this.student_no = "";
				this.firstName = "";
				this.lastName = "";
				this.middleName = "";
				this.personId = 0;
				this.isActive = true;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00025250 File Offset: 0x00024250
		public Exception SetPassword(string newPassword, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			Exception result;
			if (this.personId > 0)
			{
				string commandText = "IF EXISTS(SELECT personid FROM userinfo WHERE personid=@pid AND username=@une)\r\nBEGIN\r\n\tUPDATE userinfo SET pass=@passe,lastpasswordchangedate=getdate() WHERE personid=@pid AND username=@une\r\nEND\r\nELSE\r\nBEGIN\r\n\tINSERT INTO userinfo (username,pass,personid,requirepasswordchange,lastpasswordchangedate)\r\n\t\tVALUES (@une,@passe,@pid,0,getdate())\r\nEND";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", this.personId);
				da.SelectCommand.Parameters.Add("@une", tripleDES.Encrypt(this.student_no));
				da.SelectCommand.Parameters.Add("@passe", tripleDES.Encrypt(newPassword));
				string text;
				da.Fill(new DataTable(), out text);
				if (string.IsNullOrEmpty(text))
				{
					result = null;
				}
				else
				{
					result = new Exception(text);
				}
			}
			else
			{
				result = new Exception("Invalid person id.");
			}
			return result;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0002532C File Offset: 0x0002432C
		public int Save(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			string commandText;
			if (this.personId > 0)
			{
				commandText = "UPDATE people SET firstname=@fne,lastname=@lne,student_no=@sne,isactive=@isactive,middlename=@mne\r\nWHERE personid=@pid";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", this.personId);
				da.SelectCommand.Parameters.Add("@fne", tripleDES.Encrypt(this.firstName));
				da.SelectCommand.Parameters.Add("@lne", tripleDES.Encrypt(this.lastName));
				da.SelectCommand.Parameters.Add("@sne", tripleDES.Encrypt(this.student_no));
				da.SelectCommand.Parameters.Add("@isactive", this.isActive);
				da.SelectCommand.Parameters.Add("@mne", tripleDES.Encrypt(this.middleName));
				string value;
				da.Fill(new DataTable(), out value);
				if (!string.IsNullOrEmpty(value))
				{
					return 0;
				}
			}
			else
			{
				commandText = "INSERT INTO people (firstname,lastname,student_no,isactive,dateadded,middlename) \r\nSELECT @fne,@lne,@sne,@isactive,getdate(),@mne WHERE NOT EXISTS(SELECT personid FROM people WHERE student_no=@sne)";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@fne", tripleDES.Encrypt(this.firstName));
				da.SelectCommand.Parameters.Add("@lne", tripleDES.Encrypt(this.lastName));
				da.SelectCommand.Parameters.Add("@sne", tripleDES.Encrypt(this.student_no));
				da.SelectCommand.Parameters.Add("@isactive", this.isActive);
				da.SelectCommand.Parameters.Add("@mne", tripleDES.Encrypt(this.middleName));
				this.personId = da.FillReturnIdentity(new DataTable(), "personid", "people");
				if (this.personId <= 0)
				{
					return 0;
				}
			}
			string groupIdsCommaSeparated = this.groups.GroupIdsCommaSeparated;
			commandText = "INSERT INTO peoplegroups (groupid,personid,isprimarygroup,ordernum) \r\n    SELECT @gid,@pid,@isprimarygroup,0 \r\n        WHERE NOT EXISTS(SELECT persongroupid FROM peoplegroups WHERE personid=@pid and groupid=@gid)";
			foreach (Group group in this.groups)
			{
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", this.personId);
				da.SelectCommand.Parameters.Add("@gid", group.GroupId);
				da.SelectCommand.Parameters.Add("@isprimarygroup", group.IsPrimary);
				da.Fill(new DataTable());
			}
			commandText = "DELETE FROM peoplegroups WHERE persongroupid IN \r\n    (SELECT personidgroupid FROM peoplegroups WHERE personid=@pid \r\n        AND NOT groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))\r\n     )";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gids", groupIdsCommaSeparated);
			da.SelectCommand.Parameters.Add("@pid", this.personId);
			da.Fill(new DataTable());
			return this.personId;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000256CC File Offset: 0x000246CC
		public static string GetGroupDescription(int gid, UnivDataAdapter da, DataTable groupsTable)
		{
			string text = "";
			if (groupsTable != null)
			{
				foreach (object obj in groupsTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow["groupid"];
					if (num == gid)
					{
						text = dataRow["description"].ToString();
						break;
					}
				}
				if (text.Length < 1)
				{
					da.SelectCommand.CommandText = "SELECT description FROM groups WHERE groupid=" + gid.ToString();
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						text = dataTable.Rows[0][0].ToString();
					}
				}
			}
			return text + " (group #" + gid.ToString() + ")";
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0002580C File Offset: 0x0002480C
		public static int CreateClientAccount(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmI_id, string snum, string firstname, string middlename, string lastname, DateTime dateAdded, params int[] groupids)
		{
			int primaryGroupId = (groupids.Length > 0) ? groupids[0] : -1;
			return User.CreateClientAccount(da, tripleDES, whoAmI_id, snum, firstname, middlename, lastname, dateAdded, primaryGroupId, groupids);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00025844 File Offset: 0x00024844
		public static DataTable LoadUserGroupsInOrder(int pid, UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT DISTINCT pg.groupid,g.isprimary,g.description,g.ordernum FROM peoplegroups pg LEFT JOIN groups g on g.groupid=pg.groupid WHERE pg.personid=@pid ORDER BY g.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			return dataTable;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000258A8 File Offset: 0x000248A8
		public static int CreateClientAccount(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmI_id, PersonBaseDTO person, int primaryGroupId, params int[] groupids)
		{
			return User.CreateClientAccount(da, tripleDES, whoAmI_id, person.Student_no, person.FirstName, person.MiddleName, person.LastName, DateTime.Now, primaryGroupId, groupids);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000258E4 File Offset: 0x000248E4
		public static int CreateClientAccount(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmI_id, string snum, string firstname, string middlename, string lastname, DateTime dateAdded, int primaryGroupId, params int[] groupids)
		{
			IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
			LoadPersonByStudentNumberResp loadPersonByStudentNumberResp = personBaseClientManager.LoadPersonByStudentNumber(new LoadPersonByStudentNumberReq
			{
				Student_no = snum
			});
			int result;
			if (loadPersonByStudentNumberResp.Person != null && loadPersonByStudentNumberResp.Person.PersonId > 0)
			{
				result = loadPersonByStudentNumberResp.Person.PersonId;
			}
			else
			{
				CreateUserResp createUserResp = personBaseClientManager.CreateUser(new CreateUserReq
				{
					WhoAmI = whoAmI_id,
					User = new PersonBaseDTO
					{
						FirstName = (firstname ?? ""),
						MiddleName = (middlename ?? ""),
						LastName = (lastname ?? ""),
						Student_no = (snum ?? "")
					},
					GroupIds = new List<int>(groupids)
				});
				result = createUserResp.PersonId;
			}
			return result;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000259D4 File Offset: 0x000249D4
		public static List<User> LoadUsers(int groupId)
		{
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			string commandText = "SELECT p.personid,p.firstname,p.lastname,p.middlename,p.student_no,p.isactive\r\n,pg.groupid,g.description,g.isprimary,g.viewappsvisible,g.fulldescription,g.ordernum\r\nFROM peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nLEFT JOIN groups g ON g.groupid=pg.groupid\r\nWHERE p.isactive=1 AND pg.groupid=@gid\r\nORDER BY p.personid,pg.groupid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", groupId);
			DataTable t = new DataTable();
			da.Fill(t);
			return User.LoadUsers(t, tripleDES);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00025A58 File Offset: 0x00024A58
		public static List<User> LoadUsers(object db, object tripleDES2, int groupId)
		{
			return User.LoadUsers(groupId);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00025A70 File Offset: 0x00024A70
		private static List<User> LoadUsers(DataTable t, TripleDESEncryptionClass tripleDES)
		{
			t = tripleDES.EncryptOrDecryptNameDataTableBatch(false, t, new string[]
			{
				"firstname",
				"lastname",
				"student_no",
				"middlename"
			});
			List<User> list = new List<User>();
			GroupList groupList = new GroupList();
			int j;
			for (int i = 0; i < t.Rows.Count; i = j)
			{
				DataRow dataRow = t.Rows[i];
				int num = (int)dataRow["personid"];
				j = i;
				GroupList groupList2 = new GroupList();
				List<DataRow> list2 = new List<DataRow>();
				while (j < t.Rows.Count)
				{
					DataRow dataRow2 = t.Rows[j];
					int num2 = (int)dataRow2["personid"];
					if (num2 != num)
					{
						break;
					}
					list2.Add(dataRow2);
					j++;
				}
				User item = new User(list2, ref groupList);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00025B8C File Offset: 0x00024B8C
		public static List<User> LoadUsers(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int groupId)
		{
			string commandText = "SELECT p.personid,p.firstname,p.lastname,p.middlename,p.student_no,p.isactive\r\n,pg.groupid,g.description,g.isprimary,g.viewappsvisible,g.fulldescription,g.ordernum\r\nFROM peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nLEFT JOIN groups g ON g.groupid=pg.groupid\r\nWHERE p.isactive=1 AND pg.groupid=@gid\r\nORDER BY p.personid,pg.groupid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", groupId);
			DataTable t = new DataTable();
			string value;
			da.Fill(t, out value);
			if (!string.IsNullOrEmpty(value))
			{
			}
			return User.LoadUsers(t, tripleDES);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00025C08 File Offset: 0x00024C08
		public static string LookupEmail(int pid)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "DECLARE @emailcid int\r\nSET @emailcid = (SELECT TOP 1 settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\nSELECT psd.valtext,psd.valbytes,psd.valbytesisencrypted FROM perstudentdata2 psd WHERE psd.personid=@pid AND psd.controlid=@emailcid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				bool flag = dataRow["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow["valbytesisencrypted"]);
				if (!flag)
				{
					return dataRow["valtext"].ToString();
				}
				if (dataRow["valbytes"] != DBNull.Value)
				{
					return tripleDES.Decrypt((byte[])dataRow["valbytes"]);
				}
			}
			return "";
		}

		// Token: 0x0400037A RID: 890
		private string student_no;

		// Token: 0x0400037B RID: 891
		private string firstName;

		// Token: 0x0400037C RID: 892
		private string lastName;

		// Token: 0x0400037D RID: 893
		private string middleName;

		// Token: 0x0400037E RID: 894
		private int personId;

		// Token: 0x0400037F RID: 895
		private bool isActive;

		// Token: 0x04000380 RID: 896
		private GroupList groups;
	}
}
