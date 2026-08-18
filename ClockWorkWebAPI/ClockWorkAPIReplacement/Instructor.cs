using System;
using System.Data;
using System.Data.Common;
using Databases;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000060 RID: 96
	public class Instructor
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000218C8 File Offset: 0x0001FAC8
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x000218EF File Offset: 0x0001FAEF
		public string FirstName
		{
			get
			{
				return (this.firstName == null) ? "" : this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000218FC File Offset: 0x0001FAFC
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00021923 File Offset: 0x0001FB23
		public string LastName
		{
			get
			{
				return (this.lastName == null) ? "" : this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00021930 File Offset: 0x0001FB30
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00021948 File Offset: 0x0001FB48
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00021954 File Offset: 0x0001FB54
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00021980 File Offset: 0x0001FB80
		public string Name
		{
			get
			{
				return (this.name == null) ? "" : this.name.Trim();
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0002198C File Offset: 0x0001FB8C
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x000219B8 File Offset: 0x0001FBB8
		public string Phone
		{
			get
			{
				return (this.phone == null) ? "" : this.phone.Trim();
			}
			set
			{
				this.phone = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000219C4 File Offset: 0x0001FBC4
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public string Email
		{
			get
			{
				return (this.email == null) ? "" : this.email.Trim();
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000219FC File Offset: 0x0001FBFC
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00021A28 File Offset: 0x0001FC28
		public string Username
		{
			get
			{
				return (this.username == null) ? "" : this.username.Trim();
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00021A34 File Offset: 0x0001FC34
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00021A60 File Offset: 0x0001FC60
		public string Id
		{
			get
			{
				return (this.id == null) ? "" : this.id.Trim();
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00021A6C File Offset: 0x0001FC6C
		public Instructor()
		{
			this.instructorId = 0;
			this.name = "";
			this.phone = "";
			this.email = "";
			this.username = "";
			this.id = "";
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public int LookupInstructorId()
		{
			bool flag = this.instructorId > 0;
			int result;
			if (flag)
			{
				result = this.instructorId;
			}
			else
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				bool flag2 = !string.IsNullOrEmpty(this.id);
				string text;
				if (flag2)
				{
					text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND id=@id)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND id=@id;";
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(this.username);
					if (flag3)
					{
						text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND username=@username)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND username=@username;";
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(this.email);
						if (flag4)
						{
							text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND email=@email)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND email=@email;";
						}
						else
						{
							text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND altlookupstring=@name)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND altlookupstring=@name;";
						}
					}
				}
				text += "\r\nELSE \r\nBEGIN\r\nINSERT INTO lucoursedata (lookupstring,altlookupstring,phone,email,username,id) VALUES (@name,@name,@phone,@email,@username,@id);\r\nSELECT CAST(SCOPE_IDENTITY() AS int)\r\nEND";
				DataTable dataTable = clockWork.ExecuteQuery(text, new DbParameter[]
				{
					clockWork.GetParameter("@id", DbType.String, this.id),
					clockWork.GetParameter("@username", DbType.String, this.username),
					clockWork.GetParameter("@email", DbType.String, this.email),
					clockWork.GetParameter("@name", DbType.String, this.name),
					clockWork.GetParameter("@phone", DbType.String, this.phone)
				});
				bool flag5 = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
				if (flag5)
				{
					this.instructorId = (int)dataTable.Rows[0][0];
					result = this.instructorId;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00021C40 File Offset: 0x0001FE40
		public static void LookupInstructorId(Instructor instructor, bool createInstructorIfItDoesntExist)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string text;
			if (createInstructorIfItDoesntExist)
			{
				text = "IF EXISTS(SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=1 AND altlookupstring=@desc)\r\nBEGIN\r\n    SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=1 AND altlookupstring=@desc\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone,username,id) VALUES (1,@name,@name,@email,@phone,@username,@id);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid\r\nEND";
			}
			else
			{
				text = "SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=1 AND altlookupstring=@desc";
			}
			bool flag = !string.IsNullOrEmpty(instructor.Id);
			if (flag)
			{
				text = text.Replace("altlookupstring=@desc", "id=@id");
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(instructor.Username);
				if (flag2)
				{
					text = text.Replace("altlookupstring=@desc", "username=@username");
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(instructor.Email);
					if (flag3)
					{
						text = text.Replace("altlookupstring=@desc", "email=@email");
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(instructor.Name);
						if (!flag4)
						{
							return;
						}
						text = text.Replace("altlookupstring=@desc", "altlookupstring=@desc");
					}
				}
			}
			DataTable dataTable = clockWork.ExecuteQuery(text, new DbParameter[]
			{
				clockWork.GetParameter("@name", DbType.String, instructor.Name.Trim()),
				clockWork.GetParameter("@email", DbType.String, instructor.Email.Trim()),
				clockWork.GetParameter("@phone", DbType.String, instructor.Phone.Trim()),
				clockWork.GetParameter("@username", DbType.String, instructor.Username.Trim()),
				clockWork.GetParameter("@id", DbType.String, instructor.Id.Trim())
			});
			bool flag5 = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			if (flag5)
			{
				instructor.InstructorId = (int)dataTable.Rows[0][0];
			}
		}

		// Token: 0x04000287 RID: 647
		private int instructorId;

		// Token: 0x04000288 RID: 648
		private string name;

		// Token: 0x04000289 RID: 649
		private string phone;

		// Token: 0x0400028A RID: 650
		private string email;

		// Token: 0x0400028B RID: 651
		private string username;

		// Token: 0x0400028C RID: 652
		private string id;

		// Token: 0x0400028D RID: 653
		private string firstName;

		// Token: 0x0400028E RID: 654
		private string lastName;
	}
}
