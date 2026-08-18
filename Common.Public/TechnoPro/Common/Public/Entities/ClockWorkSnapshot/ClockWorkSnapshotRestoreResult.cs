using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ClockWorkSnapshot
{
	// Token: 0x02000447 RID: 1095
	public class ClockWorkSnapshotRestoreResult
	{
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x000254B9 File Offset: 0x000236B9
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x000254C1 File Offset: 0x000236C1
		public IList<ClockWorkSnapshotTableRestoreResult> TableResults { get; set; }

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x000254CA File Offset: 0x000236CA
		// (set) Token: 0x06002134 RID: 8500 RVA: 0x000254D2 File Offset: 0x000236D2
		public string ErrorMessage { get; set; }
	}
}
