using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000142 RID: 322
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoObjRefCtx
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x000865E4 File Offset: 0x000855E4
		public OpoObjRefCtx()
		{
			this.xmlStr = null;
			this.hexStr = null;
			this.attrname = null;
			this.objTableName = null;
		}

		// Token: 0x04000A1D RID: 2589
		public string xmlStr;

		// Token: 0x04000A1E RID: 2590
		public string hexStr;

		// Token: 0x04000A1F RID: 2591
		public string attrname;

		// Token: 0x04000A20 RID: 2592
		public string objTableName;

		// Token: 0x04000A21 RID: 2593
		public string schemaName;
	}
}
