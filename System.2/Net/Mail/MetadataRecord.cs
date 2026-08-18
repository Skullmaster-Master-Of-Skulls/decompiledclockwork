using System;
using System.Runtime.InteropServices;

namespace System.Net.Mail
{
	// Token: 0x02000260 RID: 608
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct MetadataRecord
	{
		// Token: 0x04001794 RID: 6036
		internal uint Identifier;

		// Token: 0x04001795 RID: 6037
		internal uint Attributes;

		// Token: 0x04001796 RID: 6038
		internal uint UserType;

		// Token: 0x04001797 RID: 6039
		internal uint DataType;

		// Token: 0x04001798 RID: 6040
		internal uint DataLen;

		// Token: 0x04001799 RID: 6041
		internal IntPtr DataBuf;

		// Token: 0x0400179A RID: 6042
		internal uint DataTag;
	}
}
