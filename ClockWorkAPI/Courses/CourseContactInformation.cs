using System;
using System.Data;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI.Courses
{
	// Token: 0x02000009 RID: 9
	public class CourseContactInformation
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000027F4 File Offset: 0x000017F4
		public CourseContactInformation(string name, string email, string phone, string username, Course.CourseContactPermissionLevel permissionLevel)
		{
			this.name = name;
			this.email = email;
			this.phone = phone;
			this.username = username;
			this.permissionLevel = permissionLevel;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002844 File Offset: 0x00001844
		public CourseContactInformation(DataRow dr)
		{
			this.name = dr["altname"].ToString().Trim();
			this.email = dr["altemail"].ToString().Trim();
			this.phone = dr["altphone"].ToString().Trim();
			this.username = dr["altusername"].ToString().Trim();
			int num = (dr["altpermissionLevel"] == DBNull.Value) ? 0 : ((int)dr["altpermissionLevel"]);
			try
			{
				this.permissionLevel = (Course.CourseContactPermissionLevel)num;
			}
			catch
			{
				this.permissionLevel = Course.CourseContactPermissionLevel.None;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000292C File Offset: 0x0000192C
		public CourseContactInformation(string name, string email, string phone, Course.CourseContactPermissionLevel permissionLevel)
		{
			this.name = name;
			this.email = email;
			this.phone = phone;
			this.username = "";
			this.permissionLevel = permissionLevel;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002980 File Offset: 0x00001980
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002998 File Offset: 0x00001998
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000029A4 File Offset: 0x000019A4
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000029BC File Offset: 0x000019BC
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029C8 File Offset: 0x000019C8
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000029E0 File Offset: 0x000019E0
		public string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000029EC File Offset: 0x000019EC
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002A04 File Offset: 0x00001A04
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002A10 File Offset: 0x00001A10
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002A28 File Offset: 0x00001A28
		public Course.CourseContactPermissionLevel PermissionLevel
		{
			get
			{
				return this.permissionLevel;
			}
			set
			{
				this.permissionLevel = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A34 File Offset: 0x00001A34
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002A4C File Offset: 0x00001A4C
		public int InstructorId
		{
			get
			{
				return this.instructorId;
			}
			set
			{
				this.instructorId = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A58 File Offset: 0x00001A58
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002A70 File Offset: 0x00001A70
		public int AlternateContactId
		{
			get
			{
				return this.alternateContactId;
			}
			set
			{
				this.alternateContactId = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A7C File Offset: 0x00001A7C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002A94 File Offset: 0x00001A94
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
			set
			{
				this.lucid = value;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002AA0 File Offset: 0x00001AA0
		public int LookupAlternateContactId()
		{
			int result;
			if (this.alternateContactId > 0)
			{
				result = this.alternateContactId;
			}
			else
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "IF EXISTS(SELECT alternatecontactid FROM lucoursealternatecontact WHERE username=@username)\r\n    SELECT TOP 1 alternatecontactid FROM lucoursealternatecontact WHERE username=@username)\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursealternatecontact (altname,altemail,altusername,altphone,altpermissionlevel,whocreated,datecreated,isactive)\r\n        VALUES (@name,@email,@username,@phone,0,-556,getdate(),1);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int)\r\nEND";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@name", this.name);
				da.SelectCommand.Parameters.Add("@email", this.email);
				da.SelectCommand.Parameters.Add("@username", this.username);
				da.SelectCommand.Parameters.Add("@phone", this.phone);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
				{
					this.alternateContactId = (int)dataTable.Rows[0][0];
					result = this.alternateContactId;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		private string name;

		// Token: 0x04000007 RID: 7
		private string email;

		// Token: 0x04000008 RID: 8
		private string phone;

		// Token: 0x04000009 RID: 9
		private string username;

		// Token: 0x0400000A RID: 10
		private Course.CourseContactPermissionLevel permissionLevel;

		// Token: 0x0400000B RID: 11
		private int instructorId = 0;

		// Token: 0x0400000C RID: 12
		private int alternateContactId = 0;

		// Token: 0x0400000D RID: 13
		private int lucid = 0;
	}
}
