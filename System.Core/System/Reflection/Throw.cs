using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200003B RID: 59
	internal static class Throw
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00003FAF File Offset: 0x000021AF
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidCast()
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003FB6 File Offset: 0x000021B6
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void LitteEndianArchitectureRequired()
		{
			throw new PlatformNotSupportedException("LitteEndianArchitectureRequired");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003FC2 File Offset: 0x000021C2
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidArgument(string message, string parameterName)
		{
			throw new ArgumentException(message, parameterName);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003FCB File Offset: 0x000021CB
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidOperation(string message)
		{
			throw new InvalidOperationException(message);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003FD3 File Offset: 0x000021D3
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void HeapHandleRequired()
		{
			throw new ArgumentException("NotMetadataHeapHandle", "handle");
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003FE4 File Offset: 0x000021E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void EntityOrUserStringHandleRequired()
		{
			throw new ArgumentException("NotMetadataTableOrUserStringHandle", "handle");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003FF5 File Offset: 0x000021F5
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidToken()
		{
			throw new ArgumentException("InvalidToken", "token");
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004006 File Offset: 0x00002206
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ArgumentNull(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000400E File Offset: 0x0000220E
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ValueArgumentNull()
		{
			throw new ArgumentNullException("value");
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000401A File Offset: 0x0000221A
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void BuilderArgumentNull()
		{
			throw new ArgumentNullException("builder");
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004026 File Offset: 0x00002226
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ArgumentOutOfRange(string parameterName)
		{
			throw new ArgumentOutOfRangeException(parameterName);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000402E File Offset: 0x0000222E
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ArgumentOutOfRange(string parameterName, string message)
		{
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004037 File Offset: 0x00002237
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void IndexOutOfRange()
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004043 File Offset: 0x00002243
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TableIndexOutOfRange()
		{
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000404F File Offset: 0x0000224F
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ValueArgumentOutOfRange()
		{
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000405B File Offset: 0x0000225B
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void OutOfBounds()
		{
			throw new BadImageFormatException("OutOfBoundsRead");
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004067 File Offset: 0x00002267
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void WriteOutOfBounds()
		{
			throw new InvalidOperationException("OutOfBoundsWrite");
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004073 File Offset: 0x00002273
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidCodedIndex()
		{
			throw new BadImageFormatException("InvalidCodedIndex");
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000407F File Offset: 0x0000227F
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidHandle()
		{
			throw new BadImageFormatException("InvalidHandle");
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000408B File Offset: 0x0000228B
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidCompressedInteger()
		{
			throw new BadImageFormatException("InvalidCompressedInteger");
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004097 File Offset: 0x00002297
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ImageTooSmall()
		{
			throw new BadImageFormatException("ImageTooSmall");
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000040A3 File Offset: 0x000022A3
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ImageTooSmallOrContainsInvalidOffsetOrCount()
		{
			throw new BadImageFormatException("ImageTooSmallOrContainsInvalidOffsetOrCount");
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000040AF File Offset: 0x000022AF
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ReferenceOverflow()
		{
			throw new BadImageFormatException("RowIdOrHeapOffsetTooLarge");
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000040BB File Offset: 0x000022BB
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TableNotSorted(TableIndex tableIndex)
		{
			throw new BadImageFormatException("MetadataTableNotSorted");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000040C7 File Offset: 0x000022C7
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InvalidOperation_PEImageNotAvailable()
		{
			throw new InvalidOperationException("PEImageNotAvailable");
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000040D3 File Offset: 0x000022D3
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ValueOverflow()
		{
			throw new BadImageFormatException("ValueTooLarge");
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000040DF File Offset: 0x000022DF
		internal static void SequencePointValueOutOfRange()
		{
			throw new BadImageFormatException("SequencePointValueOutOfRange");
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000040EB File Offset: 0x000022EB
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void PEReaderDisposed()
		{
			throw new ObjectDisposedException("PEReader");
		}
	}
}
