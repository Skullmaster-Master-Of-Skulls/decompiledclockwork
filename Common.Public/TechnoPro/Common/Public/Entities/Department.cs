using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000E3 RID: 227
	public class Department : BusinessBase<int>
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000E3A5 File Offset: 0x0000C5A5
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public string Name { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000E3B6 File Offset: 0x0000C5B6
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0000E3BE File Offset: 0x0000C5BE
		public string Description { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000E3C7 File Offset: 0x0000C5C7
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0000E3CF File Offset: 0x0000C5CF
		public string Institution { get; set; }
	}
}
