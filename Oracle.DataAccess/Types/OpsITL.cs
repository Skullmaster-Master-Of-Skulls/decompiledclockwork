using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000127 RID: 295
	[SuppressUnmanagedCodeSecurity]
	internal class OpsITL
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x00078F88 File Offset: 0x00077F88
		private OpsITL()
		{
		}

		// Token: 0x06000C16 RID: 3094
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsITLToStr")]
		public unsafe static extern int ToString(OpoITLValCtx* intervalCtx, int lPrec, int fPrec, out IntPtr strCtx);

		// Token: 0x06000C17 RID: 3095
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsITLCompare")]
		public unsafe static extern int Compare(OpoITLValCtx* intervalCtx1, OpoITLValCtx* intervalCtx2, ref int result);

		// Token: 0x06000C18 RID: 3096
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsITLAdd")]
		public unsafe static extern int Add(OpoITLValCtx* intervalCtx1, OpoITLValCtx* intervalCtx2, out OpoITLValCtx* intervalCtx3);

		// Token: 0x06000C19 RID: 3097
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsITLSubtract")]
		public unsafe static extern int Subtract(OpoITLValCtx* intervalCtx1, OpoITLValCtx* intervalCtx2, out OpoITLValCtx* intervalCtx3);

		// Token: 0x06000C1A RID: 3098
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsITLMultiply")]
		public unsafe static extern int Multiply(OpoITLValCtx* intervalCtx1, int multiplier, out OpoITLValCtx* intervalCtx3);

		// Token: 0x06000C1B RID: 3099
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsITLDivide")]
		public unsafe static extern int Divide(OpoITLValCtx* intervalCtx1, int divisor, out OpoITLValCtx* intervalCtx3);
	}
}
