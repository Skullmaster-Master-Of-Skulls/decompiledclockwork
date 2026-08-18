using System;
using System.Runtime.InteropServices;

namespace System.Net.Mail
{
	// Token: 0x0200068F RID: 1679
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct MetadataRecord
	{
		// Token: 0x04002FE9 RID: 12265
		internal uint Identifier;

		// Token: 0x04002FEA RID: 12266
		internal uint Attributes;

		// Token: 0x04002FEB RID: 12267
		internal uint UserType;

		// Token: 0x04002FEC RID: 12268
		internal uint DataType;

		// Token: 0x04002FED RID: 12269
		internal uint DataLen;

		// Token: 0x04002FEE RID: 12270
		internal IntPtr DataBuf;

		// Token: 0x04002FEF RID: 12271
		internal uint DataTag;
	}
}
