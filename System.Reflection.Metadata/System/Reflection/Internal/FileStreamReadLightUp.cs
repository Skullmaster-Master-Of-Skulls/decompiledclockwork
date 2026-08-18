using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x0200015F RID: 351
	internal static class FileStreamReadLightUp
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001F214 File Offset: 0x0001D414
		internal static bool IsFileStream(Stream stream)
		{
			if (FileStreamReadLightUp.FileStreamType.Value == null)
			{
				return false;
			}
			Type type = stream.GetType();
			return type == FileStreamReadLightUp.FileStreamType.Value || type.GetTypeInfo().IsSubclassOf(FileStreamReadLightUp.FileStreamType.Value);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001F25C File Offset: 0x0001D45C
		internal static SafeHandle GetSafeFileHandle(Stream stream)
		{
			if (FileStreamReadLightUp.safeFileHandleNotAvailable)
			{
				return null;
			}
			PropertyInfo value = FileStreamReadLightUp.SafeFileHandle.Value;
			if (value == null)
			{
				FileStreamReadLightUp.safeFileHandleNotAvailable = true;
				return null;
			}
			SafeHandle safeHandle;
			try
			{
				safeHandle = (SafeHandle)value.GetValue(stream);
			}
			catch (MemberAccessException)
			{
				FileStreamReadLightUp.safeFileHandleNotAvailable = true;
				return null;
			}
			catch (InvalidOperationException)
			{
				FileStreamReadLightUp.safeFileHandleNotAvailable = true;
				return null;
			}
			catch (TargetInvocationException)
			{
				return null;
			}
			if (safeHandle != null && safeHandle.IsInvalid)
			{
				return null;
			}
			return safeHandle;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		internal unsafe static bool TryReadFile(Stream stream, byte* buffer, long start, int size)
		{
			if (FileStreamReadLightUp.readFileModernNotAvailable && FileStreamReadLightUp.readFileCompatNotAvailable)
			{
				return false;
			}
			SafeHandle safeFileHandle = FileStreamReadLightUp.GetSafeFileHandle(stream);
			if (safeFileHandle == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			if (!FileStreamReadLightUp.readFileModernNotAvailable)
			{
				try
				{
					flag = FileStreamReadLightUp.NativeMethods.ReadFileModern(safeFileHandle, buffer, size, out num, IntPtr.Zero);
				}
				catch
				{
					FileStreamReadLightUp.readFileModernNotAvailable = true;
				}
			}
			if (FileStreamReadLightUp.readFileModernNotAvailable)
			{
				try
				{
					flag = FileStreamReadLightUp.NativeMethods.ReadFileCompat(safeFileHandle, buffer, size, out num, IntPtr.Zero);
				}
				catch
				{
					FileStreamReadLightUp.readFileCompatNotAvailable = true;
					return false;
				}
			}
			return flag && num == size;
		}

		// Token: 0x0400090D RID: 2317
		internal static Lazy<Type> FileStreamType = new Lazy<Type>(() => LightUpHelper.GetType("System.IO.FileStream", new string[]
		{
			"System.IO.FileSystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken = b03f5f7f11d50a3a",
			"mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
		}));

		// Token: 0x0400090E RID: 2318
		internal static Lazy<PropertyInfo> SafeFileHandle = new Lazy<PropertyInfo>(() => FileStreamReadLightUp.FileStreamType.Value.GetTypeInfo().GetDeclaredProperty("SafeFileHandle"));

		// Token: 0x0400090F RID: 2319
		internal static bool readFileCompatNotAvailable;

		// Token: 0x04000910 RID: 2320
		internal static bool readFileModernNotAvailable;

		// Token: 0x04000911 RID: 2321
		internal static bool safeFileHandleNotAvailable;

		// Token: 0x020001DE RID: 478
		private static class NativeMethods
		{
			// Token: 0x06000C69 RID: 3177
			[DllImport("api-ms-win-core-file-l1-2-0.dll", EntryPoint = "ReadFile", ExactSpelling = true, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal unsafe static extern bool ReadFileModern(SafeHandle fileHandle, byte* buffer, int byteCount, out int bytesRead, IntPtr overlapped);

			// Token: 0x06000C6A RID: 3178
			[DllImport("kernel32.dll", EntryPoint = "ReadFile", ExactSpelling = true, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal unsafe static extern bool ReadFileCompat(SafeHandle fileHandle, byte* buffer, int byteCount, out int bytesRead, IntPtr overlapped);
		}
	}
}
