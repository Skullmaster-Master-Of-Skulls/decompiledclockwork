using System;

namespace System.Xml
{
	// Token: 0x020000E4 RID: 228
	[__DynamicallyInvokable]
	public enum WriteState
	{
		// Token: 0x04000442 RID: 1090
		[__DynamicallyInvokable]
		Start,
		// Token: 0x04000443 RID: 1091
		[__DynamicallyInvokable]
		Prolog,
		// Token: 0x04000444 RID: 1092
		[__DynamicallyInvokable]
		Element,
		// Token: 0x04000445 RID: 1093
		[__DynamicallyInvokable]
		Attribute,
		// Token: 0x04000446 RID: 1094
		[__DynamicallyInvokable]
		Content,
		// Token: 0x04000447 RID: 1095
		[__DynamicallyInvokable]
		Closed,
		// Token: 0x04000448 RID: 1096
		[__DynamicallyInvokable]
		Error
	}
}
