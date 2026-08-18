using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000042 RID: 66
	internal abstract class SmiLink
	{
		// Token: 0x06000210 RID: 528
		internal abstract ulong NegotiateVersion(ulong requestedVersion);

		// Token: 0x06000211 RID: 529
		internal abstract object GetCurrentContext(SmiEventSink eventSink);

		// Token: 0x04000111 RID: 273
		internal const ulong InterfaceVersion = 210UL;
	}
}
