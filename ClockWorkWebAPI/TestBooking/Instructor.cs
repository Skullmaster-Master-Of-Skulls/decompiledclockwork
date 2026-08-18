using System;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class Instructor
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00017DEC File Offset: 0x00015FEC
		public Instructor(string InstructorName, string InstructorEmail)
		{
			this.instructorName = InstructorName;
			this.instructorEmail = InstructorEmail;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00017E04 File Offset: 0x00016004
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00017E1C File Offset: 0x0001601C
		public string InstructorName
		{
			get
			{
				return this.instructorName;
			}
			set
			{
				this.instructorName = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00017E28 File Offset: 0x00016028
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00017E40 File Offset: 0x00016040
		public string InstructorEmail
		{
			get
			{
				return this.instructorEmail;
			}
			set
			{
				this.instructorEmail = value;
			}
		}

		// Token: 0x04000194 RID: 404
		private string instructorName;

		// Token: 0x04000195 RID: 405
		private string instructorEmail;
	}
}
