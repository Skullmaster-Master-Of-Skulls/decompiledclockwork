using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200011A RID: 282
	[SuppressUnmanagedCodeSecurity]
	internal class OpsXmlStream
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x00072B01 File Offset: 0x00071B01
		private OpsXmlStream()
		{
		}

		// Token: 0x06000B49 RID: 2889
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamAllocCtx")]
		public static extern int AllocCtx(IntPtr opsConCtx, IntPtr opsXmlTypeCtx, ref IntPtr opsErrCtx, ref IntPtr opsXmlStreamCtx);

		// Token: 0x06000B4A RID: 2890
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamAllocReadParamList")]
		public unsafe static extern int AllocReadParamList(ref OpoXmlStreamReadParamList* popoXmlStreamReadParamList);

		// Token: 0x06000B4B RID: 2891
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamFreeReadParamList")]
		public unsafe static extern int FreeReadParamList(ref OpoXmlStreamReadParamList* popoXmlStreamReadParamList);

		// Token: 0x06000B4C RID: 2892
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamReadBytes")]
		public unsafe static extern int ReadBytes(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsXmlStreamCtx, IntPtr OpsXmlTypeCtx, IntPtr pBuffer, ref OpoXmlStreamReadParamList* popoXmlStreamReadParamList);

		// Token: 0x06000B4D RID: 2893
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamReadChars")]
		public unsafe static extern int ReadChars(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsXmlStreamCtx, IntPtr opsXmlTypeCtx, IntPtr pBuffer, ref OpoXmlStreamReadParamList* popoXmlStreamReadParamList);

		// Token: 0x06000B4E RID: 2894
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamFreeCtx")]
		public static extern int FreeCtx(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, ref IntPtr opsXmlTypeCtx, ref IntPtr opsXmlStreamCtx, int bFreeOciXmlType, int bFreeOciHandles);

		// Token: 0x06000B4F RID: 2895
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamGetValueBuffer")]
		public static extern int GetValueBuffer(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsXmlTypeCtx, ref IntPtr opsXmlStreamValueBuffer, ref int numCharsInBuffer);

		// Token: 0x06000B50 RID: 2896
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamFreeValueBuffer")]
		public static extern int FreeValueBuffer(ref IntPtr opsXmlStreamValueBuffer);

		// Token: 0x06000B51 RID: 2897
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsXmlStreamGetLength")]
		public static extern int GetLength(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsXmlTypeCtx, ref int lengthInChars);
	}
}
