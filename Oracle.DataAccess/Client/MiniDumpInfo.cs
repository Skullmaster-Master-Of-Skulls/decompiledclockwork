using System;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200003A RID: 58
	internal class MiniDumpInfo
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0001D448 File Offset: 0x0001C448
		internal MiniDumpInfo()
		{
			this.evt = new ManualResetEvent(false);
		}

		// Token: 0x040001F3 RID: 499
		internal int threadId;

		// Token: 0x040001F4 RID: 500
		internal IntPtr pExPtrs;

		// Token: 0x040001F5 RID: 501
		internal ManualResetEvent evt;
	}
}
