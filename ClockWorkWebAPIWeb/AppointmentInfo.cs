using System;
using TechnoPro.Common.Public.Entities.Adapters;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class AppointmentInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		public int Rid
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020D4 File Offset: 0x000002D4
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020EC File Offset: 0x000002EC
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002110 File Offset: 0x00000310
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
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public AppointmentInfo(string id, string tutorId, string subject, DateTime start, DateTime end, int rid)
		{
			this.rid = rid;
			this.tutorId = tutorId;
			this.id = id;
			this.subject = subject;
			this.start = start;
			this.end = end;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021F9 File Offset: 0x000003F9
		public string Subject
		{
			get
			{
				return this.subject ?? "";
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002204 File Offset: 0x00000404
		public string DisplayString
		{
			get
			{
				return this.Subject + " " + this.Start.ToString("h:mm tt");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000223C File Offset: 0x0000043C
		public string SubjectTime
		{
			get
			{
				return string.Format("{0}  {1}", this.start.ToString("h:mm tt"), Convert.ToInt32((this.end - this.start).TotalMinutes).GetDurationDescriptionShort());
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000022A4 File Offset: 0x000004A4
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
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022B0 File Offset: 0x000004B0
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022C8 File Offset: 0x000004C8
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
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022D4 File Offset: 0x000004D4
		public string TutorId
		{
			get
			{
				return this.tutorId;
			}
		}

		// Token: 0x04000001 RID: 1
		private int colour;

		// Token: 0x04000002 RID: 2
		private string id = "";

		// Token: 0x04000003 RID: 3
		private string tutorId = "";

		// Token: 0x04000004 RID: 4
		private string subject;

		// Token: 0x04000005 RID: 5
		private DateTime start;

		// Token: 0x04000006 RID: 6
		private DateTime end;

		// Token: 0x04000007 RID: 7
		private string name = "";

		// Token: 0x04000008 RID: 8
		private int appTypeId;

		// Token: 0x04000009 RID: 9
		private bool booked;

		// Token: 0x0400000A RID: 10
		private bool isBookedByLoggedInUser = false;

		// Token: 0x0400000B RID: 11
		private int actualAppointmentId = 0;

		// Token: 0x0400000C RID: 12
		private int rid;
	}
}
