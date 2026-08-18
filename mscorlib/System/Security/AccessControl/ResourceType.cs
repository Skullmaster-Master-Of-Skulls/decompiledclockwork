using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200091C RID: 2332
	public enum ResourceType
	{
		// Token: 0x04002BAD RID: 11181
		Unknown,
		// Token: 0x04002BAE RID: 11182
		FileObject,
		// Token: 0x04002BAF RID: 11183
		Service,
		// Token: 0x04002BB0 RID: 11184
		Printer,
		// Token: 0x04002BB1 RID: 11185
		RegistryKey,
		// Token: 0x04002BB2 RID: 11186
		LMShare,
		// Token: 0x04002BB3 RID: 11187
		KernelObject,
		// Token: 0x04002BB4 RID: 11188
		WindowObject,
		// Token: 0x04002BB5 RID: 11189
		DSObject,
		// Token: 0x04002BB6 RID: 11190
		DSObjectAll,
		// Token: 0x04002BB7 RID: 11191
		ProviderDefined,
		// Token: 0x04002BB8 RID: 11192
		WmiGuidObject,
		// Token: 0x04002BB9 RID: 11193
		RegistryWow6432Key
	}
}
