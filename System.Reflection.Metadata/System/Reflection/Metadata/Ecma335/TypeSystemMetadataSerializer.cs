using System;
using System.Collections.Immutable;
using System.Linq;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B5 RID: 181
	internal sealed class TypeSystemMetadataSerializer : MetadataSerializer
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00010F3E File Offset: 0x0000F13E
		public TypeSystemMetadataSerializer(MetadataBuilder tables, string metadataVersion, bool isMinimalDelta) : base(tables, MetadataSerializer.CreateSizes(tables, TypeSystemMetadataSerializer.EmptyRowCounts, isMinimalDelta, false), metadataVersion)
		{
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000031EB File Offset: 0x000013EB
		protected override void SerializeStandalonePdbStream(BlobBuilder writer)
		{
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00010F55 File Offset: 0x0000F155
		public void SerializeMetadata(BlobBuilder metadataWriter, int methodBodyStreamRva, int mappedFieldDataStreamRva)
		{
			base.SerializeMetadataImpl(metadataWriter, methodBodyStreamRva, mappedFieldDataStreamRva);
		}

		// Token: 0x04000479 RID: 1145
		private static readonly ImmutableArray<int> EmptyRowCounts = ImmutableArray.CreateRange<int>(Enumerable.Repeat<int>(0, MetadataTokens.TableCount));
	}
}
