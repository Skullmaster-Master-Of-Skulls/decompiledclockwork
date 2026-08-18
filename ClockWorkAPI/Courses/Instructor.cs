using System;
using System.Data;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI.Courses
{
	// Token: 0x02000027 RID: 39
	public class Instructor
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C398 File Offset: 0x0000B398
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C3BF File Offset: 0x0000B3BF
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C3CC File Offset: 0x0000B3CC
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C3F3 File Offset: 0x0000B3F3
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000C400 File Offset: 0x0000B400
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000C418 File Offset: 0x0000B418
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000C424 File Offset: 0x0000B424
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000C450 File Offset: 0x0000B450
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000C45C File Offset: 0x0000B45C
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000C488 File Offset: 0x0000B488
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000C494 File Offset: 0x0000B494
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000C4C0 File Offset: 0x0000B4C0
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000C4CC File Offset: 0x0000B4CC
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000C4F8 File Offset: 0x0000B4F8
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000C504 File Offset: 0x0000B504
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000C530 File Offset: 0x0000B530
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

		// Token: 0x06000220 RID: 544 RVA: 0x0000C53C File Offset: 0x0000B53C
		public Instructor()
		{
			this.instructorId = 0;
			this.name = "";
			this.phone = "";
			this.email = "";
			this.username = "";
			this.id = "";
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C590 File Offset: 0x0000B590
		public int LookupInstructorId()
		{
			int result;
			if (this.instructorId > 0)
			{
				result = this.instructorId;
			}
			else
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string text;
				if (!string.IsNullOrEmpty(this.id))
				{
					text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND id=@id)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND id=@id;";
				}
				else if (!string.IsNullOrEmpty(this.username))
				{
					text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND username=@username)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND username=@username;";
				}
				else if (!string.IsNullOrEmpty(this.email))
				{
					text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND email=@email)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND email=@email;";
				}
				else
				{
					text = "IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND altlookupstring=@name)\r\nSELECT TOP 1 lucoursedataid AS instructorid FROM lucoursedata WHERE looklisttype=1 AND altlookupstring=@name;";
				}
				text += "\r\nELSE \r\nBEGIN\r\nINSERT INTO lucoursedata (lookupstring,altlookupstring,phone,email,username,id) VALUES (@name,@name,@phone,@email,@username,@id);\r\nSELECT CAST(SCOPE_IDENTITY() AS int)\r\nEND";
				da.SelectCommand.CommandText = text;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@id", this.id);
				da.SelectCommand.Parameters.Add("@username", this.username);
				da.SelectCommand.Parameters.Add("@email", this.email);
				da.SelectCommand.Parameters.Add("@name", this.name);
				da.SelectCommand.Parameters.Add("@phone", this.phone);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
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

		// Token: 0x06000222 RID: 546 RVA: 0x0000C740 File Offset: 0x0000B740
		public static void LookupInstructorId(Instructor instructor, bool createInstructorIfItDoesntExist)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			string text;
			if (createInstructorIfItDoesntExist)
			{
				text = "IF EXISTS(SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=1 AND altlookupstring=@desc)\r\nBEGIN\r\n    SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=1 AND altlookupstring=@desc\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone,username,id) VALUES (1,@name,@name,@email,@phone,@username,@id);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid\r\nEND";
			}
			else
			{
				text = "SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=1 AND altlookupstring=@desc";
			}
			if (!string.IsNullOrEmpty(instructor.Id))
			{
				text = text.Replace("altlookupstring=@desc", "id=@id");
			}
			else if (!string.IsNullOrEmpty(instructor.Username))
			{
				text = text.Replace("altlookupstring=@desc", "username=@username");
			}
			else if (!string.IsNullOrEmpty(instructor.Email))
			{
				text = text.Replace("altlookupstring=@desc", "email=@email");
			}
			else
			{
				if (string.IsNullOrEmpty(instructor.Name))
				{
					return;
				}
				text = text.Replace("altlookupstring=@desc", "altlookupstring=@desc");
			}
			da.SelectCommand.CommandText = text;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@name", instructor.Name.Trim());
			da.SelectCommand.Parameters.Add("@email", instructor.Email.Trim());
			da.SelectCommand.Parameters.Add("@phone", instructor.Phone.Trim());
			da.SelectCommand.Parameters.Add("@username", instructor.Username.Trim());
			da.SelectCommand.Parameters.Add("@id", instructor.Id.Trim());
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
			{
				instructor.InstructorId = (int)dataTable.Rows[0][0];
			}
		}

		// Token: 0x04000100 RID: 256
		private int instructorId;

		// Token: 0x04000101 RID: 257
		private string name;

		// Token: 0x04000102 RID: 258
		private string phone;

		// Token: 0x04000103 RID: 259
		private string email;

		// Token: 0x04000104 RID: 260
		private string username;

		// Token: 0x04000105 RID: 261
		private string id;

		// Token: 0x04000106 RID: 262
		private string firstName;

		// Token: 0x04000107 RID: 263
		private string lastName;
	}
}
