using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000201 RID: 513
	internal sealed class InOutOfProcHelper
	{
		// Token: 0x06001FA9 RID: 8105 RVA: 0x000DA94C File Offset: 0x000D9D4C
		private InOutOfProcHelper()
		{
			IntPtr moduleHandle = SafeNativeMethods.GetModuleHandle(null);
			if (IntPtr.Zero != moduleHandle)
			{
				if (IntPtr.Zero != SafeNativeMethods.GetProcAddress(moduleHandle, "_______SQL______Process______Available@0"))
				{
					this._inProc = true;
					return;
				}
				if (IntPtr.Zero != SafeNativeMethods.GetProcAddress(moduleHandle, "______SQL______Process______Available"))
				{
					this._inProc = true;
				}
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000DA9B0 File Offset: 0x000D9DB0
		internal static bool InProc
		{
			get
			{
				return InOutOfProcHelper.SingletonInstance._inProc;
			}
		}

		// Token: 0x040011EF RID: 4591
		private static readonly InOutOfProcHelper SingletonInstance = new InOutOfProcHelper();

		// Token: 0x040011F0 RID: 4592
		private bool _inProc;
	}
}
