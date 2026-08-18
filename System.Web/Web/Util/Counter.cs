using System;

namespace System.Web.Util
{
	// Token: 0x0200075A RID: 1882
	internal sealed class Counter
	{
		// Token: 0x06005BAF RID: 23471 RVA: 0x00170595 File Offset: 0x0016F595
		private Counter()
		{
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x001705A0 File Offset: 0x0016F5A0
		internal static long Value
		{
			get
			{
				long result = 0L;
				SafeNativeMethods.QueryPerformanceCounter(ref result);
				return result;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x001705BC File Offset: 0x0016F5BC
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
