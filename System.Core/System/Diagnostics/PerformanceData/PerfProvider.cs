using System;
using System.ComponentModel;
using System.Security;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics.PerformanceData
{
	// Token: 0x020002A5 RID: 677
	internal sealed class PerfProvider
	{
		// Token: 0x06001883 RID: 6275 RVA: 0x00059670 File Offset: 0x00057870
		[SecurityCritical]
		internal PerfProvider(Guid providerGuid)
		{
			this.m_providerGuid = providerGuid;
			uint num = UnsafeNativeMethods.PerfStartProvider(ref this.m_providerGuid, null, out this.m_hProvider);
			if (num != 0U)
			{
				throw new Win32Exception((int)num);
			}
		}

		// Token: 0x04000BED RID: 3053
		internal Guid m_providerGuid;

		// Token: 0x04000BEE RID: 3054
		internal int m_counterSet;

		// Token: 0x04000BEF RID: 3055
		[SecurityCritical]
		internal SafePerfProviderHandle m_hProvider;
	}
}
