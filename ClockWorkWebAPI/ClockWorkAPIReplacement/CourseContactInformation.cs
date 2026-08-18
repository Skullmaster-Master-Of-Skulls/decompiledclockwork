using System;
using System.Data;
using System.Data.Common;
using Databases;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000051 RID: 81
	public class CourseContactInformation
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		public CourseContactInformation(string name, string email, string phone, string username, CourseContactPermissionLevel permissionLevel)
		{
			this.name = name;
			this.email = email;
			this.phone = phone;
			this.username = username;
			this.permissionLevel = permissionLevel;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001E004 File Offset: 0x0001C204
		public CourseContactInformation(DataRow dr)
		{
			this.name = dr["altname"].ToString().Trim();
			this.email = dr["altemail"].ToString().Trim();
			this.phone = dr["altphone"].ToString().Trim();
			this.username = dr["altusername"].ToString().Trim();
			int num = (dr["altpermissionLevel"] == DBNull.Value) ? 0 : ((int)dr["altpermissionLevel"]);
			try
			{
				this.permissionLevel = (CourseContactPermissionLevel)num;
			}
			catch
			{
				this.permissionLevel = CourseContactPermissionLevel.None;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public CourseContactInformation(string name, string email, string phone, CourseContactPermissionLevel permissionLevel)
		{
			this.name = name;
			this.email = email;
			this.phone = phone;
			this.username = "";
			this.permissionLevel = permissionLevel;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001E13C File Offset: 0x0001C33C
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001E154 File Offset: 0x0001C354
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

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001E160 File Offset: 0x0001C360
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0001E178 File Offset: 0x0001C378
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001E184 File Offset: 0x0001C384
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0001E19C File Offset: 0x0001C39C
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001E1CC File Offset: 0x0001C3CC
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		public CourseContactPermissionLevel PermissionLevel
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001E1F0 File Offset: 0x0001C3F0
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0001E208 File Offset: 0x0001C408
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

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001E214 File Offset: 0x0001C414
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0001E22C File Offset: 0x0001C42C
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001E238 File Offset: 0x0001C438
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001E250 File Offset: 0x0001C450
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

		// Token: 0x0600042B RID: 1067 RVA: 0x0001E25C File Offset: 0x0001C45C
		public int LookupAlternateContactId()
		{
			bool flag = this.alternateContactId > 0;
			int result;
			if (flag)
			{
				result = this.alternateContactId;
			}
			else
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string query = "IF EXISTS(SELECT alternatecontactid FROM lucoursealternatecontact WHERE username=@username)\r\n    SELECT TOP 1 alternatecontactid FROM lucoursealternatecontact WHERE username=@username)\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursealternatecontact (altname,altemail,altusername,altphone,altpermissionlevel,whocreated,datecreated,isactive)\r\n        VALUES (@name,@email,@username,@phone,0,-556,getdate(),1);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int)\r\nEND";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@name", DbType.String, this.name),
					clockWork.GetParameter("@email", DbType.String, this.email),
					clockWork.GetParameter("@username", DbType.String, this.username),
					clockWork.GetParameter("@phone", DbType.String, this.phone)
				});
				bool flag2 = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
				if (flag2)
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

		// Token: 0x0400020C RID: 524
		private string name;

		// Token: 0x0400020D RID: 525
		private string email;

		// Token: 0x0400020E RID: 526
		private string phone;

		// Token: 0x0400020F RID: 527
		private string username;

		// Token: 0x04000210 RID: 528
		private CourseContactPermissionLevel permissionLevel;

		// Token: 0x04000211 RID: 529
		private int instructorId = 0;

		// Token: 0x04000212 RID: 530
		private int alternateContactId = 0;

		// Token: 0x04000213 RID: 531
		private int lucid = 0;
	}
}
