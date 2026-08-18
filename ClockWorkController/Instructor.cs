using System;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;

namespace ClockWorkController
{
	// Token: 0x0200000A RID: 10
	public class Instructor
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000040E4 File Offset: 0x000022E4
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000040FC File Offset: 0x000022FC
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00004108 File Offset: 0x00002308
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000412F File Offset: 0x0000232F
		public string InstructorName
		{
			get
			{
				return (this.instructorName == null) ? "" : this.instructorName;
			}
			set
			{
				this.instructorName = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000413C File Offset: 0x0000233C
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00004163 File Offset: 0x00002363
		public string InstructorEmail
		{
			get
			{
				return (this.instructorEmail == null) ? "" : this.instructorEmail;
			}
			set
			{
				this.instructorEmail = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00004170 File Offset: 0x00002370
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00004197 File Offset: 0x00002397
		public string InstructorPhone
		{
			get
			{
				return (this.instructorPhone == null) ? "" : this.instructorPhone;
			}
			set
			{
				this.instructorPhone = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000041A4 File Offset: 0x000023A4
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000041CB File Offset: 0x000023CB
		public string InstructorUsername
		{
			get
			{
				return (this.instructorUsername == null) ? "" : this.instructorUsername;
			}
			set
			{
				this.instructorUsername = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000041D8 File Offset: 0x000023D8
		public string SubjectEmail
		{
			get
			{
				return this.subjectEmail;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000041F0 File Offset: 0x000023F0
		public Instructor()
		{
			this.Reset();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004204 File Offset: 0x00002404
		private void Reset()
		{
			this.instructorId = 0;
			this.instructorName = "";
			this.instructorEmail = "";
			this.instructorPhone = "";
			this.instructorUsername = "";
			this.subjectEmail = "";
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004250 File Offset: 0x00002450
		public Instructor(int luCourseId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, luCourseId)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseInfo, parameters);
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				bool flag2 = dataRow["instructorid"] != DBNull.Value;
				if (flag2)
				{
					this.instructorId = (int)dataRow["instructorid"];
					this.instructorName = dataRow["instructor"].ToString();
					this.instructorEmail = dataRow["instructoremail"].ToString();
					this.instructorPhone = dataRow["instructorphone"].ToString();
					this.instructorUsername = dataRow["username"].ToString();
					this.subjectEmail = dataRow["subjectemail"].ToString();
				}
				else
				{
					this.Reset();
				}
			}
			else
			{
				this.Reset();
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004378 File Offset: 0x00002578
		public static Instructor LoadInstructor(int instructorId)
		{
			Instructor instructor = new Instructor();
			instructor.InstructorId = instructorId;
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT lucd.altlookupstring,lucd.email,lucd.phone,lucd.username\r\nFROM lucoursedata lucd WHERE lucd.lucoursedataid=@id";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@id", DbType.Int32, instructorId)
			});
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				instructor.InstructorName = dataRow["altlookupstring"].ToString();
				instructor.InstructorPhone = dataRow["phone"].ToString();
				instructor.InstructorEmail = dataRow["email"].ToString();
				instructor.InstructorUsername = dataRow["username"].ToString();
			}
			else
			{
				instructor = null;
				instructor.InstructorName = "";
				instructor.InstructorPhone = "";
				instructor.InstructorEmail = "";
				instructor.InstructorUsername = "";
			}
			return instructor;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000448C File Offset: 0x0000268C
		public static Instructor LoadInstructor(string instructorUsername)
		{
			return Instructor.LoadInstructor(instructorUsername, "");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000044AC File Offset: 0x000026AC
		public static Instructor LoadInstructor(string instructorUsername, string iemail)
		{
			ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
			LookupInstructorDTO lookupInstructorDTO = null;
			bool flag = !string.IsNullOrEmpty(instructorUsername);
			if (flag)
			{
				lookupInstructorDTO = lookupInstructorClientManager.LoadInstructorByUsername(instructorUsername);
			}
			bool flag2 = lookupInstructorDTO == null && !string.IsNullOrEmpty(iemail);
			if (flag2)
			{
				lookupInstructorDTO = lookupInstructorClientManager.LoadInstructorByEmail(iemail);
			}
			bool flag3 = lookupInstructorDTO != null;
			Instructor result;
			if (flag3)
			{
				result = new Instructor
				{
					InstructorId = lookupInstructorDTO.InstructorId,
					InstructorName = lookupInstructorDTO.Name,
					InstructorPhone = lookupInstructorDTO.Phone,
					InstructorEmail = lookupInstructorDTO.Email,
					InstructorUsername = lookupInstructorDTO.Username
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004550 File Offset: 0x00002750
		public static CourseContactInformation LoadAlternateContact(string altContactUsername)
		{
			return Instructor.LoadAlternateContact(altContactUsername, "");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004570 File Offset: 0x00002770
		public static CourseContactInformation LoadAlternateContact(string altContactUsername, string altContactEmail)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT \r\nlucd.alternatecontactid,lucd.altname,lucd.altemail,lucd.altphone,lucd.altusername,altpermissionlevel\r\nFROM lucoursealternatecontact lucd WHERE ((NOT @username='' AND lucd.altusername=@username) OR (NOT @email='' AND lucd.altemail=@email)) AND lucd.isactive=1";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@username", DbType.String, altContactUsername),
				clockWork.GetParameter("@email", DbType.String, altContactEmail)
			});
			bool flag = dataTable.Rows.Count > 0;
			CourseContactInformation result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				string name = dataRow["altname"].ToString();
				string email = dataRow["altemail"].ToString();
				string phone = dataRow["altphone"].ToString();
				string username = dataRow["altusername"].ToString();
				int alternateContactId = (int)dataRow[0];
				CourseContactPermissionLevel permissionLevel = (CourseContactPermissionLevel)((dataRow["altpermissionlevel"] == DBNull.Value) ? 0 : ((int)dataRow["altpermissionlevel"]));
				result = new CourseContactInformation(name, email, phone, username, permissionLevel)
				{
					Lucid = 0,
					InstructorId = 0,
					AlternateContactId = alternateContactId
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000046A4 File Offset: 0x000028A4
		public static CourseContactInformation LoadAlternateContact(int altContactId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT \r\nlucd.alternatecontactid,lucd.altname,lucd.altemail,lucd.altphone,lucd.altusername,altpermissionlevel\r\nFROM lucoursealternatecontact lucd WHERE lucd.alternatecontactid=@altcontactid AND lucd.isactive=1";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@altcontactid", DbType.Int32, altContactId)
			});
			bool flag = dataTable.Rows.Count > 0;
			CourseContactInformation result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				string name = dataRow["altname"].ToString();
				string email = dataRow["altemail"].ToString();
				string phone = dataRow["altphone"].ToString();
				string username = dataRow["altusername"].ToString();
				int alternateContactId = (int)dataRow[0];
				CourseContactPermissionLevel permissionLevel = (CourseContactPermissionLevel)((dataRow["altpermissionlevel"] == DBNull.Value) ? 0 : ((int)dataRow["altpermissionlevel"]));
				result = new CourseContactInformation(name, email, phone, username, permissionLevel)
				{
					Lucid = 0,
					InstructorId = 0,
					AlternateContactId = alternateContactId
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400000D RID: 13
		private int instructorId;

		// Token: 0x0400000E RID: 14
		private string instructorName;

		// Token: 0x0400000F RID: 15
		private string instructorEmail;

		// Token: 0x04000010 RID: 16
		private string instructorPhone;

		// Token: 0x04000011 RID: 17
		private string instructorUsername;

		// Token: 0x04000012 RID: 18
		private string subjectEmail;
	}
}
