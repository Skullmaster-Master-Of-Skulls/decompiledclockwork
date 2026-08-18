using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000084 RID: 132
	[SuppressUnmanagedCodeSecurity]
	internal class OpsIYM
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0003E8D7 File Offset: 0x0003D8D7
		private OpsIYM()
		{
		}

		// Token: 0x060005C6 RID: 1478
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoITLValCtx* ctx);

		// Token: 0x060005C7 RID: 1479
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoITLValCtx* ctx);

		// Token: 0x060005C8 RID: 1480
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMFreeOCI")]
		public static extern int FreeOCI(IntPtr intervalCtx);

		// Token: 0x060005C9 RID: 1481
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocValCtxFromYears")]
		public unsafe static extern int AllocValCtxFromYears(double years, ref OpoITLValCtx* intervalCtx);

		// Token: 0x060005CA RID: 1482
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocValCtxFromStr")]
		public unsafe static extern int AllocValCtxFromStr(IntPtr ansiStr, ref OpoITLValCtx* intervalCtx);

		// Token: 0x060005CB RID: 1483
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocValCtxFromData")]
		public unsafe static extern int AllocValCtxFromData(int years, int months, ref OpoITLValCtx* intervalCtx);

		// Token: 0x060005CC RID: 1484
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocValCtxFromOCI")]
		public unsafe static extern int AllocValCtxFromOCI(IntPtr pConCtx, IntPtr pErrCtx, IntPtr pOCIInterval, out OpoITLValCtx* pCtx);

		// Token: 0x060005CD RID: 1485
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocOCIFromValCtx")]
		public unsafe static extern int AllocOCIFromValCtx(IntPtr pConCtx, IntPtr pErrCtx, OpoITLValCtx* pValCtx, out IntPtr pOCIInterval);

		// Token: 0x060005CE RID: 1486
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMToYears")]
		public unsafe static extern int ToYears(OpoITLValCtx* intervalCtx, double* years);

		// Token: 0x060005CF RID: 1487
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMToBytes")]
		public unsafe static extern int ToBytes(OpoITLValCtx* pValCtx1, byte[] bytes);

		// Token: 0x060005D0 RID: 1488
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(byte[] bytes, out OpoITLValCtx* pValCtx1, int yearPrecision);

		// Token: 0x060005D1 RID: 1489
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMNegate")]
		public unsafe static extern int Negate(OpoITLValCtx* pValCtx1, out OpoITLValCtx* pValCtx2);

		// Token: 0x060005D2 RID: 1490
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIYMDupValCtx")]
		public unsafe static extern int DupValCtx(OpoITLValCtx* pSrcCtx, out IntPtr pNewCtx);
	}
}
