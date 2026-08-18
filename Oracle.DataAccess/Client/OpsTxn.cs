using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000FB RID: 251
	[SuppressUnmanagedCodeSecurity]
	internal class OpsTxn
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x0005C8F8 File Offset: 0x0005B8F8
		private OpsTxn()
		{
		}

		// Token: 0x0600093D RID: 2365
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTxnAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoTxnValCtx* pOpoTxnValCtx);

		// Token: 0x0600093E RID: 2366
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTxnFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoTxnValCtx* pOpoTxnValCtx);

		// Token: 0x0600093F RID: 2367
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTxnBegin")]
		public unsafe static extern int Begin(IntPtr opsConCtx, out IntPtr opsErrCtx, OpoTxnValCtx* opoTxnValCtx);

		// Token: 0x06000940 RID: 2368
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTxnCommit")]
		public unsafe static extern int Commit(IntPtr opsConCtx, IntPtr opsErrCtx, OpoTxnValCtx* pOpoTxnValCtx);

		// Token: 0x06000941 RID: 2369
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTxnRollback")]
		public unsafe static extern int Rollback(IntPtr opsConCtx, IntPtr opsErrCtx, OpoTxnValCtx* pOpoTxnValCtx);

		// Token: 0x06000942 RID: 2370
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTxnDispose")]
		public unsafe static extern int Dispose(IntPtr opsErrCtx, OpoTxnValCtx* pOpoTxnValCtx);
	}
}
