using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200060B RID: 1547
	internal struct IcmpEchoReply
	{
		// Token: 0x04002DBB RID: 11707
		internal uint address;

		// Token: 0x04002DBC RID: 11708
		internal uint status;

		// Token: 0x04002DBD RID: 11709
		internal uint roundTripTime;

		// Token: 0x04002DBE RID: 11710
		internal ushort dataSize;

		// Token: 0x04002DBF RID: 11711
		internal ushort reserved;

		// Token: 0x04002DC0 RID: 11712
		internal IntPtr data;

		// Token: 0x04002DC1 RID: 11713
		internal IPOptions options;
	}
}
