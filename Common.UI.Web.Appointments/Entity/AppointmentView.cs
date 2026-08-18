using System;

namespace TechnoPro.Common.UI.Web.Appointments.Entity
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class AppointmentView
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		public AppointmentView()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A8 File Offset: 0x000002A8
		public AppointmentView(string id, string tutorId, string subject, DateTime start, DateTime end, int rid)
		{
			this.rid = rid;
			this.tutorId = tutorId;
			this.id = id;
			this.subject = subject;
			this.start = start;
			this.end = end;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002109 File Offset: 0x00000309
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002111 File Offset: 0x00000311
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000211A File Offset: 0x0000031A
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002122 File Offset: 0x00000322
		public int ActualAppointmentId
		{
			get
			{
				return this.actualAppointmentId;
			}
			set
			{
				this.actualAppointmentId = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002133 File Offset: 0x00000333
		public bool IsBookedByLoggedInUser
		{
			get
			{
				return this.isBookedByLoggedInUser;
			}
			set
			{
				this.isBookedByLoggedInUser = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000213C File Offset: 0x0000033C
		public int Rid
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000214C File Offset: 0x0000034C
		public bool Booked
		{
			get
			{
				return this.booked;
			}
			set
			{
				this.booked = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002155 File Offset: 0x00000355
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000215D File Offset: 0x0000035D
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002166 File Offset: 0x00000366
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000216E File Offset: 0x0000036E
		public int Colour
		{
			get
			{
				return this.colour;
			}
			set
			{
				this.colour = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002177 File Offset: 0x00000377
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000217F File Offset: 0x0000037F
		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002188 File Offset: 0x00000388
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002190 File Offset: 0x00000390
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002199 File Offset: 0x00000399
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000021A1 File Offset: 0x000003A1
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021AA File Offset: 0x000003AA
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021B2 File Offset: 0x000003B2
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021BB File Offset: 0x000003BB
		public string TutorId
		{
			get
			{
				return this.tutorId;
			}
		}

		// Token: 0x04000003 RID: 3
		private int colour;

		// Token: 0x04000004 RID: 4
		private string id = "";

		// Token: 0x04000005 RID: 5
		private string tutorId = "";

		// Token: 0x04000006 RID: 6
		private string subject;

		// Token: 0x04000007 RID: 7
		private DateTime start;

		// Token: 0x04000008 RID: 8
		private DateTime end;

		// Token: 0x04000009 RID: 9
		private string name = "";

		// Token: 0x0400000A RID: 10
		private int appTypeId;

		// Token: 0x0400000B RID: 11
		private bool booked;

		// Token: 0x0400000C RID: 12
		private bool isBookedByLoggedInUser;

		// Token: 0x0400000D RID: 13
		private int actualAppointmentId;

		// Token: 0x0400000E RID: 14
		private int rid;
	}
}
