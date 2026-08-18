using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.FastSync
{
	// Token: 0x020004E6 RID: 1254
	public class ClockWorkSyncAppointmentChange : BusinessBase<int>
	{
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x00028968 File Offset: 0x00026B68
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ClockWorkAppointmentID
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

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x00028980 File Offset: 0x00026B80
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x00028988 File Offset: 0x00026B88
		public eAppointmentSyncChangeType AppointmentSyncChangeType { get; set; }

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x00028991 File Offset: 0x00026B91
		// (set) Token: 0x060025F9 RID: 9721 RVA: 0x00028999 File Offset: 0x00026B99
		public DateTime LastModifiedDate { get; set; }

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x000289A2 File Offset: 0x00026BA2
		// (set) Token: 0x060025FB RID: 9723 RVA: 0x000289AA File Offset: 0x00026BAA
		public ClockWorkExternalAppMapping Mapping { get; set; }

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000289B3 File Offset: 0x00026BB3
		// (set) Token: 0x060025FD RID: 9725 RVA: 0x000289BB File Offset: 0x00026BBB
		public bool IsAllDayEvent { get; set; }

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000289C4 File Offset: 0x00026BC4
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x000289CC File Offset: 0x00026BCC
		public bool IsPrivate { get; set; }
	}
}
