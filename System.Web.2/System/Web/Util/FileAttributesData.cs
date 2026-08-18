using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web.Util
{
	// Token: 0x02000202 RID: 514
	internal sealed class FileAttributesData
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x0004E3AA File Offset: 0x0004C5AA
		internal static FileAttributesData NonExistantAttributesData
		{
			get
			{
				return new FileAttributesData();
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
		internal static int GetFileAttributes(string path, out FileAttributesData fad)
		{
			fad = null;
			UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA;
			if (!UnsafeNativeMethods.GetFileAttributesEx(path, 0, out win32_FILE_ATTRIBUTE_DATA))
			{
				return HttpException.HResultFromLastError(Marshal.GetLastWin32Error());
			}
			fad = new FileAttributesData(ref win32_FILE_ATTRIBUTE_DATA);
			return 0;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0004E3E4 File Offset: 0x0004C5E4
		private FileAttributesData()
		{
			this.FileSize = -1L;
			this.UtcCreationTime = new DateTime(0L, DateTimeKind.Utc);
			this.UtcLastAccessTime = new DateTime(0L, DateTimeKind.Utc);
			this.UtcLastWriteTime = new DateTime(0L, DateTimeKind.Utc);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0004E420 File Offset: 0x0004C620
		private FileAttributesData(ref UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA data)
		{
			this.FileAttributes = (FileAttributes)data.fileAttributes;
			this.UtcCreationTime = DateTimeUtil.FromFileTimeToUtc((long)((ulong)data.ftCreationTimeHigh << 32 | (ulong)data.ftCreationTimeLow));
			this.UtcLastAccessTime = DateTimeUtil.FromFileTimeToUtc((long)((ulong)data.ftLastAccessTimeHigh << 32 | (ulong)data.ftLastAccessTimeLow));
			this.UtcLastWriteTime = DateTimeUtil.FromFileTimeToUtc((long)((ulong)data.ftLastWriteTimeHigh << 32 | (ulong)data.ftLastWriteTimeLow));
			this.FileSize = (long)((ulong)data.fileSizeHigh << 32 | (ulong)data.fileSizeLow);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		internal FileAttributesData(ref UnsafeNativeMethods.WIN32_FIND_DATA wfd)
		{
			this.FileAttributes = (FileAttributes)wfd.dwFileAttributes;
			this.UtcCreationTime = DateTimeUtil.FromFileTimeToUtc((long)((ulong)wfd.ftCreationTime_dwHighDateTime << 32 | (ulong)wfd.ftCreationTime_dwLowDateTime));
			this.UtcLastAccessTime = DateTimeUtil.FromFileTimeToUtc((long)((ulong)wfd.ftLastAccessTime_dwHighDateTime << 32 | (ulong)wfd.ftLastAccessTime_dwLowDateTime));
			this.UtcLastWriteTime = DateTimeUtil.FromFileTimeToUtc((long)((ulong)wfd.ftLastWriteTime_dwHighDateTime << 32 | (ulong)wfd.ftLastWriteTime_dwLowDateTime));
			this.FileSize = (long)((ulong)wfd.nFileSizeHigh << 32 | (ulong)wfd.nFileSizeLow);
		}

		// Token: 0x040017B6 RID: 6070
		internal readonly FileAttributes FileAttributes;

		// Token: 0x040017B7 RID: 6071
		internal readonly DateTime UtcCreationTime;

		// Token: 0x040017B8 RID: 6072
		internal readonly DateTime UtcLastAccessTime;

		// Token: 0x040017B9 RID: 6073
		internal readonly DateTime UtcLastWriteTime;

		// Token: 0x040017BA RID: 6074
		internal readonly long FileSize;
	}
}
