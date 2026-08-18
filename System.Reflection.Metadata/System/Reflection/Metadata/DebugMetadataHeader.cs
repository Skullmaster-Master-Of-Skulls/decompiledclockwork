using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008A RID: 138
	public sealed class DebugMetadataHeader
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000ED76 File Offset: 0x0000CF76
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000ED7E File Offset: 0x0000CF7E
		public ImmutableArray<byte> Id { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000ED87 File Offset: 0x0000CF87
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000ED8F File Offset: 0x0000CF8F
		public MethodDefinitionHandle EntryPoint { get; private set; }

		// Token: 0x06000631 RID: 1585 RVA: 0x0000ED98 File Offset: 0x0000CF98
		internal DebugMetadataHeader(ImmutableArray<byte> id, MethodDefinitionHandle entryPoint)
		{
			this.Id = id;
			this.EntryPoint = entryPoint;
		}
	}
}
