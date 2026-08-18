using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000DE RID: 222
	internal sealed class NamespaceData
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x00017B72 File Offset: 0x00015D72
		public NamespaceData(StringHandle name, string fullName, NamespaceDefinitionHandle parent, ImmutableArray<NamespaceDefinitionHandle> namespaceDefinitions, ImmutableArray<TypeDefinitionHandle> typeDefinitions, ImmutableArray<ExportedTypeHandle> exportedTypes)
		{
			this.Name = name;
			this.FullName = fullName;
			this.Parent = parent;
			this.NamespaceDefinitions = namespaceDefinitions;
			this.TypeDefinitions = typeDefinitions;
			this.ExportedTypes = exportedTypes;
		}

		// Token: 0x04000689 RID: 1673
		public readonly StringHandle Name;

		// Token: 0x0400068A RID: 1674
		public readonly string FullName;

		// Token: 0x0400068B RID: 1675
		public readonly NamespaceDefinitionHandle Parent;

		// Token: 0x0400068C RID: 1676
		public readonly ImmutableArray<NamespaceDefinitionHandle> NamespaceDefinitions;

		// Token: 0x0400068D RID: 1677
		public readonly ImmutableArray<TypeDefinitionHandle> TypeDefinitions;

		// Token: 0x0400068E RID: 1678
		public readonly ImmutableArray<ExportedTypeHandle> ExportedTypes;
	}
}
