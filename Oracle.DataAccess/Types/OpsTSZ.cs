using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000FC RID: 252
	[SuppressUnmanagedCodeSecurity]
	internal class OpsTSZ
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x0005C900 File Offset: 0x0005B900
		private OpsTSZ()
		{
		}

		// Token: 0x06000944 RID: 2372
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoTSValCtx* ctx);

		// Token: 0x06000945 RID: 2373
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSZAllocValCtxFromData")]
		public unsafe static extern int AllocValCtxFromData(int year, int month, int day, int hour, int minute, int second, int fSecond, int tzHours, int tzMinutes, string regionName, out OpoTSValCtx* pValCtx);

		// Token: 0x06000946 RID: 2374
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(byte[] bytes, out OpoTSValCtx* pValCtx1, int fracSecPrecision);

		// Token: 0x06000947 RID: 2375
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSZAllocValCtxFromStr")]
		public unsafe static extern int AllocValCtxFromStr(string tsStr, OpoITLValCtx* pTZOffsetstring, out OpoTSValCtx* pValCtx1);

		// Token: 0x06000948 RID: 2376
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSZAllocValCtxFromOCI")]
		public unsafe static extern int AllocValCtxFromOCI(IntPtr pOCIDateTime, out OpoTSValCtx* pValCtx);

		// Token: 0x06000949 RID: 2377
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSZAllocOCIFromValCtx")]
		public unsafe static extern int AllocOCIFromValCtx(IntPtr pOpsConCtx, OpoTSValCtx* pValCtx, out IntPtr pOCIDateTime);

		// Token: 0x0600094A RID: 2378
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoTSValCtx* ctx);

		// Token: 0x0600094B RID: 2379
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZFreeOCI")]
		public static extern int FreeOCI(IntPtr TSCtx);

		// Token: 0x0600094C RID: 2380
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZToBytes")]
		public unsafe static extern int ToBytes(OpoTSValCtx* pValCtx1, byte[] bytes, int* len);

		// Token: 0x0600094D RID: 2381
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZAllocValCtxForSysDate")]
		public unsafe static extern int AllocValCtxForSysDate(out OpoTSValCtx* pValCtx1);

		// Token: 0x0600094E RID: 2382
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSZGetTZName")]
		public static extern int GetTimeZoneName(int tzHours, int tzMinutes, int regId, out string tzStr);

		// Token: 0x0600094F RID: 2383
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsTSZToString")]
		public unsafe static extern int ToString(OpoTSValCtx* pValCtx1, int fSecondPrec, out string tsStr);

		// Token: 0x06000950 RID: 2384
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZConvertToTSL")]
		public unsafe static extern int ConvertToTSL(OpoTSValCtx* pDatCtx1, OpoTSValCtx* pValCtx1);

		// Token: 0x06000951 RID: 2385
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZAllocMaxValue")]
		public unsafe static extern int AllocMaxValue(int year, int month, int day, int hour, int minute, int second, int fSecond, int tzHours, int tzMinutes, out OpoTSValCtx* pTSCtx);

		// Token: 0x06000952 RID: 2386
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZAllocMinValue")]
		public unsafe static extern int AllocMinValue(int year, int month, int day, int hour, int minute, int second, int fSecond, int tzHours, int tzMinutes, out OpoTSValCtx* pTSCtx);

		// Token: 0x06000953 RID: 2387
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsTSZAllocValCtxForFromDate")]
		public unsafe static extern int AllocValCtxForFromDate(OpoDatValCtx* pDatCtx1, out OpoTSValCtx* pValCtx1);
	}
}
