using System;

namespace System.Net
{
	// Token: 0x02000112 RID: 274
	[__DynamicallyInvokable]
	public interface ICredentialsByHost
	{
		// Token: 0x06000B02 RID: 2818
		[__DynamicallyInvokable]
		NetworkCredential GetCredential(string host, int port, string authenticationType);
	}
}
