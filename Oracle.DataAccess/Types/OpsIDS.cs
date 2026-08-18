using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000126 RID: 294
	[SuppressUnmanagedCodeSecurity]
	internal class OpsIDS
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x00078F80 File Offset: 0x00077F80
		private OpsIDS()
		{
		}

		// Token: 0x06000C08 RID: 3080
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoITLValCtx* ctx);

		// Token: 0x06000C09 RID: 3081
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoITLValCtx* ctx);

		// Token: 0x06000C0A RID: 3082
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocValCtxFromDays")]
		public unsafe static extern int AllocValCtxFromDays(double days, ref OpoITLValCtx* intervalCtx);

		// Token: 0x06000C0B RID: 3083
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocValCtxFromStr")]
		public unsafe static extern int AllocValCtxFromStr(IntPtr strCtx, ref OpoITLValCtx* intervalCtx);

		// Token: 0x06000C0C RID: 3084
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocValCtxFromData")]
		public unsafe static extern int AllocValCtxFromData(int days, int hours, int minutes, int seconds, int fSeconds, ref OpoITLValCtx* intervalCtx);

		// Token: 0x06000C0D RID: 3085
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocValCtxFromOCI")]
		public unsafe static extern int AllocValCtxFromOCI(IntPtr pConCtx, IntPtr pErrCtx, IntPtr pOCIInterval, out OpoITLValCtx* pCtx);

		// Token: 0x06000C0E RID: 3086
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocOCIFromValCtx")]
		public unsafe static extern int AllocOCIFromValCtx(IntPtr pConCtx, IntPtr pErrCtx, OpoITLValCtx* pValCtx, out IntPtr pOCIInterval);

		// Token: 0x06000C0F RID: 3087
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSFreeOCI")]
		public static extern int FreeOCI(IntPtr intervalCtx);

		// Token: 0x06000C10 RID: 3088
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSToDays")]
		public unsafe static extern int ToDays(OpoITLValCtx* intervalCtx, double* days);

		// Token: 0x06000C11 RID: 3089
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSToBytes")]
		public unsafe static extern int ToBytes(OpoITLValCtx* pValCtx1, byte[] bytes);

		// Token: 0x06000C12 RID: 3090
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(byte[] bytes, out OpoITLValCtx* pValCtx1, int dayPrecision, int fracSecPrecision);

		// Token: 0x06000C13 RID: 3091
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSNegate")]
		public unsafe static extern int Negate(OpoITLValCtx* pValCtx1, out OpoITLValCtx* pValCtx2);

		// Token: 0x06000C14 RID: 3092
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsIDSDupValCtx")]
		public unsafe static extern int DupValCtx(OpoITLValCtx* oldCtx, out IntPtr pNewCtx);
	}
}
