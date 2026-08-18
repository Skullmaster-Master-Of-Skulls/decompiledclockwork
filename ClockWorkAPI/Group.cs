using System;
using System.Collections.Generic;
using System.Data;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.People;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.People;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200005C RID: 92
	public class Group
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000196DF File Offset: 0x000186DF
		public Group()
		{
			this.groupId = 0;
			this.title = "";
			this.isVisible = true;
			this.isPrimary = false;
			this.orderNum = 0;
			this.longDescription = "";
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001971C File Offset: 0x0001871C
		public Group(UnivDataAdapter da, int groupId)
		{
			this.groupId = groupId;
			string commandText = "SELECT g.groupid,g.description,g.isprimary,g.viewappsvisible,g.fulldescription,g.ordernum FROM groups WHERE groupid=@gid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", groupId);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				this.title = dataRow["description"].ToString();
				this.isVisible = (dataRow["viewappsvisible"] != DBNull.Value && Convert.ToBoolean(dataRow["viewappsvisible"]));
				this.isPrimary = (dataRow["isprimary"] != DBNull.Value && Convert.ToBoolean(dataRow["isprimary"]));
				this.longDescription = dataRow["fulldescription"].ToString();
				this.orderNum = ((dataRow["ordernum"] == DBNull.Value) ? 0 : ((int)dataRow["ordernum"]));
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001985C File Offset: 0x0001885C
		public Group(DataRow dr)
		{
			if (dr != null && dr.RowState != DataRowState.Deleted)
			{
				this.groupId = (int)dr["groupid"];
				this.title = dr["description"].ToString();
				this.isVisible = (dr["viewappsvisible"] != DBNull.Value && Convert.ToBoolean(dr["viewappsvisible"]));
				this.isPrimary = (dr["isprimary"] != DBNull.Value && Convert.ToBoolean(dr["isprimary"]));
				this.longDescription = dr["fulldescription"].ToString();
				this.orderNum = ((dr["ordernum"] == DBNull.Value) ? 0 : ((int)dr["ordernum"]));
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00019950 File Offset: 0x00018950
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00019968 File Offset: 0x00018968
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00019974 File Offset: 0x00018974
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001998C File Offset: 0x0001898C
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00019998 File Offset: 0x00018998
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x000199B0 File Offset: 0x000189B0
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000199BC File Offset: 0x000189BC
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x000199D4 File Offset: 0x000189D4
		public bool IsPrimary
		{
			get
			{
				return this.isPrimary;
			}
			set
			{
				this.isPrimary = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000199E0 File Offset: 0x000189E0
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x000199F8 File Offset: 0x000189F8
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00019A04 File Offset: 0x00018A04
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00019A1C File Offset: 0x00018A1C
		public string LongDescription
		{
			get
			{
				return this.longDescription;
			}
			set
			{
				this.longDescription = value;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00019A28 File Offset: 0x00018A28
		public int Save(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int result;
			if (this.groupId <= 0)
			{
				string commandText = "INSERT INTO groups (description,isPrimary,viewAppsVisible,fullDescription,ordernum) \r\n    SELECT @description,@isprimary,@viewappsvisible,@fulldescription,@ordernum WHERE NOT EXISTS(SELECT groupid FROM groups WHERE description=@description)";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@description", this.title);
				da.SelectCommand.Parameters.Add("@isprimary", this.isPrimary);
				da.SelectCommand.Parameters.Add("@viewappsvisible", this.isVisible);
				da.SelectCommand.Parameters.Add("@fulldescription", this.longDescription);
				da.SelectCommand.Parameters.Add("@ordernum", this.orderNum);
				this.groupId = da.FillReturnIdentity(new DataTable(), "groupid", "groups");
				result = this.groupId;
			}
			else
			{
				string commandText = "UPDATE groups SET description=@description,isprimary=@isprimary,viewappsvisible=@viewappsvisible\r\n                    ,fulldescription=@fulldescription,ordernum=@ordernum WHERE groupid=@gid";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@gid", this.groupId);
				da.SelectCommand.Parameters.Add("@description", this.title);
				da.SelectCommand.Parameters.Add("@isprimary", this.isPrimary);
				da.SelectCommand.Parameters.Add("@viewappsvisible", this.isVisible);
				da.SelectCommand.Parameters.Add("@fulldescription", this.longDescription);
				da.SelectCommand.Parameters.Add("@ordernum", this.orderNum);
				string value;
				da.Fill(new DataTable(), out value);
				if (!string.IsNullOrEmpty(value))
				{
					result = 0;
				}
				else
				{
					result = this.groupId;
				}
			}
			return result;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00019C38 File Offset: 0x00018C38
		public override string ToString()
		{
			return this.title;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00019C50 File Offset: 0x00018C50
		public static List<Group> LoadAllGroups(UnivDataAdapter da)
		{
			string commandText = "SELECT g.groupid,g.description,g.isprimary,g.viewappsvisible,g.fulldescription,g.ordernum\r\nFROM groups g \r\nORDER BY g.ordernum,g.description";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			List<Group> list = new List<Group>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Group item = new Group(dr);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00019D0C File Offset: 0x00018D0C
		public static Group FindGroup(List<Group> groups, int groupId)
		{
			foreach (Group group in groups)
			{
				if (group.GroupId == groupId)
				{
					return group;
				}
			}
			return null;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00019D8C File Offset: 0x00018D8C
		public static List<PersonBaseDTO> LoadGroupMembers(int groupId)
		{
			GroupDTO groupDTO = new GroupDTO
			{
				GroupId = groupId,
				Description = ""
			};
			IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
			LoadGroupMembersResp loadGroupMembersResp = personBaseClientManager.LoadGroupMembers(new LoadGroupMembersReq
			{
				GroupId = groupDTO.GroupId
			});
			List<PersonBaseDTO> groupMembers = loadGroupMembersResp.GroupMembers;
			return groupMembers.ConvertAll<PersonBaseDTO>((PersonBaseDTO p) => p);
		}

		// Token: 0x040001E2 RID: 482
		private int groupId;

		// Token: 0x040001E3 RID: 483
		private string title;

		// Token: 0x040001E4 RID: 484
		private bool isVisible;

		// Token: 0x040001E5 RID: 485
		private bool isPrimary;

		// Token: 0x040001E6 RID: 486
		private int orderNum;

		// Token: 0x040001E7 RID: 487
		private string longDescription;

		// Token: 0x0200005D RID: 93
		public enum eGroup
		{
			// Token: 0x040001EA RID: 490
			None,
			// Token: 0x040001EB RID: 491
			Student,
			// Token: 0x040001EC RID: 492
			Staff,
			// Token: 0x040001ED RID: 493
			Room,
			// Token: 0x040001EE RID: 494
			Resource
		}
	}
}
