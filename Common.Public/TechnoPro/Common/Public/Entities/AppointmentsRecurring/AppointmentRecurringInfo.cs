using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsRecurring
{
	// Token: 0x02000551 RID: 1361
	[Serializable]
	public class AppointmentRecurringInfo
	{
		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x00030F32 File Offset: 0x0002F132
		// (set) Token: 0x06002BE6 RID: 11238 RVA: 0x00030F3A File Offset: 0x0002F13A
		public List<RecurringAppointment> Appointments { get; set; }

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x00030F43 File Offset: 0x0002F143
		// (set) Token: 0x06002BE8 RID: 11240 RVA: 0x00030F4B File Offset: 0x0002F14B
		public int MasterGroupCode { get; set; }
	}
}
