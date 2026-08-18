using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x0200003A RID: 58
	internal static class BlobUtilities
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00003F40 File Offset: 0x00002140
		[SecuritySafeCritical]
		public unsafe static byte[] ReadBytes(byte* buffer, int byteCount)
		{
			if (byteCount == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[byteCount];
			Marshal.Copy((IntPtr)((void*)buffer), array, 0, byteCount);
			return array;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003F70 File Offset: 0x00002170
		[SecuritySafeCritical]
		public unsafe static ImmutableArray<byte> ReadImmutableBytes(byte* buffer, int byteCount)
		{
			byte[] array = BlobUtilities.ReadBytes(buffer, byteCount);
			return new ImmutableArray<byte>(array);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003F8B File Offset: 0x0000218B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void ValidateRange(int bufferLength, int start, int byteCount, string byteCountParameterName)
		{
			if (start < 0 || start > bufferLength)
			{
				Throw.ArgumentOutOfRange("start");
			}
			if (byteCount < 0 || byteCount > bufferLength - start)
			{
				Throw.ArgumentOutOfRange(byteCountParameterName);
			}
		}

		// Token: 0x040001F7 RID: 503
		public const int SizeOfGuid = 16;
	}
}
