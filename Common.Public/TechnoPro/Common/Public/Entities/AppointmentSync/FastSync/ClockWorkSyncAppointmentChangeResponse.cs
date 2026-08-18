using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.FastSync
{
	// Token: 0x020004E8 RID: 1256
	public class ClockWorkSyncAppointmentChangeResponse
	{
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000289F7 File Offset: 0x00026BF7
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x000289FF File Offset: 0x00026BFF
		public IList<ClockWorkSyncAppointmentChange> ClockWorkAppointmentChanges { get; set; }

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00028A08 File Offset: 0x00026C08
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x00028A10 File Offset: 0x00026C10
		public DateTime ClockWorkSyncState { get; set; }
	}
}
