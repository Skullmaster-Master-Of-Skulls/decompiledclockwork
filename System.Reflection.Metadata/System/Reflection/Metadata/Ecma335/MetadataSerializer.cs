using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B6 RID: 182
	internal abstract class MetadataSerializer
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00010F77 File Offset: 0x0000F177
		public MetadataSerializer(MetadataBuilder tables, MetadataSizes sizes, string metadataVersion)
		{
			this._tables = tables;
			this._sizes = sizes;
			this._metadataVersion = metadataVersion;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00010F94 File Offset: 0x0000F194
		internal static MetadataSizes CreateSizes(MetadataBuilder tables, ImmutableArray<int> externalRowCounts, bool isMinimalDelta, bool isStandaloneDebugMetadata)
		{
			tables.CompleteHeaps();
			return new MetadataSizes(tables.GetRowCounts(), externalRowCounts, tables.GetHeapSizes(), isMinimalDelta, isStandaloneDebugMetadata);
		}

		// Token: 0x06000781 RID: 1921
		protected abstract void SerializeStandalonePdbStream(BlobBuilder writer);

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public MetadataSizes MetadataSizes
		{
			get
			{
				return this._sizes;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		protected void SerializeMetadataImpl(BlobBuilder metadataWriter, int methodBodyStreamRva, int mappedFieldDataStreamRva)
		{
			this.SerializeMetadataHeader(metadataWriter);
			this.SerializeStandalonePdbStream(metadataWriter);
			this._tables.SerializeMetadataTables(metadataWriter, this._sizes, methodBodyStreamRva, mappedFieldDataStreamRva);
			this._tables.WriteHeapsTo(metadataWriter);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		private void SerializeMetadataHeader(BlobBuilder writer)
		{
			int position = writer.Position;
			writer.WriteUInt32(1112167234U);
			writer.WriteUInt16(1);
			writer.WriteUInt16(1);
			writer.WriteUInt32(0U);
			writer.WriteUInt32(12U);
			int num = Math.Min(12, this._metadataVersion.Length);
			for (int i = 0; i < num; i++)
			{
				writer.WriteByte((byte)this._metadataVersion[i]);
			}
			for (int j = num; j < 12; j++)
			{
				writer.WriteByte(0);
			}
			writer.WriteUInt16(0);
			writer.WriteUInt16((ushort)(5 + (this._sizes.IsMinimalDelta ? 1 : 0) + (this._sizes.IsStandaloneDebugMetadata ? 1 : 0)));
			int metadataHeaderSize = this._sizes.MetadataHeaderSize;
			if (this._sizes.IsStandaloneDebugMetadata)
			{
				MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, this._sizes.StandalonePdbStreamSize, "#Pdb", writer);
			}
			MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, this._sizes.MetadataTableStreamSize, this._sizes.IsMetadataTableStreamCompressed ? "#~" : "#-", writer);
			MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, this._sizes.GetAlignedHeapSize(HeapIndex.String), "#Strings", writer);
			MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, this._sizes.GetAlignedHeapSize(HeapIndex.UserString), "#US", writer);
			MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, this._sizes.GetAlignedHeapSize(HeapIndex.Guid), "#GUID", writer);
			MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, this._sizes.GetAlignedHeapSize(HeapIndex.Blob), "#Blob", writer);
			if (this._sizes.IsMinimalDelta)
			{
				MetadataSerializer.SerializeStreamHeader(ref metadataHeaderSize, 0, "#JTD", writer);
			}
			int position2 = writer.Position;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00011180 File Offset: 0x0000F380
		private static void SerializeStreamHeader(ref int offsetFromStartOfMetadata, int alignedStreamSize, string streamName, BlobBuilder writer)
		{
			int metadataStreamHeaderSize = MetadataSizes.GetMetadataStreamHeaderSize(streamName);
			writer.WriteInt32(offsetFromStartOfMetadata);
			writer.WriteInt32(alignedStreamSize);
			foreach (char c in streamName)
			{
				writer.WriteByte((byte)c);
			}
			uint num = (uint)(8 + streamName.Length);
			while ((ulong)num < (ulong)((long)metadataStreamHeaderSize))
			{
				writer.WriteByte(0);
				num += 1U;
			}
			offsetFromStartOfMetadata += alignedStreamSize;
		}

		// Token: 0x0400047A RID: 1146
		protected readonly MetadataBuilder _tables;

		// Token: 0x0400047B RID: 1147
		private readonly MetadataSizes _sizes;

		// Token: 0x0400047C RID: 1148
		private readonly string _metadataVersion;
	}
}
