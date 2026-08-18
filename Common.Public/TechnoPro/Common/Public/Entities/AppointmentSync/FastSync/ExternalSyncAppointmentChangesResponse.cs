using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.FastSync
{
	// Token: 0x020004EC RID: 1260
	public class ExternalSyncAppointmentChangesResponse
	{
		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x00028B09 File Offset: 0x00026D09
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x00028B11 File Offset: 0x00026D11
		public IList<ExternalSyncAppointmentChange> AppointmentChanges { get; set; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x00028B1A File Offset: 0x00026D1A
		// (set) Token: 0x06002624 RID: 9764 RVA: 0x00028B22 File Offset: 0x00026D22
		public string SyncState { get; set; }
	}
}
