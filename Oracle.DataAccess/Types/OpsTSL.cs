using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200008F RID: 143
	[SuppressUnmanagedCodeSecurity]
	internal class OpsTSL
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x00045BE4 File Offset: 0x00044BE4
		private OpsTSL()
		{
		}

		// Token: 0x060006EB RID: 1771
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoTSValCtx* ctx);

		// Token: 0x060006EC RID: 1772
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLAllocValCtxFromData")]
		public unsafe static extern int AllocValCtxFromData(int year, int month, int day, int hour, int minute, int second, int fSecond, int tzHours, int tzMinuts, out OpoTSValCtx* pValCtx);

		// Token: 0x060006ED RID: 1773
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(byte[] bytes, out OpoTSValCtx* pValCtx1, int fracSecPrecision);

		// Token: 0x060006EE RID: 1774
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSLAllocValCtxFromStr")]
		public unsafe static extern int AllocValCtxFromStr(string tsStr, OpoITLValCtx* pITLCtx, out OpoTSValCtx* pValCtx1);

		// Token: 0x060006EF RID: 1775
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoTSValCtx* ctx);

		// Token: 0x060006F0 RID: 1776
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLFreeOCI")]
		public static extern int FreeOCI(IntPtr TSCtx);

		// Token: 0x060006F1 RID: 1777
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLToBytes")]
		public unsafe static extern int ToBytes(OpoTSValCtx* pValCtx1, byte[] bytes, int* len);

		// Token: 0x060006F2 RID: 1778
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLAllocValCtxForSysDate")]
		public unsafe static extern int AllocValCtxForSysDate(out OpoTSValCtx* pValCtx1);

		// Token: 0x060006F3 RID: 1779
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSLToString")]
		public unsafe static extern int ToString(OpoTSValCtx* pValCtx1, OpoITLValCtx* pTZCtx, int fSecondPrec, out string tsStr);

		// Token: 0x060006F4 RID: 1780
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSLAllocValCtxForFromDate")]
		public unsafe static extern int AllocValCtxForFromDate(OpoDatValCtx* pDatCtx1, out OpoTSValCtx* pValCtx1);

		// Token: 0x060006F5 RID: 1781
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSLAllocValCtxFromOCI")]
		public unsafe static extern int AllocValCtxFromOCI(IntPtr pOCIDateTime, out OpoTSValCtx* pValCtx);

		// Token: 0x060006F6 RID: 1782
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSLAllocOCIFromValCtx")]
		public unsafe static extern int AllocOCIFromValCtx(IntPtr pOpsConCtx, OpoTSValCtx* pValCtx, out IntPtr pOCIDateTime);
	}
}
