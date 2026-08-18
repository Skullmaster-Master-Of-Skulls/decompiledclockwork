using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x0200008D RID: 141
	internal static class StreamExtensions
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00008DFC File Offset: 0x00006FFC
		[SecurityCritical]
		internal unsafe static void CopyTo(this Stream source, byte* destination, int size)
		{
			byte[] array = new byte[Math.Min(81920, size)];
			while (size > 0)
			{
				int num = Math.Min(size, array.Length);
				int num2 = source.Read(array, 0, num);
				if (num2 <= 0 || num2 > num)
				{
					throw new IOException("UnexpectedStreamEnd");
				}
				Marshal.Copy(array, 0, (IntPtr)((void*)destination), num2);
				destination += num2;
				size -= num2;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00008E60 File Offset: 0x00007060
		internal static int TryReadAll(this Stream stream, byte[] buffer, int offset, int count)
		{
			int i;
			int num;
			for (i = 0; i < count; i += num)
			{
				num = stream.Read(buffer, offset + i, count - i);
				if (num == 0)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00008E90 File Offset: 0x00007090
		internal static int GetAndValidateSize(Stream stream, int size, string streamParameterName)
		{
			long num = stream.Length - stream.Position;
			if (size < 0 || (long)size > num)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size != 0)
			{
				return size;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentException("StreamTooLarge", streamParameterName);
			}
			return (int)num;
		}

		// Token: 0x040004A1 RID: 1185
		internal const int StreamCopyBufferSize = 81920;
	}
}
