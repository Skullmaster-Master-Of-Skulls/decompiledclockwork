using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x0200009E RID: 158
	internal static class __Error
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x0000C284 File Offset: 0x0000A484
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("IO_EOF_ReadBeyondEOF"));
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C295 File Offset: 0x0000A495
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_FileClosed"));
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000C2A7 File Offset: 0x0000A4A7
		internal static void PipeNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_PipeClosed"));
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		internal static void StreamIsClosed()
		{
			throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamIsClosed"));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000C2CB File Offset: 0x0000A4CB
		internal static void ReadNotSupported()
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnreadableStream"));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		internal static void SeekNotSupported()
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000C2ED File Offset: 0x0000A4ED
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("Argument_WrongAsyncResult"));
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000C2FE File Offset: 0x0000A4FE
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndReadCalledMultiple"));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000C30F File Offset: 0x0000A50F
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndWriteCalledMultiple"));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000C320 File Offset: 0x0000A520
		internal static void EndWaitForConnectionCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndWaitForConnectionCalledMultiple"));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000C334 File Offset: 0x0000A534
		[SecuritySafeCritical]
		internal static string GetDisplayablePath(string path, bool isInvalidPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			bool flag = false;
			if (path.Length < 2)
			{
				return path;
			}
			if (path[0] == Path.DirectorySeparatorChar && path[1] == Path.DirectorySeparatorChar)
			{
				flag = true;
			}
			else if (path[1] == Path.VolumeSeparatorChar)
			{
				flag = true;
			}
			if (!flag && !isInvalidPath)
			{
				return path;
			}
			bool flag2 = false;
			try
			{
				if (!isInvalidPath)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[]
					{
						path
					}).Demand();
					flag2 = true;
				}
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			if (!flag2)
			{
				if (path[path.Length - 1] == Path.DirectorySeparatorChar)
				{
					path = SR.GetString("IO_IO_NoPermissionToDirectoryName");
				}
				else
				{
					path = Path.GetFileName(path);
				}
			}
			return path;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000C410 File Offset: 0x0000A610
		[SecurityCritical]
		internal static void WinIOError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			__Error.WinIOError(lastWin32Error, string.Empty);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000C430 File Offset: 0x0000A630
		[SecurityCritical]
		internal static void WinIOError(int errorCode, string maybeFullPath)
		{
			bool isInvalidPath = errorCode == 123 || errorCode == 161;
			string displayablePath = __Error.GetDisplayablePath(maybeFullPath, isInvalidPath);
			if (errorCode <= 80)
			{
				if (errorCode <= 15)
				{
					switch (errorCode)
					{
					case 2:
						if (displayablePath.Length == 0)
						{
							throw new FileNotFoundException(SR.GetString("IO_FileNotFound"));
						}
						throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, SR.GetString("IO_FileNotFound_FileName"), new object[]
						{
							displayablePath
						}), displayablePath);
					case 3:
						if (displayablePath.Length == 0)
						{
							throw new DirectoryNotFoundException(SR.GetString("IO_PathNotFound_NoPathName"));
						}
						throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, SR.GetString("IO_PathNotFound_Path"), new object[]
						{
							displayablePath
						}));
					case 4:
						break;
					case 5:
						if (displayablePath.Length == 0)
						{
							throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_NoPathName"));
						}
						throw new UnauthorizedAccessException(string.Format(CultureInfo.CurrentCulture, SR.GetString("UnauthorizedAccess_IODenied_Path"), new object[]
						{
							displayablePath
						}));
					default:
						if (errorCode == 15)
						{
							throw new DriveNotFoundException(string.Format(CultureInfo.CurrentCulture, SR.GetString("IO_DriveNotFound_Drive"), new object[]
							{
								displayablePath
							}));
						}
						break;
					}
				}
				else if (errorCode != 32)
				{
					if (errorCode == 80)
					{
						if (displayablePath.Length != 0)
						{
							throw new IOException(string.Format(CultureInfo.CurrentCulture, SR.GetString("IO_IO_FileExists_Name"), new object[]
							{
								displayablePath
							}), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
						}
					}
				}
				else
				{
					if (displayablePath.Length == 0)
					{
						throw new IOException(SR.GetString("IO_IO_SharingViolation_NoFileName"), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
					}
					throw new IOException(SR.GetString("IO_IO_SharingViolation_File", new object[]
					{
						displayablePath
					}), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode == 87)
				{
					throw new IOException(UnsafeNativeMethods.GetMessage(errorCode), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
				}
				if (errorCode == 183)
				{
					if (displayablePath.Length != 0)
					{
						throw new IOException(SR.GetString("IO_IO_AlreadyExists_Name", new object[]
						{
							displayablePath
						}), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(SR.GetString("IO_PathTooLong"));
				}
				if (errorCode == 995)
				{
					throw new OperationCanceledException();
				}
			}
			throw new IOException(UnsafeNativeMethods.GetMessage(errorCode), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C682 File Offset: 0x0000A882
		internal static void WriteNotSupported()
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnwritableStream"));
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C693 File Offset: 0x0000A893
		internal static void OperationAborted()
		{
			throw new IOException(SR.GetString("IO_OperationAborted"));
		}
	}
}
