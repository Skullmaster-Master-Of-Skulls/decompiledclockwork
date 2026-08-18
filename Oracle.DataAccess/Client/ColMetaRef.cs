using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000DA RID: 218
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class ColMetaRef
	{
		// Token: 0x040006B9 RID: 1721
		public string pColAlias;

		// Token: 0x040006BA RID: 1722
		public IntPtr pTabAlias;

		// Token: 0x040006BB RID: 1723
		public string pColName;

		// Token: 0x040006BC RID: 1724
		public string pTabName;

		// Token: 0x040006BD RID: 1725
		public string pSchemaName;

		// Token: 0x040006BE RID: 1726
		public string pUdtSchemaName;

		// Token: 0x040006BF RID: 1727
		public string pUdtTypeName;
	}
}
