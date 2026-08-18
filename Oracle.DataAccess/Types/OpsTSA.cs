using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000101 RID: 257
	[SuppressUnmanagedCodeSecurity]
	internal class OpsTSA
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0005C918 File Offset: 0x0005B918
		private OpsTSA()
		{
		}

		// Token: 0x06000963 RID: 2403
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSACompare")]
		public unsafe static extern int Compare(OpoTSValCtx* pValCtx1, OpoTSValCtx* pValCtx2, ref int result);

		// Token: 0x06000964 RID: 2404
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddInterval")]
		public unsafe static extern int AllocValCtxForAddInterval(OpoTSValCtx* pValCtx1, OpoITLValCtx* pIDS1, out OpoTSValCtx* pCtx2);

		// Token: 0x06000965 RID: 2405
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForSubInterval")]
		public unsafe static extern int AllocValCtxForSubInterval(OpoTSValCtx* pValCtx1, OpoITLValCtx* pIDS1, out OpoTSValCtx* pCtx2);

		// Token: 0x06000966 RID: 2406
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForSubTSToIDS")]
		public unsafe static extern int AllocValCtxForSubTSToIDS(OpoTSValCtx* pValCtx1, OpoTSValCtx* pCtx2, out OpoITLValCtx* pIDS1);

		// Token: 0x06000967 RID: 2407
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForSubTSToIYM")]
		public unsafe static extern int AllocValCtxForSubTSToIYM(OpoTSValCtx* pValCtx1, OpoTSValCtx* pCtx2, out OpoITLValCtx* pIDS1);

		// Token: 0x06000968 RID: 2408
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForToDate")]
		public unsafe static extern int AllocValCtxForToDate(OpoTSValCtx* pValCtx1, out OpoDatValCtx* pDatCtx1);

		// Token: 0x06000969 RID: 2409
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddYears")]
		public unsafe static extern int AllocValCtxForAddYears(OpoTSValCtx* pDatCtx1, int years, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600096A RID: 2410
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddMonths")]
		public unsafe static extern int AllocValCtxForAddMonths(OpoTSValCtx* pDatCtx1, long months, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600096B RID: 2411
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddDays")]
		public unsafe static extern int AllocValCtxForAddDays(OpoTSValCtx* pDatCtx1, double days, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600096C RID: 2412
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddHours")]
		public unsafe static extern int AllocValCtxForAddHours(OpoTSValCtx* pDatCtx1, double hours, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600096D RID: 2413
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddMinutes")]
		public unsafe static extern int AllocValCtxForAddMinutes(OpoTSValCtx* pDatCtx1, double minutes, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600096E RID: 2414
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddSeconds")]
		public unsafe static extern int AllocValCtxForAddSeconds(OpoTSValCtx* pDatCtx1, double seconds, out OpoTSValCtx* pValCtx1);

		// Token: 0x0600096F RID: 2415
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddMilliseconds")]
		public unsafe static extern int AllocValCtxForAddMilliseconds(OpoTSValCtx* pDatCtx1, double milliseconds, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000970 RID: 2416
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForAddNanoseconds")]
		public unsafe static extern int AllocValCtxForAddNanoseconds(OpoTSValCtx* pDatCtx1, long nanoseconds, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000971 RID: 2417
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForToTS")]
		public unsafe static extern int AllocValCtxForToTS(OpoTSValCtx* pDatCtx1, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000972 RID: 2418
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForToTSL")]
		public unsafe static extern int AllocValCtxForToTSL(OpoTSValCtx* pDatCtx1, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000973 RID: 2419
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForToTSZ")]
		public unsafe static extern int AllocValCtxForToTSZ(OpoTSValCtx* pDatCtx1, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000974 RID: 2420
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAAllocValCtxForToUTC")]
		public unsafe static extern int AllocValCtxForToUTC(OpoTSValCtx* pValCtx1, out OpoTSValCtx* pCtx2);

		// Token: 0x06000975 RID: 2421
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSAGetSysTZName")]
		public static extern int GetSysTZName(out string tzStr);

		// Token: 0x06000976 RID: 2422
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSAGetSysTZOffset")]
		public unsafe static extern int GetTimeZoneOffset(int* tzHours, int* tzMinutes);

		// Token: 0x06000977 RID: 2423
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSADupValCtx")]
		public unsafe static extern int DupValCtx(OpoTSValCtx* pSrcCtx, out IntPtr pNewCtx, TimeStampType tsType);
	}
}
