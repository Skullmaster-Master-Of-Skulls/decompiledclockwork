using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000541 RID: 1345
	public class FindPotentialBookingsResp
	{
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x0002E254 File Offset: 0x0002C454
		// (set) Token: 0x06002B07 RID: 11015 RVA: 0x0002E25C File Offset: 0x0002C45C
		public string EmailBody { get; set; }

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x0002E265 File Offset: 0x0002C465
		// (set) Token: 0x06002B09 RID: 11017 RVA: 0x0002E26D File Offset: 0x0002C46D
		public IList<int> IconIds { get; set; }

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0002E276 File Offset: 0x0002C476
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x0002E27E File Offset: 0x0002C47E
		public IList<PrivateNote> PrivateNotes { get; set; }

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0002E287 File Offset: 0x0002C487
		// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0002E28F File Offset: 0x0002C48F
		public BookingResults BookingResults { get; set; }

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0002E298 File Offset: 0x0002C498
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public IList<PotentialTest> PotentialTests { get; set; }

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0002E2A9 File Offset: 0x0002C4A9
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x0002E2B1 File Offset: 0x0002C4B1
		public IList<string> DebugNotes { get; set; }
	}
}
