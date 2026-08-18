using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000246 RID: 582
	public class DataSyncMoveDataIntoClockWorkParameters
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000184DE File Offset: 0x000166DE
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x000184E6 File Offset: 0x000166E6
		public IList<DataSyncMoveDataIntoClockWorkItem> Items { get; set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x000184EF File Offset: 0x000166EF
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x000184F7 File Offset: 0x000166F7
		public DataSyncMoveDataIntoClockWorkSourceFileInfo SourceFileInfo { get; set; }
	}
}
