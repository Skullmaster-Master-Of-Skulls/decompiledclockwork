using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000145 RID: 325
	[SuppressUnmanagedCodeSecurity]
	internal class OpsDat
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00086610 File Offset: 0x00085610
		private OpsDat()
		{
		}

		// Token: 0x06000CD7 RID: 3287
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoDatValCtx* ctx);

		// Token: 0x06000CD8 RID: 3288
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatFreeValCtx")]
		public unsafe static extern int FreeValCtx(OpoDatValCtx* ctx);

		// Token: 0x06000CD9 RID: 3289
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatAllocValCtxFromData")]
		public unsafe static extern int AllocValCtxFromData(int year, int month, int day, int hour, int minute, int second, out OpoDatValCtx* ctx);

		// Token: 0x06000CDA RID: 3290
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(IntPtr dateCtx, out OpoDatValCtx* pValCtx);

		// Token: 0x06000CDB RID: 3291
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatAllocValCtxFromBytes")]
		public unsafe static extern int AllocValCtxFromBytes(byte[] bytes, out OpoDatValCtx* pValCtx);

		// Token: 0x06000CDC RID: 3292
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDatAllocValCtxFromStr")]
		public unsafe static extern int AllocValCtxFromStr(string datStr, out OpoDatValCtx* pValCtx);

		// Token: 0x06000CDD RID: 3293
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDatGetValCtxFromStr")]
		public unsafe static extern int GetValCtxFromStr(string datStr, OpoDatValCtx* pValCtx);

		// Token: 0x06000CDE RID: 3294
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatCompare")]
		public unsafe static extern int Compare(OpoDatValCtx* pValCtx1, OpoDatValCtx* pValCtx2, ref int result);

		// Token: 0x06000CDF RID: 3295
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatGetDaysBetween")]
		public unsafe static extern int GetDaysBetween(OpoDatValCtx* pValCtx1, OpoDatValCtx* pValCtx2, int* numOfDays);

		// Token: 0x06000CE0 RID: 3296
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatAllocValCtxForSysDate")]
		public unsafe static extern int AllocValCtxForSysDate(out OpoDatValCtx* pValCtx1);

		// Token: 0x06000CE1 RID: 3297
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDatToString")]
		public unsafe static extern int ToString(OpoDatValCtx* pValCtx1, out string datStr);

		// Token: 0x06000CE2 RID: 3298
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsDatAllocValCtxFromCtx")]
		public unsafe static extern int AllocValCtxFromCtx(OpoDatValCtx* oldCtx, out IntPtr pNewCtx);
	}
}
