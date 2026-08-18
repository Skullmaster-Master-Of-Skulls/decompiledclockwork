using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000121 RID: 289
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoXmlTypeRefCtx
	{
		// Token: 0x04000975 RID: 2421
		public string rootElement;

		// Token: 0x04000976 RID: 2422
		public string schemaUrl;

		// Token: 0x04000977 RID: 2423
		public IntPtr schema_opsXmlTypeCtx;
	}
}
