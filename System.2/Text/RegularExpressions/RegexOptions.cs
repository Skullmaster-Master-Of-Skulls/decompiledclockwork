using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A2 RID: 1698
	[Flags]
	[__DynamicallyInvokable]
	public enum RegexOptions
	{
		// Token: 0x04002E3B RID: 11835
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04002E3C RID: 11836
		[__DynamicallyInvokable]
		IgnoreCase = 1,
		// Token: 0x04002E3D RID: 11837
		[__DynamicallyInvokable]
		Multiline = 2,
		// Token: 0x04002E3E RID: 11838
		[__DynamicallyInvokable]
		ExplicitCapture = 4,
		// Token: 0x04002E3F RID: 11839
		[__DynamicallyInvokable]
		Compiled = 8,
		// Token: 0x04002E40 RID: 11840
		[__DynamicallyInvokable]
		Singleline = 16,
		// Token: 0x04002E41 RID: 11841
		[__DynamicallyInvokable]
		IgnorePatternWhitespace = 32,
		// Token: 0x04002E42 RID: 11842
		[__DynamicallyInvokable]
		RightToLeft = 64,
		// Token: 0x04002E43 RID: 11843
		[__DynamicallyInvokable]
		ECMAScript = 256,
		// Token: 0x04002E44 RID: 11844
		[__DynamicallyInvokable]
		CultureInvariant = 512
	}
}
