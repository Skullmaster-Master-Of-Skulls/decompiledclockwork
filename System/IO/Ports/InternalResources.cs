using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	// Token: 0x020007AA RID: 1962
	internal static class InternalResources
	{
		// Token: 0x06003C49 RID: 15433 RVA: 0x00101888 File Offset: 0x00100888
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("IO_EOF_ReadBeyondEOF"));
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x0010189C File Offset: 0x0010089C
		internal static string GetMessage(int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			int num = SafeNativeMethods.FormatMessage(12800, new HandleRef(null, IntPtr.Zero), errorCode, 0, stringBuilder, stringBuilder.Capacity, IntPtr.Zero);
			if (num != 0)
			{
				return stringBuilder.ToString();
			}
			return SR.GetString("IO_UnknownError", new object[]
			{
				errorCode
			});
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x001018FF File Offset: 0x001008FF
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("Port_not_open"));
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00101911 File Offset: 0x00100911
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("Arg_WrongAsyncResult"));
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x00101922 File Offset: 0x00100922
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndReadCalledMultiple"));
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x00101933 File Offset: 0x00100933
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndWriteCalledMultiple"));
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x00101944 File Offset: 0x00100944
		internal static void WinIOError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			InternalResources.WinIOError(lastWin32Error, string.Empty);
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x00101964 File Offset: 0x00100964
		internal static void WinIOError(string str)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			InternalResources.WinIOError(lastWin32Error, str);
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x00101980 File Offset: 0x00100980
		internal static void WinIOError(int errorCode, string str)
		{
			switch (errorCode)
			{
			case 2:
			case 3:
				if (str.Length == 0)
				{
					throw new IOException(SR.GetString("IO_PortNotFound"));
				}
				throw new IOException(SR.GetString("IO_PortNotFoundFileName", new object[]
				{
					str
				}));
			case 4:
				break;
			case 5:
				if (str.Length == 0)
				{
					throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_NoPathName"));
				}
				throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_Path", new object[]
				{
					str
				}));
			default:
				if (errorCode != 32)
				{
					if (errorCode == 206)
					{
						throw new PathTooLongException(SR.GetString("IO_PathTooLong"));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(SR.GetString("IO_SharingViolation_NoFileName"));
					}
					throw new IOException(SR.GetString("IO_SharingViolation_File", new object[]
					{
						str
					}));
				}
				break;
			}
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00101A79 File Offset: 0x00100A79
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
