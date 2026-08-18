using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000056 RID: 86
	internal static class HandleKindExtensions
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00006741 File Offset: 0x00004941
		internal static bool IsHeapHandle(this HandleKind kind)
		{
			return kind >= HandleKind.NamespaceDefinition;
		}
	}
}
