using System;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000CD RID: 205
	internal struct BlobStreamReader
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x00016D78 File Offset: 0x00014F78
		internal BlobStreamReader(MemoryBlock block, MetadataKind metadataKind)
		{
			this._lazyVirtualHeapBlobs = null;
			this.Block = block;
			if (BlobStreamReader.s_virtualHeapBlobs == null && metadataKind != MetadataKind.Ecma335)
			{
				BlobStreamReader.s_virtualHeapBlobs = new byte[][]
				{
					default(byte[]),
					new byte[]
					{
						176,
						63,
						95,
						127,
						17,
						213,
						10,
						58
					},
					new byte[]
					{
						0,
						36,
						0,
						0,
						4,
						128,
						0,
						0,
						148,
						0,
						0,
						0,
						6,
						2,
						0,
						0,
						0,
						36,
						0,
						0,
						82,
						83,
						65,
						49,
						0,
						4,
						0,
						0,
						1,
						0,
						1,
						0,
						7,
						209,
						250,
						87,
						196,
						174,
						217,
						240,
						163,
						46,
						132,
						170,
						15,
						174,
						253,
						13,
						233,
						232,
						253,
						106,
						236,
						143,
						135,
						251,
						3,
						118,
						108,
						131,
						76,
						153,
						146,
						30,
						178,
						59,
						231,
						154,
						217,
						213,
						220,
						193,
						221,
						154,
						210,
						54,
						19,
						33,
						2,
						144,
						11,
						114,
						60,
						249,
						128,
						149,
						127,
						196,
						225,
						119,
						16,
						143,
						198,
						7,
						119,
						79,
						41,
						232,
						50,
						14,
						146,
						234,
						5,
						236,
						228,
						232,
						33,
						192,
						165,
						239,
						232,
						241,
						100,
						92,
						76,
						12,
						147,
						193,
						171,
						153,
						40,
						93,
						98,
						44,
						170,
						101,
						44,
						29,
						250,
						214,
						61,
						116,
						93,
						111,
						45,
						229,
						241,
						126,
						94,
						175,
						15,
						196,
						150,
						61,
						38,
						28,
						138,
						18,
						67,
						101,
						24,
						32,
						109,
						192,
						147,
						52,
						77,
						90,
						210,
						147
					},
					new byte[]
					{
						1,
						0,
						0,
						0,
						0,
						0,
						1,
						0,
						84,
						2,
						13,
						65,
						108,
						108,
						111,
						119,
						77,
						117,
						108,
						116,
						105,
						112,
						108,
						101,
						0
					},
					new byte[]
					{
						1,
						0,
						0,
						0,
						0,
						0,
						1,
						0,
						84,
						2,
						13,
						65,
						108,
						108,
						111,
						119,
						77,
						117,
						108,
						116,
						105,
						112,
						108,
						101,
						1
					}
				};
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00016E00 File Offset: 0x00015000
		internal byte[] GetBytes(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				return this.GetVirtualBlobArray(handle, true);
			}
			int heapOffset = handle.GetHeapOffset();
			int num2;
			int num = this.Block.PeekCompressedInteger(heapOffset, out num2);
			if (num == 2147483647)
			{
				return EmptyArray<byte>.Instance;
			}
			return this.Block.PeekBytes(heapOffset + num2, num);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00016E5C File Offset: 0x0001505C
		internal unsafe MemoryBlock GetMemoryBlock(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				if (this._lazyVirtualHeapBlobs == null)
				{
					Interlocked.CompareExchange<BlobStreamReader.VirtualHeapBlobTable>(ref this._lazyVirtualHeapBlobs, new BlobStreamReader.VirtualHeapBlobTable(), null);
				}
				int virtualIndex = (int)handle.GetVirtualIndex();
				int length = BlobStreamReader.s_virtualHeapBlobs[virtualIndex].Length;
				BlobStreamReader.VirtualHeapBlobTable lazyVirtualHeapBlobs = this._lazyVirtualHeapBlobs;
				BlobStreamReader.VirtualHeapBlob virtualHeapBlob;
				lock (lazyVirtualHeapBlobs)
				{
					if (!this._lazyVirtualHeapBlobs.Table.TryGetValue(handle, out virtualHeapBlob))
					{
						virtualHeapBlob = new BlobStreamReader.VirtualHeapBlob(this.GetVirtualBlobArray(handle, false));
						this._lazyVirtualHeapBlobs.Table.Add(handle, virtualHeapBlob);
					}
				}
				return new MemoryBlock((byte*)((void*)virtualHeapBlob.Pinned.AddrOfPinnedObject()), length);
			}
			int offset;
			int length2;
			this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out offset, out length2);
			return this.Block.GetMemoryBlockAt(offset, length2);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00016F54 File Offset: 0x00015154
		internal BlobReader GetBlobReader(BlobHandle handle)
		{
			return new BlobReader(this.GetMemoryBlock(handle));
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00016F64 File Offset: 0x00015164
		internal BlobHandle GetNextHandle(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				return default(BlobHandle);
			}
			int num;
			int num2;
			if (!this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out num, out num2))
			{
				return default(BlobHandle);
			}
			int num3 = num + num2;
			if (num3 >= this.Block.Length)
			{
				return default(BlobHandle);
			}
			return BlobHandle.FromOffset(num3);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00016FCC File Offset: 0x000151CC
		internal byte[] GetVirtualBlobArray(BlobHandle handle, bool unique)
		{
			BlobHandle.VirtualIndex virtualIndex = handle.GetVirtualIndex();
			byte[] array = BlobStreamReader.s_virtualHeapBlobs[(int)virtualIndex];
			if (virtualIndex == BlobHandle.VirtualIndex.AttributeUsage_AllowSingle || virtualIndex == BlobHandle.VirtualIndex.AttributeUsage_AllowMultiple)
			{
				array = (byte[])array.Clone();
				handle.SubstituteTemplateParameters(array);
			}
			else if (unique)
			{
				array = (byte[])array.Clone();
			}
			return array;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00017018 File Offset: 0x00015218
		public string GetDocumentName(DocumentNameBlobHandle handle)
		{
			BlobReader blobReader = this.GetBlobReader(handle);
			int num = (int)blobReader.ReadByte();
			if (num > 127)
			{
				throw new BadImageFormatException(string.Format(SR.InvalidDocumentName, new object[]
				{
					num
				}));
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
				BlobReader blobReader2;
				builder.Append(this.GetBlobReader(blobReader.ReadBlobHandle()).ReadUTF8(blobReader2.Length));
				flag = false;
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000170B8 File Offset: 0x000152B8
		internal bool DocumentNameEquals(DocumentNameBlobHandle handle, string other, bool ignoreCase)
		{
			BlobReader blobReader = this.GetBlobReader(handle);
			int num = (int)blobReader.ReadByte();
			if (num > 127)
			{
				return false;
			}
			int ignoreCaseMask = StringUtils.IgnoreCaseMask(ignoreCase);
			int num2 = 0;
			bool flag = true;
			while (blobReader.RemainingBytes > 0)
			{
				if (num != 0 && !flag)
				{
					if (num2 == other.Length || !StringUtils.IsEqualAscii((int)other[num2], num, ignoreCaseMask))
					{
						return false;
					}
					num2++;
				}
				MemoryBlock memoryBlock = this.GetMemoryBlock(blobReader.ReadBlobHandle());
				int num3;
				MemoryBlock.FastComparisonResult fastComparisonResult = memoryBlock.Utf8NullTerminatedFastCompare(0, other, num2, out num3, '\0', ignoreCase);
				if (fastComparisonResult == MemoryBlock.FastComparisonResult.Inconclusive)
				{
					return this.GetDocumentName(handle).Equals(other, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
				}
				if (fastComparisonResult == MemoryBlock.FastComparisonResult.Unequal || num3 - num2 != memoryBlock.Length)
				{
					return false;
				}
				num2 = num3;
				flag = false;
			}
			return num2 == other.Length;
		}

		// Token: 0x040005AF RID: 1455
		private BlobStreamReader.VirtualHeapBlobTable _lazyVirtualHeapBlobs;

		// Token: 0x040005B0 RID: 1456
		private static byte[][] s_virtualHeapBlobs;

		// Token: 0x040005B1 RID: 1457
		internal readonly MemoryBlock Block;

		// Token: 0x020001D3 RID: 467
		private struct VirtualHeapBlob
		{
			// Token: 0x06000C51 RID: 3153 RVA: 0x0002252F File Offset: 0x0002072F
			public VirtualHeapBlob(byte[] array)
			{
				this.Pinned = GCHandle.Alloc(array, GCHandleType.Pinned);
				this.Array = array;
			}

			// Token: 0x04000B3C RID: 2876
			public readonly GCHandle Pinned;

			// Token: 0x04000B3D RID: 2877
			public readonly byte[] Array;
		}

		// Token: 0x020001D4 RID: 468
		private sealed class VirtualHeapBlobTable
		{
			// Token: 0x06000C52 RID: 3154 RVA: 0x00022545 File Offset: 0x00020745
			public VirtualHeapBlobTable()
			{
				this.Table = new Dictionary<BlobHandle, BlobStreamReader.VirtualHeapBlob>();
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x00022558 File Offset: 0x00020758
			protected override void Finalize()
			{
				try
				{
					if (this.Table != null)
					{
						foreach (BlobStreamReader.VirtualHeapBlob virtualHeapBlob in this.Table.Values)
						{
							virtualHeapBlob.Pinned.Free();
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x04000B3E RID: 2878
			public readonly Dictionary<BlobHandle, BlobStreamReader.VirtualHeapBlob> Table;
		}
	}
}
