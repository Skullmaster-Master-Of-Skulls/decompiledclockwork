using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.General
{
	// Token: 0x02000330 RID: 816
	public class ModificationHistoryItem
	{
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0001DFA1 File Offset: 0x0001C1A1
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x0001DFA9 File Offset: 0x0001C1A9
		public DateTime? DateCreated { get; set; }

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0001DFB2 File Offset: 0x0001C1B2
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x0001DFBA File Offset: 0x0001C1BA
		public PersonBase WhoCreated { get; set; }

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0001DFC3 File Offset: 0x0001C1C3
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x0001DFCB File Offset: 0x0001C1CB
		public DateTime? DateLastModified { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0001DFD4 File Offset: 0x0001C1D4
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public PersonBase WhoLastModified { get; set; }
	}
}
