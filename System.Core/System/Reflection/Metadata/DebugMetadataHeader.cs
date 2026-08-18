using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005D RID: 93
	internal sealed class DebugMetadataHeader
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000071E8 File Offset: 0x000053E8
		public ImmutableArray<byte> Id { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000071F0 File Offset: 0x000053F0
		public MethodDefinitionHandle EntryPoint { get; }

		// Token: 0x0600029C RID: 668 RVA: 0x000071F8 File Offset: 0x000053F8
		internal DebugMetadataHeader(ImmutableArray<byte> id, MethodDefinitionHandle entryPoint)
		{
			this.Id = id;
			this.EntryPoint = entryPoint;
		}
	}
}
