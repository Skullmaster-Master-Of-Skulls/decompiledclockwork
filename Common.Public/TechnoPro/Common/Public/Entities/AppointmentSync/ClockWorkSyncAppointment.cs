using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004DC RID: 1244
	public class ClockWorkSyncAppointment : BusinessBase<int>
	{
		// Token: 0x06002572 RID: 9586 RVA: 0x00028284 File Offset: 0x00026484
		public ClockWorkSyncAppointment()
		{
			this.AppointmentType = new ClockWorkSyncAppType();
			this.Subtitle = "";
			this.Memo = "";
			this.Attendees = new List<ClockWorkSyncAttendee>();
			this.Location = "";
			this.Mapping = new ClockWorkExternalAppMapping
			{
				ClockWorkAppointmentId = 0,
				ExternalApplicationUniqueAppointmentId = "",
				ExternalApplicationUniqueAppointmentId2 = "",
				ExternalApplicationGlobalAppointmentId = "",
				ClockWorkLastUpdatedDate = null,
				ExternalApplicationLastUpdatedDate = null
			};
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x0002832D File Offset: 0x0002652D
		// (set) Token: 0x06002574 RID: 9588 RVA: 0x00028335 File Offset: 0x00026535
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x0002833E File Offset: 0x0002653E
		// (set) Token: 0x06002576 RID: 9590 RVA: 0x00028346 File Offset: 0x00026546
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x0002834F File Offset: 0x0002654F
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x00028357 File Offset: 0x00026557
		public ClockWorkSyncAppType AppointmentType { get; set; }

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x00028360 File Offset: 0x00026560
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x00028368 File Offset: 0x00026568
		public string Location { get; set; }

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x00028371 File Offset: 0x00026571
		// (set) Token: 0x0600257C RID: 9596 RVA: 0x00028379 File Offset: 0x00026579
		public string Subtitle { get; set; }

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x00028382 File Offset: 0x00026582
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x0002838A File Offset: 0x0002658A
		public bool IsCancelled { get; set; }

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00028393 File Offset: 0x00026593
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x0002839B File Offset: 0x0002659B
		public bool IsPrivate { get; set; }

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x000283A4 File Offset: 0x000265A4
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x000283AC File Offset: 0x000265AC
		public string Memo { get; set; }

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x000283B5 File Offset: 0x000265B5
		// (set) Token: 0x06002584 RID: 9604 RVA: 0x000283BD File Offset: 0x000265BD
		public DateTime LastModifiedTime { get; set; }

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x000283C6 File Offset: 0x000265C6
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x000283CE File Offset: 0x000265CE
		public ClockWorkExternalAppMapping Mapping { get; set; }

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x000283D7 File Offset: 0x000265D7
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x000283DF File Offset: 0x000265DF
		public List<ClockWorkSyncAttendee> Attendees { get; set; }

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x000283E8 File Offset: 0x000265E8
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
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

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x00028400 File Offset: 0x00026600
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x00028408 File Offset: 0x00026608
		public bool IsAllDayEvent { get; set; }
	}
}
