using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000007 RID: 7
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoDscRefCtx
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002296 File Offset: 0x00001296
		public OpoDscRefCtx()
		{
			this.SchemaName = null;
			this.TypeName = null;
		}

		// Token: 0x04000013 RID: 19
		public string SchemaName;

		// Token: 0x04000014 RID: 20
		public string TypeName;
	}
}
