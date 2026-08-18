using System;

namespace System.Security.Authentication
{
	// Token: 0x0200043C RID: 1084
	[Flags]
	[__DynamicallyInvokable]
	public enum SslProtocols
	{
		// Token: 0x04002240 RID: 8768
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04002241 RID: 8769
		[__DynamicallyInvokable]
		Ssl2 = 12,
		// Token: 0x04002242 RID: 8770
		[__DynamicallyInvokable]
		Ssl3 = 48,
		// Token: 0x04002243 RID: 8771
		[__DynamicallyInvokable]
		Tls = 192,
		// Token: 0x04002244 RID: 8772
		[__DynamicallyInvokable]
		Tls11 = 768,
		// Token: 0x04002245 RID: 8773
		[__DynamicallyInvokable]
		Tls12 = 3072,
		// Token: 0x04002246 RID: 8774
		Tls13 = 12288,
		// Token: 0x04002247 RID: 8775
		Default = 240
	}
}
