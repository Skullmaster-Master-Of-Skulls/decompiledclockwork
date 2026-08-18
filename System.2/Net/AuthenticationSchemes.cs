using System;

namespace System.Net
{
	// Token: 0x020000C4 RID: 196
	[Flags]
	[__DynamicallyInvokable]
	public enum AuthenticationSchemes
	{
		// Token: 0x04000C80 RID: 3200
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04000C81 RID: 3201
		[__DynamicallyInvokable]
		Digest = 1,
		// Token: 0x04000C82 RID: 3202
		[__DynamicallyInvokable]
		Negotiate = 2,
		// Token: 0x04000C83 RID: 3203
		[__DynamicallyInvokable]
		Ntlm = 4,
		// Token: 0x04000C84 RID: 3204
		[__DynamicallyInvokable]
		Basic = 8,
		// Token: 0x04000C85 RID: 3205
		[__DynamicallyInvokable]
		Anonymous = 32768,
		// Token: 0x04000C86 RID: 3206
		[__DynamicallyInvokable]
		IntegratedWindowsAuthentication = 6
	}
}
