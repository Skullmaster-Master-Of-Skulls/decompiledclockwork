using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000051 RID: 81
	[SuppressUnmanagedCodeSecurity]
	internal class OpsStr
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00029EAC File Offset: 0x00028EAC
		private OpsStr()
		{
		}

		// Token: 0x060003A8 RID: 936
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsStrBytesToUnicode")]
		public static extern int BytesToUnicode(IntPtr byteSrc, int srcLen, int index, int count, out string dst);

		// Token: 0x060003A9 RID: 937
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsStrUnicodeToBytes")]
		public static extern int UnicodeToBytes(IntPtr str, int srcLen, out IntPtr dst, out uint dstLen);

		// Token: 0x060003AA RID: 938
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsStrStrCompare")]
		public static extern int StrCompare(IntPtr src1, int src1Len, IntPtr src2, int src2Len, int isCaseInsensitive, out int res);
	}
}
