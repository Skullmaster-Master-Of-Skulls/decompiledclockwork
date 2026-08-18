using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.FastSync
{
	// Token: 0x020004EB RID: 1259
	public class ExternalSyncAppointmentChangesRequest
	{
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00028AD6 File Offset: 0x00026CD6
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x00028ADE File Offset: 0x00026CDE
		public string Username { get; set; }

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x00028AE7 File Offset: 0x00026CE7
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x00028AEF File Offset: 0x00026CEF
		public string SyncState { get; set; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x00028AF8 File Offset: 0x00026CF8
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x00028B00 File Offset: 0x00026D00
		public DateTime? LastSyncDateTime { get; set; }
	}
}
