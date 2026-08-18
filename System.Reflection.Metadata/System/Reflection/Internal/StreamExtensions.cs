using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000169 RID: 361
	internal static class StreamExtensions
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x00020894 File Offset: 0x0001EA94
		internal unsafe static void CopyTo(this Stream source, byte* destination, int size)
		{
			byte[] array = new byte[Math.Min(81920, size)];
			while (size > 0)
			{
				int num = Math.Min(size, array.Length);
				int num2 = source.Read(array, 0, num);
				if (num2 <= 0 || num2 > num)
				{
					throw new IOException(SR.UnexpectedStreamEnd);
				}
				Marshal.Copy(array, 0, (IntPtr)((void*)destination), num2);
				destination += num2;
				size -= num2;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000208F8 File Offset: 0x0001EAF8
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

		// Token: 0x04000931 RID: 2353
		internal const int StreamCopyBufferSize = 81920;
	}
}
