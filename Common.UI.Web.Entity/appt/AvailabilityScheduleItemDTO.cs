using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;

namespace TechnoPro.Common.UI.Web.Entity.appt
{
	// Token: 0x02000045 RID: 69
	[Obsolete("This is for legacy purposes only and will be phased out in the future.")]
	public class AvailabilityScheduleItemDTO
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00003EFC File Offset: 0x000020FC
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00003F04 File Offset: 0x00002104
		public int AvailabilityScheduleId { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00003F0D File Offset: 0x0000210D
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00003F15 File Offset: 0x00002115
		public virtual DateTime StartDateTime { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00003F1E File Offset: 0x0000211E
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00003F26 File Offset: 0x00002126
		public DateTime EndDateTime { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003F2F File Offset: 0x0000212F
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00003F37 File Offset: 0x00002137
		public int SubCode { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003F40 File Offset: 0x00002140
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00003F48 File Offset: 0x00002148
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003F51 File Offset: 0x00002151
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00003F59 File Offset: 0x00002159
		public AvailabilityGroupDTO AvailabilityGroup { get; set; }
	}
}
