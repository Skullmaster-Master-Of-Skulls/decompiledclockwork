using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000009 RID: 9
	internal static class Throw
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004409 File Offset: 0x00002609
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void HeapHandleRequired()
		{
			throw new ArgumentException(SR.NotMetadataHeapHandle, "handle");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000441A File Offset: 0x0000261A
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void EntityOrUserStringHandleRequired()
		{
			throw new ArgumentException(SR.NotMetadataTableOrUserStringHandle, "handle");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000442B File Offset: 0x0000262B
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidToken()
		{
			throw new ArgumentException(SR.InvalidToken, "token");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000443C File Offset: 0x0000263C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ValueArgumentNull()
		{
			throw new ArgumentNullException("value");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004448 File Offset: 0x00002648
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void IndexOutOfRange()
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004454 File Offset: 0x00002654
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TableIndexOutOfRange()
		{
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004460 File Offset: 0x00002660
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void OutOfBounds()
		{
			throw new BadImageFormatException(SR.OutOfBoundsRead);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000446C File Offset: 0x0000266C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidCodedIndex()
		{
			throw new BadImageFormatException(SR.InvalidCodedIndex);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004478 File Offset: 0x00002678
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidHandle()
		{
			throw new BadImageFormatException(SR.InvalidHandle);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004484 File Offset: 0x00002684
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidCompressedInteger()
		{
			throw new BadImageFormatException(SR.InvalidCompressedInteger);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004490 File Offset: 0x00002690
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidSerializedString()
		{
			throw new BadImageFormatException(SR.InvalidSerializedString);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000449C File Offset: 0x0000269C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ImageTooSmall()
		{
			throw new BadImageFormatException(SR.ImageTooSmall);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000044A8 File Offset: 0x000026A8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ImageTooSmallOrContainsInvalidOffsetOrCount()
		{
			throw new BadImageFormatException(SR.ImageTooSmallOrContainsInvalidOffsetOrCount);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000044B4 File Offset: 0x000026B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ReferenceOverflow()
		{
			throw new BadImageFormatException(SR.RowIdOrHeapOffsetTooLarge);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000044C0 File Offset: 0x000026C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TableNotSorted(TableIndex tableIndex)
		{
			throw new BadImageFormatException(SR.Format(SR.MetadataTableNotSorted, (int)tableIndex));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000044D7 File Offset: 0x000026D7
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TooManySubnamespaces()
		{
			throw new BadImageFormatException(SR.TooManySubnamespaces);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000044E3 File Offset: 0x000026E3
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidCast()
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000044EA File Offset: 0x000026EA
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void SequencePointValueOutOfRange()
		{
			throw new BadImageFormatException(SR.SequencePointValueOutOfRange);
		}
	}
}
