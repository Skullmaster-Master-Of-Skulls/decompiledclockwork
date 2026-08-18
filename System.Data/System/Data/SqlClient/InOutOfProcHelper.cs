using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000313 RID: 787
	internal sealed class InOutOfProcHelper
	{
		// Token: 0x06002907 RID: 10503 RVA: 0x002B3838 File Offset: 0x002B2C38
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

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x002B38A8 File Offset: 0x002B2CA8
		internal static bool InProc
		{
			get
			{
				return InOutOfProcHelper.SingletonInstance._inProc;
			}
		}

		// Token: 0x040019B7 RID: 6583
		private static readonly InOutOfProcHelper SingletonInstance = new InOutOfProcHelper();

		// Token: 0x040019B8 RID: 6584
		private bool _inProc;
	}
}
