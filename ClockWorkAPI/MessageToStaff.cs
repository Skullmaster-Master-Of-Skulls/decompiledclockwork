using System;
using System.Collections.Generic;
using System.Data;
using ClockWorkAPI.EntityExtensions;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000093 RID: 147
	public class MessageToStaff
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00029198 File Offset: 0x00028198
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x000291B0 File Offset: 0x000281B0
		public int StaffMessageId
		{
			get
			{
				return this.staffMessageId;
			}
			set
			{
				this.staffMessageId = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000291BC File Offset: 0x000281BC
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x000291D4 File Offset: 0x000281D4
		public PersonBaseDTO WhoEntered
		{
			get
			{
				return this.whoEntered;
			}
			set
			{
				this.whoEntered = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000291E0 File Offset: 0x000281E0
		public string WhoEnteredName
		{
			get
			{
				return (this.whoEntered == null) ? "" : this.whoEntered.GetName();
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0002920C File Offset: 0x0002820C
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x00029224 File Offset: 0x00028224
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00029230 File Offset: 0x00028230
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x00029248 File Offset: 0x00028248
		public Group Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00029254 File Offset: 0x00028254
		public string GroupDescription
		{
			get
			{
				return (this.group == null) ? "" : this.group.Title;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00029280 File Offset: 0x00028280
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00029298 File Offset: 0x00028298
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000292A4 File Offset: 0x000282A4
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x000292BC File Offset: 0x000282BC
		public DateTime? EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000292C8 File Offset: 0x000282C8
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x000292E0 File Offset: 0x000282E0
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000292EC File Offset: 0x000282EC
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00029304 File Offset: 0x00028304
		public string Msg
		{
			get
			{
				return this.msg;
			}
			set
			{
				this.msg = value;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00029310 File Offset: 0x00028310
		public MessageToStaff()
		{
			this.staffMessageId = 0;
			this.whoEntered = null;
			this.isActive = true;
			this.group = null;
			this.startDate = DateTime.Now;
			this.endDate = null;
			this.title = "";
			this.msg = "";
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00029370 File Offset: 0x00028370
		public MessageToStaff(DataRow dr)
		{
			this.staffMessageId = ((dr["staffmessageid"] == DBNull.Value) ? 0 : ((int)dr["staffmessageid"]));
			int personId = (dr["whoentered"] == DBNull.Value) ? 0 : ((int)dr["whoentered"]);
			string firstName = dr["firstname"].ToString();
			string lastName = dr["lastname"].ToString();
			this.whoEntered = new PersonBaseDTO
			{
				PersonId = personId,
				FirstName = firstName,
				MiddleName = "",
				LastName = lastName,
				Student_no = "",
				CoreGroup = eCoreGroupDTO.Students,
				Tag = new PersonExt(),
				Groups = new List<GroupDTO>()
			};
			this.isActive = (bool)dr["isactive"];
			int groupId = (dr["groupid"] == DBNull.Value) ? 0 : ((int)dr["groupid"]);
			string text = dr["groupdescription"].ToString();
			this.group = new Group();
			this.group.GroupId = groupId;
			this.group.Title = text;
			this.startDate = (DateTime)dr["startdate"];
			this.endDate = ((dr["enddate"] == DBNull.Value) ? null : ((DateTime?)dr["enddate"]));
			this.title = dr["title"].ToString();
			this.msg = dr["msg"].ToString();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0002954C File Offset: 0x0002854C
		public bool Save(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmIPid)
		{
			bool result;
			if (this.staffMessageId > 0)
			{
				string commandText = "UPDATE StaffMessages SET isactive=@isactive,groupid=@groupid,startdate=@startdate,enddate=@enddate,title=@title,msg=@msg WHERE staffmessageid=@id";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@isactive", this.isActive);
				if (this.group == null || this.group.GroupId < 1)
				{
					da.SelectCommand.Parameters.Add("@groupid", null);
				}
				else
				{
					da.SelectCommand.Parameters.Add("@groupid", this.group.GroupId);
				}
				da.SelectCommand.Parameters.Add("@startdate", this.startDate);
				da.SelectCommand.Parameters.Add("@enddate", this.endDate);
				da.SelectCommand.Parameters.Add("@title", this.title);
				da.SelectCommand.Parameters.Add("@msg", tripleDES.Encrypt(this.msg));
				da.SelectCommand.Parameters.Add("@id", this.staffMessageId);
				da.Fill(new DataTable());
				result = true;
			}
			else
			{
				string commandText = "INSERT INTO StaffMessages (whoentered,isactive,groupid,startdate,enddate,title,msg) \r\nVALUES (@whoentered,@isactive,@groupid,@startdate,@enddate,@title,@msg);\r\nSELECT CAST(SCOPE_IDENTITY AS int32) AS staffmessageid";
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@isactive", this.isActive);
				if (this.group == null || this.group.GroupId < 1)
				{
					da.SelectCommand.Parameters.Add("@groupid", null);
				}
				else
				{
					da.SelectCommand.Parameters.Add("@groupid", this.group.GroupId);
				}
				da.SelectCommand.Parameters.Add("@startdate", this.startDate);
				da.SelectCommand.Parameters.Add("@enddate", this.endDate);
				da.SelectCommand.Parameters.Add("@title", this.title);
				da.SelectCommand.Parameters.Add("@msg", tripleDES.Encrypt(this.msg));
				da.SelectCommand.Parameters.Add("@whoentered", whoAmIPid);
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
				{
					this.staffMessageId = (int)dataTable.Rows[0][0];
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002986C File Offset: 0x0002886C
		public bool Delete(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmIPid)
		{
			string commandText = "DELETE FROM StaffMessage WHERE staffmessageid=@id";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@id", this.staffMessageId);
			da.Fill(new DataTable());
			return true;
		}

		// Token: 0x040003B7 RID: 951
		private int staffMessageId;

		// Token: 0x040003B8 RID: 952
		private PersonBaseDTO whoEntered;

		// Token: 0x040003B9 RID: 953
		private bool isActive;

		// Token: 0x040003BA RID: 954
		private Group group;

		// Token: 0x040003BB RID: 955
		private DateTime startDate;

		// Token: 0x040003BC RID: 956
		private DateTime? endDate;

		// Token: 0x040003BD RID: 957
		private string title;

		// Token: 0x040003BE RID: 958
		private string msg;
	}
}
