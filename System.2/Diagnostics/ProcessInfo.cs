using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004F0 RID: 1264
	internal class ProcessInfo
	{
		// Token: 0x04002868 RID: 10344
		public ArrayList threadInfoList = new ArrayList();

		// Token: 0x04002869 RID: 10345
		public int basePriority;

		// Token: 0x0400286A RID: 10346
		public string processName;

		// Token: 0x0400286B RID: 10347
		public int processId;

		// Token: 0x0400286C RID: 10348
		public int handleCount;

		// Token: 0x0400286D RID: 10349
		public long poolPagedBytes;

		// Token: 0x0400286E RID: 10350
		public long poolNonpagedBytes;

		// Token: 0x0400286F RID: 10351
		public long virtualBytes;

		// Token: 0x04002870 RID: 10352
		public long virtualBytesPeak;

		// Token: 0x04002871 RID: 10353
		public long workingSetPeak;

		// Token: 0x04002872 RID: 10354
		public long workingSet;

		// Token: 0x04002873 RID: 10355
		public long pageFileBytesPeak;

		// Token: 0x04002874 RID: 10356
		public long pageFileBytes;

		// Token: 0x04002875 RID: 10357
		public long privateBytes;

		// Token: 0x04002876 RID: 10358
		public int mainModuleId;

		// Token: 0x04002877 RID: 10359
		public int sessionId;
	}
}
