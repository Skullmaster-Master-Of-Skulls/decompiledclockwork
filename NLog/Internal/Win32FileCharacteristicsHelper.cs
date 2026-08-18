using System;

namespace NLog.Internal
{
	// Token: 0x020000B8 RID: 184
	internal class Win32FileCharacteristicsHelper : FileCharacteristicsHelper
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0000C720 File Offset: 0x0000A920
		public override FileCharacteristics GetFileCharacteristics(string fileName, IntPtr fileHandle)
		{
			Win32FileNativeMethods.BY_HANDLE_FILE_INFORMATION by_HANDLE_FILE_INFORMATION;
			if (Win32FileNativeMethods.GetFileInformationByHandle(fileHandle, out by_HANDLE_FILE_INFORMATION))
			{
				return new FileCharacteristics(DateTime.FromFileTimeUtc(by_HANDLE_FILE_INFORMATION.ftCreationTime), DateTime.FromFileTimeUtc(by_HANDLE_FILE_INFORMATION.ftLastWriteTime), (long)((ulong)by_HANDLE_FILE_INFORMATION.nFileSizeLow + ((ulong)by_HANDLE_FILE_INFORMATION.nFileSizeHigh << 32)));
			}
			return null;
		}
	}
}
