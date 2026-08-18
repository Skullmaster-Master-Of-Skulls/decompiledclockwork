using System;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.UI.Web.Entity.appt
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class AvailabilitySelectedArgs : EventArgs
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000275E File Offset: 0x0000095E
		public AvailabilitySelectedArgs()
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00003E73 File Offset: 0x00002073
		public AvailabilitySelectedArgs(DateTime startDateTime, DateTime endDateTime, int userPidToBookWith, string userToBookWithName, ChannelAvailability channelAvailability)
		{
			this.StartDateTime = startDateTime;
			this.EndDateTime = endDateTime;
			this.UserPidToBookWith = userPidToBookWith;
			this.ChannelAvailability = channelAvailability;
			this.UserToBookWithName = userToBookWithName;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00003EA7 File Offset: 0x000020A7
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00003EAF File Offset: 0x000020AF
		public DateTime StartDateTime { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00003EB8 File Offset: 0x000020B8
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00003EC0 File Offset: 0x000020C0
		public DateTime EndDateTime { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00003EC9 File Offset: 0x000020C9
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00003ED1 File Offset: 0x000020D1
		public int UserPidToBookWith { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00003EDA File Offset: 0x000020DA
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00003EE2 File Offset: 0x000020E2
		public string UserToBookWithName { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00003EEB File Offset: 0x000020EB
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00003EF3 File Offset: 0x000020F3
		public ChannelAvailability ChannelAvailability { get; set; }
	}
}
