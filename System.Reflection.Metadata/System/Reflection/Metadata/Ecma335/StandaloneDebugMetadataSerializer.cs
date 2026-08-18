using System;
using System.Collections.Immutable;
using System.Reflection.PortableExecutable;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B4 RID: 180
	internal sealed class StandaloneDebugMetadataSerializer : MetadataSerializer
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00010E60 File Offset: 0x0000F060
		public StandaloneDebugMetadataSerializer(MetadataBuilder builder, ImmutableArray<int> typeSystemRowCounts, MethodDefinitionHandle entryPoint, bool isMinimalDelta) : base(builder, MetadataSerializer.CreateSizes(builder, typeSystemRowCounts, isMinimalDelta, true), "PDB v1.0")
		{
			this._entryPoint = entryPoint;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00010E80 File Offset: 0x0000F080
		protected override void SerializeStandalonePdbStream(BlobBuilder builder)
		{
			int position = builder.Position;
			this._pdbIdBlob = builder.ReserveBytes(20);
			builder.WriteInt32(this._entryPoint.IsNil ? 0 : MetadataTokens.GetToken(this._entryPoint));
			builder.WriteUInt64(base.MetadataSizes.ExternalTablesMask);
			MetadataWriterUtilities.SerializeRowCounts(builder, base.MetadataSizes.ExternalRowCounts);
			int position2 = builder.Position;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public void SerializeMetadata(BlobBuilder builder, Func<BlobBuilder, ContentId> idProvider, out ContentId contentId)
		{
			base.SerializeMetadataImpl(builder, 0, 0);
			contentId = idProvider(builder);
			BlobWriter blobWriter = new BlobWriter(this._pdbIdBlob);
			blobWriter.WriteBytes(contentId.Guid);
			blobWriter.WriteBytes(contentId.Stamp);
		}

		// Token: 0x04000476 RID: 1142
		private const string DebugMetadataVersionString = "PDB v1.0";

		// Token: 0x04000477 RID: 1143
		private Blob _pdbIdBlob;

		// Token: 0x04000478 RID: 1144
		private readonly MethodDefinitionHandle _entryPoint;
	}
}
