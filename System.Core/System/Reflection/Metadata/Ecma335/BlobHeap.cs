using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Text;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200006A RID: 106
	internal struct BlobHeap
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00007935 File Offset: 0x00005B35
		internal BlobHeap(MemoryBlock block, MetadataKind metadataKind)
		{
			this.Block = block;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00007940 File Offset: 0x00005B40
		internal byte[] GetBytes(BlobHandle handle)
		{
			int heapOffset = handle.GetHeapOffset();
			int num2;
			int num = this.Block.PeekCompressedInteger(heapOffset, out num2);
			if (num == 2147483647)
			{
				return ImmutableArray<byte>.Empty.UnderlyingArray;
			}
			return this.Block.PeekBytes(heapOffset + num2, num);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000798C File Offset: 0x00005B8C
		internal MemoryBlock GetMemoryBlock(BlobHandle handle)
		{
			int offset;
			int length;
			this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out offset, out length);
			return this.Block.GetMemoryBlockAt(offset, length);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000079C3 File Offset: 0x00005BC3
		internal BlobReader GetBlobReader(BlobHandle handle)
		{
			return new BlobReader(this.GetMemoryBlock(handle));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000079D4 File Offset: 0x00005BD4
		public string GetDocumentName(DocumentNameBlobHandle handle)
		{
			BlobReader blobReader = this.GetBlobReader(handle);
			int num = (int)blobReader.ReadByte();
			if (num > 127)
			{
				throw new BadImageFormatException("InvalidDocumentName");
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			bool flag = true;
			while (blobReader.RemainingBytes > 0)
			{
				if (num != 0 && !flag)
				{
					builder.Append((char)num);
				}
				BlobReader blobReader2 = this.GetBlobReader(blobReader.ReadBlobHandle());
				builder.Append(blobReader2.ReadUTF8(blobReader2.Length));
				flag = false;
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x0400039F RID: 927
		internal readonly MemoryBlock Block;
	}
}
