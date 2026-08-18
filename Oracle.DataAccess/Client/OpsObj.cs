using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000143 RID: 323
	[SuppressUnmanagedCodeSecurity]
	internal class OpsObj
	{
		// Token: 0x06000CD2 RID: 3282
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsObjAllocValCtx")]
		public unsafe static extern int AllocObjValCtx(ref OpoObjValCtx* pOpoObjValCtx);

		// Token: 0x06000CD3 RID: 3283
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsObjFreeValCtx")]
		public unsafe static extern int FreeValCtx(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr pComplexObjCtx, OpoObjValCtx* pOpoObjValCtx);

		// Token: 0x06000CD4 RID: 3284
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsObjNew")]
		public unsafe static extern int New(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsUdtCtx, ref OpoObjValCtx* pOpoObjValCtx, OpoObjRefCtx pOpoObjRefCtx, ref IntPtr pUDT, ref IntPtr pOCIRef, ref IntPtr pObjInd);
	}
}
