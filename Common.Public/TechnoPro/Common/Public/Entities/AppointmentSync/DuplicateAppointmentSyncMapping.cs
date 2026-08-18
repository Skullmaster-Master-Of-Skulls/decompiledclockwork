using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004CE RID: 1230
	public class DuplicateAppointmentSyncMapping
	{
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x00028063 File Offset: 0x00026263
		// (set) Token: 0x0600252B RID: 9515 RVA: 0x0002806B File Offset: 0x0002626B
		public IList<ClockWorkSyncAppointment> ClockWorkAppointments { get; set; }

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x00028074 File Offset: 0x00026274
		// (set) Token: 0x0600252D RID: 9517 RVA: 0x0002807C File Offset: 0x0002627C
		public IList<ExternalAppointment> ExternalAppointments { get; set; }
	}
}
