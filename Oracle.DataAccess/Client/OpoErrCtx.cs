using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000004 RID: 4
	[SuppressUnmanagedCodeSecurity]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoErrCtx
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002286 File Offset: 0x00001286
		internal OpoErrCtx()
		{
		}

		// Token: 0x04000003 RID: 3
		[MarshalAs(UnmanagedType.LPWStr)]
		public string m_message;

		// Token: 0x04000004 RID: 4
		public int m_errNumber;

		// Token: 0x04000005 RID: 5
		public int m_status;

		// Token: 0x04000006 RID: 6
		public int m_arrayBindIndex;
	}
}
