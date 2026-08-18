using System;

namespace TechnoPro.Common.UI.Web.ClockWork.Entity
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class AppointmentView
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000210C File Offset: 0x0000030C
		public AppointmentView()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00000348
		public AppointmentView(string id, string tutorId, string subject, DateTime start, DateTime end, int rid)
		{
			this.rid = rid;
			this.tutorId = tutorId;
			this.id = id;
			this.subject = subject;
			this.start = start;
			this.end = end;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000221C File Offset: 0x0000041C
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002228 File Offset: 0x00000428
		public int Rid
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002258 File Offset: 0x00000458
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002264 File Offset: 0x00000464
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000227C File Offset: 0x0000047C
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002288 File Offset: 0x00000488
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022A0 File Offset: 0x000004A0
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022C4 File Offset: 0x000004C4
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022D0 File Offset: 0x000004D0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000022E8 File Offset: 0x000004E8
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000230C File Offset: 0x0000050C
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002318 File Offset: 0x00000518
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002330 File Offset: 0x00000530
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000233C File Offset: 0x0000053C
		public string TutorId
		{
			get
			{
				return this.tutorId;
			}
		}

		// Token: 0x04000005 RID: 5
		private int colour;

		// Token: 0x04000006 RID: 6
		private string id = "";

		// Token: 0x04000007 RID: 7
		private string tutorId = "";

		// Token: 0x04000008 RID: 8
		private string subject;

		// Token: 0x04000009 RID: 9
		private DateTime start;

		// Token: 0x0400000A RID: 10
		private DateTime end;

		// Token: 0x0400000B RID: 11
		private string name = "";

		// Token: 0x0400000C RID: 12
		private int appTypeId;

		// Token: 0x0400000D RID: 13
		private bool booked;

		// Token: 0x0400000E RID: 14
		private bool isBookedByLoggedInUser = false;

		// Token: 0x0400000F RID: 15
		private int actualAppointmentId = 0;

		// Token: 0x04000010 RID: 16
		private int rid;
	}
}
