using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x02000086 RID: 134
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct MemoryBlock
	{
		// Token: 0x06000341 RID: 833 RVA: 0x000081FC File Offset: 0x000063FC
		[SecurityCritical]
		internal unsafe MemoryBlock(byte* buffer, int length)
		{
			this.Pointer = buffer;
			this.Length = length;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000820C File Offset: 0x0000640C
		[SecurityCritical]
		internal unsafe static MemoryBlock CreateChecked(byte* buffer, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (buffer == null && length != 0)
			{
				throw new ArgumentNullException("buffer");
			}
			return new MemoryBlock(buffer, length);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008237 File Offset: 0x00006437
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckBounds(int offset, int byteCount)
		{
			if ((ulong)offset + (ulong)byteCount > (ulong)((long)this.Length))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000824C File Offset: 0x0000644C
		[SecuritySafeCritical]
		internal byte[] ToArray()
		{
			if (this.Pointer != null)
			{
				return this.PeekBytes(0, this.Length);
			}
			return null;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008268 File Offset: 0x00006468
		[SecuritySafeCritical]
		private string GetDebuggerDisplay()
		{
			if (this.Pointer == null)
			{
				return "<null>";
			}
			int num;
			return this.GetDebuggerDisplay(out num);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008290 File Offset: 0x00006490
		internal string GetDebuggerDisplay(out int displayedBytes)
		{
			displayedBytes = Math.Min(this.Length, 64);
			string text = BitConverter.ToString(this.PeekBytes(0, displayedBytes));
			if (displayedBytes < this.Length)
			{
				text += "-...";
			}
			return text;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000082D4 File Offset: 0x000064D4
		[SecuritySafeCritical]
		internal string GetDebuggerDisplay(int offset)
		{
			if (this.Pointer == null)
			{
				return "<null>";
			}
			int num;
			string text = this.GetDebuggerDisplay(out num);
			if (offset < num)
			{
				text = text.Insert(offset * 3, "*");
			}
			else if (num == this.Length)
			{
				text += "*";
			}
			else
			{
				text += "*...";
			}
			return text;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00008333 File Offset: 0x00006533
		[SecuritySafeCritical]
		internal MemoryBlock GetMemoryBlockAt(int offset, int length)
		{
			this.CheckBounds(offset, length);
			return new MemoryBlock(this.Pointer + offset, length);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000834B File Offset: 0x0000654B
		[SecuritySafeCritical]
		internal unsafe byte PeekByte(int offset)
		{
			this.CheckBounds(offset, 1);
			return this.Pointer[offset];
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00008360 File Offset: 0x00006560
		internal int PeekInt32(int offset)
		{
			uint num = this.PeekUInt32(offset);
			if ((long)num != (long)((ulong)num))
			{
				Throw.ValueOverflow();
			}
			return (int)num;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008384 File Offset: 0x00006584
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe uint PeekUInt32(int offset)
		{
			this.CheckBounds(offset, 4);
			byte* ptr = this.Pointer + offset;
			return (uint)((int)(*ptr) | (int)ptr[1] << 8 | (int)ptr[2] << 16 | (int)ptr[3] << 24);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000083BC File Offset: 0x000065BC
		[SecuritySafeCritical]
		internal unsafe int PeekCompressedInteger(int offset, out int numberOfBytesRead)
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			long num = (long)(this.Length - offset);
			if (num == 0L)
			{
				numberOfBytesRead = 0;
				return int.MaxValue;
			}
			byte b = *ptr;
			if ((b & 128) == 0)
			{
				numberOfBytesRead = 1;
				return (int)b;
			}
			if ((b & 64) == 0)
			{
				if (num >= 2L)
				{
					numberOfBytesRead = 2;
					return (int)(b & 63) << 8 | (int)ptr[1];
				}
			}
			else if ((b & 32) == 0 && num >= 4L)
			{
				numberOfBytesRead = 4;
				return (int)(b & 31) << 24 | (int)ptr[1] << 16 | (int)ptr[2] << 8 | (int)ptr[3];
			}
			numberOfBytesRead = 0;
			return int.MaxValue;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008450 File Offset: 0x00006650
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe ushort PeekUInt16(int offset)
		{
			this.CheckBounds(offset, 2);
			byte* ptr = this.Pointer + offset;
			return (ushort)((int)(*ptr) | (int)ptr[1] << 8);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008478 File Offset: 0x00006678
		internal uint PeekTaggedReference(int offset, bool smallRefSize)
		{
			return this.PeekReferenceUnchecked(offset, smallRefSize);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008482 File Offset: 0x00006682
		internal uint PeekReferenceUnchecked(int offset, bool smallRefSize)
		{
			if (!smallRefSize)
			{
				return this.PeekUInt32(offset);
			}
			return (uint)this.PeekUInt16(offset);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008498 File Offset: 0x00006698
		internal int PeekReference(int offset, bool smallRefSize)
		{
			if (smallRefSize)
			{
				return (int)this.PeekUInt16(offset);
			}
			uint num = this.PeekUInt32(offset);
			if (!TokenTypeIds.IsValidRowId(num))
			{
				Throw.ReferenceOverflow();
			}
			return (int)num;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000084C8 File Offset: 0x000066C8
		internal int PeekHeapReference(int offset, bool smallRefSize)
		{
			if (smallRefSize)
			{
				return (int)this.PeekUInt16(offset);
			}
			uint num = this.PeekUInt32(offset);
			if (!HeapHandleType.IsValidHeapOffset(num))
			{
				Throw.ReferenceOverflow();
			}
			return (int)num;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000084F8 File Offset: 0x000066F8
		[SecuritySafeCritical]
		internal unsafe Guid PeekGuid(int offset)
		{
			this.CheckBounds(offset, sizeof(Guid));
			byte* ptr = this.Pointer + offset;
			if (BitConverter.IsLittleEndian)
			{
				return *(Guid*)ptr;
			}
			return new Guid((int)(*ptr) | (int)ptr[1] << 8 | (int)ptr[2] << 16 | (int)ptr[3] << 24, (short)((int)ptr[4] | (int)ptr[5] << 8), (short)((int)ptr[6] | (int)ptr[7] << 8), ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008588 File Offset: 0x00006788
		[SecuritySafeCritical]
		internal unsafe string PeekUtf16(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			byte* ptr = this.Pointer + offset;
			if (BitConverter.IsLittleEndian)
			{
				return new string((char*)ptr, 0, byteCount / 2);
			}
			return Encoding.Unicode.GetString(ptr, byteCount);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000085C4 File Offset: 0x000067C4
		[SecuritySafeCritical]
		internal string PeekUtf8(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			return Encoding.UTF8.GetString(this.Pointer + offset, byteCount);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000085E4 File Offset: 0x000067E4
		[SecuritySafeCritical]
		internal unsafe string PeekUtf8NullTerminated(int offset, out int numberOfBytesRead, char terminator = '\0')
		{
			this.CheckBounds(offset, 0);
			int utf8NullTerminatedLength = this.GetUtf8NullTerminatedLength(offset, out numberOfBytesRead, terminator);
			return new string((sbyte*)this.Pointer, offset, utf8NullTerminatedLength, Encoding.UTF8);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008618 File Offset: 0x00006818
		[SecuritySafeCritical]
		internal unsafe int GetUtf8NullTerminatedLength(int offset, out int numberOfBytesRead, char terminator = '\0')
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			byte* ptr2 = this.Pointer + this.Length;
			byte* ptr3;
			for (ptr3 = ptr; ptr3 < ptr2; ptr3++)
			{
				byte b = *ptr3;
				if (b == 0 || (char)b == terminator)
				{
					break;
				}
			}
			int num = (int)((long)(ptr3 - ptr));
			numberOfBytesRead = num;
			if (ptr3 < ptr2)
			{
				numberOfBytesRead++;
			}
			return num;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00008673 File Offset: 0x00006873
		[SecuritySafeCritical]
		internal byte[] PeekBytes(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			return BlobUtilities.ReadBytes(this.Pointer + offset, byteCount);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000868B File Offset: 0x0000688B
		internal int IndexOf(byte b, int start)
		{
			this.CheckBounds(start, 0);
			return this.IndexOfUnchecked(b, start);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000086A0 File Offset: 0x000068A0
		[SecuritySafeCritical]
		internal unsafe int IndexOfUnchecked(byte b, int start)
		{
			byte* ptr = this.Pointer + start;
			byte* ptr2 = this.Pointer + this.Length;
			while (ptr < ptr2)
			{
				if (*ptr == b)
				{
					return (int)((long)(ptr - this.Pointer));
				}
				ptr++;
			}
			return -1;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000086E4 File Offset: 0x000068E4
		internal int BinarySearchReference(int rowCount, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall)
		{
			int i = 0;
			int num = rowCount - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				uint num3 = this.PeekReferenceUnchecked(num2 * rowSize + referenceOffset, isReferenceSmall);
				if (referenceValue > num3)
				{
					i = num2 + 1;
				}
				else
				{
					if (referenceValue >= num3)
					{
						return num2;
					}
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000872C File Offset: 0x0000692C
		internal int BinarySearchReference(int[] ptrTable, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall)
		{
			int i = 0;
			int num = ptrTable.Length - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				uint num3 = this.PeekReferenceUnchecked((ptrTable[num2] - 1) * rowSize + referenceOffset, isReferenceSmall);
				if (referenceValue > num3)
				{
					i = num2 + 1;
				}
				else
				{
					if (referenceValue >= num3)
					{
						return num2;
					}
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00008778 File Offset: 0x00006978
		internal int[] BuildPtrTable(int numberOfRows, int rowSize, int referenceOffset, bool isReferenceSmall)
		{
			int[] array = new int[numberOfRows];
			uint[] unsortedReferences = new uint[numberOfRows];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i + 1;
			}
			this.ReadColumn(unsortedReferences, rowSize, referenceOffset, isReferenceSmall);
			Array.Sort<int>(array, (int a, int b) => unsortedReferences[a - 1].CompareTo(unsortedReferences[b - 1]));
			return array;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000087D8 File Offset: 0x000069D8
		private void ReadColumn(uint[] result, int rowSize, int referenceOffset, bool isReferenceSmall)
		{
			int i = referenceOffset;
			int length = this.Length;
			int num = 0;
			while (i < length)
			{
				result[num] = this.PeekReferenceUnchecked(i, isReferenceSmall);
				i += rowSize;
				num++;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000880C File Offset: 0x00006A0C
		internal bool PeekHeapValueOffsetAndSize(int index, out int offset, out int size)
		{
			int num2;
			int num = this.PeekCompressedInteger(index, out num2);
			if (num == 2147483647)
			{
				offset = 0;
				size = 0;
				return false;
			}
			offset = index + num2;
			size = num;
			return true;
		}

		// Token: 0x04000493 RID: 1171
		[SecurityCritical]
		internal unsafe readonly byte* Pointer;

		// Token: 0x04000494 RID: 1172
		internal readonly int Length;
	}
}
