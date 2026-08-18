using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200000A RID: 10
	[SuppressUnmanagedCodeSecurity]
	internal class OpsDsc
	{
		// Token: 0x06000019 RID: 25
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscDescribeAllObjAttrs")]
		public unsafe static extern int DescribeAllObjAttrs(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x0600001A RID: 26
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscDescribeArrElem")]
		public unsafe static extern int DescribeArrElem(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x0600001B RID: 27
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscDescribeObjAttr")]
		public unsafe static extern int DescribeObjAttr(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx, int attrIndex);

		// Token: 0x0600001C RID: 28
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscDescribeUdt")]
		public unsafe static extern int DescribeUdt(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x0600001D RID: 29
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscDispose")]
		public unsafe static extern int Dispose(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x0600001E RID: 30
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscGetArrTypeCode")]
		public unsafe static extern int GetArrTypeCode(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x0600001F RID: 31
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscGetNumArrElems")]
		public unsafe static extern int GetNumArrElems(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x06000020 RID: 32
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscGetNumObjAttrs")]
		public unsafe static extern int GetNumObjAttrs(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx);

		// Token: 0x06000021 RID: 33
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscGetTDO")]
		public unsafe static extern int GetTDO(IntPtr opsConCtx, out IntPtr opsErrCtx, ref IntPtr opsDscCtx, out OpoDscValCtx* pOpoDscValCtx, OpoDscRefCtx opoDscRefCtx);

		// Token: 0x06000022 RID: 34
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscGetUdtTypeName")]
		public unsafe static extern int GetUdtTypeName(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsDscCtx, OpoDscValCtx* pOpoDscValCtx, ref OpoDscRefCtx opoDscRefCtx);

		// Token: 0x06000023 RID: 35
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsDscUnpinTDO")]
		public static extern int UnpinTDO(IntPtr opsConCtx, IntPtr tdo);
	}
}
