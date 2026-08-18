using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003C RID: 60
	internal abstract class SmiLink
	{
		// Token: 0x06000213 RID: 531
		internal abstract ulong NegotiateVersion(ulong requestedVersion);

		// Token: 0x06000214 RID: 532
		internal abstract object GetCurrentContext(SmiEventSink eventSink);

		// Token: 0x0400059F RID: 1439
		internal const ulong InterfaceVersion = 210UL;
	}
}
