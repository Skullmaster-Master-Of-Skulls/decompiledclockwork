using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000095 RID: 149
	// (Invoke) Token: 0x06000766 RID: 1894
	internal delegate void OnChangeCallback([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr opsErrCtx, IntPtr opsChgNTFNDesc, NotiVal notiVal);
}
