using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000005 RID: 5
	[SuppressUnmanagedCodeSecurity]
	internal class OpsErr
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000228E File Offset: 0x0000128E
		private OpsErr()
		{
		}

		// Token: 0x06000010 RID: 16
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsErrGetOpoCtx")]
		public static extern int GetOpoCtx(IntPtr opsErrCtx, ref OpoErrCtx opoErrCtx);

		// Token: 0x06000011 RID: 17
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsErrGetBatchErrCtx")]
		public static extern int GetBatchErrCtx(IntPtr opsErrCtx, IntPtr opsConCtx, int batchErrCnt, [In] [Out] IntPtr[] batchOpsErrCtx, [In] [Out] int[] batchOpsErrOffset);

		// Token: 0x06000012 RID: 18
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsErrGetCtxCnt")]
		public static extern int GetCtxCnt(ref int cnt, IntPtr opsErrCtx, IntPtr opsSqlCtx);

		// Token: 0x06000013 RID: 19
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsErrAllocCtx")]
		public static extern int AllocCtx(ref IntPtr opsErrCtx, IntPtr opsConCtx);

		// Token: 0x06000014 RID: 20
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsErrFreeCtx")]
		public static extern int FreeCtx(ref IntPtr opsErrCtx);

		// Token: 0x06000015 RID: 21
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsErrGetOraMesg")]
		public static extern int GetOraMesg(int errNum, out string errMsg);

		// Token: 0x06000016 RID: 22
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsErrGetTypeMsg")]
		public static extern int GetTypeMsg(int errNum, out string typMsg);
	}
}
