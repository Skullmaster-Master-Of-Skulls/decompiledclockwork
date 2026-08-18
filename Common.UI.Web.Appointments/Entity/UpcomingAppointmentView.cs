using System;

namespace TechnoPro.Common.UI.Web.Appointments.Entity
{
	// Token: 0x02000004 RID: 4
	public class UpcomingAppointmentView
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021C3 File Offset: 0x000003C3
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021CB File Offset: 0x000003CB
		public string DisplayDateAndTime { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021DC File Offset: 0x000003DC
		public string DisplayLocation { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021E5 File Offset: 0x000003E5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021ED File Offset: 0x000003ED
		public string DisplayTitle { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021F6 File Offset: 0x000003F6
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000021FE File Offset: 0x000003FE
		public string DisplayWho { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002207 File Offset: 0x00000407
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000220F File Offset: 0x0000040F
		public string Action { get; set; }
	}
}
