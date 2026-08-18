using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x02000164 RID: 356
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct MemoryBlock
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0001F684 File Offset: 0x0001D884
		internal unsafe MemoryBlock(byte* buffer, int length)
		{
			this.Pointer = buffer;
			this.Length = length;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001F694 File Offset: 0x0001D894
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
			if (!BitConverter.IsLittleEndian)
			{
				throw new PlatformNotSupportedException(SR.LitteEndianArchitectureRequired);
			}
			return new MemoryBlock(buffer, length);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001F6D1 File Offset: 0x0001D8D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckBounds(int offset, int byteCount)
		{
			if ((ulong)offset + (ulong)byteCount > (ulong)((long)this.Length))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001F6E6 File Offset: 0x0001D8E6
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ThrowValueOverflow()
		{
			throw new BadImageFormatException(SR.ValueTooLarge);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001F6F2 File Offset: 0x0001D8F2
		internal byte[] ToArray()
		{
			if (this.Pointer != null)
			{
				return this.PeekBytes(0, this.Length);
			}
			return null;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001F710 File Offset: 0x0001D910
		private string GetDebuggerDisplay()
		{
			if (this.Pointer == null)
			{
				return "<null>";
			}
			int num;
			return this.GetDebuggerDisplay(out num);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001F738 File Offset: 0x0001D938
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

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001F77C File Offset: 0x0001D97C
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

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001F7DB File Offset: 0x0001D9DB
		internal MemoryBlock GetMemoryBlockAt(int offset, int length)
		{
			this.CheckBounds(offset, length);
			return new MemoryBlock(this.Pointer + offset, length);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001F7F3 File Offset: 0x0001D9F3
		internal unsafe byte PeekByte(int offset)
		{
			this.CheckBounds(offset, 1);
			return this.Pointer[offset];
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001F808 File Offset: 0x0001DA08
		internal int PeekInt32(int offset)
		{
			uint num = this.PeekUInt32(offset);
			if ((long)num != (long)((ulong)num))
			{
				MemoryBlock.ThrowValueOverflow();
			}
			return (int)num;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001F829 File Offset: 0x0001DA29
		internal unsafe uint PeekUInt32(int offset)
		{
			this.CheckBounds(offset, 4);
			return *(uint*)(this.Pointer + offset);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001F83C File Offset: 0x0001DA3C
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

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001F8CD File Offset: 0x0001DACD
		internal unsafe ushort PeekUInt16(int offset)
		{
			this.CheckBounds(offset, 2);
			return *(ushort*)(this.Pointer + offset);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		internal uint PeekTaggedReference(int offset, bool smallRefSize)
		{
			return this.PeekReferenceUnchecked(offset, smallRefSize);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001F8EA File Offset: 0x0001DAEA
		internal uint PeekReferenceUnchecked(int offset, bool smallRefSize)
		{
			if (!smallRefSize)
			{
				return this.PeekUInt32(offset);
			}
			return (uint)this.PeekUInt16(offset);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001F8FE File Offset: 0x0001DAFE
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

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001F91F File Offset: 0x0001DB1F
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

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001F940 File Offset: 0x0001DB40
		internal unsafe Guid PeekGuid(int offset)
		{
			this.CheckBounds(offset, sizeof(Guid));
			return *(Guid*)(this.Pointer + offset);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001F95C File Offset: 0x0001DB5C
		internal unsafe string PeekUtf16(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			return new string((char*)(this.Pointer + offset), 0, byteCount / 2);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001F977 File Offset: 0x0001DB77
		internal string PeekUtf8(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			return Encoding.UTF8.GetString(this.Pointer + offset, byteCount);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001F994 File Offset: 0x0001DB94
		internal string PeekUtf8NullTerminated(int offset, byte[] prefix, MetadataStringDecoder utf8Decoder, out int numberOfBytesRead, char terminator = '\0')
		{
			this.CheckBounds(offset, 0);
			int utf8NullTerminatedLength = this.GetUtf8NullTerminatedLength(offset, out numberOfBytesRead, terminator);
			return EncodingHelper.DecodeUtf8(this.Pointer + offset, utf8NullTerminatedLength, prefix, utf8Decoder);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
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

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001FA24 File Offset: 0x0001DC24
		internal unsafe int Utf8NullTerminatedOffsetOfAsciiChar(int startOffset, char asciiChar)
		{
			this.CheckBounds(startOffset, 0);
			for (int i = startOffset; i < this.Length; i++)
			{
				byte b = this.Pointer[i];
				if (b == 0)
				{
					break;
				}
				if ((char)b == asciiChar)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001FA60 File Offset: 0x0001DC60
		internal bool Utf8NullTerminatedEquals(int offset, string text, MetadataStringDecoder utf8Decoder, char terminator, bool ignoreCase)
		{
			int num;
			MemoryBlock.FastComparisonResult fastComparisonResult = this.Utf8NullTerminatedFastCompare(offset, text, 0, out num, terminator, ignoreCase);
			if (fastComparisonResult == MemoryBlock.FastComparisonResult.Inconclusive)
			{
				int num2;
				return this.PeekUtf8NullTerminated(offset, null, utf8Decoder, out num2, terminator).Equals(text, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return fastComparisonResult == MemoryBlock.FastComparisonResult.Equal;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		internal bool Utf8NullTerminatedStartsWith(int offset, string text, MetadataStringDecoder utf8Decoder, char terminator, bool ignoreCase)
		{
			int num;
			switch (this.Utf8NullTerminatedFastCompare(offset, text, 0, out num, terminator, ignoreCase))
			{
			case MemoryBlock.FastComparisonResult.Equal:
			case MemoryBlock.FastComparisonResult.BytesStartWithText:
				return true;
			case MemoryBlock.FastComparisonResult.TextStartsWithBytes:
			case MemoryBlock.FastComparisonResult.Unequal:
				return false;
			default:
			{
				int num2;
				return this.PeekUtf8NullTerminated(offset, null, utf8Decoder, out num2, terminator).StartsWith(text, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001FAF8 File Offset: 0x0001DCF8
		internal unsafe MemoryBlock.FastComparisonResult Utf8NullTerminatedFastCompare(int offset, string text, int textStart, out int firstDifferenceIndex, char terminator, bool ignoreCase)
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			byte* ptr2 = this.Pointer + this.Length;
			byte* ptr3 = ptr;
			int ignoreCaseMask = StringUtils.IgnoreCaseMask(ignoreCase);
			int num = textStart;
			while (num < text.Length && ptr3 != ptr2)
			{
				byte b = *ptr3;
				if (b == 0 || (char)b == terminator)
				{
					break;
				}
				char c = text[num];
				if ((b & 128) == 0 && StringUtils.IsEqualAscii((int)c, (int)b, ignoreCaseMask))
				{
					num++;
					ptr3++;
				}
				else
				{
					firstDifferenceIndex = num;
					if (c <= '\u007f')
					{
						return MemoryBlock.FastComparisonResult.Unequal;
					}
					return MemoryBlock.FastComparisonResult.Inconclusive;
				}
			}
			firstDifferenceIndex = num;
			bool flag = num == text.Length;
			bool flag2 = ptr3 == ptr2 || *ptr3 == 0 || (char)(*ptr3) == terminator;
			if (flag && flag2)
			{
				return MemoryBlock.FastComparisonResult.Equal;
			}
			if (!flag)
			{
				return MemoryBlock.FastComparisonResult.TextStartsWithBytes;
			}
			return MemoryBlock.FastComparisonResult.BytesStartWithText;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		internal unsafe bool Utf8NullTerminatedStringStartsWithAsciiPrefix(int offset, string asciiPrefix)
		{
			this.CheckBounds(offset, 0);
			if (asciiPrefix.Length > this.Length - offset)
			{
				return false;
			}
			byte* ptr = this.Pointer + offset;
			for (int i = 0; i < asciiPrefix.Length; i++)
			{
				if (asciiPrefix[i] != (char)(*ptr))
				{
					return false;
				}
				ptr++;
			}
			return true;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001FC08 File Offset: 0x0001DE08
		internal unsafe int CompareUtf8NullTerminatedStringWithAsciiString(int offset, string asciiString)
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			int num = this.Length - offset;
			for (int i = 0; i < asciiString.Length; i++)
			{
				if (i > num)
				{
					return -1;
				}
				if ((char)(*ptr) != asciiString[i])
				{
					return (int)((char)(*ptr) - asciiString[i]);
				}
				ptr++;
			}
			if (*ptr != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001FC68 File Offset: 0x0001DE68
		internal unsafe byte[] PeekBytes(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			if (byteCount == 0)
			{
				return EmptyArray<byte>.Instance;
			}
			byte[] array = new byte[byteCount];
			Marshal.Copy((IntPtr)((void*)(this.Pointer + offset)), array, 0, byteCount);
			return array;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		internal unsafe int IndexOf(byte b, int start)
		{
			this.CheckBounds(start, 0);
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

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		internal int BinarySearch(string[] asciiKeys, int offset)
		{
			int i = 0;
			int num = asciiKeys.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				string asciiString = asciiKeys[num2];
				int num3 = this.CompareUtf8NullTerminatedStringWithAsciiString(offset, asciiString);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return ~i;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001FD38 File Offset: 0x0001DF38
		internal int BinarySearchForSlot(int rowCount, int rowSize, int referenceListOffset, uint referenceValue, bool isReferenceSmall)
		{
			int num = 0;
			int num2 = rowCount - 1;
			uint num3 = this.PeekReferenceUnchecked(num * rowSize + referenceListOffset, isReferenceSmall);
			uint num4 = this.PeekReferenceUnchecked(num2 * rowSize + referenceListOffset, isReferenceSmall);
			if (num2 != 1)
			{
				while (num2 - num > 1)
				{
					if (referenceValue <= num3)
					{
						if (referenceValue != num3)
						{
							return num - 1;
						}
						return num;
					}
					else if (referenceValue >= num4)
					{
						if (referenceValue != num4)
						{
							return num2 + 1;
						}
						return num2;
					}
					else
					{
						int num5 = (num + num2) / 2;
						uint num6 = this.PeekReferenceUnchecked(num5 * rowSize + referenceListOffset, isReferenceSmall);
						if (referenceValue > num6)
						{
							num = num5;
							num3 = num6;
						}
						else
						{
							if (referenceValue >= num6)
							{
								return num5;
							}
							num2 = num5;
							num4 = num6;
						}
					}
				}
				return num;
			}
			if (referenceValue >= num4)
			{
				return num2;
			}
			return num;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
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

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001FE1C File Offset: 0x0001E01C
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

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001FE68 File Offset: 0x0001E068
		internal void BinarySearchReferenceRange(int rowCount, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall, out int startRowNumber, out int endRowNumber)
		{
			int num = this.BinarySearchReference(rowCount, rowSize, referenceOffset, referenceValue, isReferenceSmall);
			if (num == -1)
			{
				startRowNumber = -1;
				endRowNumber = -1;
				return;
			}
			startRowNumber = num;
			while (startRowNumber > 0 && this.PeekReferenceUnchecked((startRowNumber - 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				startRowNumber--;
			}
			endRowNumber = num;
			while (endRowNumber + 1 < rowCount && this.PeekReferenceUnchecked((endRowNumber + 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				endRowNumber++;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		internal void BinarySearchReferenceRange(int[] ptrTable, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall, out int startRowNumber, out int endRowNumber)
		{
			int num = this.BinarySearchReference(ptrTable, rowSize, referenceOffset, referenceValue, isReferenceSmall);
			if (num == -1)
			{
				startRowNumber = -1;
				endRowNumber = -1;
				return;
			}
			startRowNumber = num;
			while (startRowNumber > 0 && this.PeekReferenceUnchecked((ptrTable[startRowNumber - 1] - 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				startRowNumber--;
			}
			endRowNumber = num;
			while (endRowNumber + 1 < ptrTable.Length && this.PeekReferenceUnchecked((ptrTable[endRowNumber + 1] - 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				endRowNumber++;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001FF68 File Offset: 0x0001E168
		internal int LinearSearchReference(int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall)
		{
			int i = referenceOffset;
			int length = this.Length;
			while (i < length)
			{
				if (this.PeekReferenceUnchecked(i, isReferenceSmall) == referenceValue)
				{
					return i / rowSize;
				}
				i += rowSize;
			}
			return -1;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001FF9C File Offset: 0x0001E19C
		internal bool IsOrderedByReferenceAscending(int rowSize, int referenceOffset, bool isReferenceSmall)
		{
			int i = referenceOffset;
			int length = this.Length;
			uint num = 0U;
			while (i < length)
			{
				uint num2 = this.PeekReferenceUnchecked(i, isReferenceSmall);
				if (num2 < num)
				{
					return false;
				}
				num = num2;
				i += rowSize;
			}
			return true;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
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

		// Token: 0x06000B2A RID: 2858 RVA: 0x00020030 File Offset: 0x0001E230
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

		// Token: 0x06000B2B RID: 2859 RVA: 0x00020064 File Offset: 0x0001E264
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

		// Token: 0x04000916 RID: 2326
		internal unsafe readonly byte* Pointer;

		// Token: 0x04000917 RID: 2327
		internal readonly int Length;

		// Token: 0x020001E1 RID: 481
		internal enum FastComparisonResult
		{
			// Token: 0x04000B55 RID: 2901
			Equal,
			// Token: 0x04000B56 RID: 2902
			BytesStartWithText,
			// Token: 0x04000B57 RID: 2903
			TextStartsWithBytes,
			// Token: 0x04000B58 RID: 2904
			Unequal,
			// Token: 0x04000B59 RID: 2905
			Inconclusive
		}
	}
}
