using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x02000479 RID: 1145
	public class AddAvailabilitiesActionResult
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000266F7 File Offset: 0x000248F7
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x000266FF File Offset: 0x000248FF
		public bool AbortedEntireProcess { get; set; }

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x00026708 File Offset: 0x00024908
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x00026710 File Offset: 0x00024910
		public IList<AddAvailabilityActionResult> Results { get; set; }
	}
}
