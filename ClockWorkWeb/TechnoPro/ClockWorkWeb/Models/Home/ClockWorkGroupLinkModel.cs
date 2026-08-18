using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkWeb.Controllers;

namespace TechnoPro.ClockWorkWeb.Models.Home
{
	// Token: 0x0200010F RID: 271
	public class ClockWorkGroupLinkModel
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0003A691 File Offset: 0x00038891
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0003A699 File Offset: 0x00038899
		public string GroupName { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0003A6A2 File Offset: 0x000388A2
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0003A6AA File Offset: 0x000388AA
		public IList<ClockWorkLinkDisplayInfo> Links { get; set; }
	}
}
