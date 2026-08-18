using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DataMigration.Internal
{
	// Token: 0x02000417 RID: 1047
	public class MigrationFileInternal
	{
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x00024539 File Offset: 0x00022739
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x00024541 File Offset: 0x00022741
		public string StudentNumber { get; set; }

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0002454A File Offset: 0x0002274A
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x00024552 File Offset: 0x00022752
		public int PersonId { get; set; }

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0002455B File Offset: 0x0002275B
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x00024563 File Offset: 0x00022763
		public IList<MigrationFileInfo> FilesForStudent { get; set; }
	}
}
