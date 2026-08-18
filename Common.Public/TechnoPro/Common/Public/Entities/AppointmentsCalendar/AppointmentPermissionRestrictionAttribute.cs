using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x0200055E RID: 1374
	public class AppointmentPermissionRestrictionAttribute : Attribute
	{
		// Token: 0x06002C2D RID: 11309 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public AppointmentPermissionRestrictionAttribute()
		{
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x00031451 File Offset: 0x0002F651
		public AppointmentPermissionRestrictionAttribute(eAppointmentPermissionRestrictionResult result, string title)
		{
			this.Result = result;
			this.Title = title;
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x0003146B File Offset: 0x0002F66B
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x00031473 File Offset: 0x0002F673
		public eAppointmentPermissionRestrictionResult Result { get; set; }

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x0003147C File Offset: 0x0002F67C
		// (set) Token: 0x06002C32 RID: 11314 RVA: 0x00031484 File Offset: 0x0002F684
		public string Title { get; set; }
	}
}
