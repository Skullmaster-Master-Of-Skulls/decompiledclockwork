using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsReminder
{
	// Token: 0x0200054D RID: 1357
	[Serializable]
	public class AppointmentReminder : BusinessBase<int>
	{
		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x00030E2C File Offset: 0x0002F02C
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x0000E258 File Offset: 0x0000C458
		public int AppointmentReminderID
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x00030E44 File Offset: 0x0002F044
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x00030E4C File Offset: 0x0002F04C
		public int AppointmentID { get; set; }

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x00030E55 File Offset: 0x0002F055
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x00030E5D File Offset: 0x0002F05D
		public int AttendeePersonID { get; set; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x00030E66 File Offset: 0x0002F066
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x00030E6E File Offset: 0x0002F06E
		public DateTime StartDate { get; set; }

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x00030E77 File Offset: 0x0002F077
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x00030E7F File Offset: 0x0002F07F
		public DateTime EndDate { get; set; }

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x00030E88 File Offset: 0x0002F088
		// (set) Token: 0x06002BCF RID: 11215 RVA: 0x00030E90 File Offset: 0x0002F090
		public string Subject { get; set; }

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x00030E99 File Offset: 0x0002F099
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x00030EA1 File Offset: 0x0002F0A1
		public DateTime NotificationDatetime { get; set; }

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x00030EAA File Offset: 0x0002F0AA
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x00030EB2 File Offset: 0x0002F0B2
		public bool AlreadyNotified { get; set; }

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x00030EBB File Offset: 0x0002F0BB
		// (set) Token: 0x06002BD5 RID: 11221 RVA: 0x00030EC3 File Offset: 0x0002F0C3
		public bool WasDeleted { get; set; }
	}
}
