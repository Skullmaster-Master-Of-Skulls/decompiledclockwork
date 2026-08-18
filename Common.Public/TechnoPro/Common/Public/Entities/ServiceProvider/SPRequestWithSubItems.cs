using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001F2 RID: 498
	public class SPRequestWithSubItems
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00016784 File Offset: 0x00014984
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0001678C File Offset: 0x0001498C
		public SPRequest Request { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00016795 File Offset: 0x00014995
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0001679D File Offset: 0x0001499D
		public IList<SPRequestCourse> Courses { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000167A6 File Offset: 0x000149A6
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x000167AE File Offset: 0x000149AE
		public IList<SPRequestEvent> Events { get; set; }
	}
}
