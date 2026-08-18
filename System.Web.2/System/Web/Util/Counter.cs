using System;

namespace System.Web.Util
{
	// Token: 0x020001F0 RID: 496
	internal sealed class Counter
	{
		// Token: 0x060018BB RID: 6331 RVA: 0x000030B5 File Offset: 0x000012B5
		private Counter()
		{
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0004CAF8 File Offset: 0x0004ACF8
		internal static long Value
		{
			get
			{
				long result = 0L;
				SafeNativeMethods.QueryPerformanceCounter(ref result);
				return result;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0004CB14 File Offset: 0x0004AD14
		internal static long Frequency
		{
			get
			{
				long result = 0L;
				SafeNativeMethods.QueryPerformanceFrequency(ref result);
				return result;
			}
		}
	}
}
