using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NLog.Targets;

namespace NLog.Internal
{
	// Token: 0x020000B9 RID: 185
	internal static class Win32FileNativeMethods
	{
		// Token: 0x06000581 RID: 1409
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeFileHandle CreateFile(string lpFileName, Win32FileNativeMethods.FileAccess dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, Win32FileNativeMethods.CreationDisposition dwCreationDisposition, Win32FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000582 RID: 1410
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetFileInformationByHandle(IntPtr hFile, out Win32FileNativeMethods.BY_HANDLE_FILE_INFORMATION lpFileInformation);

		// Token: 0x0400012A RID: 298
		public const int FILE_SHARE_READ = 1;

		// Token: 0x0400012B RID: 299
		public const int FILE_SHARE_WRITE = 2;

		// Token: 0x0400012C RID: 300
		public const int FILE_SHARE_DELETE = 4;

		// Token: 0x020000BA RID: 186
		[Flags]
		public enum FileAccess : uint
		{
			// Token: 0x0400012E RID: 302
			GenericRead = 2147483648U,
			// Token: 0x0400012F RID: 303
			GenericWrite = 1073741824U,
			// Token: 0x04000130 RID: 304
			GenericExecute = 536870912U,
			// Token: 0x04000131 RID: 305
			GenericAll = 268435456U
		}

		// Token: 0x020000BB RID: 187
		public enum CreationDisposition : uint
		{
			// Token: 0x04000133 RID: 307
			New = 1U,
			// Token: 0x04000134 RID: 308
			CreateAlways,
			// Token: 0x04000135 RID: 309
			OpenExisting,
			// Token: 0x04000136 RID: 310
			OpenAlways,
			// Token: 0x04000137 RID: 311
			TruncateExisting
		}

		// Token: 0x020000BC RID: 188
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BY_HANDLE_FILE_INFORMATION
		{
			// Token: 0x04000138 RID: 312
			public uint dwFileAttributes;

			// Token: 0x04000139 RID: 313
			public long ftCreationTime;

			// Token: 0x0400013A RID: 314
			public long ftLastAccessTime;

			// Token: 0x0400013B RID: 315
			public long ftLastWriteTime;

			// Token: 0x0400013C RID: 316
			public uint dwVolumeSerialNumber;

			// Token: 0x0400013D RID: 317
			public uint nFileSizeHigh;

			// Token: 0x0400013E RID: 318
			public uint nFileSizeLow;

			// Token: 0x0400013F RID: 319
			public uint nNumberOfLinks;

			// Token: 0x04000140 RID: 320
			public uint nFileIndexHigh;

			// Token: 0x04000141 RID: 321
			public uint nFileIndexLow;
		}
	}
}
