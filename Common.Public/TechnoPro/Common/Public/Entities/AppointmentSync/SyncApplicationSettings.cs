using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004E0 RID: 1248
	public class SyncApplicationSettings
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x000284D4 File Offset: 0x000266D4
		public SyncApplicationSettings()
		{
			this.SyncUsers = new List<ClockWorkExternalApplicationSyncUser>();
			this.DisabledSyncUsers = new List<ClockWorkExternalApplicationSyncUser>();
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000284F6 File Offset: 0x000266F6
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000284FE File Offset: 0x000266FE
		public bool SyncIsActive { get; set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00028507 File Offset: 0x00026707
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x0002850F File Offset: 0x0002670F
		public bool FastSyncIsActive { get; set; }

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00028518 File Offset: 0x00026718
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x00028520 File Offset: 0x00026720
		public SyncApplicationConnection SyncConnection { get; set; }

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x00028529 File Offset: 0x00026729
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x00028531 File Offset: 0x00026731
		public int SyncFrequencyInMinutes { get; set; }

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x0002853A File Offset: 0x0002673A
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x00028542 File Offset: 0x00026742
		public List<ClockWorkExternalApplicationSyncUser> SyncUsers { get; set; }

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x0002854B File Offset: 0x0002674B
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x00028553 File Offset: 0x00026753
		public int SyncIntervalInDays { get; set; }

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x0002855C File Offset: 0x0002675C
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x00028564 File Offset: 0x00026764
		public int SyncIntervalCount { get; set; }

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0002856D File Offset: 0x0002676D
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x00028575 File Offset: 0x00026775
		public bool ShowNonOutlookUsersInMemoWhenCreatingUpdatingOutlookAppointment { get; set; }

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0002857E File Offset: 0x0002677E
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x00028586 File Offset: 0x00026786
		public List<ClockWorkExternalApplicationSyncUser> DisabledSyncUsers { get; set; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x0002858F File Offset: 0x0002678F
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x00028597 File Offset: 0x00026797
		public bool SkipAllDayAppointments { get; set; }

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000285A0 File Offset: 0x000267A0
		// (set) Token: 0x060025B7 RID: 9655 RVA: 0x000285A8 File Offset: 0x000267A8
		public bool SkipPrivateAppointments { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000285B1 File Offset: 0x000267B1
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x000285B9 File Offset: 0x000267B9
		public int TimeToWaitBeforeStartNewFastSyncInMinutes { get; set; }

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000285C2 File Offset: 0x000267C2
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x000285CA File Offset: 0x000267CA
		public IList<TimeSpan> SlowSyncDayRunningTimeSchedule { get; set; }

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000285D3 File Offset: 0x000267D3
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x000285DB File Offset: 0x000267DB
		public bool SkipRecurringAppointmentsInFastSync { get; set; }
	}
}
