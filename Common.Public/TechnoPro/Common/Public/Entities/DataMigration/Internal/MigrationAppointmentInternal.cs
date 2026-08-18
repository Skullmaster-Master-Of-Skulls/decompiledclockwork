using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DataMigration.Internal
{
	// Token: 0x02000416 RID: 1046
	public class MigrationAppointmentInternal : MigrationAppointment
	{
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x00024506 File Offset: 0x00022706
		// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x0002450E File Offset: 0x0002270E
		public PersonBase ClockWorkStaff { get; set; }

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00024517 File Offset: 0x00022717
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x0002451F File Offset: 0x0002271F
		public PersonBase ClockWorkStudent { get; set; }

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00024528 File Offset: 0x00022728
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x00024530 File Offset: 0x00022730
		public AppType AppType { get; set; }
	}
}
