using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000009 RID: 9
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class AttrMetaRef
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000022AC File Offset: 0x000012AC
		public AttrMetaRef()
		{
			this.AttrName = null;
			this.AttrSchemaName = null;
			this.AttrTypeName = null;
		}

		// Token: 0x04000021 RID: 33
		public string AttrName;

		// Token: 0x04000022 RID: 34
		public string AttrSchemaName;

		// Token: 0x04000023 RID: 35
		public string AttrTypeName;

		// Token: 0x04000024 RID: 36
		public IntPtr AttrNameConverted;

		// Token: 0x04000025 RID: 37
		public int AttrNameCovertedLen;
	}
}
