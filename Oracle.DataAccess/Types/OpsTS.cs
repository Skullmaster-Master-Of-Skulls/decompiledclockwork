using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000100 RID: 256
	[SuppressUnmanagedCodeSecurity]
	internal class OpsTS
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0005C910 File Offset: 0x0005B910
		private OpsTS()
		{
		}

		// Token: 0x06000956 RID: 2390
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoTSValCtx* ctx);

		// Token: 0x06000957 RID: 2391
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocValCtxFromData")]
		public unsafe static extern int AllocValCtxFromData(int year, int month, int day, int hour, int minute, int second, int fSecond, out OpoTSValCtx* pValCtx);

		// Token: 0x06000958 RID: 2392
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(byte[] bytes, out OpoTSValCtx* pValCtx1, int fracSecPrecision);

		// Token: 0x06000959 RID: 2393
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSAllocValCtxFromStr")]
		public unsafe static extern int AllocValCtxFromStr(string tsStr, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600095A RID: 2394
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoTSValCtx* ctx);

		// Token: 0x0600095B RID: 2395
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSFreeOCI")]
		public static extern int FreeOCI(IntPtr TSCtx);

		// Token: 0x0600095C RID: 2396
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSToBytes")]
		public unsafe static extern int ToBytes(OpoTSValCtx* pValCtx1, byte[] bytes, int* len);

		// Token: 0x0600095D RID: 2397
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocValCtxForSysDate")]
		public unsafe static extern int AllocValCtxForSysDate(out OpoTSValCtx* pValCtx1);

		// Token: 0x0600095E RID: 2398
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSToString")]
		public unsafe static extern int ToString(OpoTSValCtx* pValCtx1, int fSecondPrec, out string tsStr);

		// Token: 0x0600095F RID: 2399
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocValCtxForFromDate")]
		public unsafe static extern int AllocValCtxForFromDate(OpoDatValCtx* pDatCtx1, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000960 RID: 2400
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocValCtxFromOCI")]
		public unsafe static extern int AllocValCtxFromOCI(IntPtr pOCIDateTime, out OpoTSValCtx* pLdiDateTimeCtx);

		// Token: 0x06000961 RID: 2401
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAllocOCIFromValCtx")]
		public unsafe static extern int AllocOCIFromValCtx(IntPtr pOpsConCtx, OpoTSValCtx* pValCtx, out IntPtr pOCIDateTime);
	}
}
