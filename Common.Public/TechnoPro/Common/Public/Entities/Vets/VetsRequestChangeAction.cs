using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x0200010A RID: 266
	public class VetsRequestChangeAction
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000F185 File Offset: 0x0000D385
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000F18D File Offset: 0x0000D38D
		public eVetsRequestChangeActionType ActionType { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000F196 File Offset: 0x0000D396
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000F19E File Offset: 0x0000D39E
		public DateTime DateOfChange { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000F1A7 File Offset: 0x0000D3A7
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000F1AF File Offset: 0x0000D3AF
		public PersonBase WhoChanged { get; set; }
	}
}
