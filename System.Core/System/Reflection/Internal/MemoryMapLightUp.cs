using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x02000087 RID: 135
	internal static class MemoryMapLightUp
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000883C File Offset: 0x00006A3C
		internal static bool IsAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000883F File Offset: 0x00006A3F
		internal static IDisposable CreateMemoryMap(Stream stream)
		{
			return MemoryMappedFile.CreateFromFile((FileStream)stream, null, 0L, MemoryMappedFileAccess.Read, HandleInheritability.None, true);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00008854 File Offset: 0x00006A54
		internal static IDisposable CreateViewAccessor(object memoryMap, long start, int size)
		{
			IDisposable result;
			try
			{
				result = ((MemoryMappedFile)memoryMap).CreateViewAccessor(start, (long)size, MemoryMappedFileAccess.Read);
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new IOException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00008894 File Offset: 0x00006A94
		[SecurityCritical]
		internal static bool TryGetSafeBufferAndPointerOffset(object accessor, out SafeBuffer safeBuffer, out long offset)
		{
			MemoryMappedViewAccessor memoryMappedViewAccessor = (MemoryMappedViewAccessor)accessor;
			safeBuffer = memoryMappedViewAccessor.SafeMemoryMappedViewHandle;
			offset = memoryMappedViewAccessor.PointerOffset;
			return true;
		}
	}
}
