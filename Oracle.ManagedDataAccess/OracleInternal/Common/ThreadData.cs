using System;
using System.Diagnostics;
using System.IO;

namespace OracleInternal.Common
{
	// Token: 0x020000C2 RID: 194
	internal class ThreadData : IDisposable
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00045AD4 File Offset: 0x00043CD4
		internal bool IsOutdated
		{
			get
			{
				return this.timeStampHash != ThreadData.TimeStampHash;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00045AE8 File Offset: 0x00043CE8
		internal static void RegenerateTimeStampHash()
		{
			ThreadData.TimeStampHash = DateTime.Now.GetHashCode();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00045B10 File Offset: 0x00043D10
		public void Dispose()
		{
			if (this.textListener != null)
			{
				this.textListener.Dispose();
			}
			this.textListener = null;
			this.timeStampHash = 0;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00045B34 File Offset: 0x00043D34
		~ThreadData()
		{
			try
			{
				this.Dispose();
			}
			catch
			{
			}
		}

		// Token: 0x04000A22 RID: 2594
		internal Stream traceFile;

		// Token: 0x04000A23 RID: 2595
		internal TextWriterTraceListener textListener;

		// Token: 0x04000A24 RID: 2596
		internal int timeStampHash = ThreadData.TimeStampHash;

		// Token: 0x04000A25 RID: 2597
		internal static int TimeStampHash = DateTime.Now.GetHashCode();
	}
}
