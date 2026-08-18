using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200007F RID: 127
	[SuppressUnmanagedCodeSecurity]
	internal class OpsSubscr
	{
		// Token: 0x060005AD RID: 1453
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsSubscrAllocGlobalCtx")]
		public static extern int AllocGlobalCtx(out IntPtr opsEnvCtx, out IntPtr opsErrCtx);

		// Token: 0x060005AE RID: 1454
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsSubscrFreeGlobalCtx")]
		public static extern int FreeGlobalCtx(out IntPtr opsEnvCtx, out IntPtr opsErrCtx);

		// Token: 0x060005AF RID: 1455
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSubscrAllocCtx")]
		public static extern int AllocCtx(IntPtr opsEnvCtx, out IntPtr opsErrCtx, out IntPtr opsSubscrCtx);

		// Token: 0x060005B0 RID: 1456
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSubscrFreeCtx")]
		public static extern int FreeCtx(IntPtr opsEnvCtx, out IntPtr opsErrCtx, out IntPtr opsSubscrCtx);

		// Token: 0x060005B1 RID: 1457
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSubscrSetChgNTFN")]
		public static extern int SetChgNTFN(IntPtr opsSubscrEnvCtx, IntPtr opsSubscrCtx, IntPtr opsErrCtx, string invalidationStr, int isPersistent, int isNotifiedOnce, int isRowidReq, uint timeout);

		// Token: 0x060005B2 RID: 1458
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsSubscrUnRegister")]
		public static extern int UnRegister(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsSubscrCtx);

		// Token: 0x060005B3 RID: 1459
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSubscrSetPort")]
		public static extern int SetPort(IntPtr opsEnvCtx, IntPtr opsErrCtx, uint port);

		// Token: 0x060005B4 RID: 1460
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsSubscrGetPort")]
		public static extern int GetPort(IntPtr opsEnvCtx, IntPtr opsErrCtx, out uint port);
	}
}
