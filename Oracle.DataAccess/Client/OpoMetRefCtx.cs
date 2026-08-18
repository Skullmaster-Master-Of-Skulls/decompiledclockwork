using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000DC RID: 220
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoMetRefCtx
	{
		// Token: 0x040006DB RID: 1755
		public string pTableName;

		// Token: 0x040006DC RID: 1756
		public string pSchemaName;

		// Token: 0x040006DD RID: 1757
		public IntPtr pColMetaRef;
	}
}
