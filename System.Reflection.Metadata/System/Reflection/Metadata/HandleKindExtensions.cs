using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000055 RID: 85
	internal static class HandleKindExtensions
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00008DC0 File Offset: 0x00006FC0
		internal static bool IsHeapHandle(this HandleKind kind)
		{
			return kind >= HandleKind.NamespaceDefinition;
		}
	}
}
