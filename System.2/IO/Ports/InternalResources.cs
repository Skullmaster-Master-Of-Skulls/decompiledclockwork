using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	// Token: 0x02000409 RID: 1033
	internal static class InternalResources
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x000B22F9 File Offset: 0x000B04F9
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("IO_EOF_ReadBeyondEOF"));
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000B230C File Offset: 0x000B050C
		internal static string GetMessage(int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			int num = SafeNativeMethods.FormatMessage(12800, IntPtr.Zero, (uint)errorCode, 0, stringBuilder, stringBuilder.Capacity, null);
			if (num != 0)
			{
				return stringBuilder.ToString();
			}
			return SR.GetString("IO_UnknownError", new object[]
			{
				errorCode
			});
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000B2363 File Offset: 0x000B0563
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("Port_not_open"));
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000B2375 File Offset: 0x000B0575
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("Arg_WrongAsyncResult"));
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000B2386 File Offset: 0x000B0586
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndReadCalledMultiple"));
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000B2397 File Offset: 0x000B0597
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndWriteCalledMultiple"));
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000B23A8 File Offset: 0x000B05A8
		internal static void WinIOError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			InternalResources.WinIOError(lastWin32Error, string.Empty);
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000B23C8 File Offset: 0x000B05C8
		internal static void WinIOError(string str)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			InternalResources.WinIOError(lastWin32Error, str);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000B23E4 File Offset: 0x000B05E4
		internal static void WinIOError(int errorCode, string str)
		{
			if (errorCode <= 5)
			{
				if (errorCode - 2 > 1)
				{
					if (errorCode == 5)
					{
						if (str.Length == 0)
						{
							throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_NoPathName"));
						}
						throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_Path", new object[]
						{
							str
						}));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(SR.GetString("IO_PortNotFound"));
					}
					throw new IOException(SR.GetString("IO_PortNotFoundFileName", new object[]
					{
						str
					}));
				}
			}
			else if (errorCode != 32)
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
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000B24D0 File Offset: 0x000B06D0
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
