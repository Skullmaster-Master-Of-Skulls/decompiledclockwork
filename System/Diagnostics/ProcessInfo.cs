using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000778 RID: 1912
	internal class ProcessInfo
	{
		// Token: 0x040033C7 RID: 13255
		public ArrayList threadInfoList = new ArrayList();

		// Token: 0x040033C8 RID: 13256
		public int basePriority;

		// Token: 0x040033C9 RID: 13257
		public string processName;

		// Token: 0x040033CA RID: 13258
		public int processId;

		// Token: 0x040033CB RID: 13259
		public int handleCount;

		// Token: 0x040033CC RID: 13260
		public long poolPagedBytes;

		// Token: 0x040033CD RID: 13261
		public long poolNonpagedBytes;

		// Token: 0x040033CE RID: 13262
		public long virtualBytes;

		// Token: 0x040033CF RID: 13263
		public long virtualBytesPeak;

		// Token: 0x040033D0 RID: 13264
		public long workingSetPeak;

		// Token: 0x040033D1 RID: 13265
		public long workingSet;

		// Token: 0x040033D2 RID: 13266
		public long pageFileBytesPeak;

		// Token: 0x040033D3 RID: 13267
		public long pageFileBytes;

		// Token: 0x040033D4 RID: 13268
		public long privateBytes;

		// Token: 0x040033D5 RID: 13269
		public int mainModuleId;

		// Token: 0x040033D6 RID: 13270
		public int sessionId;
	}
}
